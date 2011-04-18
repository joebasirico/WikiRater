using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiRaterWeb
{
	public class AchievementValidator
	{
		DataClassesDataContext dc = new DataClassesDataContext();

		public AchievementValidator()
		{
		}

		internal List<Achievement> CheckAchievements(int userID)
		{
			List<Achievement> achievements = new List<Achievement>();
			try
			{
				if (CheckIP(userID))
					achievements.Add(dc.Achievements.First(a => a.ShortName == "CompletedIP"));
			}
			catch
			{
			}
			try
			{
				if (dc.Ratings.Count(r => r.UserID == userID && r.IsLatest) >= 1)
					achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated1"));
			}
			catch
			{
			}
			try
			{
				if (dc.Ratings.Count(r => r.UserID == userID && r.IsLatest) >= 10)
					achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated10"));
			}
			catch
			{
			}
			try
			{
				if (dc.Ratings.Count(r => r.UserID == userID && r.IsLatest) >= 50)
					achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated50"));
			}
			catch
			{
			}
			try
			{
				if (dc.Ratings.Count(r => r.UserID == userID && r.IsLatest) >= 100)
					achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated100"));
			}
			catch
			{
			}
			try
			{
				if (dc.Ratings.Count(r => r.UserID == userID && r.IsLatest) >= 1000)
					achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated1000"));
			}
			catch
			{
			}
			try
			{
				if (CheckDistribution(userID))
					achievements.Add(dc.Achievements.First(a => a.ShortName == "GoodDistribution"));
			}
			catch
			{
			}
			try
			{
				if (CheckAlphabetizer(userID))
					achievements.Add(dc.Achievements.First(a => a.ShortName == "Alpha"));
			}
			catch (Exception ex)
			{
				ex.ToString();
			}
			try
			{
				if (NightOwl(userID))
					achievements.Add(dc.Achievements.First(a => a.ShortName == "NightOwl"));
			}
			catch (Exception ex)
			{
				ex.ToString();
			}

			return achievements;

		}

		private bool NightOwl(int userID)
		{
			int ratedArticles = (from art in dc.Ratings
								 where art.UserID == userID && 
								 art.IsLatest &&
								 art.DateCreated.TimeOfDay.CompareTo(new TimeSpan(0, 0, 0)) > 0 &&
								 art.DateCreated.TimeOfDay.CompareTo(new TimeSpan(6, 0, 0)) < 0
								 select art.Value).Count();
			return ratedArticles > 10;
		}

		private bool CheckAlphabetizer(int userID)
		{
			var ratedArticles = (from art in dc.Ratings
								 where art.UserID == userID && art.IsLatest
								 select art.Article).Distinct();
			List<char> letters = new List<char>();
			foreach (string a in ratedArticles)
			{
				if (!string.IsNullOrEmpty(a))
				{
					char currentLetter = a.ToLower()[0];
					if(currentLetter > 96 && currentLetter < 123)
					if (!letters.Contains(currentLetter))
						letters.Add(currentLetter);
				}
			}

			if (letters.Count == 26)
				return true;
			else
				return false;
		}

		private bool CheckDistribution(int userID)
		{
			var ratedArticles = (from art in dc.Ratings
								 where art.UserID == userID && art.IsLatest
								 select art.Value).Distinct();
			int distinctRatings = 0;
			foreach (int a in ratedArticles)
				distinctRatings++;

			if (distinctRatings == 10)
				return true;
			else
				return false;
		}

		internal bool CheckIP(int userID)
		{
			var IPList = from art in dc.ImprovementProgramLists
						 select art.Title;

			var RatedArticles = from rArt in dc.Ratings
								where rArt.UserID == userID
									&& rArt.IsLatest
								select rArt.Article;

			bool ratedAll = true;

			foreach (string article in IPList)
			{
				bool found = false;
				foreach (string ratedArt in RatedArticles)
				{
					if (article == ratedArt)
					{
						found = true;
					}
				}
				if (!found)
				{
					ratedAll = false;
					break;
				}
			}
			return ratedAll;
		}

		internal void AddNewAchievements(List<Achievement> newAchievements, int userID)
		{
			foreach (Achievement a in newAchievements)
				dc.AddAchievementMap(userID, a.ShortName);
		}

		internal List<Achievement> CheckIfNewAchievements(List<Achievement> achivementList, int userID)
		{
			var currentAchievements = from am in dc.AchievementMaps
									  where am.UserID == userID
									  select am.AchievementShortName;
			List<Achievement> newAchievements = new List<Achievement>();
			foreach (Achievement a in achivementList)
			{
				bool found = false;
				foreach (string ca in currentAchievements)
				{
					if (ca == a.ShortName)
					{
						found = true;
						break;
					}
				}
				if (!found)
					newAchievements.Add(a);
			}

			return newAchievements;
		}

		internal int GetPoints(int userID, bool calculate)
		{
			int totalPoints = 0;
			totalPoints += dc.Ratings.Count(r => r.IsLatest && r.UserID == userID);

			if (calculate)
			{
				foreach (Achievement a in CheckAchievements(userID))
				{
					totalPoints += a.Value;
				}
			}
			else
			{
				var pointList = (from a in dc.Achievements
								 where a.AchievementMap.UserID == userID
								 select a.Value);
				foreach (int p in pointList)
				{
					totalPoints += p;
				}
			}
			return totalPoints;
		}

		internal Achievement GetAchievementByShortName(string shortName)
		{
			return (from a in dc.Achievements
							  where a.ShortName == shortName
							  select a).FirstOrDefault();
		}
	}
}