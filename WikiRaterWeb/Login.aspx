<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultNarrow.Master" AutoEventWireup="true"
	CodeBehind="Login.aspx.cs" Inherits="WikiRaterWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:Panel ID="LoginPanel" runat="server">
		<div class="contentNarrow">
		<h1>Login</h1>
			<asp:Label ID="Message" runat="server"></asp:Label>
			<table>
				<tr>
					<td>
						<strong>Please Login</strong>
					</td>
					<td align="right">
						<a href="Register.aspx">...or register</a>
					</td>
				</tr>
				<tr>
					<td>
						Username:
					</td>
					<td>
						<asp:TextBox runat="server" ID="UsernameBox" />
					</td>
				</tr>
				<tr>
					<td>
						Password:
					</td>
					<td>
						<asp:TextBox runat="server" ID="PasswordBox" TextMode="Password" />
					</td>
				</tr>
				<tr>
					<td colspan="2" >
						<asp:CheckBox ID="RememberMe" runat="server" Text="Remember for one month" />
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right">
						<asp:Button Text="Login" ID="DoLogin" runat="server" OnClick="DoLogin_Click" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	<asp:Panel ID="LoginCompletePanel" runat="server" Visible="false">
		<div class="contentNarrow">
			<strong>Login Complete</strong><br />
			You have been logged in and now may begin rating wikipedia articles. Don't forget
			the bookmarlet (in the footer if you need it)!
			<br />
			<br />
			<em>note: if you were redirected to the login page, by attempting to vote you'll need
				to vote again.</em><br />
			--Joe
		</div>
	</asp:Panel>
	<asp:Panel ID="AlreadyLoggedIn" runat="server" Visible="false">
		<div class="contentNarrow">
			<strong>Already Logged In</strong><br />
			It looks like you're already logged in. Grab the bookmarklet (in the footer) and
			start rating articles!<br />
			<br />
			If you need to <a href="Logout.aspx">logout</a>, you can do that too.
		</div>
	</asp:Panel>
</asp:Content>
