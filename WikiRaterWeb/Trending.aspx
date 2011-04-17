<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Trending.aspx.cs" Inherits="WikiRaterWeb.Trending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="subnavbar">
		<a href="CurrentRatings.aspx">Articles by Rating</a> | <a href="Trending.aspx">Trending
			Articles</a></div>
	<div class="content">
		<h1>
			Trending</h1>
		<div class="section">
			This page shows which articles are being rated most often in the WikiRater system.
			As more people rate an article it will naturally rise to the top of the list, it
			will naturally fall over time.
		</div>
		<asp:ListView ID="TrendingListView" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0" class="leaderboard">
					<tr>
						<td>
							<strong>Wikipedia Article</strong>
						</td>
						<td class="leaderboardRight">
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
					<td class="leaderboardRight">
						<%# Eval("Points")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
				<h2>No Trending Articles</h2>
				It looks like it's been a while since people have rated any articles. 
				You can get your favorite article on this list by rating one! Why don't you try to let me 
				find something you'll be interested it? <br /> <br />
				<a href="RandomPage.aspx?lowerBound=8">Check out a random interesting article</a>
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
