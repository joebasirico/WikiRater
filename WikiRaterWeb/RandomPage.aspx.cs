using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class RandomPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			double lowerBound = Settings.Default.defaultLowerBound;
			double upperBound = Settings.Default.defaultUpperBound;
			bool hasLower = false;
			bool hasUpper = false;

			if (Request["lowerBound"] != null
				&& !string.IsNullOrEmpty(Request["lowerBound"]))
				hasLower = double.TryParse(Request["lowerBound"], out lowerBound);

			if (Request["upperBound"] != null
				&& !string.IsNullOrEmpty(Request["upperBound"]))
				hasUpper = double.TryParse(Request["upperBound"], out upperBound);

			//if (hasLower || hasUpper)
			//{
			int userID = 0;
			Guid session = new Guid();
			//we've never seen this user before or they've cleared their cookies
			if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				userID = Auth.checkSession(session);

			//DataClassesDataContext dc = new DataClassesDataContext();
			//Random rand = new Random();
			//int ratingCount = dc.Ratings.Count();
			//List<Rating> allRatings = (from r in dc.Ratings
			//                          select r).ToList();
			//string title = "";
			//Rating searchRating = null;
			//while (searchRating == null)
			//{
			//    searchRating = dc.Ratings.FirstOrDefault(r1 => r1.RatingID == rand.Next(ratingCount) && r1.UserID != userID);
			//}
			//double value = RatingHelper.GetWeightedAverage(searchRating.Article, allRatings);
			//if (value > lowerBound && value < upperBound)
			//{
			//    title = searchRating.Article;
			//}
			//Response.Redirect(Settings.Default.WikipediaBaseURL + title);


			List<Tuple<string, double, bool>> unratedArticles = RatingHelper.GetAllRatedArticles(userID, lowerBound, upperBound);

			Response.Redirect(Settings.Default.WikipediaBaseURL + unratedArticles[new Random().Next(unratedArticles.Count)].Item1);
			//}
		}
	}
}