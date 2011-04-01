<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="User.aspx.cs" Inherits="WikiRaterWeb.User1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<div class="Points">
			<div class="PointValue">
				<asp:Label ID="Points" runat="server"></asp:Label></div>
			Points
		</div>
		<h1>
			<asp:Label ID="UserName" runat="server"></asp:Label></h1>
		Has been a member since
		<asp:Label ID="MemberSince" runat="server"></asp:Label>. During which time he or she
		has rated <strong>
			<asp:Label ID="UniqueRatings" runat="server"></asp:Label></strong> unique articles
		articles.
		<h2>
			Achievements</h2>
		<asp:ListView ID="AchievementsList" runat="server">
			<LayoutTemplate>
				<div class="Achievements">
					<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
				</div>
			</LayoutTemplate>
			<ItemTemplate>
				<div class='<%# Eval("Achieved")%>'>
					<img src='<%# Eval("Icon")%>' style='display:<%# Eval("HasIcon")%>' />
					<strong>
						<%# Eval("Title")%></strong> (worth <%# Eval("Value")%> points) - 
					<%# Eval("Description")%></div>
			</ItemTemplate>
			<EmptyDataTemplate>
				No Achievements yet.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
