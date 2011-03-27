<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="InstallOnIE.aspx.cs" Inherits="WikiRaterWeb.Help.InstallOnIE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Installing WikiRater on IE</h1>
		<div class="paragraph">
			Of course, if you haven't already, go <a href="../Register.aspx">register</a>.
		</div>
		<div class="paragraph">
			So, you're installing WikiRater on IE, huh? I'm going to describe the steps needed
			to get WikiRater running on IE8 (if you're running IE9, pat yourself on the back,
			and follow along). If you're still running IE 7 or earlier I highly suggest you
			<a href="http://windows.microsoft.com/en-US/internet-explorer/downloads/ie">upgrade
				to the latest version</a>, or <a href="http://www.google.com/chrome">get</a>
			<a href="http://www.mozilla.com/firefox">another</a> <a href="http://www.apple.com/safari/">
				browser</a>.
		</div>
		<div class="paragraph">
			OK, so here we go. First right click the link below and click "Add To Favorites"
			(it's the bookmarklet you've been hearing so much about)</br>
			<asp:HyperLink ID="Bookmarklet" runat="server" /><br />
			IE likes you to be safe, so if it says something like what is below. Feel confident
			in your decision and click "Yes"!<br />
			<img src="../images/IESecurityWarning.PNG" /><br />
		</div>
		<div class="paragraph">
			Next you probably want to save the bookmarklet in your bookmarks bar for easy access,
			so select that from the "Create In" dropdown. Then click "Add"
			<br />
			<img src="../images/IEAddAsFavorite.PNG" />
		</div>
		<div class="paragraph">
			We're almost done. One last gnarly bit. IE <em>really</em> doesn't like popups.
			Especially from Bookmarklets that you click! So we'll have to add a popup exception
			so WikiRater can popup a new window when you're on a wikipedia page. To do that
			click Tools (in the upper right hand corner) then click "Internet Options"
		</div>
		<div class="paragraph">
			You may not think it, but popups are part of your privacy. So click the "Privacy"
			tab at the top of the Internet Options window. Now click the "Settings" button in
			the Pop-up Blocker section.<br />
			<img src="../images/InternetOptions.PNG" />
		</div>
		<div class="paragraph">
			Now type "en.wikipedia.org" or "*.wikipedia.org" if you're in another country into
			the text box at the top of the window called "Address of website to allow". Click
			Add and you're all done. Click "Close" to this window then "OK" to the window underneath
			and you can browse to any wikipedia article you'd like and rate to your hearts desire!
			<br />
			<img src="../images/IEAddPopupException.PNG" />
		</div>
	</div>
</asp:Content>
