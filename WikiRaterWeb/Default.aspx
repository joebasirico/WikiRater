<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="WikiRaterWeb.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Welcome to WikiRater</h1>
		<div class="paragraph">
			Thank you for your interest in WikiRater. Getting setup is pretty easy, just <a href="Register.aspx">
				register</a> or <a href="Login.aspx">login</a> and add the bookmarklet below
			to your browser. When you're on a wikipedia article you'd like to rate just click
			the bookmarklet and a little window will popup that will let you rate that article,
			simple and easy!</div>
		<div class="paragraph">
			Try it out on a <a href="http://en.wikipedia.org/wiki/Special:Random">Random Wikipedia
				Article</a>
		</div>
		<div class="paragraph">
			Or <a href="ImprovementProgram.aspx">Join the Improvement Program</a> to help make
			WikiRater better.
		</div>
		<div class="paragraph">
			<h2>
				Get the Bookmarklet</h2>
			<asp:HyperLink runat="server" ID="RateOnWikiRater" />
		</div>
	</div>
</asp:Content>
