﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Guid session = new Guid();
			//we've never seen this user before or they've cleared their cookies
			if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
			{
				int userID = Auth.checkSession(session);
				if (userID != 0)
				{
					LoginPanel.Visible = false;
					AlreadyLoggedIn.Visible = true;
				}
			}
		}

		protected void DoLogin_Click(object sender, EventArgs e)
		{
			try
			{
				int userID = 0;

				userID = Auth.checkCredentials(UsernameBox.Text,
									Auth.ByteToHex(
										SHA512Managed.Create().ComputeHash(
											Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))));

				//grrr stupid mistake.
				if (Settings.Default.AllowLegacyHash && userID == 0)
				{
					userID = Auth.checkCredentials(UsernameBox.Text,
									Encoding.ASCII.GetString(
										SHA512Managed.Create().ComputeHash(
											Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))));

					//update it so we don't have to deal with this in the future
					Auth.UpdatePassword(userID, Auth.ByteToHex(
										SHA512Managed.Create().ComputeHash(
											Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))));
				}

				if (userID != 0)
				{
					Guid session = Guid.NewGuid();
					Auth.createSession(userID, session);
					Auth.CreateEvent("Successful Login", "By user: " + UsernameBox.Text, Request.UserHostAddress);

					Response.Cookies.Add(new HttpCookie("session", session.ToString()));
					if (RememberMe.Checked)
						Response.Cookies["session"].Expires = DateTime.Now.AddMonths(1);

					//if they've been redirected here from Vote we'll register their vote now.
					if (!(string.IsNullOrWhiteSpace(Request["Article"])))
					{
						string url = Request["Article"];
						Response.Redirect("Vote.aspx?Article=" + Server.UrlEncode("http://en.wikipedia.org/wiki/" + Server.UrlDecode(url)));
					}

					if (!(string.IsNullOrWhiteSpace(Request["Page"])))
					{
						string page = Request["Page"];
						Response.Redirect(Server.UrlEncode(page));
					}

					Response.Redirect("/Default.aspx");
				}
				else
				{
					Message.Text = "Incorrect username or password, please try again.<br/>";
					Auth.CreateEvent("Failed Login Attempt", "By user: " + UsernameBox.Text, Request.UserHostAddress);
				}
			}
			catch (ThreadAbortException)
			{

			}
			catch (Exception ex)
			{
				Message.Text = "Sorry, you couldn't be logged in because: " + Server.HtmlEncode(ex.Message);
				Auth.CreateEvent("Could not log user in" + ex.Message, ex.ToString(), Request.UserHostAddress);
			}
		}
	}
}