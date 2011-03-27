<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="404.aspx.cs" Inherits="WikiRaterWeb._404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="errorContent">
		<h1>
			404 - Page not found</h1>
		<img src="/images/sadguy.JPG" alt="We're really sorry!" class="errorImg" />
		<div class="paragraph">
			I'm <em><strong>really, really</strong></em> sorry, but I just could not find the
			page you were looking for. I looked all over, but I'm sad to say I've failed.
		</div>
		<div class="paragraph">
			Perhaps you'd like to go back to the front page, or to a <a href="http://en.wikipedia.org/wiki/Special:Random">
				Random Wikipedia</a> article to <a href="/CurrentRatings.aspx">continue your quest</a>
			to <a href="/Leaderboards.aspx">rate more articles than anybody else</a>?
		</div>
	</div>
</asp:Content>
