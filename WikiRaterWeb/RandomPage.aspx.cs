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

			List<Tuple<string, double>> unratedArticles = RatingHelper.GetAllRatedArticles(userID, "unrated", lowerBound, upperBound);

			Response.Redirect(Settings.Default.WikipediaBaseURL + unratedArticles[new Random().Next(unratedArticles.Count)].Item1);
			//}
		}
	}
}