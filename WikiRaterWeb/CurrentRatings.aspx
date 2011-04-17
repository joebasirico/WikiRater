<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="CurrentRatings.aspx.cs" Inherits="WikiRaterWeb.CurrentRatings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="subnavbar">
		<a href="CurrentRatings.aspx">Articles by Rating</a> | <a href="Trending.aspx">Trending
			Articles</a></div>
	<div class="content">
		<h1>
			Article Ratings</h1>
		<div class="section">
			This page shows all the pages that have been rated by all the users of WikiRater.
			If you're logged in you'll see some articles are highlighted in green and some in
			pink. Green means that you've already rated this article (thanks!) and pink means
			you haven't. A great way to help the program is to rate articles that others have
			already rated. More data points means a better algorithm in the end.
		</div>
		<div class="paragraph">
			Filter by range
			<asp:TextBox runat="server" ID="lowerBoundBox">9</asp:TextBox>
			to
			<asp:TextBox runat="server" ID="upperBoundBox">10</asp:TextBox><br />
			<asp:Button ID="filter" Text="Go" runat="server" OnClick="filter_Click" /></div>
		<asp:ListView ID="RatingsListView" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0" class="leaderboard" width="100%">
					<tr>
						<td>
							<strong>Wikipedia Article</strong>
						</td>
						<td class="leaderboardRight">
							<strong>Average Human Rating</strong>
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
						<%# Eval("Rating", "{0:0.00}")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
			<h2>No Articles Found</h2>
				Your filter didn't return any articles. This might mean you're being too restricitive,
				you have an odd overlap (like 7 to 3) or there aren't any articles with an average
				rating in that range. Try playing around with your filter a bit more.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
