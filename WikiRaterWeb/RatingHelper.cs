using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Data;
using WikiRaterWeb.Properties;
using System.Configuration;

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
			List<Tuple<string, double>> articles = new List<Tuple<string, double>>();

			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				List<Tuple<string, double>> ratedArticleList = GetUserRatings(userID);

				conn.Open();
				SqlCommand command = new SqlCommand("GetCurrentRatingsAverages", conn);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@LowerBound", lowerBound));
				command.Parameters.Add(new SqlParameter("@UpperBound", upperBound));
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					string article = reader.GetString(0);
					double average = reader.GetDouble(1);

					if (Contains(ratedArticleList, article) && hasRated == "rated")
						articles.Add(new Tuple<string, double>(article, average));
					else if (hasRated == "unrated")
						articles.Add(new Tuple<string, double>(article, average));
					else if (hasRated == "all")
						articles.Add(new Tuple<string, double>(article, average));
				}
				reader.Close();
			}

			return articles;
		}

		public static bool Contains(List<Tuple<string, double>> userRatings, string p)
		{
			bool found = false;
			foreach (Tuple<string, double> a in userRatings)
			{
				if (a.Item1 == p)
				{
					found = true;
					break;
				}
			}
			return found;
		}

		public static List<Tuple<string, double>> GetUserRatings(int userID)
		{
			List<Tuple<string, double>> ratedArticleList = new List<Tuple<string, double>>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();
				SqlCommand getUserRatings = new SqlCommand("GetUserRatings", conn);
				getUserRatings.CommandType = CommandType.StoredProcedure;
				getUserRatings.Parameters.Add(new SqlParameter("UserID", userID));
				SqlDataReader getRatingsReader = getUserRatings.ExecuteReader();
				while (getRatingsReader.Read())
					ratedArticleList.Add(new Tuple<string,double>(getRatingsReader.GetString(0), getRatingsReader.GetDouble(1)));
				conn.Close();
			}
			return ratedArticleList;
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
			List<Tuple<string, double, bool>> articles = new List<Tuple<string, double, bool>>();

			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();

				List<string> ratedArticleList = new List<string>();
				SqlCommand getUserRatings = new SqlCommand("GetUserRatings", conn);
				getUserRatings.CommandType = CommandType.StoredProcedure;
				getUserRatings.Parameters.Add(new SqlParameter("@UserID", userID));
				SqlDataReader getRatingsReader = getUserRatings.ExecuteReader();
				while (getRatingsReader.Read())
					ratedArticleList.Add(getRatingsReader.GetString(0));
				conn.Close();

				conn.Open();
				SqlCommand command = new SqlCommand("GetCurrentRatingsAverages", conn);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@LowerBound", lowerBound));
				command.Parameters.Add(new SqlParameter("@UpperBound", upperBound));
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					string article = reader.GetString(0);
					double average = reader.GetDouble(1);

					if (ratedArticleList.Contains(article))
						articles.Add(new Tuple<string, double, bool>(article, average, true));
					else
						articles.Add(new Tuple<string, double, bool>(article, average, false));
				}
				reader.Close();
			}
			return articles;
		}

		public static List<Tuple<string, int, DateTime, double>> GetTrendingValues()
		{
			List<Tuple<string, int, DateTime, double>> articles = new List<Tuple<string, int, DateTime, double>>();

			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("GetTrendValues", conn);
				command.CommandType = CommandType.StoredProcedure;
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					articles.Add(new Tuple<string, int, DateTime, double>(reader.GetString(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDouble(3)));
				}
				reader.Close();
			}
			return articles;
		}

		public static double GetWeightedAverage(string article)
		{
			double average = 0.0;
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("GetArticleAverage", conn);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Article", article));
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
					average = reader.GetDouble(1);
				reader.Close();
			}
			return average;
		}

		public static bool hasBeenRated(int userID, string article)
		{
			bool found = false;
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("HasBeenRated", conn);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Article", article));
				command.Parameters.Add(new SqlParameter("@UserID", userID));

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					found = true;
					break;
				}
				reader.Close();
			}
			return found;
		}

		internal static List<string> GetImprovementProgramList()
		{
			List<string> articles = new List<string>();

			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT Title FROM ImprovementProgramList", conn);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					articles.Add(reader.GetString(0));
				}
				reader.Close();
			}
			return articles;
		}
	}
}