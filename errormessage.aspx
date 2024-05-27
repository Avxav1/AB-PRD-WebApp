<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errormessage.aspx.cs" Inherits="errormessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Security-Policy" content="Content-Security-Policy: default-src https: 'self'; script-src https: 'unsafe-inline'; style-src https: 'unsafe-inline'" />
	<title>Error Message</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge;chrome=1" />
	<link href="Content/ABDS/feedBackFormBootstrap.css" rel="stylesheet" />
	<link href="Content/ABDS/feedBackForm.css" rel="stylesheet" />
</head>
<body class="disable-selection">

	<form id="form1" runat="server">
		<div id="container">

			<div class="row">
			
				<div runat="server" dir='<%$ Resources: Resource,TextDirection%>' style="width: 92%">
					<div class="productlistright" style="margin-left: 3%; margin-top: 1%;">

						<div class="column" style="padding: 0; width: 100%;">
							<div class="alltext">
							</div>

							<div style="color: white; font-size: 15pt; font-family: 'Myriad Pro'; padding: 20px;">

								<div style="border: none; width: 826px;">

									<br /><br /><br /><br /><br /><br /><br /><br />

									<div style="padding-left:150px"><asp:Label  ID="lblErrorMessage" runat="server"
                    Text=""></asp:Label></div>

								</div>

							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

	</form>

</body>
</html>