using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using RatingEngine;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class Vote : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
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
						Regex reg = new Regex("^.*(wiki/|index.php\\?title=)([\\w\\.\\(\\)'%,/!\\-]+)(noui)?.*?$");
						string urlmatch = reg.Match(Request["URL"]).Groups[2].Value;
						if (urlmatch.EndsWith("noui"))
							urlmatch = urlmatch.Substring(0, urlmatch.Length - 4);
						url.Text = Server.HtmlEncode(urlmatch);
						if(Settings.Default.LogAllURLs)
							Auth.CreateEvent("URL to Match", "by: " + Auth.LookupUserName(userID) + " \r\n" + Request["URL"], Request.UserHostAddress);
					}
					else
					{
						Auth.CreateEvent("Invalid ", "by: " + Auth.LookupUserName(userID) + " \r\n" + "Cookie Value: " + Request.Cookies["session"].Value, Request.UserHostAddress);
						Response.Redirect("Login.aspx");
					}
				}
				else
				{
					Auth.CreateEvent("Could Not Parse Session Cookie", "Cookie Value: " + Request.Cookies["session"].Value, Request.UserHostAddress);
					Response.Redirect("Login.aspx");
				}
			}
			catch (Exception ex)
			{
				Auth.CreateEvent("Could Not Add Rating:" + ex.Message, ex.ToString() + "\r\nPage: " + Request["URL"], Request.UserHostAddress);
				Response.Redirect("Login.aspx");
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
						Article art = new Article("http://en.wikipedia.org/wiki/" + url.Text);
						WikiRaterRating.Text = art.rating.ToString();
						UserRating.Text = votes.ToString();
						DataClassesDataContext dc = new DataClassesDataContext();
						dc.AddRating(userID, url.Text, votes);
						Auth.CreateEvent("New Vote Added", Auth.LookupUserName(userID) + " rated " + url.Text + " a " + votes, Request.UserHostAddress);
						VotePanel.Visible = false;
						VoteCompletedPanel.Visible = true;
					}
				}
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