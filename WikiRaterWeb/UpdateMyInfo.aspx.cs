using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WikiRaterWeb
{
	public partial class UpdateMyInfo : System.Web.UI.Page
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		User currentUser = new User();

		protected void Page_Load(object sender, EventArgs e)
		{
			string username = GetCurrentUser();
			currentUser = dc.Users.First(u => u.UserName == username);

			if (!Page.IsPostBack)
			{
				email.Text = currentUser.email;
			}
		}

		private string GetCurrentUser()
		{
			Guid session = new Guid();
			int userID = 0;

			try
			{
				if (Request.Cookies["session"] != null && Guid.TryParse(Request.Cookies["session"].Value, out session))
				{
					userID = Auth.checkSession(session);
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

		protected void submit_Click(object sender, EventArgs e)
		{
			if (currentUser.email != email.Text)
			{
				User checkUser = dc.Users.FirstOrDefault(u => u.email == email.Text);
				if (null == checkUser || currentUser.UserID == checkUser.UserID)
				{
					Auth.UpdateEmail(currentUser.UserID, email.Text);

					message.Visible = true;
					message.Text = "Your information has been successfully updated!";
					message.CssClass = "messageBoxGood";
				}
				else
				{
					message.Visible = true;
					message.Text = @"It looks like somebody has already registered that e-mail address 
(it was probably you ;-) ). If you think you might have two users on the system use the forgot password feature to reset your password.";
					message.CssClass = "messageBoxError";
				}
			}
			if (!string.IsNullOrEmpty(currentPassword.Text))
			{
				if (Password1.Text == Password2.Text)
				{
					if (currentUser.UserID == Auth.checkCredentials(currentUser.UserName, Auth.ByteToHex(
										SHA512Managed.Create().ComputeHash(
											Encoding.ASCII.GetBytes(currentUser.UserName +
											Auth.getSaltyGoo() + currentPassword.Text)))))
					{
						Auth.UpdatePassword(currentUser.UserID, Auth.ByteToHex(
										SHA512Managed.Create().ComputeHash(
											Encoding.ASCII.GetBytes(currentUser.UserName +
											Auth.getSaltyGoo() + Password1.Text))));
					}
					message.Visible = true;
					message.Text = "Your information has been successfully updated!";
					message.CssClass = "messageBoxGood";
				}
				else
				{
					message.Visible = true;
					message.Text = "Both of the passwords must match.";
					message.CssClass = "messageBoxError";
				}
			}
		}
	}
}