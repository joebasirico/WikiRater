<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.Master" AutoEventWireup="true"
	CodeBehind="ShowBookmarksBar.aspx.cs" Inherits="WikiRaterWeb.Help.ShowBookmarksBar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHelp" runat="server">
	<div class="content">
		<h1>
			Show your bookmarks bar</h1>
		<div class="paragraph">
			Although we wish they'd be the same, every browser is unique. I'll walk you through
			what it takes to display the bookmarks bar (chrome), bookmarks toolbar (firefox),
			or favorites bar (IE).
		</div>
		<h2>
			Chrome</h2>
		<div class="paragraph">
			Open a new tab, so it shows the most visited links, or apps or whatever it is chrome
			is showing on that page right now. Then right click the bookmarks bar and select
			always show bookmarks bar. A cute little animation will show it merging into your
			window and it'll stick there. That's it for chrome.
		</div>
		<h2>
			Firefox</h2>
		<div class="paragraph">
			Just click View, then hover over Toolbars and select "Bookmarks Toolbar" (so that it has a checkbox next to it).
		</div>
		<h2>Internet Explorer
		</h2>
		<div class="paragraph">
			Almost the same as Firefox. Click Tools, Toolbars then select Favorites Bar (so it has a checkbox next to it).
		</div>
	</div>
</asp:Content>
