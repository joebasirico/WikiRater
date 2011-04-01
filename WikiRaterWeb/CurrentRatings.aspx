<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="CurrentRatings.aspx.cs" Inherits="WikiRaterWeb.CurrentRatings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Article Ratings</h1>
		This page shows all the pages that have been rated by all the users of WikiRater.
		If you're logged in you'll see some articles are highlighted in green and some in
		pink. Green means that you've already rated this article (thanks!) and pink means
		you haven't. A great way to help the program is to rate articles that others have
		already rated. More data points means a better algorithm in the end.
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
				<tr <%# Eval("RatedStyle", "class='{0}'")%>>
					<td>
						<a href='http://en.wikipedia.org/wiki/<%# Eval("Article")%>'>
							<%# Eval("Article")%></a>
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
