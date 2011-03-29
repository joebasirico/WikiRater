<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="WikiRaterWeb.Help.Default" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHelp" runat="server">
	<div class="content">
		<h1>
			Help</h1>
		<div class="paragraph">
			Before we get started, be sure to <a href="../Register.aspx">register</a>. There's
			no use in going through all of this if you haven't registered.
		</div>
		<div class="paragraph">
			The crux of using WikiRater is installing a <a href="http://en.wikipedia.org/wiki/Bookmarklet">
				bookmarklet</a>, which isn't something that everybody's done before. For that
			reason I'd like to walk you through a little bit. A bookmarklet hangs out on yoru
			bookmarks bar and adds some functionality to your browsing experience. For example
			it may allow you to easily use another service like <a href="http://delicious.com">delicious</a>
			or to change the way a webpage looks.
		</div>
		<div class="paragraph">
			The WikiRater bookmarklet simply opens a small window that will let you quickly
			rate an article. You will send the page your currently viewing to WikiRater and
			that is all.
		</div>
		<div class="paragraph">
			<h2>
				Installing</h2>
			On to the fun stuff, getting this sucker installed. Installation is as simple as
			dragging the link to your bookmarks bar.
			<br />
			<em>note: installing on Internet Explorer is a little more tricky, for those instructions
				I've had to create a <a href="InstallOnIE.aspx">new page dedicated to IE</a>.</em>
			<br />
			First make sure your <a href="ShowBookmarksBar.aspx">Bookmarks bar is displayed</a>
			<br />
			Now click on the bookmarklet link and drag it to the bookmarks bar.<br />
			The bookmarklets are all over the place, but there's one right here<br />
			<asp:HyperLink ID="Bookmarklet" runat="server" /><br />
			and one at the bottom of every page. So don't bother going back to the main page
			of the site, the image is there just for demonstration purposes.<br />
			<img src="/images/DragBookmarklet.png" alt="Drag Bookmarklet to the bookmarks bar" />
		</div>
		<div class="paragraph">
			<h2>
				Using</h2>
			Once you've got the bookmarklet on your bookmarks bar using WikiRater's a breeze!
			Just click the bookmarklet when you're on a wikipedia page and another small window
			will popup that will let you vote. Click your vote and close the window, you're
			done!
			<br />
			<strong>Browse to a Wikipedia Article and click the Bookmarklet</strong><br />
			<img src="/images/Rate.PNG" alt="Browse to a Wikipedia Article and click the Bookmarklet" /><br />
			Once you've rated the article you'll see what WikiRater would have rated the article.
			After a while you may find yourself anticipating what WikiRater might rate an article,
			but please resist the urge to match how you think WikiRater will rate the article.
			I want WikiRater to learn from you, not the other way around!
			<br />
			<img src="/images/FinishedRating.PNG" alt="Rating done" />
		</div>
	</div>
</asp:Content>
