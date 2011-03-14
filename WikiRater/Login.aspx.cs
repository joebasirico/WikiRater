using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

public partial class Login : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
	protected void DoLogin_Click(object sender, EventArgs e)
	{
		try
		{
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

				//if they've been redirected here from Vote we'll register their vote now.
				if (!(string.IsNullOrEmpty(Request["URL"]) || string.IsNullOrEmpty(Request["Rating"])))
				{
					string url = Request["URL"];
					int votes = 0;
					int.TryParse(Request["Rating"], out votes);

					DataClassesDataContext dc = new DataClassesDataContext();
					dc.AddRating(userID, url, votes);
				}

				LoginPanel.Visible = false;
				LoginCompletePanel.Visible = true;
			}
			else
			{
				Message.Text = "Incorrect username or password, please try again.<br/>";
				Auth.CreateEvent("Failed Login Attempt", "By user: " + UsernameBox.Text, Request.UserHostAddress);
			}
		}
		catch (Exception ex)
		{
			Message.Text = "Sorry, you couldn't be logged in because: " + Server.HtmlEncode(ex.Message);
			Auth.CreateEvent("Could not log user in" + ex.Message, ex.ToString(), Request.UserHostAddress);
		}
	}
}