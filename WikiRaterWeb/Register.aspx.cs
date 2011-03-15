using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

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
				if (RegCode.Text == "TikiHead")
				{
					if (!Auth.UserExists(UsernameBox.Text))
					{
						if (!new Regex("^[a-zA-Z0-9_\\.\\-]{3,}$").IsMatch(UsernameBox.Text))
							Message.Text = "Usernames must be greater than three characters and only contain only a-z 0-9, _, -, .";
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
					Auth.CreateEvent("Registration Attepmt", "User failed to supply correct Registration Code", Request.UserHostAddress);
			}
			catch (Exception ex)
			{
				Message.Text = "Sorry, you couldn't be registered because: " + Server.HtmlEncode(ex.Message);
				Auth.CreateEvent("Could not Register:" + ex.Message, ex.ToString(), Request.UserHostAddress);
			}
		}
	}
}