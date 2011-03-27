<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="CurrentRatings.aspx.cs" Inherits="WikiRaterWeb.CurrentRatings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<asp:ListView ID="RatingsListView" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0" class="leaderboard">
					<tr>
						<td>
							<strong>Wikipedia Article</strong>
						</td>
						<td>
							<strong>Average Human Rating</strong>
						</td>
					</tr>
					<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
				</table>
			</LayoutTemplate>
			<ItemTemplate>
				<tr>
					<td>
						<a href='http://en.wikipedia.org/wiki/<%# Eval("Article")%>'><%# Eval("Article")%></a>
					</td>
					<td>
						<%# Eval("Rating", "{0:0.00}")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
				No reviews yet.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
