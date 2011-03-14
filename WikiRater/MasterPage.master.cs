using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Guid session = new Guid();
		if (Request.Cookies["session"] != null && !string.IsNullOrEmpty(Request.Cookies["session"].Value) && Guid.TryParse(Request.Cookies["session"].Value, out session))
		{
			int userID = Auth.checkSession(session);
			string username = Auth.LookupUserName(userID);
			if (!string.IsNullOrEmpty(username))
				footerStuff.Text = "You are currently logged in as: " + username + " <a href=\"Logout.aspx\">click here to logout</a>";
			else
				footerStuff.Text = "";
		}
		else
			footerStuff.Text = "";

		TitleLink.NavigateUrl = "http://wikirater.whoisjoe.com";//change to settings.
	}
}
