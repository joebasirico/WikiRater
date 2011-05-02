using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb.Help
{
	public partial class Mobile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			bookmarkletBox.Text = String.Format(Settings.Default.Bookmarklet, Settings.Default.CurrentDomain);
		}
	}
}