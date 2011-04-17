﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="WikiRaterWeb.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Welcome to WikiRater</h1>
		<div class="section">
			<h2>
				What is WikiRater?</h2>
			WikiRater is a project to help you learn about new and interesting things by rating
			wikipedia articles. If you're really interested in the backstory of this website
			check out the <a href="About.aspx">about page</a>.
			<br />
			<br />
			The idea is pretty simple:
			<ol>
				<li>People Rate Wikipedia articles by how interesting they find them</li>
				<li>You browse the artcicles and find something that's interesting</li>
				<li>Profit?</li>
			</ol>
		</div>
		<div class="section">
			<h2>
				Get Started</h2>
			You can help the WikiRater Project by <a href="Register.aspx">signing up</a> and
			rating articles.<br />
			<br />
			Or see what other people think are the most interesting articles on wikipedia by
			looking at what other people think:
			<table class="callToActionTable">
				<tr>
					<td>
						<a href="Trending.aspx">Browse Trending Articles</a>
					</td>
					<td>
						<a href="CurrentRatings.aspx">See All Articles</a>
					</td>
					<td>
						<a href="RandomPage.aspx?lowerBound=8">Read a Random, Interesting Article</a>
					</td>
				</tr>
			</table>
		</div>
		<div class="section">
			<h2>
				What's New (April 15th 2011)</h2>
			I've tried to make the site easier and more fun to use this time around.
			<br />
			<br />
			<strong><a href="Trending.aspx">Trending Articles</a></strong> – You can now see
			the articles that are most often rated by other users on the trending page. If you
			are familiar with <a href="http://reddit.com">reddit</a> or <a href="http://news.ycombinator.com">
				hacker news</a> this page works very similarly. As an article is rated more
			often it will organically make its way in the top.
			<br />
			<br />
			<strong><a href="RandomPage.aspx">Random Interesting Article</a></strong> – The
			whole point of this site was for me to be able to set my homepage to the random
			wikipedia article and learn about new and interesting things. If you set your homepage
			to the random page you will get a page that has been randomly selected from all
			the articles that have already been rated. If you'd like you can set a lower or
			upper bound to get more or less interesting articles. Do this by using the lowerBound
			and upperBound parameters: <a href="RandomPage.aspx?lowerBound=4&upperBound=8">RandomPage.aspx?lowerBound=4&upperBound=8</a>
			would give you a random article that has an average rating of something between
			four and eight.<br />
			<br />
			<strong>Bug Fixes and Usability</strong> – There have been lots of little bug fixes
			around, so hopefully you either didn't notice before or you'll notice a little more
			polish to the site. As with any (very) beta site, things are always in flux, but
			hopefully trending to the better.<br />
			<br />
		</div>
	</div>
</asp:Content>
