using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace RatingEngine
{
	class Rater
	{
		string title = "";

		int numSentences = 0;
		int numWords = 0;
		int numSyllables = 0;
		int numHeaders = 0;
		int numImages = 0;
		double flesch = 0;
		double readingLevel = 0;

		private int Rate(string article)
		{

			return 10;
		}

		private int FindRatingByRange(int value, int level1, int level2, int level3, int level4,
			int level5, int level6, int level7, int level8, int level9, int level10)
		{
			int rating = 0;

			if (value <= level1)
				rating = 1;
			else if (value > level1 && value <= level2)
				rating = 2;
			else if (value > level2 && value <= level3)
				rating = 3;
			else if (value > level3 && value <= level4)
				rating = 4;
			else if (value > level4 && value <= level5)
				rating = 5;
			else if (value > level5 && value <= level6)
				rating = 6;
			else if (value > level6 && value <= level7)
				rating = 7;
			else if (value > level7 && value <= level8)
				rating = 8;
			else if (value > level8 && value <= level9)
				rating = 9;
			else if (value > level10)
				rating = 10;

			return rating;
		}

		private int GetIsFeatured(string input)
		{
			string FeaturedIcon = "14px-Cscr-featured.svg.png";
			if (Regex.IsMatch(input, FeaturedIcon))
				return 10;
			else
				return 5;
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

			return FindRatingByRange(linksto, 10, 20, 50, 100, 150, 200, 300, 400, 500, 650);
		}

		private int GetViewsInLast30DaysRating()
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

			return FindRatingByRange(views, 30, 100, 400, 1000, 1500, 2500, 3500, 4500, 5500, 6500);
		}

		private int CountParagraphs(string input)
		{
			int count = 0;
			foreach (String s in Regex.Split(input, "\r\n"))
			{
				if (CountWords(s) > 2)
					count++;
			}
			return count;
		}

		private void GetFlesch(string input)
		{
			Regex sentences = new Regex(@".+?\. ");
			foreach (Match s in sentences.Matches(input))
			{
				numSentences++;
				Regex words = new Regex(".+? ");
				foreach (Match w in words.Matches(s.Value))
				{
					numWords++;
					string word = Regex.Match(w.Value, @"\w+").Value;
					numSyllables += CountSyllables(word);
				}
			}

			flesch = 206.835 - 1.015 * (numWords / numSentences) - 84.6 * (numSyllables / numWords);
			readingLevel = 0.39 * (numWords / numSentences) + 11.8 * (numSyllables / numWords) - 15.59;
		}

		/// <summary>
		/// How I am Finding Syllables:
		///Count the number of vowels (a, e, i, o, u, and sometimes y) in the word.
		///Subtract any silent vowels (like the silent 'e' at the end of a word).
		///Subtract 1 vowel from every diphthong.
		///A diphthong is when two volwels make only 1 sound (oi, oy, ou, ow, au, aw, oo, ...).
		///The number you are left with should be the number of vowels in the word.
		///http://www.howmanysyllables.com/howtocountsyllables.html
		///
		/// This method is inaccurate, but pretty close. It screws up on challenging 
		/// words like "hyphenation" and "invisible"
		/// 
		/// I'd like to return to this and implement something more accurate as was discussed in
		/// this 1983 NLP thesis: http://www.tug.org/docs/liang/
		/// 
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		private int CountSyllables(string word)
		{
			char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
			string currentWord = word;
			int numVowels = 0;
			bool lastWasVowel = false;
			foreach (char wc in currentWord)
			{
				bool foundVowel = false;
				foreach (char v in vowels)
				{
					//don't count diphthongs
					if (v == wc && lastWasVowel)
					{
						foundVowel = true;
						lastWasVowel = true;
						break;
					}
					else if (v == wc && !lastWasVowel)
					{
						numVowels++;
						foundVowel = true;
						lastWasVowel = true;
						break;
					}
				}

				//if full cycle and no vowel found, set lastWasVowel to false;
				if (!foundVowel)
					lastWasVowel = false;
			}
			//remove es, it's _usually? silent
			if (currentWord.Length > 2 &&
				currentWord.Substring(currentWord.Length - 2) == "es")
				numVowels--;
			// remove silent e
			else if (currentWord.Length > 1 &&
				currentWord.Substring(currentWord.Length - 1) == "e")
				numVowels--;

			return numVowels;
		}

		private int hardCodedWords(string word)
		{
			return 0;
		}

		private int CountWords(string input)
		{
			Regex r = new Regex(".+? ");
			return r.Matches(input).Count;
		}

		private string ParseWiki(string input)
		{
			string output = input;
			string[] regexs = {	
								@"""\*""", //beginning star
								@"\{\{.*?\}\}", //stach
								@"'''(.*?)'''", //bold
								@"\[\[Image:(.*?\||)?([\w\s=\.\-/'%&?+,:;@]*?)\]\]", //Images
								@"\[\[(.*?\|?)?([\w\s=\.\-/%&?+,:;'@]*?)\]\]",//Internal Links
								@"<ref.*?>.*?<\/ref>", //references
								@"''(.*?)''", //italic
								@"===(.*?)===", //H3
								@"==(.*?)==", //H2
								@"=(.*?)=", //H1
								@"<source.*?>.*?<\/source>", //Remove source lines, count them later
								@"[ ]+\*", //bullets
								@"[ ]+\d",//numbered lists, 
								@"\[http[s]?://[A-Z0-9.-]+\.[A-Z]{2,6}.+?\]", //External Links
								@"http[s]?://[a-z0-9=\.\-/%&?+,:';@]+" //External Links without brackets
							  };

			foreach (string regex in regexs)
			{
				Regex r = new Regex(regex, RegexOptions.IgnoreCase);

				int zerocount = 0;
				int onecount = 0;
				int twocount = 0;
				foreach (Match m in r.Matches(output))
				{
					if (m.Groups.Count == 3)
					{
						output = output.Replace(m.Groups[0].Value, m.Groups[2].Value);
						twocount++;
					}
					else if (m.Groups.Count == 2)
					{
						output = output.Replace(m.Groups[0].Value, m.Groups[1].Value);
						onecount++;
					}
					else
					{
						output = r.Replace(output, "");
						zerocount++;
					}
				}
				//Count stuff using regex
				if (regex == @"==(.*?)==")
					numHeaders =  onecount;
				else if (regex == @"\[\[Image:(.*?\||)?([\w\s=\.\-/'%&?+,:;@]*?)\]\]")
					numImages = twocount;
			}
			output = output.Replace("\\r", "\r\n");

			return output;
		}
	}
}
