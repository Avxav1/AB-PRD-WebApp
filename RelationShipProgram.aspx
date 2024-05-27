<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelationShipProgram.aspx.cs" Inherits="Products" %>

<!DOCTYPE html>
<% 
    //Response.Buffer = false;
    Response.CacheControl = "private";
%> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="x-ua-compatible" content="IE=edge" />
	<title>Relationship Program Swipe</title>
	<!-------------------------------jQuery---------------------------------->
	<script src="Script/jquery.min.js"></script>
	<script src="Script/jquery.touchSwipe.min.js"></script>
	<script src="Script/ABDS/products.js"></script>
	<script src="lib/js/jquery.boutique.min.js"></script>
	<link rel="stylesheet" href="lib/css/boutique.css" />
	<link href="Content/ABDS/products.css" rel="stylesheet" />
	<!------------------------------------------------------------------------>
</head>
<%--<body onload="dvProgress.style.display = 'none';"> lib/demo_files/images/loading.gif--%>
<body class="disable-selection" onbeforeunload="bodyUnload();" onclick="clicked=true;" id="thebody" runat="server">
	<form id="form1" runat="server">
		<div id="container">
			<div class="row">
				<div class="column left">
					<div class="productlist" runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
						<div class="productlistinner" id="scrollbararea">
							
                            <p>
								<asp:Label ID="lblRelationshipPrograms" runat="server" Text="Relationship Programs:" meta:resourcekey="lblRelationshipPrograms"></asp:Label>
							</p>
							<ul class="demo">
								<li runat="server"  id="liElite" class="buttona btn active">»
									<asp:Label ID="lblElite" runat="server" Text="Elite" meta:resourcekey="lblElite"></asp:Label></li>
								<li runat="server"  id="liArabiPremium" class="buttonb btn">»
									<asp:Label ID="lblArabiPremium" runat="server" Text="Arabi Premium" meta:resourcekey="lblArabi"></asp:Label></li>
								<li runat="server"  id="liArabiCrossBoarders" class="buttonc btn">»
									<asp:Label ID="lblArabiCrossBoarders" runat="server" Text="Arabi Cross Borders" meta:resourcekey="lblArabiCrossBoarders"></asp:Label></li>
								<li runat="server"  id="liArabiExtra" class="buttond btn">»
									<asp:Label ID="lblArabiExtra" runat="server" Text="Arabi Extra" meta:resourcekey="lblArabiExtra"></asp:Label></li>
								<li runat="server"  id="liShabab" class="buttone btn">»
									<asp:Label ID="lblShabab" runat="server" Text="Shabab" meta:resourcekey="lblShabab"></asp:Label></li>
								<li runat="server"  id="liJeelAlArabi" class="buttonf btn">»
									<asp:Label ID="lblJeelAlArabi" runat="server" Text="Arabi Junior" meta:resourcekey="lblJeelAlArabi"></asp:Label></li>
								<li runat="server"  id="liTabeebPlus" class="buttong btn">»
									<asp:Label ID="lblTabeebPlus" runat="server" Text="Tabeeb Plus" meta:resourcekey="lblJeelAlArabi"></asp:Label></li>
							</ul>
						</div>
					</div>

				</div>
				<div class="column right">
					<div id="parent">
						<img id="prev" src="demo_files/images/previous.png" width="50" onclick="boutique_previous()" />
						<img id="next" src="demo_files/images/next.png" width="50" onclick="boutique_next()" />
						<!-- The Boutique HTML: -->
						<ul id="boutique">

							<li runat="server" id="imgleaflet1"><a id="img1">
                            <img id="leaflet1" runat="server" class="swipe" onclick="img1()" src="demo_files/images/elite_NN.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgleaflet2"><a id="img2">
							<img id="leaflet2" runat="server" class="swipe" onclick="img2()" src="demo_files/images/arabi_premium_English.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgleaflet3"><a id="img3">
							<img id="leaflet3" runat="server" class="swipe" onclick="img3()" src="demo_files/images/cros_border.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgleaflet4"><a id="img4">
							<img id="leaflet4" runat="server" class="swipe" onclick="img4()" src="demo_files/images/arabi_extra_program_new.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgleaflet5"><a id="img5">
							<img id="leaflet5" runat="server" class="swipe" onclick="img5()" src="demo_files/images/Shabab-Revised.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgleaflet6"><a id="img6">
							<img id="leaflet6" runat="server" class="swipe" onclick="img6()" src="demo_files/images/jeel_al_arabi.jpg" width="200" height="400" /></a></li>
							<li runat="server" id="imgleaflet7"><a id="img7">
							<img id="leaflet7" runat="server" class="swipe" onclick="img7()" src="demo_files/images/jeel_al_arabi.jpg" width="200" height="400" /></a></li>

						</ul>

						<div class="centertext">
							<asp:Label ID="lblRealtionShipTaptoSelect" runat="server" Text="Tap to Select" meta:resourcekey="lblRealtionShipTaptoSelect"></asp:Label>
						</div>
					</div>
				</div>
			</div>
		</div>

        <asp:Button runat="server" ID="btnLoan" Text="" style="display:none;" OnClick="btnLoan_Click" />  
        <asp:Button runat="server" ID="btnSession" Text="" style="display:none;" OnClick="btnSession_Click" />    

		<input id="txtProduct" type="hidden" runat="server" />
        <input id="txtSubProduct" type="hidden" runat="server" />
        <input id="txtLangHidden" type="hidden" runat="server" />
        <input id="txtCountryHidden" type="hidden" runat="server" />
        <input id="txtDiscoveryBenchHidden" type="hidden" runat="server" />
        <input id="txtBranchNumberHidden" type="hidden" runat="server" />
        <input id="txtBranchNameHidden" type="hidden" runat="server" />
        <input id="txttabletype" type="hidden" runat="server" />

	</form>
</body>
</html>
<script>
	
    function img1() {

        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "Elite";
        document.getElementById("btnLoan").click();
	}
    function img2() {

        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "ArabiPremium";
        document.getElementById("btnLoan").click();
	}
    function img3() {

        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "ArabiCrossBoarders";
        document.getElementById("btnLoan").click();
	}
    function img4() {

        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "ArabiExtra";
        document.getElementById("btnLoan").click();
	}
    function img5() {

        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "Shabab";
        document.getElementById("btnLoan").click();
	}
    function img6() {
        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "JeelAlArabi";
        document.getElementById("btnLoan").click();
	}
    function img7() {
        document.getElementById('txtProduct').value = "RelationShipProgram";
        document.getElementById('txtSubProduct').value = "TabeebPlus";
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