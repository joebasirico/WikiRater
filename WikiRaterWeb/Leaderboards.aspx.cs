using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WikiRaterWeb
{
	public partial class Leaderboards : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataClassesDataContext dc = new DataClassesDataContext();
			var uniqueusers = (from u in dc.Ratings
							   select u.UserID).Distinct();

			DataTable dt = new DataTable();
			dt.Columns.Add("UserID");
			dt.Columns.Add("UserName");
			dt.Columns.Add("Count", System.Type.GetType("System.Int32"));

			foreach (int uid in uniqueusers)
			{
				DataRow dr = dt.NewRow();
				dr["UserID"] = uid;
				dr["UserName"] = Auth.LookupUserName(uid);
				dr["Count"] =  (from c in dc.Ratings
								where c.UserID == uid
							select c.Value).Count();

				dt.Rows.Add(dr);
			}
			dt.DefaultView.Sort = "Count DESC";
			LeaderboardList.DataSource = dt.DefaultView;
			LeaderboardList.DataBind();

		}
	}
}