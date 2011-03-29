<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="Admin.aspx.cs" Inherits="WikiRaterWeb.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<h1>
			Admin Tools</h1>
		<div class="section">
			<h2>Add Article to Improvement Program</h2>
			Article Title: <asp:TextBox ID="ArticleTitle" runat="server"></asp:TextBox><br />
			<asp:Button ID="Submit" runat="server" Text="Submit" onclick="Submit_Click" />
		</div>
	</div>
</asp:Content>
