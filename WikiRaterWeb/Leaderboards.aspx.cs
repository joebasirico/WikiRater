using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WikiRaterWeb.Properties;
using System.Configuration;

namespace WikiRaterWeb
{
	public partial class Leaderboards : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		AchievementValidator av = new AchievementValidator();
		protected void Page_Load(object sender, EventArgs e)
		{

			DataTable dt = new DataTable();
			dt.Columns.Add("UserID");
			dt.Columns.Add("UserName");
			dt.Columns.Add("Count", System.Type.GetType("System.Int32"));

			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand("SELECT UserID, UserName, TimeCreated FROM [User] WHERE Active = 1;", conn);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					int userID = reader.GetInt32(0);
					string username = reader.GetString(1);
					if (username != Settings.Default.WikiRaterName)
					{
						DataRow dr = dt.NewRow();
						dr["UserID"] = userID;
						//already validated, but encode anyway
						dr["UserName"] = Server.HtmlEncode(username);
						int points = av.GetPoints(userID, false);
						dr["Count"] = points;
						if (points > 0)
							dt.Rows.Add(dr);
					}
				}
			}
			dt.DefaultView.Sort = "Count DESC";
			LeaderboardList.DataSource = dt.DefaultView;
			LeaderboardList.DataBind();
		}
	}
}