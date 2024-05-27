<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsAccounts.aspx.cs" Inherits="ProductsAccounts" %>

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
    <script src="lib/js/jquery.boutique.min.js"></script>
    <script src="Script/ABDS/productsAccounts.js"></script>
    <!---------------------------------CSS------------------------------------>
    <link rel="stylesheet" href="lib/css/boutique.css" />
    <link href="Content/ABDS/products.css" rel="stylesheet" />
    <!------------------------------------------------------------------------>

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

                            <!-----HiddenTextBox-------------------------------------->

                            <p>
                                <asp:Label ID="lblAccounts" runat="server" Text="Accounts:" meta:resourcekey="lblAccounts"></asp:Label>
                            </p>
                            <ul class="demo">
                                <li class="buttonq btn active" id="lifixedDepositJD" runat="server">»
									<asp:Label ID="lblfixedDepositJD" runat="server" Text="Fixed Deposit - JOD" meta:resourcekey="lblfixedDepositJD"></asp:Label>
                                </li>
                                <li runat="server" id="liFixedDepositFC" class="buttonr btn">»
									<asp:Label ID="lblFixedDepositFC" runat="server" Text="Fixed Deposit – Foreign Currencies" meta:resourcekey="lblFixedDepositFC"></asp:Label>
                                </li>
                                <li runat="server" id="liElectronicDeposits" class="buttonu btn">»
									<asp:Label ID="lblElectronicDeposits" runat="server" Text="Electronic Deposits" meta:resourcekey="lblElectronicDeposits"></asp:Label>
                                </li>
                                <li runat="server" id="liCurrentAccount" class="buttons btn">»
									<asp:Label ID="lblCurrentAccount" runat="server" Text="Current Account" meta:resourcekey="lblCurrentAccount"></asp:Label>
                                </li>
                                <li runat="server" id="liSavingAccounts" class="buttont btn">»
									<asp:Label ID="lblSavingAccounts" runat="server" Text="Savings Account" meta:resourcekey="lblSavingAccounts"></asp:Label>
                                </li>
                                <li runat="server" id="liNewbutton1" class="buttonv btn">»
									<asp:Label ID="lblNewbutton1" runat="server" Text="New button1" meta:resourcekey="lblCurrentAccount"></asp:Label>
                                </li>
                                <li runat="server" id="liNewbutton2" class="buttonw btn">»
									<asp:Label ID="lblNewbutton2" runat="server" Text="New button2" meta:resourcekey="lblSavingAccounts"></asp:Label>
                                </li>
                                 <li runat="server" id="liNewbutton3" class="buttonx btn">»
									<asp:Label ID="lblNewbutton3" runat="server" Text="New button3" meta:resourcekey="lbleTawfeer"></asp:Label>
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

                            <li runat="server" id="imgleafletFixed">
                                <a id="img17" >
                                <img id="leaflet1" runat="server" class="swipe" onclick="img17()" src="demo_files/images/1Fixed deposite JD.jpg" width="200" height="400" />
                                </a>
                            </li>
                            <li runat="server" id="imgleaflet">
                                <a id="img18">
                                <img id="leaflet2" runat="server" class="swipe" onclick="img18()" src="demo_files/images/2Fixed deposite FC.jpg" width="200" height="400" />
                                </a>
                            </li>
                             <li runat="server" id="imgleaflet3">
                                <a id="img19">
                                <img id="leaflet3" runat="server" class="swipe" onclick="img19()" src="demo_files/images/Current_account_front_En.jpg" width="200" height="400" />
                                </a>
                            </li>
                            <li runat="server" id="imgleaflet4">
                                <a id="img20">
                                <img id="leaflet4" runat="server" class="swipe" onclick="img20()" src="demo_files/images/4savings account.jpg" width="200" height="400" />
                                </a>
                            </li>
                            <li runat="server" id="imgNewleafetelec">
                                <a id="img21">
                                <img id="leaflet5" runat="server" class="swipe" onclick="img21()" src="demo_files/images/Electronic Deposits_E1.jpg" width="200" height="400" />
                                </a>
                            </li>
                            <li runat="server" id="imgNewleafet1">
                                <a id="img22">
                                <img id="leaflet6" runat="server" class="swipe" onclick="img22()" src="demo_files/images/4savings account.jpg" width="200" height="400" />
                                </a>
                            </li>
                            <li runat="server" id="imgNewleafet2">
                                <a id="img23">
                                <img id="leaflet7" runat="server" class="swipe" onclick="img23()" src="demo_files/images/Electronic Deposits_E1.jpg" width="200" height="400" />
                                </a>
                            </li>
                            <li runat="server" id="imgNewleafet3">
                                <a id="img24">
                                <img id="leaflet8" runat="server" class="swipe" onclick="img24()" src="demo_files/images/Electronic Deposits_E1.jpg" width="200" height="400" />
                                </a>
                            </li>
                        </ul>

                        <div class="centertext">
                            <asp:Label ID="lblAccountToToSelct" runat="server" Text="Tap to Select" meta:resourcekey="lblAccountToToSelct"></asp:Label>
                        </div>


                    </div>
                </div>
            </div>

        </div>
        <asp:Button runat="server" ID="btnLoan" Text="" Style="display: none;" OnClick="btnLoan_Click" />
        <asp:Button runat="server" ID="btnSession" Text="" Style="display: none;" OnClick="btnSession_Click" />
    </form>
</body>
</html>
<script>

    function img17() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "FixedDepositJD";
        document.getElementById("btnLoan").click();
    }
    function img18() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "FixedDepositForeign";
        document.getElementById("btnLoan").click();
    }
    function img19() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "Current";
        document.getElementById("btnLoan").click();
    }
    function img20() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "Savings";
        document.getElementById("btnLoan").click();
    }
    function img21() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "ElectronicDeposits";
        document.getElementById("btnLoan").click();
    }
    function img22() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "NewButton1";
        document.getElementById("btnLoan").click();
    }
    function img23() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "NewButton2";
        document.getElementById("btnLoan").click();
    }
    function img24() {
        document.getElementById('txtProduct').value = "accounts";
        document.getElementById('txtSubProduct').value = "NewButton3";
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
