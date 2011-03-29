using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WikiRaterWeb
{
	public partial class CurrentRatings : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			bool isLoggedIn = false;
			int userID = 0;
			Guid session = new Guid();
			//we've never seen this user before or they've cleared their cookies
			if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
			{
				userID = Auth.checkSession(session);
				if(userID > 0)
					isLoggedIn = true;
			}

			DataClassesDataContext dc = new DataClassesDataContext();
			var allRatings = from rating in dc.Ratings 
						 where rating.IsLatest == true
						 group rating by rating.Article into result
						 select new
						 {
							 Article = result.Key,
							 Average = result.Average(i => (Double)i.Value)
						 };

			DataTable dt = new DataTable();
			dt.Columns.Add("Article");
			dt.Columns.Add("Rating", System.Type.GetType("System.Double"));
			dt.Columns.Add("RatedStyle", System.Type.GetType("System.String"));

			foreach (var ratingValue in allRatings)
			{
				if (isLoggedIn)
				{
					var RatedArticles = from rArt in dc.Ratings
										where rArt.UserID == userID
											&& rArt.IsLatest == true
										select rArt.Article;
					bool found = false;

					foreach (string ratedArticle in RatedArticles)
					{
						if (ratingValue.Article == ratedArticle)
						{
							DataRow dr = dt.NewRow();
								//encode
								dr["Article"] = Server.HtmlEncode(ratedArticle);
								dr["Rating"] = ratingValue.Average;
								dr["RatedStyle"] = "Rated";

								dt.Rows.Add(dr);
								found = true;
								break;
						}
					}
					if (!found)
					{
						DataRow dr = dt.NewRow();
						//encode
						dr["Article"] = Server.HtmlEncode(ratingValue.Article);
						dr["Rating"] = ratingValue.Average;
						dr["RatedStyle"] = "NotRated";

						dt.Rows.Add(dr);
					}
				}
				else
				{
					DataRow dr = dt.NewRow();
					//encode
					dr["Article"] = Server.HtmlEncode(ratingValue.Article);
					dr["Rating"] = ratingValue.Average;
					dr["RatedStyle"] = "none";
					dt.Rows.Add(dr);
				}
			}
			dt.DefaultView.Sort = "Rating DESC";
			RatingsListView.DataSource = dt.DefaultView;
			RatingsListView.DataBind();
		}
	}
}