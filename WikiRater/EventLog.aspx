<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="EventLog.aspx.cs" Inherits="EventLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<div class="eventsTable">
		<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
			AutoGenerateColumns="False" DataKeyNames="LogID" DataSourceID="SqlDataSource1">
			<Columns>
				<asp:BoundField DataField="LogID" HeaderText="LogID" InsertVisible="False" ReadOnly="True"
					SortExpression="LogID" htmlencode="true"  />
				<asp:BoundField DataField="LogTitle" HeaderText="LogTitle" SortExpression="LogTitle" htmlencode="true" />
				<asp:BoundField DataField="EventTime" HeaderText="EventTime" SortExpression="EventTime" htmlencode="true"  />
				<asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" htmlencode="true"  />
				<asp:BoundField DataField="LogBody" HeaderText="LogBody" SortExpression="LogBody" htmlencode="true"  />
			</Columns>
		</asp:GridView>
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WikiVoterConnectionString %>"
			SelectCommand="SELECT * FROM [EventLog] ORDER BY [EventTime] DESC"></asp:SqlDataSource>
	</div>
</asp:Content>
