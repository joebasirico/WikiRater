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
			if (CheckIP(userID))
				achievements.Add(dc.Achievements.First(a => a.ShortName == "CompletedIP"));
			if(dc.Ratings.Count(r => r.UserID == userID) >= 1)
				achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated1"));
			if (dc.Ratings.Count(r => r.UserID == userID) >= 10)
				achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated10"));
			if (dc.Ratings.Count(r => r.UserID == userID) >= 50)
				achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated50"));
			if (dc.Ratings.Count(r => r.UserID == userID) >= 100)
				achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated100"));
			if (dc.Ratings.Count(r => r.UserID == userID) >= 1000)
				achievements.Add(dc.Achievements.First(a => a.ShortName == "Rated1000"));
			if(CheckDistribution(userID))
				achievements.Add(dc.Achievements.First(a => a.ShortName == "GoodDistribution"));

			return achievements;

		}

		private bool CheckDistribution(int userID)
		{
			var ratedArticles = (from art in dc.Ratings
								where art.UserID == userID
									 select art.Value).Distinct();
			int distinctRatings = 0;
			foreach (int a in ratedArticles)
				distinctRatings++;

			if (distinctRatings == 9)
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
									&& rArt.IsLatest == true
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
	}
}