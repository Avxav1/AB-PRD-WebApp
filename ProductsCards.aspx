<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsCards.aspx.cs" Inherits="ProductsCards" %>

<!DOCTYPE html>
<% 
    //Response.Buffer = false;
    Response.CacheControl = "private";
%> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="x-ua-compatible" content="IE=edge" />
	<title>Product Swipe</title>
	<script src="Script/jquery.min.js"></script>
	<script src="Script/jquery.touchSwipe.min.js"></script>
	<link rel="stylesheet" href="lib/css/boutique.css" />
    <link href="Content/ABDS/products.css" rel="stylesheet" />
	<script src="lib/js/jquery.boutique.min.js"></script>
	<script src="Script/ABDS/productsCards.js"></script>
</head>

<body class="disable-selection" onbeforeunload="bodyUnload();" onclick="clicked=true;" id="thebody" runat="server">
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
								<asp:Label ID="lblCards" runat="server" Text="Cards:" meta:resourcekey="lblCards"></asp:Label>
							</p>
							<ul class="demo">
								<li runat="server"  id="liVisaDebitCard" class="buttone btn active">»
								<asp:Label ID="lblVisaDebitCard" runat="server" Text="Visa Debit Card" meta:resourcekey="lblVisaDebitCard"></asp:Label>
								</li>
								<li runat="server"  id="liInternetShoppingCards" class="buttonf btn">»
									<asp:Label ID="lblInternetShoppingCards" runat="server" Text="Internet Shopping Card" meta:resourcekey="lblInternetShoppingCards"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankVisaSignatureCC" class="buttong btn">»
									<asp:Label ID="lblArabBankVisaSignatureCC" runat="server" Text="Arab Bank Visa Signature Credit Card" meta:resourcekey="lblArabBankVisaSignatureCC"></asp:Label>
								</li>
								<li runat="server"  id="liTogetherPlatinumCC" class="buttonh btn">»
									<asp:Label ID="lblTogetherPlatinumCC" runat="server" Text="Together Platinum Credit Card" meta:resourcekey="lblTogetherPlatinumCC"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankVisaPlatinumCC" class="buttoni btn">»
									<asp:Label ID="lblArabBankVisaPlatinumCC" runat="server" Text="Arab Bank Visa Platinum Credit Card" meta:resourcekey="lblArabBankVisaPlatinumCC"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankVisaBlackCC" class="buttonj btn">»
									<asp:Label ID="lblArabBankVisaBlackCC" runat="server" Text="Arab Bank Visa Black Credit Card" meta:resourcekey="lblArabBankVisaBlackCC"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankMasterCardTitaniumCC" class="buttonk btn">»
									<asp:Label ID="lblArabBankMasterCardTitaniumCC" runat="server" Text="Arab Bank MasterCard Titanium Credit Card" meta:resourcekey="lblArabBankMasterCardTitaniumCC"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankVisaGoldCreditCard" class="buttonl btn">»
									<asp:Label ID="lblArabBankVisaGoldCreditCard" runat="server" Text="Arab Bank Visa Gold Credit Card" meta:resourcekey="lblArabBankVisaGoldCreditCard"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankVisaSilverCard" class="buttonm btn">»
									<asp:Label ID="lblArabBankVisaSilverCard" runat="server" Text="Arab Bank Visa Silver Card" meta:resourcekey="lblArabBankVisaSilverCard"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankZainVisaCC" class="buttonn btn">»
									<asp:Label ID="lblArabBankZainVisaCC" runat="server" Text="Arab Bank - Zain Visa Credit Card" meta:resourcekey="lblArabBankZainVisaCC"></asp:Label>
								</li>
								<li runat="server"  id="liArabBankRoyalJordanianVisaCC" class="buttono btn">»
									<asp:Label ID="lblArabBankRoyalJordanianVisaCC" runat="server" Text="Arab Bank - Royal Jordanian Visa Credit Card" meta:resourcekey="lblArabBankRoyalJordanianVisaCC"></asp:Label>
								</li>
								<li runat="server"  id="liNashamaVisaCC" class="buttonp btn">»
									<asp:Label ID="lblNashamaVisaCC" runat="server" Text="Nashama Visa Credit Card" meta:resourcekey="lblNashamaVisaCC"></asp:Label>
								</li>
                                
                                <li runat="server"  id="liShababVisaCreditCard" class="buttono btn">»
									<asp:Label ID="Label1" runat="server" Text="Shabab Visa Credit Card" meta:resourcekey="lblShababVisaCreditCard"></asp:Label>
								</li>
								<li runat="server"  id="liShababVisaDebitCard" class="buttonp btn">»
									<asp:Label ID="Label2" runat="server" Text="Shabab Visa Debit Card" meta:resourcekey="lblShababVisaDebitCard"></asp:Label>
								</li>
								<li runat="server"  id="liVisaTravelMateCC" class="buttonq btn">»
									<asp:Label ID="Label3" runat="server" Text="Visa Travel Mate Credit Card" meta:resourcekey="lblVisaTravelMateCC"></asp:Label>
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

							<li runat="server" id="imgleaflet1"><a id="img5">
								<img id="leaflet1" runat="server" class="swipe" onclick="img5()" src="demo_files/images/visa debit_new.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet2"><a id="img6">
								<img id="leaflet2" runat="server" class="swipe" onclick="img6()" src="demo_files/images/2internet shopping.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet3"><a id="img7">
								<img id="leaflet3" runat="server" class="swipe" onclick="img16()" src="demo_files/images/12Visa Signature.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet4"><a id="img8">
								<img id="leaflet4" runat="server" class="swipe" onclick="img14()" src="demo_files/images/TogetherPlatinum.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet5"><a id="img9">
								<img id="leaflet5" runat="server" class="swipe" onclick="img13()" src="demo_files/images/9Visa Platinum.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet6"><a id="img10">
								<img id="leaflet6" runat="server" class="swipe" onclick="img9()" src="demo_files/images/visa-black-CC_front_english.jpg" width="200" height="400" />

							</a>
							</li>
							<li runat="server" id="imgleaflet7"><a id="img11">
								<img id="leaflet7" runat="server" class="swipe" onclick="img12()" src="demo_files/images/8mc titanium.jpg" width="200" height="400" />

							</a>
							</li>
							<li runat="server" id="imgleaflet8"><a id="img12">
								<img id="leaflet8" runat="server" class="swipe" onclick="img8()" src="demo_files/images/4Visa Gold.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet9"><a id="img13">
								<img id="leaflet9" runat="server" class="swipe" onclick="img7()" src="demo_files/images/silver credit.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet10"><a id="img14"></a>
								<img id="leaflet10" runat="server" class="swipe" onclick="img10()" src="demo_files/images/6zain visa.jpg" width="200" height="400" />
							</li>
							<li runat="server" id="imgleaflet11"><a id="img15">
								<img id="leaflet11" runat="server" class="swipe" onclick="img11()" src="demo_files/images/7royal jordanian cc.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet12"><a id="img16"></a>
								<img id="leaflet12" runat="server" class="swipe" onclick="img15()" src="demo_files/images/11nashama.jpg" width="200" height="400" />
							</li>


                            <li runat="server" id="imgleaflet13"><a id="img17"></a>
								<img id="leaflet13" runat="server" class="swipe" onclick="img17()" src="LeafletEN/Disc/EnCards/ShababVisaCreditCard/files/page/4.jpg" width="200" height="400" />
							</li>
							<li runat="server" id="imgleaflet14"><a id="img18">
								<img id="leaflet14" runat="server" class="swipe" onclick="img18()" src="LeafletEN/Disc/EnCards/ShababVisaDebitCard/files/page/4.jpg" width="200" height="400" />
							</a>
							</li>
							<li runat="server" id="imgleaflet15"><a id="img19"></a>
								<img id="leaflet15" runat="server" class="swipe" onclick="img19()" src="LeafletEN/Disc/EnCards/VisaTravelMateCC/files/page/4.jpg" width="200" height="400" />
							</li>
							
						</ul>

						<div class="centertext">
							<asp:Label ID="lblCardsTaptoSelect" runat="server" Text="Tap to Select" meta:resourcekey="lblCardsTaptoSelect"></asp:Label>
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

        function img5() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaDebitCard";
            document.getElementById("btnLoan").click();
		}
        function img6() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "InternetShoppingCard";
            document.getElementById("btnLoan").click();
		}
        function img7() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaSilver";
            document.getElementById("btnLoan").click();
		}
        function img8() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaGoldCC";
            document.getElementById("btnLoan").click();
		}
        function img9() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaBlackCC";
            document.getElementById("btnLoan").click();
		}
        function img10() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "ZainVisa";
            document.getElementById("btnLoan").click();
		}
        function img11() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "RJCreditCard";
            document.getElementById("btnLoan").click();
		}
        function img12() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "MasterCardTitanium";
            document.getElementById("btnLoan").click();
		}
        function img13() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaPlatinumCC";
            document.getElementById("btnLoan").click();
		}
        function img14() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "TogetherPlatinumCC";
            document.getElementById("btnLoan").click();
		}
        function img15() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "NashamaVisaCredit";
            document.getElementById("btnLoan").click();
		}
        function img16() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaSignatureCreditCard";
            document.getElementById("btnLoan").click();
		}
        function img17() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "ShababVisaCreditCard";
            document.getElementById("btnLoan").click();
        }
        function img18() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "ShababVisaDebitCard";
            document.getElementById("btnLoan").click();
        }
        function img19() {
            document.getElementById('txtProduct').value = "Cards";
            document.getElementById('txtSubProduct').value = "VisaTravelMateCC";
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
