using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using RatingEngine;
using WikiRaterWeb.Properties;
using System.Threading;
using System.Text;

namespace WikiRaterWeb
{
	public partial class Vote : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		AchievementValidator av = new AchievementValidator();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				string urlmatch = "";
				try
				{
					try
					{
						Regex reg = new Regex(Settings.Default.WikiTitleRegex);
						urlmatch = reg.Match(Request["Article"]).Groups[2].Value;
						if (urlmatch.EndsWith("noui"))
							urlmatch = urlmatch.Substring(0, urlmatch.Length - 4);
					}
					catch (Exception ex)
					{
						Auth.CreateEvent("Could not Parse Article", ex.ToString(), Request.UserHostAddress);
					}
					Guid session = new Guid();
					//we've never seen this user before or they've cleared their cookies
					if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
					{
						int userID = Auth.checkSession(session);
						if (userID != 0)
						{
							if (urlmatch.Length > 0)
							{
								url.Text = FormatTitle(urlmatch);
								if (Settings.Default.LogAllURLs)
									Auth.CreateEvent("URL to Match", "by: " + Auth.LookupUserName(userID) + " \r\n" + Request["Article"], Request.UserHostAddress);
							}
							else if (Server.UrlDecode(Request["Article"]).Contains(Settings.Default.CurrentDomain))
							{
								VotePanel.Visible = false;
								stillOnWikiRater.Visible = true;
							}
							else
							{
								VotePanel.Visible = false;
								InvalidPage.Visible = true;
							}
						}
						else
						{
							Auth.CreateEvent("Invalid ", "by: " + Auth.LookupUserName(userID) + " \r\n" + "Cookie Value: " + Request.Cookies["session"].Value, Request.UserHostAddress);
							Response.Redirect("Login.aspx?Article=" + Server.UrlEncode(urlmatch));
						}
					}
					else
					{
						Auth.CreateEvent("Could Not Parse Session Cookie", "Cookie Value: " + Request.Cookies["session"].Value, Request.UserHostAddress);
						Response.Redirect("Login.aspx?Article=" + Server.UrlEncode(urlmatch));
					}
				}
				catch (ThreadAbortException)
				{

				}
				catch (Exception ex)
				{
					Auth.CreateEvent("Could Not Add Rating:" + ex.Message, ex.ToString() + "\r\nPage: " + Request["Article"], Request.UserHostAddress);
					Response.Redirect("Login.aspx?Article=" + Server.UrlEncode(urlmatch));
				}
			}
		}

		private string FormatTitle(string title)
		{
			return Regex.Replace(title, "[^a-z0-9]", " ", RegexOptions.IgnoreCase);
		}
		private void MakeVote(int votes)
		{
			try
			{
				Guid session = new Guid();
				//we've never seen this user before or they've cleared their cookies
				if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				{
					int userID = Auth.checkSession(session);
					if (userID != 0)
					{
						try
						{
							Regex reg = new Regex(Settings.Default.WikiTitleRegex);
							string urlmatch = reg.Match(Request["Article"]).Groups[2].Value;
							if (urlmatch.EndsWith("noui"))
								urlmatch = urlmatch.Substring(0, urlmatch.Length - 4);

							DataClassesDataContext dc = new DataClassesDataContext();
							dc.AddRating(userID, urlmatch, votes, DateTime.Now);
							Auth.CreateEvent("New Vote Added", Auth.LookupUserName(userID) + " rated " + urlmatch + " a " + votes, Request.UserHostAddress);
							UserRating.Text = votes.ToString();
							if (RatingExists(urlmatch))
							{
								int rating = LookupRating(urlmatch);
								WikiRaterRating.Text = "WikiRater would have rated this article: " + rating + "<br />";
							}
							else
							{
								try
								{
									Article art = new Article(urlmatch);
									if (art.rating > 0)
									{
										WikiRaterRating.Text = "WikiRater would have rated this article: " + art.rating.ToString() + "<br />";
										SaveRating(urlmatch, art.rating);
									}
									else
										WikiRaterRating.Visible = false;
								}
								catch (Exception ex)
								{
									Auth.CreateEvent("Could Not Rate Article: " + ex.Message, "by: " + Auth.LookupUserName(userID) + "\r\n" + ex.ToString(), Request.UserHostAddress);
								}
							}
						}
						catch (Exception ex)
						{
							Auth.CreateEvent("Could Not Add Rating: " + ex.Message, "by: " + Auth.LookupUserName(userID) + "\r\n" + ex.ToString(), Request.UserHostAddress);
						}

						CheckAndPopulateAchievements(userID);


						VotePanel.Visible = false;
						VoteCompletedPanel.Visible = true;
					}
				}
			}
			catch (ThreadAbortException)
			{

			}
			catch (Exception ex)
			{
				Auth.CreateEvent("Could Not Add Rating:" + ex.Message, ex.ToString(), Request.UserHostAddress);
				Response.Redirect("Login.aspx");
			}
		}

		private void CheckAndPopulateAchievements(int userID)
		{
			List<Achievement> achieves = av.CheckAchievements(userID);
			if (isMobileBrowser())
				achieves.Add(av.GetAchievementByShortName("Mobile"));

			List<Achievement> newAchievements = av.CheckIfNewAchievements(achieves, userID);
			av.AddNewAchievements(newAchievements, userID);
			StringBuilder sb = new StringBuilder();
			if (newAchievements.Count > 0)
				AchievementPanel.Visible = true;
			foreach (Achievement a in newAchievements)
			{
				if (!string.IsNullOrEmpty(a.Icon))
					sb.AppendFormat(Settings.Default.AchievementFormat, a.Icon, a.Name, a.Description);
				else
					sb.AppendFormat(Settings.Default.AchievementFormatNoIcon, a.Name, a.Description);
			}

			AchievementList.Text = sb.ToString();
		}

		//from CodeProject
		//http://www.codeproject.com/KB/aspnet/mobiledetect.aspx
		public bool isMobileBrowser()
		{
			//GETS THE CURRENT USER CONTEXT
			HttpContext context = HttpContext.Current;

			//FIRST TRY BUILT IN ASP.NT CHECK
			if (context.Request.Browser.IsMobileDevice)
			{
				return true;
			}
			//THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
			if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
			{
				return true;
			}
			//THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
			if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
				context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
			{
				return true;
			}
			//AND FINALLY CHECK THE HTTP_USER_AGENT 
			//HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
			if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
			{
				//Create a list of all mobile types
				string[] mobiles =
					new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone", "ipad"
                };

				//Loop through each item in the list created above 
				//and check if the header contains that text
				foreach (string s in mobiles)
				{
					if (context.Request.ServerVariables["HTTP_USER_AGENT"].
														ToLower().Contains(s.ToLower()))
					{
						return true;
					}
				}
			}

			return false;
		}

		private void SaveRating(string title, int value)
		{
			if (!dc.Users.Any(u => u.UserName == Settings.Default.WikiRaterName))
				dc.CreateUser(Settings.Default.WikiRaterName, "DNE", DateTime.Now, false, "wikirater@example.com");
			dc.AddRating(dc.Users.First(u => u.UserName == Settings.Default.WikiRaterName).UserID,
				title, value, DateTime.Now);
		}

		private int LookupRating(string title)
		{
			return dc.Ratings.First(r => r.Article == title &&
				r.User.UserName == Settings.Default.WikiRaterName).Value;
		}

		private bool RatingExists(string title)
		{
			return dc.Ratings.Any(r => r.Article == title && r.User.UserName == Settings.Default.WikiRaterName);
		}

		protected void vote1_Click(object sender, EventArgs e)
		{
			MakeVote(1);
		}
		protected void vote2_Click(object sender, EventArgs e)
		{
			MakeVote(2);
		}
		protected void vote3_Click(object sender, EventArgs e)
		{
			MakeVote(3);
		}
		protected void vote4_Click(object sender, EventArgs e)
		{
			MakeVote(4);
		}
		protected void vote5_Click(object sender, EventArgs e)
		{
			MakeVote(5);
		}
		protected void vote6_Click(object sender, EventArgs e)
		{
			MakeVote(6);
		}
		protected void vote7_Click(object sender, EventArgs e)
		{
			MakeVote(7);
		}
		protected void vote8_Click(object sender, EventArgs e)
		{
			MakeVote(8);
		}
		protected void vote9_Click(object sender, EventArgs e)
		{
			MakeVote(9);
		}
		protected void vote10_Click(object sender, EventArgs e)
		{
			MakeVote(10);
		}
	}
}