<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="WikiRaterWeb.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>Welcome to WikiRater</h1>
		Thank you for your interest in WikiRater. Getting setup is pretty easy, just <a href="Register.aspx">
			register</a> or <a href="Login.aspx">login</a> and add the bookmarklet below
		to your browser. When you're on a wikipedia article you'd like to rate just click
		the bookmarklet and a little window will popup that will let you rate that article,
		simple and easy!<br />
		Try it out on a <a href="http://en.wikipedia.org/wiki/Special:Random">Random Wikipedia Article</a><br />
		Or go rate what other people have rated and help give me more data! <a href="CurrentRatings.aspx">Current Ratings</a>
		<br />
		<br />
		<h2>Bookmarklet</h2>
		<asp:HyperLink runat="server" ID="RateOnWikiRater" />
		<br />
		<br />
		For more information see the <a href="About.aspx">About</a> &amp; <a href="Help/Default.aspx">Help</a> pages.<br />
		Also check out the <a href="CurrentRatings.aspx">Current Best Ratings</a> and the
		<a href="Leaderboards.aspx">Leaderboards</a>
	</div>
</asp:Content>
