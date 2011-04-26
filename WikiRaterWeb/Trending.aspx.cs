using System;
using System.Collections.Generic;
using System.Linq;
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

			DataLoadOptions dlo = new DataLoadOptions();
			dlo.LoadWith<Rating>(rating => rating.User);
			dc.LoadOptions = dlo;

			var allRatings = from rating in dc.Ratings
							 select rating;

			var uniqueRating = (from a in allRatings
								where a.DateCreated.CompareTo(DateTime.Now.Subtract(new TimeSpan(24 * 7, 0, 0))) > 0
								select a.Article).Distinct();
			foreach (string article in uniqueRating)
			{

				int votes = (from r in allRatings
							 where r.Article == article
							 && r.IsLatest == true
							 select r).Count();

				DateTime firstOcc = (from r in dc.Ratings
									 where r.Article == article
									 select r.DateCreated).OrderBy(ra => ra).FirstOrDefault();

				int hours = (int)DateTime.Now.Subtract(firstOcc).TotalHours;

				DataRow dr = dt.NewRow();
				if (article.Length > Settings.Default.TruncateArticleLength)
					dr["Article"] = Server.HtmlEncode(article.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
				else
					dr["Article"] = Server.HtmlEncode(article);
				//subtract one for the initial vote
				votes--;
				double averageValue = RatingHelper.GetWeightedAverage(article, allRatings.ToList());
				dr["Points"] = (int)Math.Round(((double)votes) / System.Math.Pow(((double)hours + 2), 1.5) * 100 * averageValue);
					
				string description = "";
				if (votes < 2)
					description += votes + " vote";
				else
					description += votes + " votes";

				if (hours < 1)
					description += " just now.";
				else
					description += " in " + hours + " hours.";

				description += "<br/>Average: " + Math.Round(averageValue, 2);

				dr["Description"] = description;

				if (!isLoggedIn)
					dr["RatedStyle"] = "none";
				else if (RatingHelper.hasBeenRated(currentUserID, article, allRatings.ToList()))
					dr["RatedStyle"] = "Rated";
				else
					dr["RatedStyle"] = "NotRated";

				dt.Rows.Add(dr);
			}
			dt.DefaultView.Sort = "Points DESC";
			dt.DefaultView.RowFilter = "Points > 0";
			TrendingListView.DataSource = dt.DefaultView;
			TrendingListView.DataBind();
		}
	}
}