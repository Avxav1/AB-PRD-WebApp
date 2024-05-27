<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<!DOCTYPE html>
<% 
    //Response.Buffer = false;
    Response.CacheControl = "private";
%> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="x-ua-compatible" content="IE=edge" />
	<title>Product Swipe</title>
	<!-------------------------------jQuery---------------------------------->
	<script src="Script/jquery.min.js"></script>
	<script src="Script/jquery.touchSwipe.min.js"></script>
	<script src="Script/ABDS/products.js"></script>
	<script src="lib/js/jquery.boutique.min.js"></script>
	<!---------------------------------CSS------------------------------------>
	<link rel="stylesheet" href="lib/css/boutique.css" />
	<link href="Content/ABDS/products.css" rel="stylesheet" />
	<!------------------------------------------------------------------------>
</head>
<%--<body onload="dvProgress.style.display = 'none';">         lib/demo_files/images/loading.gif--%>
<body onbeforeunload="bodyUnload();" onclick="clicked=true;"  id="thebody" runat="server">
	<form id="form1" runat="server">
		<div id="container">
			<div class="row">
				<div class="column left">
					<div class="productlist" runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
						<div class="productlistinner" id="scrollbararea">
                            <input id="txtProduct" type="hidden" runat="server" />
                            <input id="txtSubProduct" type="hidden" runat="server" />
                            <input id="txtLangHidden" type="hidden" runat="server" />
                            <input id="txtCountryHidden" type="hidden" runat="server" />
                            <input id="txtDiscoveryBenchHidden" type="hidden" runat="server" />
                            <input id="txtBranchNumberHidden" type="hidden" runat="server" />
                            <input id="txtBranchNameHidden" type="hidden" runat="server" />
                            <input id="txttabletype" type="hidden" runat="server" />
                            <p>
							<asp:Label ID="lblLoan" runat="server" Text="Loans:" meta:resourcekey="lblLoan"></asp:Label>
							</p>
							<ul class="demo">
                                <li runat="server" id="liPersonalLoan" class="buttona btn active">»<asp:Label ID="lblPersonalLoan" runat="server" Text="» Personal Loan" meta:resourcekey="lblPersonalLoan"></asp:Label></li>
								<li runat="server" id="liAutoLoan" class="buttonb btn">»<asp:Label ID="lblAutoLoan" runat="server" Text="» Auto Loan" meta:resourcekey="lblAutoLoan"></asp:Label></li>
								<li runat="server" id="liHousingLoan" class="buttonc btn">»<asp:Label ID="lblHousingLoan" runat="server" Text="» Housing Loan" meta:resourcekey="lblHousingLoan"></asp:Label></li>
								<li runat="server" id="liNonRJHomeLoan" class="buttond btn">»<asp:Label ID="lblNonRJHomeLoan" runat="server" Text="» Non Resident Jordanians mortgage Loan" meta:resourcekey="lblNonRJHomeLoan"></asp:Label></li>
							</ul>
						</div>
					</div>
				</div>
				<div class="column right">
					<div id="parent">
						<div>
							<img id="prev" src="demo_files/images/previous.png" width="50" onclick="boutique_previous()" />
							<div class="centertext">
								<asp:Label ID="Label1" runat="server" Text="Tap to Select" meta:resourcekey="lblTapToSelect"></asp:Label>
							</div>
							<img id="next" src="demo_files/images/next.png" width="50" onclick="boutique_next()" />
						</div>
						<!-- The Boutique HTML: -->
						<ul id="boutique">
							<li runat="server" id="imgPersonalLoan"><a id="img1">
								<img id="leaflet1" runat="server" class="swipe" onclick="img1()"  src="demo_files/images/PersonalLoan.jpg" width="200" height="400" /></a>
							</li>
							<li runat="server" id="imgAutoloan"><a id="img2">
								<img id="leaflet2" runat="server" class="swipe" onclick="img2()" src="demo_files/images/Auto-Loan.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgHousingloan"><a id="img3">
								<img id="leaflet3" runat="server" class="swipe" onclick="img3()" src="demo_files/images/4housingloan.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgnrjhomeloan"><a id="img4">
								<img id="leaflet4" runat="server" class="swipe" onclick="img4()" src="demo_files/images/NRJ-HomeLoan.jpg" width="200" height="400" /></a></li>
						</ul>

						<div class="centertext">
							<asp:Label ID="lblTapToSelect" runat="server" Text="Tap to Select" meta:resourcekey="lblTapToSelect"></asp:Label>
						</div>

					</div>
				</div>
			</div>

		</div>
	    <asp:Button runat="server" ID="btnLoan" Text="" style="display:none;" OnClick="btnLoan_Click" />  
        <asp:Button runat="server" ID="btnSession" Text="" style="display:none;" OnClick="btnSession_Click" />    
	</form>
</body>
</html>
<script>

    function img1() {
        document.getElementById('txtProduct').value = "loan";
        document.getElementById('txtSubProduct').value = "personal";
        document.getElementById("btnLoan").click();
	}
    function img2() {

        document.getElementById('txtProduct').value = "loan";
        document.getElementById('txtSubProduct').value = "AutoLoan";
        document.getElementById("btnLoan").click();
	}
    function img3() {

        document.getElementById('txtProduct').value = "loan";
        document.getElementById('txtSubProduct').value = "housing";
        document.getElementById("btnLoan").click();
	}
    function img4() {

        document.getElementById('txtProduct').value = "loan";
        document.getElementById('txtSubProduct').value = "nonResidentJordan";
        document.getElementById("btnLoan").click();
	}
</script>

<script type="text/javascript">
	var clicked = false;
	function CheckBrowser() {
		if (clicked == false) {
			//Browser closed   
		} else {
			//redirected
			clicked = false;
		}
	}
    function bodyUnload() {
        //debugger;
		if (clicked == false)//browser is closed  
		{
			var request = GetRequest();
			//request.open("POST", "../LogOut.aspx", false);
			//request.send();
            document.getElementById("btnSession").click();
		}
	}

	function GetRequest() {
		var xmlhttp;
		if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
			xmlhttp = new XMLHttpRequest();
		}
		else {// code for IE6, IE5
			xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
		}
		return xmlhttp;
	}
	
</script>
<script>
	// Add active class to the current button (highlight it)
	var btns = document.getElementsByClassName("btn");

	for (var i = 0; i < btns.length; i++) {
		btns[i].addEventListener("click", function () {
			var current = document.getElementsByClassName("active");
			current[0].className = current[0].className.replace(" active", "");
			this.className += " active";
		});
	}
</script>