<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Logout.aspx.cs" Inherits="WikiRaterWeb.Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="contentSmall">
		<strong>Logout successful</strong><br />
		You have been successfully logged out. If you'd like to log back in you can do that
		<a href="Login.aspx">over there...</a>
	</div>
</asp:Content>
