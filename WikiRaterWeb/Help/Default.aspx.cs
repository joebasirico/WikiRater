using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb.Help
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Bookmarklet.Text = Settings.Default.RateOnWikiRaterText;
			Bookmarklet.NavigateUrl = String.Format(Settings.Default.Bookmarklet, Settings.Default.CurrentDomain);
		}
	}
}