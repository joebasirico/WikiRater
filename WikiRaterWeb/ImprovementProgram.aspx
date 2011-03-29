<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="ImprovementProgram.aspx.cs" Inherits="WikiRaterWeb.ImprovementProgram" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Improvement Program Page</h1>
		<div class="paragraph">
			Thanks for participating in the Improvement Program! If I have ratings from a bunch
			of people for the same articles I better see how people rate a specific article.
			If you rate all the articles on this list I'll be that much closer to the master
			algorithm.
		</div>
		<asp:ListView ID="IPListView" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0" class="leaderboard">
					<tr>
						<td>
							<strong>Wikipedia Article</strong>
						</td>
						<td>
							<strong>Rated?</strong>
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
						<%# Eval("Rated")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
				Nothing to review yet, please check back shortly. I'm picking each article out by hand.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
