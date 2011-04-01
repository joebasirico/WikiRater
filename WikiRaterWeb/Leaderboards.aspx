<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Leaderboards.aspx.cs" Inherits="WikiRaterWeb.Leaderboards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Leaderboards</h1>
		The Leaderboards show the users on the system with the most points. Points are awarded
		by rating more articles and winning achievements. To find out more about a user
		check out their user page by clicking their name.
		<asp:ListView ID="LeaderboardList" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0" class="leaderboard">
					<tr>
						<td>
							<strong>Username</strong>
						</td>
						<td>
							<strong>Number of articles rated</strong>
						</td>
					</tr>
					<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
				</table>
			</LayoutTemplate>
			<ItemTemplate>
				<tr>
					<td>
						<a href='User.aspx?Username=<%# Eval("Username")%>'>
							<%# Eval("Username")%></a>
					</td>
					<td>
						<%# Eval("Count")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
				No reviews yet.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
