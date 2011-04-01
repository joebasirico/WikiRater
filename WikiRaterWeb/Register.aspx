<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Register.aspx.cs" Inherits="WikiRaterWeb.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:Panel ID="RegisterPanel" runat="server">
		<div class="content">
			<asp:Label ID="Message" runat="server"></asp:Label>
			<table>
				<tr>
					<td>
						<strong>Please Register</strong>
					</td>
					<td align="right">
						<a href="Login.aspx">...or login</a>
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
						email*:
					</td>
					<td>
						<asp:TextBox runat="server" ID="email" />
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
<%--				<tr>
					<td>
						Registration Code:
					</td>
					<td>
						<asp:TextBox runat="server" ID="RegCode" />
					</td>
				</tr>--%>
				<tr>
					<td colspan="2" align="right">
						<asp:Button Text="Register" ID="DoLogin" runat="server" OnClick="DoRegister_Click" />
					</td>
				</tr>
			</table>
			<em>*a little note about e-mail. I will never sell, give away, hoc, or otherwise lose
				your e-mail address or other data on this website. I'll keep it safe, <a href="http://www.youtube.com/watch?v=0o8XMlL8rqY">
					front pocket</a>.</em>
		</div>
	</asp:Panel>
	<asp:Panel ID="RegistrationCompletePanel" runat="server" Visible="false">
		<div class="content">
			<strong>Registration Complete</strong><br />
			Thank you for registering with WikiRater. You have been logged in and now may begin
			rating wikipedia articles.<br />
			<br />
			For easy rating may I suggest you install this bookmarklet
			<asp:HyperLink ID="Bookmarklet" runat="server"></asp:HyperLink>?
			<br />
			Just drag it to the toolbar or bookmarks bar on your browser.
		</div>
	</asp:Panel>
	<asp:Panel ID="AlreadyLoggedIn" runat="server" Visible="false">
		<div class="content">
			<strong>Already Logged In</strong><br />
			It looks like you're already registered and logged in. Grab the bookmarklet (in
			the footer) and start rating articles!<br />
			<br />
			Maybe you want to start with a <a href="http://en.wikipedia.org/wiki/Special:Random">
				Random Wikipedia Article</a>
			<br />
			If you need to <a href="Logout.aspx">logout</a>, you can do that too.
		</div>
	</asp:Panel>
	<asp:Panel ID="ErrorPanel" runat="server" Visible="false">
		<div class="content">
			<strong>Something went wrong</strong><br />
			We're sorry there was an issue logging you in after you signed up. You can try to
			<a href="Login.aspx">login</a>, or <a href="mailto:wikirater@whoisjoe.com">notify me</a>
			and I'll work to fix the error.
		</div>
	</asp:Panel>
</asp:Content>
