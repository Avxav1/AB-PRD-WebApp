<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuccessPage.aspx.cs" Inherits="SuccessPage" %>

<!DOCTYPE html>
<% 
    Response.Buffer = false;
    Response.CacheControl = "private";
%> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Feedback Form</title>

	<link rel="stylesheet" href="lib/css/modalpopup.css" />
	<link rel="stylesheet" href="lib/css/bootstrap.min.css" />
	<link rel="stylesheet" href="lib/css/jqbtk.css" />


	<%--<link rel="stylesheet" type="text/css" href="http://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" />
  <link rel="stylesheet" type="text/css" href="lib/css/jquery.ml-keyboard.css" />
  <link rel="stylesheet" type="text/css" href="lib/css/demo.css" />--%>


	<link href="Content/ABDS/successPage.css" rel="stylesheet" />

</head>
<body>
	<form id="form1" runat="server">
		<div id="container">
			<div class="row">
				<div class="column right">
					<div class="productlistright" style="    margin-left: 3%;width: 1179px;    margin-top: 2px;">
									<asp:Label ID="lblAlertThankYou" runat="server" CssClass="labelclose" Text="<%$ Resources:Resource, lblAlertThankYou%>" Style="text-align: center; margin-top: 200px; width: 100%;" meta:resourcekey="lblAlertThankYou"></asp:Label>

						<%--<div class="column">
							<div style="color: white; font-size: 17pt; font-family: 'Myriad Pro'; padding: 20px;">
								<div style="border: none; overflow-x: hidden; width: 654px; text-align: left; padding-left: 200px; padding-top: 150px;">
								</div>
							</div>
						</div>--%>
					</div>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
