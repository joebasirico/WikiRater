<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="content">
		<strong>Welcome to WikiRater</strong><br />
		Thank you for your interest in WikiRater. Getting setup is pretty easy, just <a href="Register.aspx">
			register</a> and add the bookmarklet below to your browser. When you're on a
		wikipedia article you'd like to rate just click the bookmarklet and a little window
		will popup that will let you rate that article, simple and easy!
		<br />
		<br />
		<strong>Bookmarklet</strong><br />
		<a href="javascript:(function(){f='http://wikirater.whoisjoe.com/Vote.aspx?url='+encodeURIComponent(window.location.href);a=function(){if(!window.open(f+'noui=1&jump=doclose','wikivoter','location=yes,links=no,scrollbars=no,toolbar=no,width=550,height=370'))location.href=f+'jump=yes'};if(/Firefox/.test(navigator.userAgent)){setTimeout(a,0)}else{a()}})()">
			Rate on WikiRater</a>
	</div>
</asp:Content>
