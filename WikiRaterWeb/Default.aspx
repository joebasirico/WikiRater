<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="WikiRaterWeb.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Welcome to WikiRater</h1>
		<div class="paragraph">
			Thank you for your interest in WikiRater. I created WikiRater as a project to help
			me create an algorithm to automatically rate the quality of a Wikipedia article.
			I'd love to be able to request a random, high quality article from Wikipedia, read
			the top 100 best articles or to help the Wikimedia foundation out by highlighting
			the bottom 100 articles that need help. If you register and rate articles you'll
			be helping me do that. Each time you rate an article I'll let you know what WikiRater
			would have rated, so sign up and join the fun!</div>
		<div class="paragraph">
			Getting setup is pretty easy, just <a href="Register.aspx">register</a> or <a href="Login.aspx">
				login</a> and add the bookmarklet below to your browser. When you're on a wikipedia
			article you'd like to rate just click the bookmarklet and a little window will popup
			that will let you rate that article, simple and easy!</div>
		<div class="paragraph">
			Or <a href="ImprovementProgram.aspx">Join the Improvement Program</a> to help make
			WikiRater better.
		</div>
		<div class="paragraph">
			Try it out on a <a href="http://en.wikipedia.org/wiki/Special:Random">Random Wikipedia
				Article</a>
		</div>
		<div class="paragraph">
			<h2>
				Get the Bookmarklet</h2>
			<asp:HyperLink runat="server" ID="RateOnWikiRater" />
		</div>
		<div class="paragraph">
			<h2>
				What's New</h2>
			I've been hard at work! Since the last update I've added quite a few features that
			I think you'll like.<br />
			<br />
			<strong>#1 Achievements!</strong> – Hopefully this will make rating Wikipedia articles
			even more fun. As you rate articles you'll win achievements and badges that will
			show how awesome you are. Things like the number of ratings, or the types of ratings
			will all give you achievements. When you win an Achievement you'll be notified immediately
			on the vote page, but you can also check in on your progress on your own user page,
			just click the <a href="/User.aspx">About Me</a> link in the navigation bar<br />
			<br />
			<strong>#2 Points</strong> – Instead of ranking people by the number of articles
			they've rated I've decided to implement a point system. Points are simply Total
			number of articles rated + total value of your achievements, but this should get
			you excited about competing with your friends!<br />
			<br />
			<strong>#3 User pages</strong> – Each user now has a <a href="/User.aspx">User page</a>
			where you can find out their total point score, how many articles they've rated
			and the Achievements they've accomplished. You can see your accomplishments on your
			user page by clicking the About Me link above, or anybody else's by going to the
			<a href="/CurrentRatings.aspx">Current Ratings</a> page and clicking on a user you're
			interested in.<br />
			<br />
			<strong>#4 Faster</strong> – If you rate an article that WikiRater has already seen
			it'll look up its last rating instead of trying to rate it every time. This should
			make you power users much happier<br />
			<br />
			<strong>#5 Much better design</strong> – I heard from a few people they didn't like
			my light grey on dark grey color scheme, so I've bumped up the contrast a bit. Hopefully
			this will make you happier :-)
		</div>
	</div>
</asp:Content>
