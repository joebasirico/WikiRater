<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AllRatings.aspx.cs" Inherits="WikiRaterWeb.AllRatings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">

		<asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
			AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="RatingID" 
			DataSourceID="SqlDataSource1" PageSize="100">
			<Columns>
				<asp:BoundField DataField="RatingID" HeaderText="RatingID" 
					InsertVisible="False" ReadOnly="True" SortExpression="RatingID" />
				<asp:BoundField DataField="UserID" HeaderText="UserID" 
					SortExpression="UserID" />
				<asp:BoundField DataField="Article" HeaderText="Article" 
					SortExpression="Article" />
				<asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:WikiVoterConnectionString %>" 
			SelectCommand="SELECT * FROM [Rating] ORDER BY [Article], [Value] DESC">
		</asp:SqlDataSource>
	</div>
</asp:Content>
