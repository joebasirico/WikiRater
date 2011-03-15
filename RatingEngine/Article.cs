using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace RatingEngine
{
	public class Article
	{
		public string title = "";
		public string url = "";
		//public string wikiTitle = "";
		public string body = "";
		public int rating = 0;
		string statsPage = "http://toolserver.org/~soxred93/articleinfo/index.php?article={0}&lang=en&wiki=wikipedia";

		/// <summary>
		/// Get a random wiki article
		/// </summary>
		public Article()
		{
			url = "http://en.wikipedia.org/wiki/Special:Random";
			//url = "https://secure.wikimedia.org/wikipedia/en/wiki/Ernie_Walker";
			//url = "https://secure.wikimedia.org/wikipedia/en/wiki/Black_Friday_(1945)";
			GetBody();
			GetTitle();
			Rate();
			url = "http://en.wikipedia.org/wiki/" + title;
		}

		/// <summary>
		/// Get a specific wiki article
		/// </summary>
		/// <param name="url"></param>
		public Article(string URL)
		{
			url = URL;
			GetBody();
			GetTitle();
			Rate();
		}


		private void GetTitle()
		{
			string titleMatch = "<h1 id=\"firstHeading\" class=\"firstHeading\">(.+)</h1>";
			Regex titleRegex = new Regex(titleMatch);
			title = titleRegex.Match(body).Groups[1].Value;
		}

		/// <summary>
		/// rate the current article and store it in rating
		/// use myArticle.rating to retrieve the rating.
		/// </summary>
		public void Rate()
		{
			///rating = (linksInWeight * linksIn) + 
			///         (linksOutWeight * linksOut) + 
			///         (minutesSinceLastEditWeight * minutesSinceLastEdit * -1) + 
			///         (totalEditsWeight * totalEdits) + 
			///         (totalMinorEditsWeight * totalMinorEdits) + 
			///         (isFeaturedWeight * isFeatured) + 
			///         (totalLengthWeight * totalLength) + 
			///         (viewsInLast30DaysWeight * viewsInLast30Days)
			int linksToWeight = 10;
			int linksFromWeight = 5;
			int minutesSinceLastEditWeight = 0;
			int totalEditsWeight = 5;
			int totalMinorEditsWeight = 2;
			int isFeaturedWeight = 300;
			int totalLengthWeight = 1;
			int viewsInLast30DaysWeight = 2;

			int CurLinksTo = GetLinksTo();
			int CurLinksFrom = GetLinksFrom();
			int CurMinutesSinceLastEdit = GetMinSinceLastEdit();
			int CurTotalEdits = GetTotalEdits();
			int CurTotalMinorEdits = GetTotalMinEdits();
			int CurIsFeatured = GetIsFeatured();
			int CurTotalLength = GetTotalLength();
			int CurViewsInLast30Days = GetViewsInLast30Days();



			int internalRating = (linksToWeight * CurLinksTo) +
						(linksFromWeight * CurLinksFrom) +
						(minutesSinceLastEditWeight * CurMinutesSinceLastEdit) +
						(totalEditsWeight * CurTotalEdits) +
						(totalMinorEditsWeight * CurTotalMinorEdits) +
						(isFeaturedWeight * CurIsFeatured) +
						(totalLengthWeight * CurTotalLength) +
						(viewsInLast30DaysWeight * CurViewsInLast30Days);

			//Thresholds
			//1:      0 - 300
			//2:    301 - 1000
			//3:   1001 - 2500
			//4:   2501 - 4500
			//5:   4501 - 7000
			//6:   7001 - 10000
			//7:  10001 - 12000
			//8:  12001 - 15000
			//9:  15001 - 20000
			//10: 20001 - 

			if (internalRating <= 300)
				rating = 1;
			else if (internalRating > 300 && internalRating <= 1000)
				rating = 2;
			else if (internalRating > 1000 && internalRating <= 2500)
				rating = 3;
			else if (internalRating > 2500 && internalRating <= 4500)
				rating = 4;
			else if (internalRating > 4500 && internalRating <= 7000)
				rating = 5;
			else if (internalRating > 7000 && internalRating <= 10000)
				rating = 6;
			else if (internalRating > 10000 && internalRating <= 12000)
				rating = 7;
			else if (internalRating > 12000 && internalRating <= 15000)
				rating = 8;
			else if (internalRating > 15000 && internalRating <= 20000)
				rating = 9;
			else if (internalRating > 20000)
				rating = 10;
		}

		private int GetViewsInLast30Days()
		{
			string trafficStatsPage = "http://stats.grok.se/en/latest/{0}";
			int views = 0;

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(trafficStatsPage, title));
			request.AllowAutoRedirect = true;
			WebResponse response = request.GetResponse();
			StreamReader inStream = new StreamReader(response.GetResponseStream());
			string trafficStatsBody = inStream.ReadToEnd();

			string viewMatch = "been viewed (\\d+) times in the last 30 days\\. ";
			Regex viewRegex = new Regex(viewMatch);
			string viewsString = viewRegex.Match(trafficStatsBody).Groups[1].Value;

			int.TryParse(viewsString, out views);

			return views;
		}

		private int GetTotalLength()
		{
			return (body.Length - 25000) / 100;
		}

		private int GetIsFeatured()
		{
			string FeaturedIcon = "14px-Cscr-featured.svg.png";
			if (Regex.IsMatch(body, FeaturedIcon))
				return 1;
			else
				return 0;
		}

		private int GetTotalMinEdits()
		{
			return 0;
		}

		private int GetTotalEdits()
		{
			return 0;
		}

		private int GetMinSinceLastEdit()
		{
			return 0;
		}

		private int GetLinksFrom()
		{
			string LinksFrom = "<a href=\"[/wikipedia/en/]?wiki/[\\w\\s%]+\".*>";
			return Regex.Matches(body, LinksFrom).Count;
		}


		public int GetLinksTo()
		{
			int linksto = 0;
			string linkPages = "http://en.wikipedia.org/w/index.php?title=Special:WhatLinksHere&target={0}&namespace=0";
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(linkPages, title));
			request.Method = "GET";
			request.UserAgent = "IE";

			request.AllowAutoRedirect = true;
			WebResponse response = request.GetResponse();
			StreamReader inStream = new StreamReader(response.GetResponseStream());
			string linkPage = inStream.ReadToEnd();

			string matchLinks = "<li><a href=\"/wiki/.+\" title=\".+\">.+</a>";
			Regex linksRegex = new Regex(matchLinks);
			if (linksRegex.IsMatch(linkPage))
				linksto = linksRegex.Matches(linkPage).Count;

			return linksto;
		}

		void GetBody()
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
				request.Method = "GET";
				request.UserAgent = "IE";

				request.AllowAutoRedirect = true;
				WebResponse response = request.GetResponse();
				StreamReader inStream = new StreamReader(response.GetResponseStream());
				body = inStream.ReadToEnd();
			}
			catch (WebException wex)
			{
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
