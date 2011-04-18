<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Admin.aspx.cs" Inherits="WikiRaterWeb.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="subnavbar">
<a href="EventLog.aspx">Event Log</a> | <a href="AllRatings.aspx">All Ratings</a>
</div>
	<div class="content">
		<h1>
			Admin Tools</h1>
		<asp:Label ID="Message" runat="server"></asp:Label>
		<div class="section">
			<h2>
				Add Article to Improvement Program</h2>
			Article Title:
			<asp:TextBox ID="ArticleTitle" runat="server"></asp:TextBox><br />
			<asp:Button ID="Submit" runat="server" Text="Add Article" OnClick="Submit_Click" />
		</div>
		<div class="section">
			<h2>
				Add Achievement Text</h2>
			Achievement ShortName:
			<asp:TextBox ID="AchShortName" runat="server"></asp:TextBox><br />
			Achievement Title:
			<asp:TextBox ID="AchTitle" runat="server"></asp:TextBox><br />
			Achievement Value:
			<asp:TextBox ID="AchValue" runat="server"></asp:TextBox><br />
			Achievement Icon:
			<asp:TextBox ID="AchIcon" runat="server"></asp:TextBox><br />
			Achievement Description:
			<asp:TextBox ID="AchDescription" runat="server" TextMode="MultiLine" Width="90%"
				Height="150px"></asp:TextBox><br />
			<asp:Button ID="AddAchievement" runat="server" Text="Add Achievement" OnClick="AddAchievement_Click" />
		</div>
		<div class="section">
		<h2>Log everybody out!</h2>
			<asp:Button Text="Log Everybody Out!" ID="logEverybodyOut" runat="server" 
				onclick="logEverybodyOut_Click" />
		</div>
	</div>
</asp:Content>
