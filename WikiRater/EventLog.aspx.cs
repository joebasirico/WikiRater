using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EventLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		try
		{
			Guid session = new Guid();
			if (Request.Cookies["session"] == null)
				Response.Redirect("Login.aspx");
			if (Guid.TryParse(Request.Cookies["session"].Value, out session))
			{
				int userID = Auth.checkSession(session);
				if (!(userID != 0 && Auth.LookupUserName(userID) == "joe"))
				{
					Auth.CreateEvent("Unauthorized Event Log Attempt", "Attempted by: " + Auth.LookupUserName(userID), Request.UserHostAddress);
					Response.Redirect("Login.aspx");
				}
			}
		}
		catch(Exception ex)
		{
			Auth.CreateEvent("Unauthorized Event Log Attempt:" + ex.Message, ex.ToString(), Request.UserHostAddress);
		}
    }
}