using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace FindGoodRandWikiPage
{
    class Article
    {
        public string title = "";
        public string url = "";
        //public string wikiTitle = "";
        public string body = "";
        public int rating = 0;

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

            //string wikiMatch = "<link rel=\"alternate\" type=\"application/x-wiki\" title=\"Edit this page\" href=\"/wikipedia/en/w/index.php\\?title=(.+)&amp;action=edit\" />";
            //Regex wikiRegex = new Regex(wikiMatch);
            //wikiTitle = wikiRegex.Match(body).Groups[1].Value;
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
            int linksToWeight = 0;
            int linksFromWeight = 1;
            int minutesSinceLastEditWeight = 0;
            int totalEditsWeight = 0;
            int totalMinorEditsWeight = 0;
            int isFeaturedWeight = 0;
            int totalLengthWeight = 0;
            int viewsInLast30DaysWeight = 0;

            rating = (linksToWeight * GetLinksTo()) +
                     (linksFromWeight * GetLinksFrom()) +
                     (minutesSinceLastEditWeight * GetMinSinceLastEdit()) +
                     (totalEditsWeight * GetTotalEdits()) +
                     (totalMinorEditsWeight * GetTotalMinEdits()) +
                     (isFeaturedWeight * GetIsFeatured()) +
                     (totalLengthWeight * GetTotalLength()) +
                     (viewsInLast30DaysWeight * GetViewsInLast30Days());
            

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
