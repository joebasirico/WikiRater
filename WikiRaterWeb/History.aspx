<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="History.aspx.cs" Inherits="WikiRaterWeb.History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			History</h1>
		<div class="paragraph">
			This section is here as a list features that are currently in WikiRater. I'll only
			keep the newest or coolest features on the homepage, but you can get a list of all
			of them here.
		</div>
		<div class="section">
		<h3>April 1st 2011</h3>
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
