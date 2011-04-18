using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public static class RatingHelper
	{
		/// <summary>
		/// Use this method if you want to get a list of only the articles somebody has rated or not (or all articles)
		/// </summary>
		/// <param name="userID">the userID if the user you'd like to lookup</param>
		/// <param name="hasRated">use either "rated", "unrated" or "all"
		/// note: if no userID is supplied all articles will always be returned
		/// </param>
		/// <param name="lowerBound">only articles with an average rating greater than this
		/// value will be returned. 1 - 10 exclusive are valid values. If 10 or greater is 
		/// supplied no values will be returned
		/// </param>
		/// <param name="upperBound">only articles with an average rating less than this
		/// value will be returned. 1 - 10 exclusive are valid values. If 1 or smaller is 
		/// supplied no values will be returned
		/// </param>
		/// <returns>a list of Tuples (article, rating)</returns>
		public static List<Tuple<string, double>> GetAllRatedArticles(int userID, string hasRated, double lowerBound, double upperBound)
		{
			DataClassesDataContext dc = new DataClassesDataContext();

			List<Tuple<string, double>> ratedArticles = new List<Tuple<string, double>>();
			List<Tuple<string, double>> unRatedArticles = new List<Tuple<string, double>>();
			List<Tuple<string, double>> allArticles = new List<Tuple<string, double>>();

			var allRatings = from rating in dc.Ratings
							 where rating.IsLatest == true &&
								rating.User.UserName != Settings.Default.WikiRaterName
							 group rating by rating.Article into result
							 select new
							 {
								 Article = result.Key,
								 Average = result.Average(i => (Double)i.Value)
							 };

			foreach (var ratingValue in allRatings)
			{
				if (ratingValue.Average >= lowerBound &&
					ratingValue.Average <= upperBound)
				{
					if (userID > 0)
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
								ratedArticles.Add(new Tuple<string, double>(ratingValue.Article, ratingValue.Average));
								found = true;
								break;
							}
						}
						if (!found)
							unRatedArticles.Add(new Tuple<string, double>(ratingValue.Article, ratingValue.Average));
					}
					allArticles.Add(new Tuple<string, double>(ratingValue.Article, ratingValue.Average));
				}
			}

			switch (hasRated.ToLower())
			{
				case "all":
					return allArticles;
				case "rated":
					if (userID > 0)
						return ratedArticles;
					else
						return allArticles;
				case "unrated":
					if (userID > 0)
						return unRatedArticles;
					else
						return allArticles;
				default:
					return allArticles;
			}
		}



		/// <summary>
		/// Use this method if you want all articles and whether they've been rated in one list.
		/// </summary>
		/// <param name="userID">the userID if the user you'd like to lookup</param>
		/// <param name="hasRated">use either "rated", "unrated" or "all"
		/// note: if no userID is supplied all articles will always be returned
		/// </param>
		/// <param name="lowerBound">only articles with an average rating greater than this
		/// value will be returned. 1 - 10 exclusive are valid values. If 10 or greater is 
		/// supplied no values will be returned
		/// </param>
		/// <param name="upperBound">only articles with an average rating less than this
		/// value will be returned. 1 - 10 exclusive are valid values. If 1 or smaller is 
		/// supplied no values will be returned
		/// </param>
		/// <returns>a list of Tuples (article, rating, whether this has been rated)</returns>
		public static List<Tuple<string, double, bool>> GetAllRatedArticles(int userID, double lowerBound, double upperBound)
		{
			DataClassesDataContext dc = new DataClassesDataContext();

			List<Tuple<string, double, bool>> allArticles = new List<Tuple<string, double, bool>>();

			var allRatings = from rating in dc.Ratings
							 where rating.IsLatest == true &&
								rating.User.UserName != Settings.Default.WikiRaterName
							 group rating by rating.Article into result
							 select new
							 {
								 Article = result.Key,
								 Average = result.Average(i => (Double)i.Value)
							 };

			foreach (var ratingValue in allRatings)
			{
				if (ratingValue.Average >= lowerBound &&
					ratingValue.Average <= upperBound)
				{
					if (userID > 0)
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
								allArticles.Add(new Tuple<string, double, bool>(ratingValue.Article, ratingValue.Average, true));
								found = true;
								break;
							}
						}
						if (!found)
							allArticles.Add(new Tuple<string, double, bool>(ratingValue.Article, ratingValue.Average, false));
					}
					else //user is not logged in return a list of all articles that satisfy the range
						allArticles.Add(new Tuple<string, double, bool>(ratingValue.Article, ratingValue.Average, false));
				}
			}
			return allArticles;
		}
	}
}