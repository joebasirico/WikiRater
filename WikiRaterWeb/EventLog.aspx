<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="EventLog.aspx.cs" Inherits="WikiRaterWeb.EventLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
			AutoGenerateColumns="False" DataKeyNames="LogID" DataSourceID="SqlDataSource1">
			<Columns>
				<asp:BoundField DataField="LogID" HeaderText="LogID" InsertVisible="False" ReadOnly="True"
					SortExpression="LogID" HtmlEncode="true" />
				<asp:BoundField DataField="LogTitle" HeaderText="LogTitle" SortExpression="LogTitle"
					HtmlEncode="true" />
				<asp:BoundField DataField="EventTime" HeaderText="EventTime" SortExpression="EventTime"
					HtmlEncode="true" />
				<asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" HtmlEncode="true" />
				<asp:BoundField DataField="LogBody" HeaderText="LogBody" SortExpression="LogBody"
					HtmlEncode="true" />
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WikiVoterConnectionString %>"
			SelectCommand="SELECT * FROM [EventLog] ORDER BY [EventTime] DESC"></asp:SqlDataSource>
	</div>
</asp:Content>
