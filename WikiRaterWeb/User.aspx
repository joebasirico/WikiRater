<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="User.aspx.cs" Inherits="WikiRaterWeb.User1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="subnavbar">
		<a href="User.aspx">About Me</a> | <a href="UpdateMyInfo.aspx">Update My Info</a></div>
	<asp:Panel ID="Default" runat="server">
		<div class="content">
			<div class="Points">
				<div class="PointValue">
					<asp:Label ID="Points" runat="server"></asp:Label></div>
				<asp:Label ID="PointOrPoints" runat="server"></asp:Label>
			</div>
			<h1>
				<asp:Label ID="UserName" runat="server"></asp:Label></h1>
			<asp:Label ID="IntroText" runat="server"></asp:Label>
			<h2>
				Achievements</h2>
			<asp:ListView ID="AchievementsList" runat="server">
				<LayoutTemplate>
					<div class="Achievements">
						<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
					</div>
				</LayoutTemplate>
				<ItemTemplate>
					<div class='<%# Eval("Achieved")%>'>
						<img src='<%# Eval("Icon")%>' style='display: <%# Eval("HasIcon")%>' alt='<%# Eval("Title")%>'
							class="achievementIcon" />
						<strong>
							<%# Eval("Title")%></strong> (worth
						<%# Eval("Value")%>
						points) -
						<%# Eval("Description")%></div>
				</ItemTemplate>
				<EmptyDataTemplate>
					No Achievements yet.
				</EmptyDataTemplate>
			</asp:ListView>
			<asp:Panel runat="server" ID="RatedArticlePanel" Visible="false">
				<br />
				<br />
				<h2>
					The Articles You've Rated</h2>
				<asp:ListView ID="RatingsListView" runat="server">
					<LayoutTemplate>
						<table border="0" cellpadding="0" cellspacing="0" class="leaderboard" width="100%">
							<tr>
								<td>
									<strong>
										<asp:LinkButton Text="Wikipedia Article" runat="server" ID="ArticleSort" OnClick="ArticleSort_Click" /></strong>
								</td>
								<td class="leaderboardRight">
									<strong>
										<asp:LinkButton Text="Your Rating" runat="server" ID="RatingSort" OnClick="RatingSort_Click" /></strong>
								</td>
							</tr>
							<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
						</table>
					</LayoutTemplate>
					<ItemTemplate>
						<tr>
							<td>
								<a href='http://en.wikipedia.org/wiki/<%# Eval("Article")%>'>
									<%# Eval("Article")%></a>
							</td>
							<td class="leaderboardRight">
								<%# Eval("Rating")%>
							</td>
						</tr>
					</ItemTemplate>
					<EmptyDataTemplate>
						<h2>
							No Articles Found</h2>
						It looks like you haven't rated any articles. Make sure you have the bookmarklet
						installed and go on a rating spree!
						<br />
						<br />
						My I suggest a <a href="RandomPage.aspx">random article you might be interested in</a>?
					</EmptyDataTemplate>
				</asp:ListView>
			</asp:Panel>
		</div>
	</asp:Panel>
	<asp:Panel ID="NoUser" runat="server" Visible="false">
		<div class="content">
			<h1>
				No User by that name</h1>
			<div class="paragraph">
				A rose may smell as sweet by any other name, but names (and usernames) are important
				around here. I couldn't find a user by that name. I suggest you go back to the <a
					href="Leaderboards.aspx">Leaderboards page</a> and select one of those users.
			</div>
		</div>
	</asp:Panel>
</asp:Content>
