using System;
using System.Collections.Generic;
using System.Linq;
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

		}

		protected void DoRegister_Click(object sender, EventArgs e)
		{

			try
			{
				if (RegCode.Text == Settings.Default.RegCode)
				{
					if (!Auth.UserExists(UsernameBox.Text))
					{
						if (!new Regex(Settings.Default.UsernameRegex).IsMatch(UsernameBox.Text))
							Message.Text = Settings.Default.UsernameFailedMatchMessage;
						else
						{
							Auth.registerUser(UsernameBox.Text,
								Encoding.ASCII.GetString(
									SHA512Managed.Create().ComputeHash(
										Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))));
							Auth.CreateEvent("Created User", "UserName: " + UsernameBox.Text + "\r\n", Request.UserHostAddress);
							RegisterPanel.Visible = false;
							RegistrationCompletePanel.Visible = true;

							int userID = Auth.checkCredentials(UsernameBox.Text,
										Encoding.ASCII.GetString(
											SHA512Managed.Create().ComputeHash(
												Encoding.ASCII.GetBytes(UsernameBox.Text + Auth.getSaltyGoo() + PasswordBox.Text))));
							if (userID != 0)
							{
								Guid session = Guid.NewGuid();
								Auth.createSession(userID, session);
								Auth.CreateEvent("Successful Login", "By user: " + UsernameBox.Text, Request.UserHostAddress);
								Response.Cookies.Add(new HttpCookie("session", session.ToString()));
							}
							else
							{
								Auth.CreateEvent("Failed Login Attempt", "By user: " + UsernameBox.Text, Request.UserHostAddress);
							}
						}
					}
					else
						Message.Text = "That username is already in use, please select another. May I suggest: \"" + Auth.GenerateRandomUserName() + "\"?";
				}
				else
					Auth.CreateEvent("Registration Attempt", "User failed to supply correct Registration Code", Request.UserHostAddress);
			}
			catch (Exception ex)
			{
				Message.Text = "Sorry, you couldn't be registered because: " + Server.HtmlEncode(ex.Message);
				Auth.CreateEvent("Could not Register:" + ex.Message, ex.ToString(), Request.UserHostAddress);
			}
		}
	}
}