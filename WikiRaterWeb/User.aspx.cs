using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;

namespace WikiRaterWeb
{
	public partial class User1 : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		AchievementValidator av = new AchievementValidator();

		protected void Page_Load(object sender, EventArgs e)
		{
			string username = "";
			if (!(string.IsNullOrWhiteSpace(Request["Username"])))
				username = Request["Username"];
			else
				username = GetCurrentUser();

			UserName.Text = Server.HtmlEncode(username);

			User currentUser = dc.Users.First(u => u.UserName == username);
			MemberSince.Text = currentUser.TimeCreated.ToLongDateString();

			UniqueRatings.Text = dc.Ratings.Count(r => r.User.UserName == username && r.IsLatest).ToString();

			Points.Text = av.GetPoints(currentUser.UserID, false).ToString();

			GenerateAchievementList(currentUser.UserID);
		}
		
		private string GetCurrentUser()
		{
			try
			{
				Guid session = new Guid();
				if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				{
					int userID = Auth.checkSession(session);
					if (userID != 0)
						return Auth.LookupUserName(userID);
				}
				Response.Redirect("Login.aspx");
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception)
			{
			}
			return "";
		}

		private void GenerateAchievementList(int userID)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Achieved");
			dt.Columns.Add("Title");
			dt.Columns.Add("Value");
			dt.Columns.Add("Description");
			dt.Columns.Add("Icon");
			dt.Columns.Add("HasIcon");

			List<Achievement> accomplished = av.CheckAchievements(userID);

			foreach (Achievement a in dc.Achievements.ToList())
			{
				DataRow dr = dt.NewRow();
				dr["Title"] = a.Name;
				dr["Description"] = a.Description;
				dr["Value"] = a.Value;
				if (!string.IsNullOrEmpty(a.Icon))
				{
					dr["Icon"] = a.Icon;
					dr["HasIcon"] = "inherit";
				}
				else
					dr["HasIcon"] = "none";
				
				bool found = false;
				foreach (Achievement b in accomplished)
				{
					if (a.ShortName == b.ShortName)
					{
						dr["Achieved"] = "AccomplishedAchievement";
						found = true;
						break;
					}
				}
				if(!found)
					dr["Achieved"] = "UnaccomplishedAchievement";

				dt.Rows.Add(dr);
			}

			AchievementsList.DataSource = dt;
			AchievementsList.DataBind();
		}
	}
}