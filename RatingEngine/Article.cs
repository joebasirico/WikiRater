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

		///// <summary>
		///// Get a random wiki article
		///// </summary>
		//public Article()
		//{
		//    url = "http://en.wikipedia.org/wiki/Special:Random";
		//    GetBody();
		//    GetTitle();
		//    Rate();
		//    url = "http://en.wikipedia.org/wiki/" + title;
		//}

		/// <summary>
		/// Get a specific wiki article
		/// </summary>
		/// <param name="url"></param>
		public Article(string t)
		{
			url = "http://en.wikipedia.org/w/index.php?action=render&title=/" + t;
			GetBody();
			title = t;
			Rate();
		}


		//private void GetTitle()
		//{
		//    string titleMatch = 
		//    Regex titleRegex = new Regex(titleMatch);
		//    title = titleRegex.Match(body).Groups[1].Value;
		//}

		/// <summary>
		/// rate the current article and store it in rating
		/// use myArticle.rating to retrieve the rating.
		/// </summary>
		public void Rate()
		{
			//is disambiguation page, automatically a 1
			if (body.Contains("This <a href=\"/wiki/Help:Disambiguation\" title=\"Help:Disambiguation\">disambiguation</a>"))
			{
				rating = 1;
			}
			else
			{

				///Other things to use for rating input:
				///# of images
				///# of citations
				///Writing Grade Level (Flesch grade level)
				///Writing readability (Flesch-Kincaid score)
				///# of sections of source code

				int linksToWeight = 50;
				int linksFromWeight = 25;
				int minutesSinceLastEditWeight = 0;
				int totalEditsWeight = 5;
				int totalMinorEditsWeight = 2;
				//if something is featured it's probably pretty good, this will
				//push it up into the 7 Range, plus the other pieces it's close to being a 10
				int isFeaturedWeight = 10000;
				//reduce the total length weight by a decent amount
				double totalLengthWeight = 1.0 / 200.0;
				int viewsInLast30DaysWeight = 2;

				int CurLinksTo = GetLinksTo();
				int CurLinksFrom = GetLinksFrom();
				int CurMinutesSinceLastEdit = GetMinSinceLastEdit();
				int CurTotalEdits = GetTotalEdits();
				int CurTotalMinorEdits = GetTotalMinEdits();
				int CurIsFeatured = GetIsFeatured();
				//25000 bytes is about what the wiki cruft is
				int CurTotalLength = GetTotalLength() - 25000;
				int CurViewsInLast30Days = GetViewsInLast30Days();



				int internalRating = (linksToWeight * CurLinksTo) +
							(linksFromWeight * CurLinksFrom) +
							(minutesSinceLastEditWeight * CurMinutesSinceLastEdit) +
							(totalEditsWeight * CurTotalEdits) +
							(totalMinorEditsWeight * CurTotalMinorEdits) +
							(isFeaturedWeight * CurIsFeatured) +
							(int)(totalLengthWeight * (double)CurTotalLength) +
							(viewsInLast30DaysWeight * CurViewsInLast30Days);

				if (internalRating <= 300)
					rating = 1;
				else if (internalRating > 300 && internalRating <= 5000)
					rating = 2;
				else if (internalRating > 5000 && internalRating <= 10000)
					rating = 3;
				else if (internalRating > 10000 && internalRating <= 15000)
					rating = 4;
				else if (internalRating > 15000 && internalRating <= 22000)
					rating = 5;
				else if (internalRating > 22000 && internalRating <= 30000)
					rating = 6;
				else if (internalRating > 30000 && internalRating <= 38000)
					rating = 7;
				else if (internalRating > 38000 && internalRating <= 45000)
					rating = 8;
				else if (internalRating > 45000 && internalRating <= 60000)
					rating = 9;
				else if (internalRating > 60000)
					rating = 10;
			}
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
			return body.Length;
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
			string linkPages = "http://en.wikipedia.org/w/index.php?title=Special:WhatLinksHere&target={0}&namespace=0&limit=50000";
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(string.Format(linkPages, title));
			request.Method = "GET";
			request.UserAgent = "WikiRater(whoisjoe.com)/0.8";

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
			catch (WebException)
			{
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
