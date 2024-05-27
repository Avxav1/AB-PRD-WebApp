<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsBancassurance.aspx.cs" Inherits="ProductsBancassurance" %>
<!DOCTYPE html>
<% 
    //Response.Buffer = false;
    Response.CacheControl = "private";
%> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
	<meta http-equiv="x-ua-compatible" content="IE=edge" />
	<title>Product Swipe</title>
	<script src="Script/jquery.min.js"></script>
	<script src="Script/jquery.touchSwipe.min.js"></script>
    <script src="Script/ABDS/productsBancassurance.js"></script>
	<script src="lib/js/jquery.boutique.min.js"></script>
    <link rel="stylesheet" href="lib/css/boutique.css" />
    <link href="Content/ABDS/products.css" rel="stylesheet" />
</head>
<%--<body onload="dvProgress.style.display = 'none';">--%>
<body class="disable-selection" onbeforeunload="bodyUnload();" onclick="clicked=true;" id="thebody" runat="server">
	<form id="form1" runat="server">
		<div id="container">
			<div class="row">
				<div class="column left">
					<div class="productlist" runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
						<div class="productlistinner" id="scrollbararea">
                            <p>
								<asp:Label ID="lblBancassurance" runat="server" Text="Bancassurance:" meta:resourcekey="lblBancassurance"></asp:Label>
							</p>
							<ul class="demo">
								<li class="buttonu  btn active">»
									<asp:Label ID="lblCriticalIllness" runat="server" Text="Critical Illness" meta:resourcekey="lblCriticalIllness"></asp:Label>
								</li>
								<li class="buttonv btn">»
									<asp:Label ID="lblLammaYekbarou" runat="server" Text="Lamma Yekbarou" meta:resourcekey="lblLammaYekbarou"></asp:Label>
								</li>
								<li class="buttonw btn">»
									<asp:Label ID="lblJanaElOmr" runat="server" Text="Jana El Omr" meta:resourcekey="lblJanaElOmr"></asp:Label>
								</li>
								<li class="buttonx btn" runat="server" id="liAaelatyBiAman">»
									<asp:Label ID="lblAaelatyBiAman" runat="server" Text="Aaelaty" meta:resourcekey="lblAaelatyBiAman"></asp:Label>
								</li>
								<li class="buttony btn" runat="server" id="liRahetElBal">»
									<asp:Label ID="lblRahetElBal" runat="server" Text="Rahet El" meta:resourcekey="lblRahetElBal"></asp:Label>
								</li>
								<li class="buttonz btn" runat="server" id="liHattaYedersou">»
									<asp:Label ID="lblHattaYedersou" runat="server" Text="Hatta" meta:resourcekey="lblHattaYedersou"></asp:Label>
								</li>
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
							<li><a id="img21">
								<img id="leaflet1" runat="server" class="swipe" onclick="img26()" src="demo_files/images/critical-illness_E1_new.jpg" width="200" height="400"/>
							</a></li>
							<li><a id="img22">
								<img id="leaflet6" runat="server" class="swipe" onclick="img21()" src="demo_files/images/Lamma-Yek_En1_new.jpg" width="200" height="400"/>
							</a></li>
							<li><a id="img23">
								<img id="leaflet2" runat="server" class="swipe" onclick="img22()" src="demo_files/images/jana-el-omor-E1_new.jpg" width="200" height="400"/>
							</a></li>
							<li runat="server" id="imgleaf4"><a id="img24">
								<img id="leaflet3" runat="server" class="swipe" onclick="img23()" src="demo_files/images/Aelaty-Bi-Aman_E1_N.jpg" width="200" height="400"/>
							</a></li>
							<li runat="server" id="imgleaf5"><a id="img25">
								<img id="leaflet4" runat="server" class="swipe" onclick="img24()" src="demo_files/images/Rahet-El-Bal-En1_new.jpg" width="200" height="400"/>
							</a></li>
							<li runat="server" id="imgleaf6"><a id="img26">
								<img id="leaflet5" runat="server" class="swipe" onclick="img25()" src="demo_files/images/Hatta-Yedersou-E1_new.jpg" width="200" height="400"/>
							</a></li>
						</ul>
						<div class="centertext">
							<asp:Label ID="lblBancassurancTapToSelect" runat="server" Text="Tap to Select" meta:resourcekey="lblBancassurancTapToSelect"></asp:Label>
						</div>
					</div>
				</div>
			</div>
		</div>
	    <asp:Button runat="server" ID="btnLoan" Text="" style="display:none;" OnClick="btnLoan_Click"/>  
        <asp:Button runat="server" ID="btnSession" Text="" style="display:none;" OnClick="btnSession_Click"/>    
          <input id="txtProduct" type="hidden" runat="server"/>
          <input id="txtSubProduct" type="hidden" runat="server"/>
          <input id="txtLangHidden" type="hidden" runat="server"/>
          <input id="txtCountryHidden" type="hidden" runat="server"/>
          <input id="txtDiscoveryBenchHidden" type="hidden" runat="server"/>
          <input id="txtBranchNumberHidden" type="hidden" runat="server"/>
          <input id="txtBranchNameHidden" type="hidden" runat="server"/>
          <input id="txttabletype" type="hidden" runat="server"/>
	</form>
</body>
</html>
<script>
        function img21() {
            document.getElementById('txtProduct').value = "Bancassurance";
            document.getElementById('txtSubProduct').value = "LammaYekFlyer";
            document.getElementById("btnLoan").click();
		}
        function img22() {
            document.getElementById('txtProduct').value = "Bancassurance";
            document.getElementById('txtSubProduct').value = "JanaElOmor";
            document.getElementById("btnLoan").click();
		}
        function img23() {
            document.getElementById('txtProduct').value = "Bancassurance";
            document.getElementById('txtSubProduct').value = "AaelatyBiAman";
            document.getElementById("btnLoan").click();
		}
        function img24() {
            document.getElementById('txtProduct').value = "Bancassurance";
            document.getElementById('txtSubProduct').value = "RahetEIBal";
            document.getElementById("btnLoan").click();
		}
        function img25() {
            document.getElementById('txtProduct').value = "Bancassurance";
            document.getElementById('txtSubProduct').value = "HattaYedersou";
            document.getElementById("btnLoan").click();
		}
        function img26() {
            document.getElementById('txtProduct').value = "Bancassurance";
            document.getElementById('txtSubProduct').value = "CriticalIllness";
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
