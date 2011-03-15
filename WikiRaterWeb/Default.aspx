<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="WikiRaterWeb.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h3>Welcome to WikiRater</h3>
		Thank you for your interest in WikiRater. Getting setup is pretty easy, just <a href="Register.aspx">
			register</a> or <a href="Login.aspx">login</a> and add the bookmarklet below
		to your browser. When you're on a wikipedia article you'd like to rate just click
		the bookmarklet and a little window will popup that will let you rate that article,
		simple and easy!<br />
		<br />
		<strong>Bookmarklet</strong><br />
		<asp:HyperLink runat="server" ID="RateOnWikiRater" />
		<br />
		<br />
		For more information see the <a href="About.aspx">about page</a>.<br />
		Also check out the <a href="CurrentRatings.aspx">Current Best Ratings</a> and the
		<a href="Leaderboards.aspx">Leaderboards</a>
	</div>
</asp:Content>
