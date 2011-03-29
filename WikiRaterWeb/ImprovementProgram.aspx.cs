using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;

namespace WikiRaterWeb
{
	public partial class ImprovementProgram : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				Guid session = new Guid();
				//we've never seen this user before or they've cleared their cookies
				if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				{
					int userID = Auth.checkSession(session);
					if (userID != 0)
					{
						//display list of articles
						DisplayArticles();
						//highlight articles the user has or has not rated yet.
					}
					else
					{
						Response.Redirect("Login.aspx?page=ImprovementProgram.aspx");
					}
				}
				else
				{
					Response.Redirect("Login.aspx?page=ImprovementProgram.aspx");
				}
			}
			catch (ThreadAbortException)
			{

			}
			catch (Exception ex)
			{
				Auth.CreateEvent("Exception Loading Improvement Program Page:" + ex.Message, ex.ToString(), Request.UserHostAddress);
			}

		}

		private void DisplayArticles()
		{
			Guid session = new Guid();
			//we've never seen this user before or they've cleared their cookies
			if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
			{
				int userID = Auth.checkSession(session);
				if (userID != 0)
				{
					DataClassesDataContext dc = new DataClassesDataContext();
					var IPList = from art in dc.ImprovementProgramLists
								 select art.Title;

					var RatedArticles = from rArt in dc.Ratings
										where rArt.UserID == userID
											&& rArt.IsLatest == true
										select rArt.Article;


					DataTable dt = new DataTable();
					dt.Columns.Add("Article");
					dt.Columns.Add("Rated", System.Type.GetType("System.String"));
					dt.Columns.Add("RatedStyle", System.Type.GetType("System.String"));

					foreach (string article in IPList)
					{
						bool found = false;
						foreach (string ratedArt in RatedArticles)
						{
							if (article == ratedArt)
							{
								DataRow dr = dt.NewRow();
								//encode
								dr["Article"] = Server.HtmlEncode(article);
								dr["Rated"] = "Rated";
								dr["RatedStyle"] = "Rated";

								dt.Rows.Add(dr);
								found = true;
								break;
							}
						}
						if (!found)
						{
							DataRow dr = dt.NewRow();
							//encode
							dr["Article"] = Server.HtmlEncode(article);
							dr["Rated"] = "Not Yet";
							dr["RatedStyle"] = "NotRated";

							dt.Rows.Add(dr);
						}
					}
					dt.DefaultView.Sort = "Article";
					IPListView.DataSource = dt.DefaultView;
					IPListView.DataBind();
				}
			}
		}
	}
}