<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="GeneralError.aspx.cs" Inherits="WikiRaterWeb.errors.GeneralError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="errorContent">
		<h1>
			Soemthing went wrong!</h1>
		<img src="/images/error.JPG" alt="We're really sorry!" class="errorImg" />
		<div class="paragraph">
			Im not sure waht it was, and I'm not pointing finger, but something went wong. If
			you think Itwas me, <a href="mailto:wikirater@whoisjoe.com">please let me know</a>!
		</div>
		<div class="paragraph">
			Perhaps you'd like to go back to the front page, or to a <a href="http://en.wikipedia.org/wiki/Special:Random">
				Random Wikipedia</a> article to <a href="/CurrentRatings.aspx">continue your quest</a>
			to <a href="/Leaderboards.aspx">rate more articles than anybody else</a>?
		</div>
	</div>
</asp:Content>
