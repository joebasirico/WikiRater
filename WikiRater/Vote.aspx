<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
	CodeFile="Vote.aspx.cs" Inherits="Vote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<asp:Panel ID="VotePanel" runat="server">
		<div class="content">
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
		<div class="content">
			<strong>Thank you for voting!</strong><br />
			Your vote has been recorded!
		</div>
	</asp:Panel>
</asp:Content>
