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

namespace WikiRaterWeb
{
	public partial class Vote : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				try
				{
					Regex reg = new Regex(Settings.Default.WikiTitleRegex);
					string urlmatch = reg.Match(Request["Article"]).Groups[2].Value;
					if (urlmatch.EndsWith("noui"))
						urlmatch = urlmatch.Substring(0, urlmatch.Length - 4);

					Guid session = new Guid();
					//we've never seen this user before or they've cleared their cookies
					if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
					{
						int userID = Auth.checkSession(session);
						if (userID != 0)
						{
							if (urlmatch.Length > 0)
							{
								url.Text = Server.HtmlEncode(urlmatch);
								if (Settings.Default.LogAllURLs)
									Auth.CreateEvent("URL to Match", "by: " + Auth.LookupUserName(userID) + " \r\n" + Request["Article"], Request.UserHostAddress);
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
							Response.Redirect("Login.aspx?URL=" + Server.UrlEncode(urlmatch));
						}
					}
					else
					{
						Auth.CreateEvent("Could Not Parse Session Cookie", "Cookie Value: " + Request.Cookies["session"].Value, Request.UserHostAddress);
						Response.Redirect("Login.aspx?URL=" + Server.UrlEncode(urlmatch));
					}
				}
				catch (ThreadAbortException)
				{

				}
				catch (Exception ex)
				{
					Auth.CreateEvent("Could Not Add Rating:" + ex.Message, ex.ToString() + "\r\nPage: " + Request["Article"], Request.UserHostAddress);
					Response.Redirect("Login.aspx");
				}
			}
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
							DataClassesDataContext dc = new DataClassesDataContext();
							dc.AddRating(userID, url.Text, votes);
							Auth.CreateEvent("New Vote Added", Auth.LookupUserName(userID) + " rated " + url.Text + " a " + votes, Request.UserHostAddress);
							UserRating.Text = votes.ToString();
							try
							{
								Article art = new Article("http://en.wikipedia.org/wiki/" + url.Text);
								if (art.rating > 0)
									WikiRaterRating.Text = "WikiRater would have rated this article: " + art.rating.ToString() + "<br />";
								else
									WikiRaterRating.Visible = false;
							}
							catch (Exception ex)
							{
								Auth.CreateEvent("Could Not Rate Article: " + ex.Message, "by: " + Auth.LookupUserName(userID) + "\r\n" + ex.ToString(), Request.UserHostAddress);
							}
						}
						catch (Exception ex)
						{
							Auth.CreateEvent("Could Not Add Rating: " + ex.Message, "by: " + Auth.LookupUserName(userID) + "\r\n" + ex.ToString(), Request.UserHostAddress);
						}
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