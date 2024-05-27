<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketStatus.aspx.cs" Inherits="SuccessPage" %>

<!DOCTYPE html>
<% 
    Response.Buffer = false;
    Response.CacheControl = "private";
%> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Ticket Status</title>
</head>
<body>
	<form id="form1" runat="server">
		<div id="container">
			<div class="row">
				
				<asp:Button ID="ticketClosBtn" runat="server" Text="Ticket Close" OnClick="ticketClosBtn_click"/>
				<asp:Label ID="succesMsg" runat="server" Text=""></asp:Label>
			</div>
		</div>
	</form>
</body>
</html>
