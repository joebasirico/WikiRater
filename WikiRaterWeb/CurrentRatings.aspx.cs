using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class CurrentRatings : System.Web.UI.Page
	{
		bool isLoggedIn = false;
		int userID = 0;
		Guid session = new Guid();
		protected void Page_Load(object sender, EventArgs e)
		{
			//we've never seen this user before or they've cleared their cookies
			if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
			{
				userID = Auth.checkSession(session);
				if (userID > 0)
					isLoggedIn = true;
			}

			if (!Page.IsPostBack)
			{
				DataTable dt = GenerateTable(isLoggedIn, userID, Settings.Default.defaultLowerBound, Settings.Default.defaultUpperBound);
				dt.DefaultView.Sort = "Rating DESC";
				RatingsListView.DataSource = dt.DefaultView;
				RatingsListView.DataBind();
			}
		}

		private DataTable GenerateTable(bool isLoggedIn, int userID, double lowerBound, double upperBound)
		{
			//A list of all articles and their average rating if their average rating is between the lower and upper bound
			List<Tuple<string, double, bool>> articles = RatingHelper.GetAllRatedArticles(userID, lowerBound, upperBound);

			DataTable dt = new DataTable();
			dt.Columns.Add("Article");
			dt.Columns.Add("Rating", System.Type.GetType("System.Double"));
			dt.Columns.Add("RatedStyle", System.Type.GetType("System.String"));

			foreach (Tuple<string, double, bool> a in articles)
			{
				DataRow dr = dt.NewRow();
				//encode
				if (a.Item1.Length > Settings.Default.TruncateArticleLength)
					dr["Article"] = Server.HtmlEncode(a.Item1.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
				else
					dr["Article"] = Server.HtmlEncode(a.Item1);
				dr["Rating"] = a.Item2;
				if(userID == 0)
					dr["RatedStyle"] = "none";
				else
					if(a.Item3)
						dr["RatedStyle"] = "Rated";
					else
						dr["RatedStyle"] = "NotRated";

				dt.Rows.Add(dr);
			}

			////Todo: possibly use BA for rating weights instead of straight average.
			////possibly do this calculation on vote instead of request of this page for performance?
			////BA = ((AvgNumOfRatingsForAll * AvgRatingForAll) + TotalRating) / (RateCount + AvgNumOfRatingsForAll)
			//DataClassesDataContext dc = new DataClassesDataContext();
			//var allRatings = from rating in dc.Ratings
			//                 where rating.IsLatest == true &&
			//                    rating.User.UserName != Settings.Default.WikiRaterName
			//                 group rating by rating.Article into result
			//                 select new
			//                 {
			//                     Article = result.Key,
			//                     Average = result.Average(i => (Double)i.Value)
			//                 };

			//DataTable dt = new DataTable();
			//dt.Columns.Add("Article");
			//dt.Columns.Add("Rating", System.Type.GetType("System.Double"));
			//dt.Columns.Add("RatedStyle", System.Type.GetType("System.String"));

			//foreach (var ratingValue in allRatings)
			//{

			//    if (isLoggedIn)
			//    {
			//        var RatedArticles = from rArt in dc.Ratings
			//                            where rArt.UserID == userID
			//                                && rArt.IsLatest == true
			//                            select rArt.Article;
			//        bool found = false;

			//        foreach (string ratedArticle in RatedArticles)
			//        {
			//            if (ratingValue.Article == ratedArticle)
			//            {
			//                DataRow dr = dt.NewRow();
			//                //encode
			//                if (ratingValue.Article.Length > Settings.Default.TruncateArticleLength)
			//                    dr["Article"] = Server.HtmlEncode(ratingValue.Article.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
			//                else
			//                    dr["Article"] = Server.HtmlEncode(ratingValue.Article);
			//                dr["Rating"] = ratingValue.Average;
			//                dr["RatedStyle"] = "Rated";

			//                dt.Rows.Add(dr);
			//                found = true;
			//                break;
			//            }
			//        }
			//        if (!found)
			//        {
			//            DataRow dr = dt.NewRow();
			//            //encode
			//            if (ratingValue.Article.Length > Settings.Default.TruncateArticleLength)
			//                dr["Article"] = Server.HtmlEncode(ratingValue.Article.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
			//            else
			//                dr["Article"] = Server.HtmlEncode(ratingValue.Article);

			//            dr["Rating"] = ratingValue.Average;
			//            dr["RatedStyle"] = "NotRated";

			//            dt.Rows.Add(dr);
			//        }
			//    }
			//    else
			//    {
			//        DataRow dr = dt.NewRow();
			//        //encode
			//        if (ratingValue.Article.Length > Settings.Default.TruncateArticleLength)
			//            dr["Article"] = Server.HtmlEncode(ratingValue.Article.Substring(0, Settings.Default.TruncateArticleLength - 3)) + "...";
			//        else
			//            dr["Article"] = Server.HtmlEncode(ratingValue.Article);
			//        dr["Rating"] = ratingValue.Average;
			//        dr["RatedStyle"] = "none";
			//        dt.Rows.Add(dr);
			//    }
			//}
			return dt;
		}

		protected void filter_Click(object sender, EventArgs e)
		{
			double lowerBound = 0;
			double upperBound = 0;
			if (!double.TryParse(lowerBoundBox.Text, out lowerBound))
				lowerBound = Settings.Default.defaultLowerBound;
			if (!double.TryParse(upperBoundBox.Text, out upperBound))
				upperBound = Settings.Default.defaultUpperBound;

			DataTable dt = GenerateTable(isLoggedIn, userID, lowerBound, upperBound);
			dt.DefaultView.Sort = "Rating DESC";
			RatingsListView.DataSource = dt.DefaultView;
			RatingsListView.DataBind();
		}
	}
}