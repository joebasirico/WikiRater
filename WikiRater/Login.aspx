<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<asp:Panel ID="LoginPanel" runat="server">
		<div class="content">
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
					<td colspan="2" align="right">
						<asp:Button Text="Login" ID="DoLogin" runat="server" OnClick="DoLogin_Click" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	<asp:Panel ID="LoginCompletePanel" runat="server" Visible="false">
		<div class="content">
			<strong>Login Complete</strong><br />
			You have been logged in and now may begin rating wikipedia articles. <br /><br />
			<em>note: if you were redirected to the login page, by attempting to vote you'll need to vote again.</em><br />
			--Joe
		</div>
	</asp:Panel>
</asp:Content>
