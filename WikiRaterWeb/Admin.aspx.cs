using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace WikiRaterWeb
{
	public partial class Admin : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();

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
						Auth.CreateEvent("Unauthorized All Ratings Attempt", "Attempted by: " + Auth.LookupUserName(userID), Request.UserHostAddress);
						Response.Redirect("Login.aspx");
					}
				}
				else
					Response.Redirect("Login.aspx");
			}
			catch (ThreadAbortException)
			{

			}
			catch (Exception ex)
			{
				Response.Redirect("Login.aspx");
				Auth.CreateEvent("Unauthorized All Ratings Attempt:" + ex.Message, ex.ToString(), Request.UserHostAddress);
			}
		}

		protected void Submit_Click(object sender, EventArgs e)
		{
			dc.AddNewArticleToIP(ArticleTitle.Text, DateTime.Now);
			ArticleTitle.Text = "";
			Message.Text = "Article successfully added to IP";
		}

		protected void AddAchievement_Click(object sender, EventArgs e)
		{
			//check if the achievement already exists, if it does overwrite, otherwise add it
			int value = 0;
			int.TryParse(AchValue.Text, out value);
			dc.AddAchievement(AchShortName.Text, AchTitle.Text, AchDescription.Text, value, AchIcon.Text);
			AchShortName.Text = AchTitle.Text = AchDescription.Text = AchValue.Text = AchIcon.Text = "";
			Message.Text = "Achievement Text created, make sure this is represented in the code";
		}

		protected void logEverybodyOut_Click(object sender, EventArgs e)
		{
			dc.Sessions.DeleteAllOnSubmit(from s in dc.Sessions select s);
			dc.SubmitChanges();
		}
	}
}