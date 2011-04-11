using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class Leaderboards : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		AchievementValidator av = new AchievementValidator();
		protected void Page_Load(object sender, EventArgs e)
		{
			
			var uniqueusers = (from u in dc.Users
							   select u.UserID).Distinct();

			DataTable dt = new DataTable();
			dt.Columns.Add("UserID");
			dt.Columns.Add("UserName");
			dt.Columns.Add("Count", System.Type.GetType("System.Int32"));

			foreach (int uid in uniqueusers)
			{
				string username = Auth.LookupUserName(uid);
				if (username != Settings.Default.WikiRaterName)
				{
					DataRow dr = dt.NewRow();
					dr["UserID"] = uid;
					//already validated, but encode anyway
					dr["UserName"] = Server.HtmlEncode(username);
					int points = av.GetPoints(uid, false);
					dr["Count"] = points;
					if(points > 0)
						dt.Rows.Add(dr);
				}
			}
			dt.DefaultView.Sort = "Count DESC";
			LeaderboardList.DataSource = dt.DefaultView;
			LeaderboardList.DataBind();

		}
	}
}