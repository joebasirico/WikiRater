<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Vote.aspx.cs" Inherits="WikiRaterWeb.Vote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:Panel ID="VotePanel" runat="server">
		<div class="contentSmall">
			Voting on: <strong>
				<asp:Label ID="url" runat="server"></asp:Label></strong>
			<br />
			Select your interest in this article.
			<br />
			(1 is low, 10 is perfect)
			<div id="buttonContainer">
				<asp:LinkButton ID="vote1" runat="server" CssClass="voteButton" OnClick="vote1_Click">1</asp:LinkButton>
				<asp:LinkButton ID="vote2" runat="server" CssClass="voteButton" OnClick="vote2_Click">2</asp:LinkButton>
				<asp:LinkButton ID="vote3" runat="server" CssClass="voteButton" OnClick="vote3_Click">3</asp:LinkButton>
				<asp:LinkButton ID="vote4" runat="server" CssClass="voteButton" OnClick="vote4_Click">4</asp:LinkButton>
				<asp:LinkButton ID="vote5" runat="server" CssClass="voteButton" OnClick="vote5_Click">5</asp:LinkButton>
				<asp:LinkButton ID="vote6" runat="server" CssClass="voteButton" OnClick="vote6_Click">6</asp:LinkButton>
				<asp:LinkButton ID="vote7" runat="server" CssClass="voteButton" OnClick="vote7_Click">7</asp:LinkButton>
				<asp:LinkButton ID="vote8" runat="server" CssClass="voteButton" OnClick="vote8_Click">8</asp:LinkButton>
				<asp:LinkButton ID="vote9" runat="server" CssClass="voteButton" OnClick="vote9_Click">9</asp:LinkButton>
				<asp:LinkButton ID="vote10" runat="server" CssClass="voteButton" OnClick="vote10_Click">10</asp:LinkButton>
			</div>
		</div>
	</asp:Panel>
	<asp:Panel runat="server" ID="VoteCompletedPanel" Visible="false">
		<div class="contentSmall">
			<strong>Thank you for voting, Your vote has been recorded!</strong><br />
			<asp:Label runat="server" ID="WikiRaterRating" />
			(You rated it a: <asp:Label runat="server" ID="UserRating" />)
		</div>
	</asp:Panel>
	<asp:Panel runat="server" ID="InvalidPage" Visible="false">
		<div class="contentSmall">
			<strong>Couldn't find a wiki article</strong><br />
			Sorry, it doesn't look like you're on a wikipedia article. If you think you've arrived on this page
			in error <a href="mailto:wikirater@whoisjoe.com">please let me know</a>.
		</div>
	</asp:Panel>
	
</asp:Content>
