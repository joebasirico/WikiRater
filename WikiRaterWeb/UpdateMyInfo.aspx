<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="UpdateMyInfo.aspx.cs" Inherits="WikiRaterWeb.UpdateMyInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="subnavbar">
		<a href="User.aspx">About Me</a> | <a href="UpdateMyInfo.aspx">Update My Info</a></div>
	<div class="content">
		<h1>
			Update My Info</h1>
		<asp:Label ID="message" runat="server" Visible="false" CssClass="mainPageMessage" />
		<div class="paragraph">
			Use this page to update information about yourself in WikiRater. Right now the only
			information I keep is your password (so you can log in) and e-mail address (so I
			can help you reset your password if you forget), but check back later. Each new
			bit of information will come with some fun new feature.
		</div>
		<h2>
			Change Password</h2>
		<div class="paragraph">
			<table>
				<tr>
					<td width="200px">
						Current Password:
					</td>
					<td>
						<asp:TextBox ID="currentPassword" runat="server" TextMode="Password"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						New Password:
					</td>
					<td>
						<asp:TextBox ID="Password1" runat="server" TextMode="Password"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						Repeat New Password:
					</td>
					<td>
						<asp:TextBox ID="Password2" runat="server" TextMode="Password"></asp:TextBox>
					</td>
				</tr>
			</table>
		</div>
		<h2>
			Change or Add e-mail Address</h2>
		<div class="paragraph">
			<table>
				<tr>
					<td width="200px">
						e-mail Address:
					</td>
					<td>
						<asp:TextBox ID="email" runat="server" Width="300px"></asp:TextBox>
					</td>
				</tr>
			</table>
		</div>
		<asp:Button ID="submit" runat="server" Text="Update" OnClick="submit_Click" />
	</div>
</asp:Content>
