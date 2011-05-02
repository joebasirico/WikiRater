using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class Register : System.Web.UI.Page
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
					RegisterPanel.Visible = false;
					AlreadyLoggedIn.Visible = true;
				}
			}
		}

		protected void DoRegister_Click(object sender, EventArgs e)
		{

			try
			{
				if (!Auth.UserExists(UsernameBox.Text))
				{
					if (!new Regex(Settings.Default.UsernameRegex).IsMatch(UsernameBox.Text))
						Message.Text = Settings.Default.UsernameFailedMatchMessage;
					else
					{
						if (!new Regex(Settings.Default.emailRegex).IsMatch(email.Text))
							Message.Text = Settings.Default.EmailFailedMatchMessage;
						else
						{
							//Add the user
							Auth.registerUser(UsernameBox.Text,
								Encoding.ASCII.GetString(
									SHA512Managed.Create().ComputeHash(
										Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))), email.Text);
							//Log the event
							Auth.CreateEvent("Created User", "UserName: " + UsernameBox.Text + "\r\n", Request.UserHostAddress);

							//Login the new user 
							//check the user has been created properly
							int userID = Auth.checkCredentials(UsernameBox.Text,
										Encoding.ASCII.GetString(
											SHA512Managed.Create().ComputeHash(
												Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))));

							//if the user is valid and the creds are still good log them in and give them a cookie
							if (userID != 0)
							{
								Guid session = Guid.NewGuid();
								Auth.createSession(userID, session);
								Auth.CreateEvent("Successful Login", "By user: " + UsernameBox.Text, Request.UserHostAddress);
								Response.Cookies.Add(new HttpCookie("session", session.ToString()));

								//Change the UI to reflect everytihng went well
								RegisterPanel.Visible = false;
								RegistrationCompletePanel.Visible = true;
								Bookmarklet.Text = Settings.Default.RateOnWikiRaterText;
								Bookmarklet.NavigateUrl = String.Format(Settings.Default.Bookmarklet, Settings.Default.CurrentDomain);
							}
							else
							{
								Auth.CreateEvent("Failed Login Attempt", "By user: " + UsernameBox.Text, Request.UserHostAddress);
								RegisterPanel.Visible = false;
								ErrorPanel.Visible = true;
							}
						}
					}
				}
				else
					Message.Text = "That username is already in use, please select another. May I suggest: \"" + Auth.GenerateRandomUserName() + "\"?";
			}
			catch (Exception ex)
			{
				Message.Text = "Sorry, you couldn't be registered because: " + Server.HtmlEncode(ex.Message);
				Auth.CreateEvent("Could not Register:" + ex.Message, ex.ToString(), Request.UserHostAddress);
			}
		}
	}
}