using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class User1 : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		AchievementValidator av = new AchievementValidator();
		Guid session = new Guid();
		int userID = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			string username = "";
			if (!(string.IsNullOrWhiteSpace(Request["Username"])))
				username = Request["Username"];
			else
				username = GetCurrentUser();

			UserName.Text = Server.HtmlEncode(username);

			User currentUser = dc.Users.First(u => u.UserName == username);

			int points = av.GetPoints(currentUser.UserID, false);
			Points.Text = points.ToString();
			if(points > 1)
				PointOrPoints.Text = "Points";
			else
				PointOrPoints.Text = "Point";

			GenerateAchievementList(currentUser.UserID);


			if (userID > 0 && (from u in dc.Users
								   where u.UserID == userID
								   select u.UserName).First() == username)
			{
				RatedArticlePanel.Visible = true;
				PopulateViewedArticles("");
				IntroText.Text = CreateIntroText(currentUser, true);
			}
			else
				IntroText.Text = CreateIntroText(currentUser, false);
		}

		private string CreateIntroText(WikiRaterWeb.User currentUser, bool isHome)
		{
			string introString = "";
			DateTime timeCreated = currentUser.TimeCreated;
			int ratingCount = dc.Ratings.Count(r => r.UserID == currentUser.UserID && r.IsLatest == true);
			if (isHome)
			{

				introString += "You have been a member since " + timeCreated.ToLongDateString() +
					", ";
				if (ratingCount > 1)
					introString += "during which time you have rated <strong>" + ratingCount +
						"</strong> wikipedia articles. ";
				else if (ratingCount == 1)
					introString += "during which time you have rating a single article. I appreciate you signing up, but go get rating! ;-)";
			}
			else
			{

				introString += currentUser.UserName + " has been a member since " + timeCreated.ToLongDateString() +
					", ";
				if (ratingCount > 1)
					introString += "during which time they have rated <strong>" + ratingCount +
						"</strong> wikipedia articles. ";
				else if (ratingCount == 1)
					introString += "during which time they rated one article";
					
			}
			int ratingsPerDay = (int)Math.Round((double)ratingCount / DateTime.Now.Subtract(timeCreated).TotalDays);
			if (ratingsPerDay > 1)
				introString += "Which is an average of " + ratingsPerDay + " articles per day, nice!";
			else if (ratingsPerDay == 1)
				introString += "Which is an average of one rating per day. ";
			return introString;
				
		}


		private void PopulateViewedArticles(string sort)
		{
			List<Tuple<string, double>> articles = RatingHelper.GetUserRatings(userID);

			DataTable dt = new DataTable();
			dt.Columns.Add("Article");
			dt.Columns.Add("Rating", System.Type.GetType("System.Double"));

			foreach (Tuple<string, double> a in articles)
			{
				DataRow dr = dt.NewRow();
				//encode
				if (a.Item1.Length > Settings.Default.TruncateArticleLength)
					dr["Article"] = Server.HtmlEncode(a.Item1.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
				else
					dr["Article"] = Server.HtmlEncode(a.Item1);
				dr["Rating"] = a.Item2;

				dt.Rows.Add(dr);
			}
			dt.DefaultView.Sort = sort;
			RatingsListView.DataSource = dt.DefaultView;
			RatingsListView.DataBind();
		}
		
		private string GetCurrentUser()
		{
			try
			{
				if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				{
					userID = Auth.checkSession(session);
					if (userID != 0)
						return Auth.LookupUserName(userID);
				}
				Response.Redirect("Login.aspx");
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception)
			{
			}
			return "";
		}

		private void GenerateAchievementList(int userID)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Achieved");
			dt.Columns.Add("Title");
			dt.Columns.Add("Value");
			dt.Columns.Add("Description");
			dt.Columns.Add("Icon");
			dt.Columns.Add("HasIcon");

			var accomplished = from a in dc.Achievements
							   where a.AchievementMap.UserID == userID
							   select a;

			foreach (Achievement a in dc.Achievements.ToList())
			{
				DataRow dr = dt.NewRow();
				dr["Title"] = a.Name;
				dr["Description"] = a.Description;
				dr["Value"] = a.Value;
				
				bool found = false;
				foreach (Achievement b in accomplished)
				{
					if (a.ShortName == b.ShortName)
					{
						dr["Achieved"] = "AccomplishedAchievement";
						found = true;
						if (!string.IsNullOrEmpty(a.Icon))
						{
							dr["Icon"] = a.Icon;
							dr["HasIcon"] = "inherit";
						}
						else
						{
							dr["Icon"] = Settings.Default.DefaultAchievementIcon;
							dr["HasIcon"] = "inherit";
						}
						break;
					}
				}
				if (!found)
				{
					dr["Achieved"] = "UnaccomplishedAchievement";
						dr["Icon"] = Settings.Default.DefaultAchievementIcon;
						dr["HasIcon"] = "inherit";
				}

				dt.Rows.Add(dr);
			}

			AchievementsList.DataSource = dt;
			AchievementsList.DataBind();
		}

		protected void RatingSort_Click(object sender, EventArgs e)
		{
			PopulateViewedArticles("Rating DESC");
		}

		protected void ArticleSort_Click(object sender, EventArgs e)
		{
			PopulateViewedArticles("Article");
		}
	}
}