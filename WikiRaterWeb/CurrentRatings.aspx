<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
	CodeBehind="CurrentRatings.aspx.cs" Inherits="WikiRaterWeb.CurrentRatings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="content">
		<asp:ListView ID="RatingsListView" runat="server">
			<LayoutTemplate>
				<table border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							Wikipedia Article
						</td>
						<td>
							Average Human Rating
						</td>
						<td>
							WikiRater Rating
						</td>
					</tr>
					<asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
				</table>
			</LayoutTemplate>
			<ItemTemplate>
				<tr>
					<td>
						<%# Eval("Article")%>
					</td>
					<td>
						<%# Eval("Human")%>
					</td>
					<td>
						<%# Eval("Robot")%>
					</td>
				</tr>
			</ItemTemplate>
			<EmptyDataTemplate>
				No reviews yet.
			</EmptyDataTemplate>
		</asp:ListView>
	</div>
</asp:Content>
