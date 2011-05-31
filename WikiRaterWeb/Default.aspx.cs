using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WikiRaterWeb.Properties;
using System.Threading;

namespace WikiRaterWeb
{
	public partial class Default1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataClassesDataContext dc = new DataClassesDataContext();
			RateOnWikiRater.Text = Settings.Default.RateOnWikiRaterText;
			RateOnWikiRater.NavigateUrl = String.Format(Settings.Default.Bookmarklet, Settings.Default.CurrentDomain);

			User currentUser = GetCurrentUser();

			if (null != currentUser && string.IsNullOrWhiteSpace(currentUser.email))
			{
				MessagePanel.Visible = true;
				Message.Text = Settings.Default.NoEmailAddressMessage;
			}
		}

		public User GetCurrentUser()
		{
			DataClassesDataContext dc = new DataClassesDataContext();
			Guid session = new Guid();
			int userID = 0;

			try
			{
				if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				{
					userID = Auth.checkSession(session);
					if (userID != 0)
						return dc.Users.First(u => u.UserName == Auth.LookupUserName(userID));
				}
				return null;
			}
			catch (Exception)
			{
			}
			return null;
		}
	}
}