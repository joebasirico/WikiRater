using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WikiRaterWeb
{
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				Guid session = new Guid();

				if (Guid.TryParse(Request.Cookies["session"].Value, out session))
					Auth.destroySession(session);
			}
			catch
			{
			}
			Response.Cookies["session"].Value = "";
		}
	}
}