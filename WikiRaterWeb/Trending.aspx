<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Trending.aspx.cs" Inherits="WikiRaterWeb.Trending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="subnavbar">
		<a href="CurrentRatings.aspx">Articles by Rating</a> | <a href="Trending.aspx">Trending
			Articles</a></div>
	<div class="content">
		<asp:ListView ID="TrendingListView" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0" class="leaderboard">
					<tr>
						<td>
							<strong>Wikipedia Article</strong>
						</td>
						<td>
							<strong>Trending Value</strong>
						</td>
					</tr>
					<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
				</table>
			</LayoutTemplate>
			<ItemTemplate>
				<tr <%# Eval("RatedStyle", "class='{0}'")%>>
					<td>
						<a href='http://en.wikipedia.org/wiki/<%# Eval("Article")%>'>
							<%# Eval("Article")%></a>
					</td>
					<td>
						<%# Eval("Points", "{0:0.00}")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
				No reviews yet.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
