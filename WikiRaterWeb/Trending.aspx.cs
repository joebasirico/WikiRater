using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
			dt.Columns.Add(new DataColumn("RatedStyle"));

			var uniqueRating = (from a in dc.Ratings
								select a.Article).Distinct();
			foreach (string article in uniqueRating)
			{

				int votes = (from r in dc.Ratings
							 where r.Article == article
							 && r.IsLatest == true
							 select r).Count();

				DateTime firstOcc = (from r in dc.Ratings
									 where r.Article == article
									 select r.DateCreated).OrderBy(ra => ra).FirstOrDefault();

				int hours = DateTime.Now.Subtract(firstOcc).Hours;

				DataRow dr = dt.NewRow();
				dr["Article"] = article;
				dr["Points"] = ((double)votes - 1.0) / System.Math.Pow(((double)hours + 2), 1.5);
				if (!isLoggedIn)
					dr["RatedStyle"] = "none";
				else if (hasBeenRated(currentUserID, article))
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

		private bool hasBeenRated(int userID, string Article)
		{
			var RatedArticles = from rArt in dc.Ratings
								where rArt.UserID == userID
									&& rArt.IsLatest == true
								select rArt.Article;
			bool found = false;

			foreach (string ratedArticle in RatedArticles)
			{
				if (Article == ratedArticle)
				{
					found = true;
					break;
				}
			}
			return found;
		}
	}
}