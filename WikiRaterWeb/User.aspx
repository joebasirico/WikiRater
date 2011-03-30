<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="User.aspx.cs" Inherits="WikiRaterWeb.User1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h1>
		<asp:Label ID="UserName" runat="server"></asp:Label></h1>
	<div class="Points">
		<asp:Label ID="Points" runat="server"></asp:Label>
	</div>
	Member Since:
	<asp:Label ID="MemberSince" runat="server"></asp:Label>
	<asp:ListView ID="AchievementsList" runat="server">
		<LayoutTemplate>
			<div class="Achievements">
				<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
		</LayoutTemplate>
		<ItemTemplate>
			<div class='<%# Eval("Achieved")%>'>
			<asp:Image ID="AchievementImage" runat="server" Visible="false" />
				<strong>
					<%# Eval("Title")%></strong> -
				<%# Eval("Description")%></div>
		</ItemTemplate>
		<EmptyDataTemplate>
			Nothing to review yet, please check back shortly. I'm picking each article out by
			hand.
		</EmptyDataTemplate>
	</asp:ListView>
</asp:Content>
