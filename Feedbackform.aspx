<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedbackform.aspx.cs" Inherits="Feedbackform" Async="true"%>

<!DOCTYPE html>
<%--<% 
    Response.Buffer = false;
    Response.CacheControl = "private";
%> --%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Feedback Form</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge;chrome=1" />
	<link rel="stylesheet" href="lib/css/modalpopup.css" />
	<link href="Content/ABDS/feedBackFormBootstrap.css" rel="stylesheet" />
	<link rel="stylesheet" href="lib/css/jqbtk.css" />
	<link href="Content/ABDS/feedBackForm.css" rel="stylesheet" />
</head>
<body class="disable-selection">
	<script src="lib/js/jquery-1.10.1.min.js"></script>
    <form id="form1" runat="server">
		<div id="container">
			<div class="row">
				<div runat="server" dir='<%$ Resources: Resource,TextDirection%>' style="width: 92%">
					<div class="productlistright" style="margin-left: 3%; margin-top: 1%;">

						<div class="column" style="padding: 0; width: 100%;">
							<div class="alltext">
								<asp:Label ID="lblPageNameFeedBack" runat="server" Text="FeedBack" meta:resourcekey="lblPageNameFeedBack"></asp:Label>
							</div>

							<div style="color: white; font-size: 15pt; font-family: 'Myriad Pro'; padding: 20px;">

								<div style="border: none; width: 826px;">

									<table style="width: 100%;">
										<tr runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
											<td style="width: 27%">
												<asp:Label ID="lblFeedBackType" runat="server" Text="Feedback Type" meta:resourcekey="lblFeedBackType" ToolTip="numberOnly"></asp:Label></td>
											<td>
												<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" Style="">

													<asp:ListItem Value="1" Text="<%$ Resources:Resource, rdoBtnComplaint%>"></asp:ListItem>
													<asp:ListItem Value="2" Text="<%$ Resources:Resource, rdoBtnExperienceRating%>"></asp:ListItem>
													<asp:ListItem Value="3" Text="<%$ Resources:Resource, rdoBtnSuggestionAndFeedback%>"></asp:ListItem>
													<%--<asp:ListItem>Suggestion &amp; Feedback</asp:ListItem>--%>
													<%--<asp:ListItem  >Suggestion Feedback</asp:ListItem>--%>
												</asp:RadioButtonList>
												<asp:Label ID="lblRadioBtnAlert" runat="server" Text="Please select Feedback Type" Style="color: white; display: none;" meta:resourcekey="lblRadioBtnAlert"></asp:Label>
											</td>

											<%--<td>
												&nbsp;</td>--%>
										</tr>
										<tr runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
											<td>
												<asp:Label ID="lblName" runat="server" Text="Name" meta:resourcekey="lblName"></asp:Label></td>
											<td  colspan="2">
												<div style="width: 172%; display: flex;">
													<asp:TextBox onkeypress="return isspecialchara(event)" ID="txtnamehelp" runat="server" class="keyboard form-control" ToolTip="Enter Full Name" Style="width: 57.5%; font-size: 18px; margin-bottom: 4px; margin-left: 0%;"></asp:TextBox>
												</div>
                                                
												<asp:Label ID="fbNameEmptyFeildAlert" runat="server" Text="Please enter name" Style="color: white; display: none; margin-left: 1%; margin-right: 1%;" meta:resourcekey="fbNameEmptyFeildAlert"></asp:Label>
											</td>
											<td>&nbsp;</td>
										</tr>

										<tr runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
											<td>
												<asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number" meta:resourcekey="lblMobileNumber"></asp:Label></td>
											<td>
												<div style="width: 100%; display: flex; direction: ltr;">
													<div style="width: 100%; display: flex;">
														<asp:TextBox ID="txtmobileCDForm" runat="server"
															ToolTip="Enter Numbers Only" class="keyboard form-control keyboard-numpad" Style="font-size: 19px; margin-bottom: 4px;" placeholder="009xx" MaxLength="5" onkeypress="return isNumber(event)"></asp:TextBox>
														<asp:TextBox ID="txtmobilehelp" runat="server"
															ToolTip="Enter Numbers Only" class="keyboard form-control keyboard-numpad" Style="width: 98%; font-size: 18px; margin-bottom: 4px; margin-left: 0.5%;" placeholder="05xxxxxxxx" MaxLength="15" onkeypress="return isNumber(event)"></asp:TextBox>
													</div>
												</div>
                                                <div style="width: 100%; display: flex; direction: ltr;"">
                                                    <div runat="server" id="quick1" style="width:30%; display: flex;">
                                                        <asp:Label ID="fbMobCodeEmptyFeildAlert" runat="server" Text="Required" Style="color: white; display: none; margin-left: 0.5%; margin-right: 0.5%;" meta:resourcekey="fbMobCodeEmptyFeildAlert"></asp:Label>
                                                    </div>
                                                    <div runat="server" id="quick2" style="width:70%; display: flex;">
                                                        <asp:Label ID="fbMobEmptyFeildAlert" runat="server" Text="Please enter mobile number" Style="color: white; display: none; margin-left: 0.5%; margin-right: 0.5%;" meta:resourcekey="fbMobEmptyFeildAlert"></asp:Label>
                                                        <asp:Label ID="fbMobLimitAlert" runat="server" Text="Minimum 10" Style="color: white; display: none; margin-left: 0.5%; margin-right: 0.5%;"></asp:Label>
                                                    </div>
                                                </div>

                                                
                                                
												
                                                <asp:Label ID="mobAlertLbl2" runat="server" Text="Enter only Numbers." Style="color: white; display: none;" meta:resourcekey="mobAlertLbl2"></asp:Label>
											</td>

											<%--<td>&nbsp;</td>--%>
										</tr>
										<tr runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
											<td>
												<asp:Label ID="lblFeedBack" runat="server" Text="Feedback" meta:resourcekey="lblFeedBack"></asp:Label></td>
											<td colspan="2">
												<asp:TextBox ID="txtfeedback" runat="server"
													class="keyboard form-control keyboard-numpad" Height="70px" TextMode="MultiLine" Width="99%"></asp:TextBox>
												<asp:Label ID="lblfeedbackvalid" runat="server" Text="Feedback is Required" Style="color: white; display: none;" meta:resourcekey="lblPlsComment"></asp:Label>
											</td>
											<td>&nbsp;</td>
										</tr>

										<tr runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
											<td>
												<span>
													<asp:Label ID="lblChooseYourEmoji" runat="server" Text="Choose Your Emoji" meta:resourcekey="lblChooseYourEmoji"></asp:Label>
												</span>
											</td>

											<td style="display: -ms-flexbox; padding-top: 3%;">

												<table>
  <tr>
    <th><asp:Label ID="Label1" runat="server" Text="Poor" Style="color: lightgoldenrodyellow; padding-top: 15px; font-size: medium;" meta:resourcekey="lblPoor"></asp:Label></th>
    <th><div style="padding-bottom: 15px; padding-left: 5px;" id="element"></div></th>
    <th><asp:Label ID="Label2" runat="server" Text="Excellent" Style="color: lightgoldenrodyellow; padding-top: 15px; font-size: medium;" meta:resourcekey="lblExcellent"></asp:Label></th>
  </tr>
</table>


												
												
												
												<asp:Label ID="Label5" runat="server" Text="Please rate our services" Style="color: white; display: none"></asp:Label>
												<asp:TextBox ID="emojiText" runat="server" Style="visibility: hidden;"></asp:TextBox>

												<asp:Label ID="lblPlsRateUs" runat="server" Text="Please, Rate Us!" Style="color: white; display: none;" meta:resourcekey="lblPlsRateUs"></asp:Label>

											</td>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr runat="server" dir='<%$ Resources: Resource,TextDirection%>'>
											<td>&nbsp;</td>
											<td>
												<div>
													<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="subBtn"
														ToolTip="Submit" OnClick="SubmitButtonForm_Click" BackColor="#0066CC" Height="38px" Width="113px" meta:resourcekey="btnSubmit" />
													<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="cancelBtn"
														ToolTip="Clear" OnClick="ClearButtonForm_Click" BackColor="White" ForeColor="Black" Height="38px" Width="113px" meta:resourcekey="clearbutton" />
												</div>
												<%--<div>
													<asp:Button ID="btnIneedhelp" class="myButton" runat="server" Text="I need help" meta:resourcekey="btnIneedhelp" OnClick="btnIneedhelp_Click" Style="visibility: visible; margin-top: 3%; width: 39%" />
												</div>--%>
											</td>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
									</table>

									<%--<asp:Label ID="webLoanCalculatorLabel" runat="server"
                    Text="I will think"></asp:Label>--%>
								</div>

							</div>
						</div>
						<%--						<div class="column" style="width: 29%;">
						</div>--%>
					</div>
				</div>
			</div>
		</div>

		<div class="w3-container" runat="server" dir='<%$ Resources: Resource,TextDirection%>'>


			<div id="id04" runat="server" class="w3-modal w3-animate-opacity">
				<div class="w3-modal-content w3-card-4">
					<header class="w3-container w3-blue">
						
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
		</div>

	</form>
	<script src="lib/js/jquery.min.js"></script>
	<script src="lib/js/bootstrap.min.js"></script>
	<script type="text/javascript" src="js/emotion-rating.min.js"></script>

    <script type="text/javascript">
        function preventMultipleSubmissions() {
            $('#<%=btnSubmit.ClientID %>').prop('disabled', true);
        }
        window.onbeforeunload = preventMultipleSubmissions;
</script>


	<script type="text/javascript">

        var emotionsArray = ['angry', 'disappointed', 'meh', 'smile', 'inLove'];
		$("#element").emotionsRating({
			emotionSize: 50,
			bgEmotion: 'happy',
			emotions: emotionsArray,
			color: 'gold'
		});

		$('.emotion-style').click(function () {

			var emojiVal = $(this).attr("data-index");
			$("#emojiText").val(emojiVal);
		});


	</script>
	<script src="lib/js/jqbtk.js"></script>
	<script>
		//////////////////////////////Number validation////////////////////////
		function isNumber(evt) {

			evt = (evt) ? evt : window.event;
			var charCode = (evt.which) ? evt.which : evt.keyCode;
			if (charCode > 31 && (charCode < 48 || charCode > 57)) {
				document.getElementById("mobAlertLbl2").style.display = 'block';
				return false;
			}
			else {
				document.getElementById("mobAlertLbl2").style.display = 'none';
			}
			return true;
		}
		///////////////////////I need help///////////////////////////////////
		// checking on keyPress>> mobile field should contain minimum 10 digits
		$("#txtmobilehelpPP").keyup(function () {

			var countMob = $("#txtmobilehelpPP").val().length;
            if (countMob > 6 && countMob < 16) {
				document.getElementById("mobLimitAlertLbl2").style.display = 'none';
				$('#Button5').removeAttr('disabled', 'disabled');
				return false;
			}
		});

		$("#txtnamehelpPP").keyup(function () {//checking NAME textbox, whether it is filled with text or not

			var countMobCD = $("#txtnamehelpPP").val().length;
			if (countMobCD > 0) {
				document.getElementById("alertNamehelp").style.display = 'none';
				$('#Button5').removeAttr('disabled', 'disabled');
				return false;
			}
		});
		$("#txtmobilehelpCd").keyup(function () {

			var countMobCD = $("#txtmobilehelpCd").val().length;
			if (countMobCD == 5) {
				document.getElementById("mobCDLimitAlertLbl2").style.display = 'none';
				$('#Button5').removeAttr('disabled', 'disabled');
				return false;
			}
		});
		///////////////////////////////MainForm////////////////////////////////
		$("#txtnamehelp").keyup(function () {
			var countName = $("#txtnamehelp").val().length;
			if (countName > 0) {
				document.getElementById("fbNameEmptyFeildAlert").style.display = 'none';
			}
		});
		$("#txtmobileCDhelp").keyup(function () {
			var countMobcd = $("#txtmobileCDhelp").val().length;
			if (countMobcd == 5) {
				document.getElementById("fbMobEmptyFeildAlert").style.display = 'none';
			}
		});
		$("#txtmobilehelp").keyup(function () {
			var countMob = $("#txtmobilehelp").val().length;
			if (countMob == 10) {
				document.getElementById("fbMobEmptyFeildAlert").style.display = 'none';
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

	</script>

</body>
</html>
