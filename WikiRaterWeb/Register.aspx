<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Register.aspx.cs" Inherits="WikiRaterWeb.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:Panel ID="RegisterPanel" runat="server">
		<div class="contentSmall">
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
						Password:
					</td>
					<td>
						<asp:TextBox runat="server" ID="PasswordBox" TextMode="Password" />
					</td>
				</tr>
				<tr>
					<td>
						Registration Code:
					</td>
					<td>
						<asp:TextBox runat="server" ID="RegCode" />
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right">
						<asp:Button Text="Register" ID="DoLogin" runat="server" OnClick="DoRegister_Click" />
					</td>
				</tr>
			</table>
		</div>
	</asp:Panel>
	<asp:Panel ID="RegistrationCompletePanel" runat="server" Visible="false">
		<div class="contentSmall">
			<strong>Registration Complete</strong><br />
			Thank you for registering with WikiRater. You have been logged in and now may begin
			rating wikipedia articles.<br />
			<br />
			For easy rating may I suggest you install this bookmarklet: <a href="javascript:(function(){f='http://wikirater.whoisjoe.com/Vote.aspx?url='+encodeURIComponent(window.location.href);a=function(){if(!window.open(f+'noui=1&jump=doclose','wikivoter','location=yes,links=no,scrollbars=no,toolbar=no,width=550,height=370'))location.href=f+'jump=yes'};if(/Firefox/.test(navigator.userAgent)){setTimeout(a,0)}else{a()}})()">
				Rate on WikiRater</a>?
			<br />
			Just drag it to the toolbar or bookmarks bar on your browser.
		</div>
	</asp:Panel>
</asp:Content>
