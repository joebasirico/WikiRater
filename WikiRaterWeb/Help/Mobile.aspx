<%@ Page Title="" Language="C#" MasterPageFile="~/Help/Help.Master" AutoEventWireup="true"
	CodeBehind="Mobile.aspx.cs" Inherits="WikiRaterWeb.Help.Mobile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHelp" runat="server">
	<div class="content">
		<h1>
			Mobile</h1>
		<div class="paragraph">
			Whoa, you're totally awesome for wanting to use WikiRater on your mobile device.
			I'll be writing up some awesome instructions on how to set this up on your iPhone
			or iPad, but in the mean time you may just want to use it on your main computer.
		</div>
		<div class="paragraph">
			Still here huh? Well all you really have to do is to create a bookmark (any will
			do, so go ahead an bookmark this page). Then change the title of the bookmark to
			something normal, like Rate on WikiRater, or RoW if you like to be concise. Then
			copy the text in the textbox below, and change the address of the bookmark to that
			text. When you're on a Wikipedia article you can just click that bookmark and a
			new page will open up on your phone that will let you rate the article. You can
			close that page and return to your article. Thanks for being an early adopter, I
			appreciate it.
		</div>
		<div class="paragraph">
			If you want to use this on your iPhone, but didn't quite follow what I said above,
			don't worry. I'll post something better soon.
		</div>
		<asp:TextBox runat="server" ID="bookmarkletBox" TextMode="MultiLine" Width="250px"
			Height="150px"></asp:TextBox>
	</div>
</asp:Content>
