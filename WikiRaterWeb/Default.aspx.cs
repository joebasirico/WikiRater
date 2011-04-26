using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiRaterWeb.Properties;

namespace WikiRaterWeb
{
	public partial class Default1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataClassesDataContext dc = new DataClassesDataContext();
			RateOnWikiRater.Text = Settings.Default.RateOnWikiRaterText;
			RateOnWikiRater.NavigateUrl = String.Format(Settings.Default.Bookmarklet, Settings.Default.CurrentDomain);
		}
	}
}