using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WikiRaterWeb.Properties;
using System.Data.Linq;

namespace WikiRaterWeb
{
	public partial class Trending : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		protected void Page_Load(object sender, EventArgs e)
		{

			int currentUserID = 0;
			bool isLoggedIn = false;
			Guid session = new Guid();
			if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
			{
				currentUserID = Auth.checkSession(session);
				if (currentUserID > 0)
					isLoggedIn = true;
			}

			//Hacker News Formula: 
			//(p - 1) / (t + 2)^1.5
			//Description: 
			//Votes divided by age factor
			//p = votes (points) from users.
			//t = time since submission in hours.
			//p is subtracted by 1 to negate submitters vote.
			//age factor is (time since submission in hours plus two) to the power of 1.5.

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("Article"));
			dt.Columns.Add(new DataColumn("Points", System.Type.GetType("System.Double")));
			dt.Columns.Add(new DataColumn("Description"));
			dt.Columns.Add(new DataColumn("RatedStyle"));

			List<Tuple<string, double>> userRatings = RatingHelper.GetAllRatedArticles(currentUserID, "rated", 0, 10);

			foreach (Tuple<string, int, DateTime, double> article in RatingHelper.GetTrendingValues())
			{
				int hours = (int)DateTime.Now.Subtract(article.Item3).TotalHours;

				DataRow dr = dt.NewRow();
				if (article.Item1.Length > Settings.Default.TruncateArticleLength)
					dr["Article"] = Server.HtmlEncode(article.Item1.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
				else
					dr["Article"] = Server.HtmlEncode(article.Item1);
				//subtract one for the initial vote
				int votes = article.Item2 - 1;
				double averageValue = article.Item4;
				int points = (int)Math.Round(((double)votes) / System.Math.Pow(((double)hours + 2), 1.5) * 100 * averageValue);
				if (points == 0)
					continue;

				dr["Points"] = points;
				string description = "";
				if (votes < 2)
					description += votes + " vote";
				else
					description += votes + " votes";

				description += GetSaneTime(hours);

				description += "<br/>Average: " + Math.Round(averageValue, 2);

				dr["Description"] = description;

				if (!isLoggedIn)
					dr["RatedStyle"] = "none";
				else if (RatingHelper.Contains(userRatings, article.Item1))
					dr["RatedStyle"] = "Rated";
				else
					dr["RatedStyle"] = "NotRated";

				dt.Rows.Add(dr);
			}
			dt.DefaultView.Sort = "Points DESC";
			TrendingListView.DataSource = dt.DefaultView;
			TrendingListView.DataBind();
		}

		private static string GetSaneTime(int hours)
		{
			string time = "";
			if (hours < 1)
				time = " just now.";
			else
			{
				if (hours < 24)
					time = " in " + hours + " hours.";
				else if (hours < 24 * 7)
				{
					int days = (int)Math.Floor((float)hours / 24);
					if (days == 1)
						time = " in about a day.";
					else
						time = " in about " + days + " days.";
				}
				else if (hours < 24 * 30)
				{
					int weeks = (int)Math.Floor((float)hours / (24 * 7));
					if (weeks == 1)
						time = " in about a week.";
					else
						time = " in about " + weeks + " weeks.";
				}
				else
				{
					int months = (int)Math.Floor((float)hours / (24 * 30));
					if (months == 1)
						time = " in about a month.";
					else
						time = " in about " + months + " months.";
				}
			}

			return time;
		}
	}
}