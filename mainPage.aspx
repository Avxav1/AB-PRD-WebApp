<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainPage.aspx.cs" Inherits="Personalloan" Async="true" %>

<!DOCTYPE html>
<% 
    //Response.Buffer = false;
    Response.CacheControl = "private";
%> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=Edge" />

	<title>Leaflet</title>

	<meta name="Keywords" content="" />
	<meta name="Description" content="Trifold" />
	<meta name="Generator" content="Avxav" />
	<meta name="medium" content="video" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
	<meta property="og:image" content="files/shot.png" />
	<meta property="og:title" content="Trifold" />
	<meta property="og:description" content="Trifold" />
	<meta property="og:video" content="book.swf" />
	<meta property="og:video:height" content="300" />
	<meta property="og:video:width" content="420" />
	<meta property="og:video:type" content="application/x-shockwave-flash" />
	<meta name="video_height" content="300" />
	<meta name="video_width" content="420" />
	<meta name="video_type" content="application/x-shockwave-flash" />
	<meta name="og:image" content="files/shot.png" />
	<link rel="image_src" href="files/shot.png" />
	<link rel="stylesheet" href="../lib/css/modalpopup.css" />
	<link rel="stylesheet" href="../lib/css/bootstrap.min.css" />
	<link rel="stylesheet" href="../lib/css/jqbtk.css" />
	<link href="../Content/ABDS/productDetailTriFold.css" rel="stylesheet" />
	</head>
<body class="disable-selection" onbeforeunload="bodyUnload();" onclick="clicked=true;"  id="thebody" runat="server">
	<script src="lib/js/jquery-1.10.1.min.js"></script>
	<form id="form1" runat="server">
        <input id="hlanguage" runat="server" type="hidden" />
        <asp:HiddenField ID="hcountryId" runat="server" />
        <asp:HiddenField ID="hbranchId" runat="server" />
        <asp:HiddenField ID="hdiscoveryBench" runat="server" />
        <asp:HiddenField ID="httype" runat="server" />
        <asp:HiddenField ID="hcountry" runat="server" />
        <asp:HiddenField ID="hbranchNumber" runat="server" />
        <asp:HiddenField ID="hbranchName" runat="server" />
        <asp:HiddenField ID="hproductName" runat="server" />
        <asp:HiddenField ID="hsubProductName" runat="server" />
        <asp:HiddenField ID="hFrmJD" runat="server" />
        <asp:HiddenField ID="hToJD" runat="server" />
        <asp:HiddenField ID="hfrmMnth" runat="server" />
        <asp:HiddenField ID="htoMnth" runat="server" />
        <asp:HiddenField ID="hinterestRate" runat="server" />
        <asp:HiddenField ID="hpName" runat="server" />
        
		<div id="container">
			<div class="row">
				<div class="column left">
					<div class="productlist">
						<div class="w3-content w3-section" style="max-width: 500px">
							<div id="imgDiv1" style="visibility: hidden" runat="server">
								<img class="mySlides" src="../images/Personal%20Loan%20-%20Refresher.jpg" id="ritSideImage1" runat="server" style="width: 100%; visibility: hidden" />
							</div>
						</div>
						<br />
						<!----------------------------------Hidden TextBoxes-------------------------------------------->
						<asp:TextBox ID="productName" runat="server" Style="display: none"></asp:TextBox>
						<asp:TextBox ID="subProductName" runat="server" Style="display: none"></asp:TextBox>
						<asp:TextBox ID="swfNAme" runat="server" Style="display: none"></asp:TextBox>
						<!----------------------------------------------------------------------------------------------->

						<asp:Button ID="btnIwillthink" class="myButton" runat="server" Text="May be later" OnClick="btnIwillthink_Click" meta:resourcekey="btnIwillthink" />
						<br />
						<br />
						<%-- <span onclick="document.getElementById('id01').style.display='block'" class="myButton"  >I will think</span><br /><br />--%>
						<asp:Button ID="btnamInterested" class="myButton" runat="server" Text="I am Interested" OnClick="btnamInterested_Click" meta:resourcekey="btnamInterested" />
						<br />
						<br />
						<asp:Button ID="btnIneedhelp" class="myButton" runat="server" Text="I need help" OnClick="btnIneedhelp_Click" meta:resourcekey="btnIneedhelp" />
						<br />
						<br />
						<asp:Button ID="btnhousingloancalculator" class="myButton" runat="server" Text="Loan Calculator" OnClick="btnloancalcu_Click" meta:resourcekey="btnhousingloancalculator" />

						<asp:Button ID="btnhousingDepositcalculator" class="myButton" runat="server" Text="Deposit Calculator" OnClick="btnDepositcalcu_Click" meta:resourcekey="btnhousingDepositcalculator" />

					</div>

				</div>
				<div class="column right">
					<div class="productlistright">

						<div id="parent">
							<%--<div class="row">--%>


							<%-- </div>--%>
						</div>


						<div class="column" runat="server" dir='<%$ Resources: Resource,TextDirection%>'>

							<div style="color: white; font-size: 15pt; font-family: 'Myriad Pro'; height: 440px; width: 811px; margin-right: -11px;">
								<!---------------------------------topBar------------------------------------------------>
								<%--	<div style="float: right; height: 40px; width: 40px; padding-right: 5px">
								</div>--%>
								<div class="triheader">
									<asp:Label ID="productNameLbl" runat="server" Text="Label" Style="font-size: 24px; margin-left: 12px; width: 93%;"></asp:Label>
									<div style="width: 14%;"></div>
									<%--<asp:ImageButton Style="height: 48px; width: 48px; margin-bottom: -11px;" ID="backBtnImg" runat="server" ImageUrl="~/source/img/next.png" Width="48" Height="48"  />--%>
									<asp:HyperLink ID="backBtnImg" Style="height: 48px; width: 48px; margin-bottom: -11px;" runat="server" ImageUrl="~/source/img/previous1.png"></asp:HyperLink>
								</div>
								<!------------------------------------------------------------------------------------------->
								<div class="trifoldstyles">
                                      <div id="buttondisable" class="buttondisable" style="z-index: 1001; position:absolute"> 
                                          </div>
								<%--<div id="flashContent" class="flashContent">
                                    
								</div>--%>

							</div>

						</div>
					</div>
				</div>
			</div>

			<div class="w3-container" runat="server" dir='<%$ Resources: Resource,TextDirection%>'>

				<div id="id01" runat="server" class="w3-modal" style="z-index:5001">
					<!----------MayBe Later--------------------------------->
					<div class="w3-modal-content w3-card-4">
						<header class="w3-container w3-blue">
							<%--		<span id="closeBtn" runat="server" onclick="document.getElementById('id01').style.display='none'"
								class="w3-button w3-large w3-display-topright">&times;</span>--%>
							<h3>
								<asp:Label ID="lblPopupMayBeLater" runat="server" Text="Maybe later" meta:resourcekey="lblPopupMayBeLater"></asp:Label></h3>
						</header>
						<div class="w3-container">
							<br />
							<div style="border: none; overflow-x: hidden; height: 350px; width: 654px; margin-right: 20%; margin-left: 20%;">

								<asp:TextBox ID="prdctNameCntrlTxt" runat="server" name="ucHiddenTxt00" Style="visibility: hidden"></asp:TextBox><!-----------Hidden textBox------------>
								<asp:TextBox ID="suPrdctNameCntrlTxt" runat="server" name="ucHiddenTxt01" Style="visibility: hidden"></asp:TextBox><!-----------Hidden textBox------------>

								<asp:Label ID="lblPopMayBeLatrName" runat="server" CssClass="label" Text="Name" meta:resourcekey="lblPopMayBeLatrName"></asp:Label>

								<asp:TextBox  ID="txtName" onkeypress="return isspecialchara(event)" runat="server" class="keyboard form-control" ToolTip="Enter Full Name" TabIndex="1" Style=" width: 329px; font-size: 25px;"></asp:TextBox>
								<asp:Label ID="nameAlert" runat="server" Text="Please enter your name" Style="color: white; display: none; width: 100%; " meta:resourcekey="nameAlert"></asp:Label>
								<asp:Label ID="loanAmountRequiredLabel" runat="server" ForeColor="Red"
									Text="*Required Field" Visible="False"></asp:Label>
								<br />

								<asp:Label ID="lblPopMayBeLatrMobile" runat="server" CssClass="label" Text="Mobile" meta:resourcekey="lblPopMayBeLatrMobile"></asp:Label>

								<div style="display: inline-flex;  direction: ltr;">
									
									<div id="iwillthinkleft" runat="server">
										<asp:TextBox ID="txtMobileCd" Style="width: 100px; font-size: 25px; display: block;" runat="server" TabIndex="2" class="keyboard form-control" MaxLength="5" onkeypress="return isNumber(event)"></asp:TextBox>
										<asp:Label ID="lblMobileCode" runat="server" Text="eg: 009xx" meta:resourcekey="lblMobileCode"></asp:Label>
										<asp:Label ID="mobCDLimitAlertLbl" runat="server" Text="Minimum 5" Style="color: white; font-size: 13px; display: none;" meta:resourcekey="mobCDLimitAlertLbl"></asp:Label>
									</div>
									<div style="width: 2%">
									</div>
									<div id="iwillthinkright" runat="server">
										<asp:TextBox  ID="txtMobile" Style="width:100%; font-size: 25px;" runat="server" TabIndex="3" ToolTip="Enter Numbers Only" class="keyboard form-control" MaxLength="15" onkeypress="return isNumber(event)" ></asp:TextBox>
										<asp:Label ID="lblMobile" runat="server" Text="eg: 07xxxxxxxx" Style="margin: 14px;" meta:resourcekey="lblMobile"></asp:Label>
                                        <asp:Label ID="mobAlertempty1" runat="server" Text="Enter a number" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobAlertempty"></asp:Label>
										<asp:Label ID="mobLimitAlertLbl" runat="server" Text="Format Incorrect" Style="color: white;  display: none; margin-left: 15px;" meta:resourcekey="mobLimitAlertLbl"></asp:Label>
                                        <asp:Label ID="mob07tAlertLbl" runat="server" Text="Format Incorrect" Style="color: white;  display: none; margin-left: 15px;" meta:resourcekey="mobStart07AlertLbl"></asp:Label>
										<%--<asp:Label ID="mobEmptyAlertLbl" runat="server" Text="Please enter mobile number" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobEmptyAlertLbl"></asp:Label>--%>
									</div>
									<%--<div style="margin-top: 2%; margin-left: 10px; width: 26%">
										<asp:Label ID="mobAlertLbl" runat="server" Text="Please enter Numbers" Style="color: white; display: none;" meta:resourcekey="mobAlertLbl"></asp:Label>
									</div>--%>
									<%--<div style="width: 26%">
									</div>--%>
								</div>

								<asp:Label ID="interestRateRequiredLabel" runat="server" ForeColor="Red"
									Text="*Required Field" Visible="False"></asp:Label>
								<br />
								<div class="actionbutton" style="width: 62%; ">
									<asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="subBtn"
										ToolTip="Submit" OnClick="SubmitButton_Click" TabIndex="4" meta:resourcekey="SubmitButton" />
									<asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="cancelBtn"
										ToolTip="Cancel" OnClick="CancelButton_Click" meta:resourcekey="Button2" />
								</div>
							</div>
						</div>

					</div>
				</div>

				<div id="id02" runat="server" class="w3-modal" style="z-index:5001">
					<div class="w3-modal-content w3-card-4">
						<header class="w3-container w3-blue">
							<%--	<span id="closeBtn1" runat="server" onclick="document.getElementById('id02').style.display='none'"
								class="w3-button w3-large w3-display-topright">&times;</span>--%>
							<h3>
								<asp:Label ID="lblPopupIamInterested" runat="server" Text="I am Interested" meta:resourcekey="lblPopupIamInterested"></asp:Label></h3>
						</header>
						<div class="w3-container">
							<br />
							<%--  <iframe src="Iwillthink.aspx"  style="border:none; overflow-x: hidden;" height="400" width="870"></iframe>--%>
							<div style="border: none; overflow-x: hidden; height: 350px; width: 654px; margin-right: 20%; margin-left: 20%;">
								<%--<asp:Label ID="webLoanCalculatorLabel" runat="server"
                    Text="I will think"></asp:Label>--%>
								<asp:TextBox ID="prdctNameCntrlTxt1" runat="server" name="ucHiddenTxt00" Style="visibility: hidden"></asp:TextBox><!-----------Hidden textBox------------>
								<asp:TextBox ID="suPrdctNameCntrlTxt1" runat="server" name="ucHiddenTxt" Style="visibility: hidden"></asp:TextBox><!-----------Hidden textBox------------>
								<asp:Label ID="lblPopupIamInterestedName" runat="server" CssClass="label" Text="Name" meta:resourcekey="lblPopupIamInterestedName"></asp:Label>
								<asp:TextBox onkeypress="return isspecialchara(event)" ID="txtnameInterested" runat="server" class="keyboard form-control" ToolTip="Enter Full Name" Style=" width: 329px; font-size: 25px;" TabIndex="1"></asp:TextBox>
								<asp:Label ID="alertNameInterested" runat="server" Text="Please enter your name" Style="color: white; display: none; width: 100%; " meta:resourcekey="nameAlert"></asp:Label>

								<asp:Label ID="Label3" runat="server" ForeColor="Red"
									Text="*Required Field" Visible="False"></asp:Label>
								<br />

								<asp:Label ID="lblPopupIamInterestedMobile" runat="server" CssClass="label" Text="Mobile" meta:resourcekey="lblPopupIamInterestedMobile"></asp:Label>
								<div style="display: inline-flex;   direction: ltr;">
									<div id="iaminterestedleft" runat="server">
										<asp:TextBox ID="txtmobileinterestedCd" Style="width:100px; font-size: 25px;" runat="server" TabIndex="2" MaxLength="5" class="keyboard form-control" onkeypress="return isNumber(event)"></asp:TextBox>
										<asp:Label ID="lblMobileCode1" runat="server" Text="eg: 009xx" meta:resourcekey="lblMobileCode1"></asp:Label>
										<asp:Label ID="mobCDLimitAlertLbl1" runat="server" Text="Minimum 5" Style="color: white;font-size: 13px; display: none;" meta:resourcekey="mobCDLimitAlertLbl"></asp:Label>
									</div>
									<div style="width: 2%">
									</div>
									<div id="iaminterestedright" runat="server">
										<asp:TextBox ID="txtmobileinterested" Style="width:100%; font-size: 25px;" runat="server" TabIndex="3" ToolTip="Enter Numbers Only"  MaxLength="15" class="keyboard form-control" onkeypress="return isNumber(event)"></asp:TextBox>
										<asp:Label ID="lblMobile1" runat="server" Text="eg: 07xxxxxxxx" Style="margin: 14px;" meta:resourcekey="lblMobile1"></asp:Label>
                                        <asp:Label ID="mobAlertempty" runat="server" Text="Enter a number" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobAlertempty"></asp:Label>
										<asp:Label ID="mobLimitAlertLbl1" runat="server" Text="Minimum 10" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobLimitAlertLbl"></asp:Label>
                                        <asp:Label ID="mob07tAlertLbl1" runat="server" Text="Minimum 10" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobStart07AlertLbl"></asp:Label>
									</div>
									<%--<div style="margin-top: 2%; margin-left: 10px; width: 26%">
										<asp:Label ID="mobAlertLbl1" runat="server" Text="Please enter Numbers" Style="color: white; display: none;" meta:resourcekey="mobAlertLbl"></asp:Label>
									</div>--%>

								</div>

								<asp:Label ID="Label5" runat="server" ForeColor="Red"
									Text="*Required Field" Visible="False"></asp:Label>
								<br />
								<div class="actionbutton" style="width: 62%; ">
									<asp:Button ID="Button3" runat="server" Text="Submit" CssClass="subBtn"
										ToolTip="Submit" OnClick="SubmitButtoninterested_Click" TabIndex="4" meta:resourcekey="Button3" />
									<asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="cancelBtn"
										ToolTip="Cancel" OnClick="CancelButtoninterested_Click" TabIndex="5" meta:resourcekey="Button4" />
								</div>
							</div>
						</div>
						<%--<footer class="w3-container w3-teal">
        <p>Modal Footer</p>
      </footer>--%>
					</div>
				</div>

				<div id="id03" runat="server" class="w3-modal" style="z-index:5001">
					<div class="w3-modal-content w3-card-4">
						<header class="w3-container w3-blue">
							<%--<span id="closeBtn2" runat="server" onclick="document.getElementById('id03').style.display='none'"
								class="w3-button w3-large w3-display-topright">&times;</span>--%>
							<h3>
								<asp:Label ID="lblPopupIamNeedHelp" runat="server" Text="I need help" meta:resourcekey="lblPopupIamNeedHelp"></asp:Label></h3>
						</header>
						<div class="w3-container">
							<br />
							<%-- <iframe src="Ineedhelp.aspx"  style="border:none; overflow-x: hidden;" height="400" width="870"></iframe>--%>
							<div style="border: none; overflow-x: hidden; height: 350px; width: 654px; margin-right: 20%; margin-left: 20%;">
								<%--<asp:Label ID="webLoanCalculatorLabel" runat="server"
                    Text="I will think"></asp:Label>--%>
								<asp:TextBox ID="prdctNameCntrlTxt2" runat="server" name="ucHiddenTxt01" Style="visibility: hidden"></asp:TextBox><!-----------Hidden textBox------------>
								<asp:TextBox ID="suPrdctNameCntrlTxt2" runat="server" name="ucHiddenTxt01" Style="visibility: hidden"></asp:TextBox><!-----------Hidden textBox------------>
								<asp:Label ID="lblPopupIamNeedHelpName" runat="server" CssClass="label" Text="Name" meta:resourcekey="lblPopupIamNeedHelpName"></asp:Label>
								<asp:TextBox onkeypress="return isspecialchara(event)" ID="txtnamehelp" runat="server" class="keyboard form-control" ToolTip="Enter Full Name" Style=" width: 329px; font-size: 25px;" TabIndex="1"></asp:TextBox>
								<asp:Label ID="alertNamehelp" runat="server" Text="Please enter your name" Style="color: white; display: none; width: 100%; " meta:resourcekey="nameAlert"></asp:Label>
								<asp:Label ID="Label7" runat="server" ForeColor="Red"
									Text="*Required Field" Visible="False"></asp:Label>
								<br />

								<asp:Label ID="lblPopupIamNeedHelpMobile" runat="server" CssClass="label" Text="Mobile" meta:resourcekey="lblPopupIamNeedHelpMobile"></asp:Label>
								<%--				<asp:TextBox ID="txtmobilehelp" runat="server"
						ToolTip="Enter Numbers Only" class="keyboard form-control keyboard-numpad"></asp:TextBox>--%>
								<div style="display: inline-flex;  direction: ltr;">
									
									<div id="ineedhelpleft" runat="server">
										<asp:TextBox ID="txtmobilehelpCd" Style="width: 100px; font-size: 25px; display: block;" runat="server" TabIndex="2" class="keyboard form-control" MaxLength="5" onkeypress="return isNumber(event)"></asp:TextBox>
										<asp:Label ID="lblMobileCode2" runat="server" Text="eg: 009xx" meta:resourcekey="lblMobileCode2"></asp:Label>
										<asp:Label ID="mobCDLimitAlertLbl2" runat="server" Text="Minimum 5" Style="color: white; font-size: 13px; display: none;" meta:resourcekey="mobCDLimitAlertLbl"></asp:Label>
									</div>
									<div style="width: 2%">
									</div>
									<div id="ineedhelpright" runat="server">
										<asp:TextBox ID="txtmobilehelp" Style="width: 100%; font-size: 25px;" runat="server" TabIndex="3" ToolTip="Enter Numbers Only" class="keyboard form-control"  MaxLength="15" onkeypress="return isNumber(event)"></asp:TextBox>
										<asp:Label ID="lblMobile2" runat="server" Text="eg: 07xxxxxxxx" Style="margin: 14px;" meta:resourcekey="lblMobile2"></asp:Label>
                                        <asp:Label ID="mobAlertempty2" runat="server" Text="Enter a number" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobAlertempty"></asp:Label>
										<asp:Label ID="mobLimitAlertLbl2" runat="server" Text="Minimum 10" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobLimitAlertLbl"></asp:Label>
                                        <asp:Label ID="mobStart07AlertLbl2" runat="server" Text="Minimum 10" Style="color: white; display: none; margin-left: 15px;" meta:resourcekey="mobStart07AlertLbl"></asp:Label>
									</div>
									<%--<div style="margin-top: 2%; margin-left: 12px;">
										<asp:Label ID="mobAlertLbl2" runat="server" Text="Please enter Numbers" Style="color: white; display: none;" meta:resourcekey="mobAlertLbl"></asp:Label>
									</div>--%>
								</div>
								<asp:Label ID="Label9" runat="server" ForeColor="Red"
									Text="*Required Field" Visible="False"></asp:Label>
								<br />
								<div class="actionbutton" style="width: 62%; ">
									<asp:Button ID="Button5" runat="server" Text="Submit" CssClass="subBtn"
										ToolTip="Submit" OnClick="SubmitButtonhelp_Click" TabIndex="4" meta:resourcekey="Button5" />
									<asp:Button ID="Button6" runat="server" Text="Cancel" CssClass="cancelBtn"
										ToolTip="Cancel" OnClick="CancelButtonhelp_Click" TabIndex="5" meta:resourcekey="Button6" />
								</div>
							</div>



						</div>
						<%--<footer class="w3-container w3-teal">
        <p>Modal Footer</p>
      </footer>--%>
					</div>
				</div>

				<div id="id04" runat="server" class="w3-modal" style="z-index:5001">
					<div class="w3-modal-content w3-card-4">
						<header class="w3-container w3-blue">
							<%--	<span id="closeBtn3" runat="server" onclick="document.getElementById('id04').style.display='none'"
								class="w3-button w3-large w3-display-topright">&times;</span>--%>
							<%--<h3>
								<asp:Label ID="lblPopupAlert" runat="server" Text="Alert" meta:resourcekey="lblPopupAlert"></asp:Label></h3>--%>
						</header>
						<div style="border: none; overflow-x: hidden; height: 300px; width: 100%; text-align: left;">
							<br />
							<asp:Label ID="lblAlertThankYou" runat="server" CssClass="labelclose" Text="Thank you" Style="text-align: center; margin-top: 60px; width: 100%;" meta:resourcekey="lblAlertThankYou"></asp:Label>
							<br />
							<br />

							<div style="width: 100%">

								<asp:Button ID="Button1" runat="server" Text="Close" CssClass="subBtn"
									ToolTip="Ok" OnClick="SuccessButton_Click" Style="margin-left: 299px; width: 15%;" meta:resourcekey="Button1" />

							</div>


						</div>
					</div>
				</div>

				<div id="id05" runat="server" class="w3-modal" style="z-index:5001">
					<div class="w3-modal-content w3-card-4">
						<header class="w3-container w3-blue">
							<%--		<span id="closeBtn4" runat="server" onclick="document.getElementById('id05').style.display='none'"
								class="w3-button w3-large w3-display-topright">&times;</span>--%>
							

								<div class="row">
				<div class="lefthead">
					<h3>
								<asp:Label ID="lblPopupLoanCalculator" runat="server" Text="Loan Calculator" meta:resourcekey="lblPopupLoanCalculator"></asp:Label>
							</h3>
					</div>
					<div class="righthead" dir="rtl" id="radcontainer" runat="server">
						<h3>
                            <asp:RadioButtonList CssClass="radioButtonList" ID="RadioButtonListcurrency" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonListcurrency_SelectedIndexChanged">
                                <asp:ListItem Selected="True">JOD</asp:ListItem>
                                <asp:ListItem>USD</asp:ListItem>
                            </asp:RadioButtonList>
					</h3></div>
									</div>

							
						</header>
						<div class="w3-container">

							<asp:Label ID="Label10" runat="server" ForeColor="Red"
								Text="*Required Field" Visible="False"></asp:Label>
							<asp:Label ID="Label11" runat="server" ForeColor="Red"
								Text="*Required Field" Visible="False"></asp:Label>
							<br />

							<div style="border: none; overflow-x: hidden; width: 654px; height: 400px; padding-left: 20%; padding-right: 20%">


								<asp:HiddenField ID="loanAmountHiddenField" runat="server" />
								<asp:HiddenField ID="interestRateHiddenField" runat="server" />
								<asp:HiddenField ID="numberOfPaymentsHiddenField" runat="server" />
								<asp:HiddenField ID="loanPaymentAmountHiddenField" runat="server" />
								<div id="autoLoanRdBtn" runat="server" style="margin-bottom: 5%;">


									<asp:Label ID="New" runat="server" Text="New" Style="color: white; font-size: 21px;" meta:resourcekey="rdBtnNew"></asp:Label>

                                   <%-- <label class="containercheck">--%>
  	<asp:RadioButton ID="loanNewRdBtn" AutoPostBack="true" runat="server" GroupName="autoLoanRDBtn" Checked="true" OnCheckedChanged="loanradiobutton_Click" />
                                      <%--   <span class="checkmark"></span>--%>
                        <%--</label>--%>
								

									<asp:Label ID="Used" runat="server" Text="Used" Style="color: white; margin-left: 3%; font-size: 21px;" meta:resourcekey="rdBtnUsed"></asp:Label>
                                    <%--  <label class="containercheck">--%>
									<asp:RadioButton ID="loanNewUsedBtn" runat="server" GroupName="autoLoanRDBtn" OnCheckedChanged="loanradiobutton_Click" AutoPostBack="true" />
                                          <%-- <span class="checkmark"></span>--%>
                        <%--</label>--%>
								</div>
								<div>
									<asp:Label ID="lblPopupLoanCalculatorAmount" runat="server" Text="Loan Amount:" CssClass="labelcalc" meta:resourcekey="lblPopupLoanCalculatorAmount"></asp:Label>
									<asp:Label ID="lblPoppLoanCalcuAmntLimit" runat="server" Text="(From 100 to 5000)" Style="color: white"></asp:Label>
									<asp:TextBox ID="loanAmountTextBox" runat="server" ToolTip="Enter Numbers Only" CssClass="keyboard form-control" onkeypress="return isNumber(event)" Style="font-size: 25px;"></asp:TextBox>
									<asp:Label ID="calcuNumberOnlyAlert" runat="server" Text="Please enter numbers" Style="color: white; display: none;" meta:resourcekey="mobAlertLbl"></asp:Label>
									<asp:Label ID="calcuEmptyFieldAlert" runat="server" Text="Please enter a amount" Style="color: white; display: none;" meta:resourcekey="calcuEmptyFieldAlert"></asp:Label>
									<asp:Label ID="lblPoppLCalcuLimitAlert" runat="server" Text="Amount should be from JOD 100 to JOD 1000" Style="color: white; display: none;" meta:resourcekey="lblPoppLCalcuLimitAlert"></asp:Label>
									<asp:Label ID="Label12" runat="server" ForeColor="Red"
										Text="*Required Field (Only Numbers)" Visible="False"></asp:Label>
								</div>
								<br />


								<asp:Label ID="lblPleaseslide" runat="server" Text="Please slide to select the number of months:" CssClass="labelcalc" Style="color: white; font-size: 21px" meta:resourcekey="lblPleaseslide"></asp:Label>
								<div class="slidecontainer" style="display: flex">
									<asp:Label ID="lblLoanPoppMinVal" runat="server" Text="min:6" Style="color: white; margin-top: 12.5px; margin-left: 2px; margin-right: 2px"></asp:Label>
									<input id="loanMonthSlider" runat="server" type="range" min="1" max="100" value="50" />
									<asp:Label ID="lblLoanPoppMaxVal" runat="server" Text="max:96" Style="color: white; margin-top: 12.5px; margin-left: 2px; margin-right: 2px"></asp:Label>
								</div>

								<div>
									<asp:Label ID="lblPopupLoanCalculatorMonth" runat="server" Text="Number Of Months:" CssClass="labelcalc" meta:resourcekey="lblPopupLoanCalculatorMonth"></asp:Label>
									<asp:Label ID="sliderOuput" runat="server" value="1" Style="font-size: x-large; color: white;"></asp:Label>
									<asp:TextBox ID="sliderOuputTxtBx" runat="server" value="50" Style="display: none"></asp:TextBox>
								</div>

								<asp:Label ID="numberOfPaymentsRequiredLabel" runat="server" ForeColor="Red"
									Text="*Required Field (Only Numbers)" Visible="False"></asp:Label>

								<div class="actionbutton">

									<asp:Button ID="calculateButton" runat="server" Text="Submit" CssClass="subBtn"
										ToolTip="Submit" Style="margin-left: 2px; width: 98px;"  meta:resourcekey="calculateButton" />
									<asp:Button ID="cancelbutton" runat="server" Text="Cancel" CssClass="cancelBtn"
										ToolTip="Cancel" OnClick="loancancelButton_Click" Style="width: 98px;" meta:resourcekey="cancelbutton" />
									<asp:Button ID="clearbutton" runat="server" Text="Clear" CssClass="cancelBtn"
										ToolTip="Clear" OnClick="loanclearButton_Click" Style="width: 98px;" meta:resourcekey="clearbutton" />
									<br />
									<br />

								</div>
								<asp:Label ID="loanPaymentAmountLabel" runat="server" Text="..."
									Visible="False" CssClass="label"></asp:Label>
								<asp:Label ID="lbldisclaimer" runat="server" Text="" meta:resourcekey="lbldisclaimer" Font-Names="myriad pro" Font-Size="Small" ForeColor="White" ></asp:Label>

							</div>

						</div>
					</div>
				</div>

				<div id="id06" runat="server" class="w3-modal" style="z-index:5001">
					<div class="w3-modal-content w3-card-4">
						<header id="closeBtn5" runat="server" class="w3-container w3-blue">
							<%--<span onclick="document.getElementById('id06').style.display='none'"
								class="w3-button w3-large w3-display-topright">&times;</span>--%>
							<h3>
								<asp:Label ID="lblDepositCalcu" runat="server" Text="Deposit Calculator" meta:resourcekey="lblDepositCalcu"></asp:Label>
							</h3>
						</header>
						<div class="w3-container">
							<%--					<asp:TextBox ID="prdctNameCntrlTxt3" runat="server" name="ucHiddenTxt00" style="visibility:hidden"></asp:TextBox><!-----------Hidden textBox------------>
					<asp:TextBox ID="suPrdctNameCntrlTxt3" runat="server" name="ucHiddenTxt01" style="visibility:hidden"></asp:TextBox><!-----------Hidden textBox------------>--%>
							<asp:Label ID="depositLabel10" runat="server" ForeColor="Red"
								Text="*Required Field" Visible="False"></asp:Label>
							<asp:Label ID="depositLabel11" runat="server" ForeColor="Red"
								Text="*Required Field" Visible="False"></asp:Label>
							<br />
							<%-- <iframe src="Ineedhelp.aspx"  style="border:none; overflow-x: hidden;" height="400" width="870"></iframe>--%>
							<div style="border: none; overflow-x: hidden; height: 400px; width: 754px; padding-left: 25%; padding-right: 25%">
								<%--<asp:Label ID="webLoanCalculatorLabel" runat="server"
                    Text="I will think"></asp:Label>--%>

								<asp:HiddenField ID="depositAmountHiddenField" runat="server" />
								<asp:HiddenField ID="depositInterestRateHiddenField" runat="server" />
								<asp:HiddenField ID="depositNumberOfPaymentsHiddenField" runat="server" />
								<asp:HiddenField ID="depositPaymentAmountHiddenField" runat="server" />

								<div style="display: inline-block;">
									<asp:Label ID="lblDepositCalcuAmount" runat="server" Text="Deposit Amount:" CssClass="labelcalc" meta:resourcekey="lblDepositCalcuAmount"></asp:Label><br />
                                    <asp:Label ID="lblDeposithint" runat="server" Text="(From 100 to 5000)" Style="color: white"></asp:Label>
									<asp:TextBox ID="depositAmountTextBox" runat="server" ToolTip="Enter Numbers Only" onkeypress="return isNumber(event)" CssClass="keyboard form-control" Style="font-size: 25px;"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblDepositamountvalidation" runat="server" Text="(From 5000 to 100,000,000)" Style="color: white"  meta:resourcekey="lblDepositamountvalidation"></asp:Label>
								</div>
								<asp:Label ID="depositCalcuNumbrAlert" runat="server" Text="Please enter numbers" Style="color: white; display: none;" meta:resourcekey="mobAlertLbl"></asp:Label>
								<asp:Label ID="dpstCalcuEmptyFieldAlert" runat="server" Text="Please enter a amount" Style="color: white; display: none;" meta:resourcekey="calcuEmptyFieldAlert"></asp:Label>

								<asp:Label ID="depositLabel12" runat="server" ForeColor="Red"
									Text="*Required Field (Only Numbers)" Visible="False"></asp:Label>

								<br />
								<%-- <asp:Label ID="Label11" runat="server" Text="Interest Rate:" CssClass="label"></asp:Label>
            <asp:TextBox ID="interestRateTextBox" runat="server"
                    ToolTip="Enter Numbers Only" CssClass="txtstyle"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" ForeColor="Red"
                    Text="*Required Field (Only Numbers)" Visible="False"></asp:Label>--%>

								<asp:Label ID="lblDepositCalcuMonths" runat="server" Text="Number Of Months:" CssClass="labelcalc" meta:resourcekey="lblDepositCalcuMonths"></asp:Label>
								<%--<asp:TextBox ID="numberOfPaymentsTextBox" runat="server"
                    ToolTip="Enter Numbers Only" CssClass="txtstyle"></asp:TextBox>--%>
								<asp:DropDownList ID="depositDropDownList1" runat="server" CssClass="dropcalc" Style="border: 1px solid #ccc; border-radius: 4px; width: 320px;">
									<asp:ListItem>1</asp:ListItem>
									<asp:ListItem>3</asp:ListItem>
									<asp:ListItem>6</asp:ListItem>
									<asp:ListItem>12</asp:ListItem>
								</asp:DropDownList>

								<asp:Label ID="depositNumberOfPaymentsRequiredLabel" runat="server" ForeColor="Red"
									Text="*Required Field (Only Numbers)" Visible="False"></asp:Label>


								<br />
								<br />
								<div class="actionbutton">

									<asp:Button ID="depositCalculateButton" runat="server" Text="Submit" CssClass="subBtn"
										ToolTip="Submit" Style="margin-left: 2px;width: 98px;"  meta:resourcekey="depositCalculateButton" />
									<asp:Button ID="depositCancelbutton" runat="server" Text="Cancel" CssClass="cancelBtn"
										ToolTip="Cancel" OnClick="loancancelButton_Click" Style="width: 98px;" meta:resourcekey="depositCancelbutton" />
									<asp:Button ID="depositClearbutton" runat="server" Text="Clear" CssClass="cancelBtn"
										ToolTip="Clear" OnClick="loanclearButton_Click" Style="width: 98px;" meta:resourcekey="depositClearbutton" />
									<br />
									<br />

								</div>
								<asp:Label ID="depositPaymentAmountLabel" Style="width: 100%;" runat="server" Text="..."
									Visible="False" CssClass="label"></asp:Label>
								<asp:Label ID="Label1" runat="server" Text="" meta:resourcekey="lbldisclaimer" Font-Names="myriad pro" Font-Size="Small" ForeColor="White" ></asp:Label>

							</div>



						</div>
						<%--<footer class="w3-container w3-teal">
        <p>Modal Footer</p>
      </footer>--%>
					</div>
				</div>

			</div>
		</div>
             <asp:Button runat="server" ID="btnSession" Text="" style="display:none;" OnClick="btnSession_Click" />    
            </div>
	</form>
	
    <script src="lib/js/jquery.min.js"></script>
	<script src="lib/js/bootstrap.min.js"></script>
	<script src="../lib/js/jqbtk.js"></script>
	<script src="../Script/ABDS/productDetailTriFold.js"></script>

</body>
</html>

<script type="text/javascript">
    function preventMultipleSubmissions() {
        $('#<%=SubmitButton.ClientID %>').prop('disabled', true);
        $('#<%=Button3.ClientID %>').prop('disabled', true);
        $('#<%=Button5.ClientID %>').prop('disabled', true);
        $('#<%=calculateButton.ClientID %>').prop('disabled', true);
        $('#<%=depositCalculateButton.ClientID %>').prop('disabled', true);
    }
    window.onbeforeunload = preventMultipleSubmissions;
</script>


<script type="text/javascript">
                                            $(document).ready(function () {
                                               
                                               $('#buttondisable').delay(1000).fadeIn(400);
												//Get
												var pName = $('#productName').val();
												var subProductName = $('#subProductName').val();
												//Set
												$('#prdctNameCntrlTxt').val(pName);
												$('#suPrdctNameCntrlTxt').val(subProductName);

												$('#prdctNameCntrlTxt1').val(pName);
												$('#suPrdctNameCntrlTxt1').val(subProductName);

												$('#prdctNameCntrlTxt2').val(pName);
												$('#suPrdctNameCntrlTxt2').val(subProductName);

												$('#prdctNameCntrlTxt3').val(pName);
                                                $('#suPrdctNameCntrlTxt3').val(subProductName);

                                                var swf = $('#swfNAme').val();

                                                var printtrifold = "<object align='middle' id='FlipBookBuilder' data='" + swf + "' width = '100%' height='100%' type='application/x-shockwave-flash'>" +
                                                    "<param name='quality' value='high' />" +
                                                    "<param name='bgcolor' value='#ffffff'>" +
                                                    "<param name='allowfullscreen' value='true'>" +
                                                    "<param name='allowFullScreenInteractive' value='true'>" +
                                                    "<param name='wmode' value='transparent'>" +
                                                    "</object>";
                                                $(".trifoldstyles").append(printtrifold);
											});
                            </script>
                            <script>

											////////////////////////NumberValidator///////////////////////////////
											function isNumber(evt) {

												evt = (evt) ? evt : window.event;
												var charCode = (evt.which) ? evt.which : evt.keyCode;
												if (charCode > 31 && (charCode < 48 || charCode > 57)) {
													document.getElementById("mobLimitAlertLbl").style.display = 'none';
													document.getElementById("calcuNumberOnlyAlert").style.display = 'block';
													document.getElementById("depositCalcuNumbrAlert").style.display = 'block';
													document.getElementById("calcuEmptyFieldAlert").style.display = 'none';
													document.getElementById("dpstCalcuEmptyFieldAlert").style.display = 'none';
													//document.getElementById("mobAlertLbl").style.display = 'block';
													//document.getElementById("mobAlertLbl1").style.display = 'block';
													//document.getElementById("mobAlertLbl2").style.display = 'block';

													return false;
												}
												else {
													document.getElementById("mobAlertLbl").style.display = 'none';
													document.getElementById("mobAlertLbl1").style.display = 'none';
													document.getElementById("mobAlertLbl2").style.display = 'none';

												}
												return true;
											}

											///////////////////////May be later///////////////////////////////////
											// checking on keyPress>> mobile field should contain minimum 10 digits
											$("#txtMobile").keyup(function () {

												var countMob = $("#txtMobile").val().length;
                                                if (countMob > 6 && countMob < 16) {
													document.getElementById("mobLimitAlertLbl").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}


											});
											// checking on keyPress>> mobileCod field should contain minimum 5 digits
											$("#txtMobileCd").keyup(function () {

												var countMobCD = $("#txtMobileCd").val().length;
												if (countMobCD == 5) {
													document.getElementById("mobCDLimitAlertLbl").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}


											});
											$("#txtName").keyup(function () {//checking NAME textbox, whether it is filled with text or not

												var countMobCD = $("#txtName").val().length;
												if (countMobCD > 0) {
													document.getElementById("nameAlert").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}

												

											});

											function isspecialchara(evt) {
												//var key = (evt.which) ? evt.which : event.keyCode
												//if (!((key > 64 && key <= 90) || (key > 96 && key <= 122) || (key == 32) || (key > 47 && key <= 57) || (key == 8) || (key == 0) || (key == 127)))
												//	return false;
												//return true;
												var inputtxt = evt.key;
												var letters = /^[0-9a-zA-Z]+$/;
												var abLetters = /[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]/

												if (inputtxt.match(letters) || inputtxt.match(abLetters)) {
													return true;
												}
												else {
													return false;
												}
											}

											
											///////////////////////Iam Interested///////////////////////////////////
											// checking on keyPress>> mobile field should contain minimum 10 digits
											$("#txtmobileinterested").keyup(function () {

												var countMob = $("#txtmobileinterested").val().length;
                                                if (countMob > 6 && countMob < 16) {
													document.getElementById("mobLimitAlertLbl1").style.display = 'none';
													$('#Button3').removeAttr('disabled', 'disabled');
													return false;
												}


											});
											// checking on keyPress>> mobileCod field should contain minimum 5 digits
											$("#txtmobileinterestedCd").keyup(function () {

												var countMobCD = $("#txtmobileinterestedCd").val().length;
												if (countMobCD == 5) {
													document.getElementById("mobCDLimitAlertLbl1").style.display = 'none';
													$('#Button3').removeAttr('disabled', 'disabled');
													return false;
												}
											});
											$("#txtnameInterested").keyup(function () {//checking NAME textbox, whether it is filled with text or not

												var countMobCD = $("#txtnameInterested").val().length;
												if (countMobCD > 0) {
													document.getElementById("alertNameInterested").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}
											});

											///////////////////////I need help///////////////////////////////////
											// checking on keyPress>> mobile field should contain minimum 10 digits
											$("#txtmobilehelp").keyup(function () {

												var countMob = $("#txtmobilehelp").val().length;
                                                if (countMob > 6 && countMob < 16) {
													document.getElementById("mobLimitAlertLbl2").style.display = 'none';
													$('#Button5').removeAttr('disabled', 'disabled');
													return false;
												}
											});
											// checking on keyPress>> mobileCod field should contain minimum 5 digits
											$("#txtmobilehelpCd").keyup(function () {

												var countMobCD = $("#txtmobilehelpCd").val().length;
												if (countMobCD == 5) {
													document.getElementById("mobCDLimitAlertLbl2").style.display = 'none';
													$('#Button5').removeAttr('disabled', 'disabled');
													return false;
												}
											});
											$("#txtnamehelp").keyup(function () {//checking NAME textbox, whether it is filled with text or not

												var countMobCD = $("#txtnamehelp").val().length;
												if (countMobCD > 0) {
													document.getElementById("alertNamehelp").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}
											});

											///////////////////////loan Calculator///////////////////////////////////
											$("#loanAmountTextBox").keyup(function () {//checking NAME textbox, whether it is filled with text or not

												var countLoanTxtBx = $("#loanAmountTextBox").val().length;
												if (countLoanTxtBx > 0) {
													document.getElementById("calcuNumberOnlyAlert").style.display = 'none';
													document.getElementById("calcuEmptyFieldAlert").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}
											});
											///////////////////////Deposit Calculator///////////////////////////////////
											$("#depositAmountTextBox").keyup(function () {//checking NAME textbox, whether it is filled with text or not

												var countLoanTxtBx = $("#depositAmountTextBox").val().length;
												if (countLoanTxtBx > 0) {
													document.getElementById("depositCalcuNumbrAlert").style.display = 'none';
													document.getElementById("dpstCalcuEmptyFieldAlert").style.display = 'none';
													$('#SubmitButton').removeAttr('disabled', 'disabled');
													return false;
												}
											});

											///////////////////////////////Slider//////////////////////////////////////////
											var slider = document.getElementById("loanMonthSlider");
											var output = document.getElementById("sliderOuput");
											var output1 = document.getElementById("sliderOuputTxtBx");
											output.innerHTML = slider.value; // Display the default slider value

											// Update the current slider value (each time you drag the slider handle)
											slider.onchange = function () {
												output.innerHTML = this.value;
												output1.value = this.value;

											}
											////////////////////checkingamount textbox filled or not////////////////////////
											$("#loanAmountTextBox").keyup(function () {

												var amountTxt = $("#loanAmountTextBox").val().length;
												if (amountTxt > 0) {
													document.getElementById("lblPoppLCalcuLimitAlert").style.display = 'none';
												}
											});

											
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
                                                    document.getElementById("btnSession").click();
													//request.open("POST", "../LogOut.aspx", false);
													//request.send();
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
                            <script type="text/javascript" >
											function UpdateAjax(time) {
												time;
                                            }

                            </script>

