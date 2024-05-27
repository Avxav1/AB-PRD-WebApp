using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Net.Mime;
using System.Data;
using Arabbankdll;

public partial class Feedbackform : System.Web.UI.Page
{
	//private bool _isError = false;
	string lang;
	string country;
	string discoveryBench;
	string branchNumber;
	string branchName;
	string countryId;
	string branchId;
	//XMLClass xmlObj = new XMLClass();

    ConfigCls configObj = new ConfigCls();
    protected override void InitializeCulture()
	{
		string cultureName = Request.QueryString["lang"];
		if (!string.IsNullOrEmpty(cultureName))
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
        countryId = "1";
        branchId = "1";
        discoveryBench = "1";

        if (Request.QueryString["cntry"] != null && Request.QueryString["cntry"].ToString().Length < 50)
        {
            countryId = Server.HtmlEncode(Request.QueryString["cntry"]);
        }
        if (Request.QueryString["bNam"] != null && Request.QueryString["bNam"].ToString().Length < 50)
        {
            branchId = Server.HtmlEncode(Request.QueryString["bNam"]);
        }
        if (Request.QueryString["dBnch"] != null && Request.QueryString["dBnch"].ToString().Length < 50)
        {
            discoveryBench = Server.HtmlEncode(Request.QueryString["dBnch"]);
        }
       

        lang = BusinessObject.GetCurrLangCode();

        if (lang == "EN")
        {
            quick1.Attributes.Add("dir","ltr");
            quick2.Attributes.Add("dir", "ltr");
        }
        else
        {
            quick1.Attributes.Add("dir", "rtl");
            quick2.Attributes.Add("dir", "rtl");
        }

        /////////////////////getting values from db by Xml-Ids//////////////////
        DataSet results = new DataSet();
		if( BusinessObject.getByBranchId(branchId, out DataSet value)<0)
        {
            return;
        }
		results = value;
        DataRow row = results.Tables[0].Rows[0];
        country = Convert.ToString(row["CountryName"]);
		branchNumber = Convert.ToString(row["BranchNumber"]);
		branchName = Convert.ToString(row["BranchName"]);
        ///////////////////////////////////////////////////////////////////////////

        //if (country == "UAE" || country == "uae" || countryId == "2")
        //{
        //    txtmobilehelp.Attributes["placeholder"] = "5xxxxxxxx";
        //}
        //if (country == "EGYPT" || country == "egypt" || countryId == "5")
        //{
        //    txtmobilehelp.Attributes["placeholder"] = "01xxxxxxxx";
        //}

        //if (!IsPostBack)
        //{
        //    if (country == "jo" || country == "Jordan" || country == "JO" || country == "JOR")
        //    {
        //        txtmobileCDForm.Text = "00962";
        //    }
        //    else if (country == "UAE" || country == "uae" || countryId == "2")
        //    {
        //        txtmobileCDForm.Text = "00971";
        //        txtmobilehelp.Attributes["placeholder"] = "5xxxxxxxx";
        //    }
        //    else if (country == "EGYPT" || country == "egypt" || countryId == "5")
        //    {
        //        txtmobileCDForm.Text = "002";
        //        txtmobilehelp.Attributes["placeholder"] = "01xxxxxxxxx";
        //    }
        //    else if (country == "Palestine" || countryId == "3")
        //    {
        //        txtmobileCDForm.Text = "00970";
        //        txtmobilehelp.Attributes["placeholder"] = "5xxxxxxxxx";
        //    }
        //}
    }
	protected void SubmitButtonForm_Click(object sender, EventArgs e)
	{
        if (configObj.RetrieveBranch(Convert.ToInt64(branchId)) < 0)
        {
            return;
        }


        string toMail = "";

        if (RadioButtonList1.Text == "1")
        {
            toMail = configObj.ToMail_fb2.ToString();
        }
        else
        {
            toMail = configObj.ToMail_fb.ToString();
        }

        string frmMail = configObj.FromMail.ToString();
      


        /////////////////////////Generating Auto Ticket ID////////////////////////////////////////////
        generateAutoNumber generateAutoObj = new generateAutoNumber();
		string ticketId = generateAutoObj.generateAutoNumbers_FeedBack();

        string encryptedticketid = BusinessObject.EncodeString(ticketId);
        string encryptedformname = BusinessObject.EncodeString("ABFeedback");

        string linkWithId = "/TicketStatus.aspx?tId=" + encryptedticketid + "&form="+ encryptedformname;
		/////////////////////////////////////////////////////////////////////////////////////////////
		Feedbackclass feedbackobj = new Feedbackclass();
		string expRating = Resources.Resource.rdoBtnExperienceRating;

		if (RadioButtonList1.Text != "")
		{
			feedbackobj.FeedbackType = RadioButtonList1.SelectedItem.Text;
			lblRadioBtnAlert.Style.Add(HtmlTextWriterStyle.Display, "None");
			
		}
		else
		{
			lblRadioBtnAlert.Style.Add(HtmlTextWriterStyle.Display, "Block");
			return;
		}


		if (RadioButtonList1.Text == "2" && emojiText.Text == "")
		{
			lblPlsRateUs.Style.Add(HtmlTextWriterStyle.Display, "Block");
			return;
		}
		else
		{
			feedbackobj.FeedbackType = RadioButtonList1.SelectedItem.Text;
			feedbackobj.Emoji = emojiText.Text;
			lblPlsRateUs.Style.Add(HtmlTextWriterStyle.Display, "none");
			lblfeedbackvalid.Style.Add(HtmlTextWriterStyle.Display, "none");
			lblRadioBtnAlert.Style.Add(HtmlTextWriterStyle.Display, "None");
			fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            fbMobCodeEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        string Str = txtnamehelp.Text.Trim();
        double Num;
        bool isNum = double.TryParse(Str, out Num);

        if (RadioButtonList1.Text != "2")
        {
            if (txtnamehelp.Text != "")
            {
                fbNameEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
            else
            {
                fbNameEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            if (isNum)
            {
                fbNameEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
            {
                fbNameEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
  
            if (txtmobileCDForm.Text == "")
            {
                fbMobCodeEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
            if (txtmobilehelp.Text == "")
            {
                fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
            if (txtfeedback.Text == "")
            {
                lblfeedbackvalid.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
            else
            {
                lblfeedbackvalid.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }

        if (txtmobileCDForm.Text != "" && txtmobilehelp.Text != "")
        {
            if (countryId == "5")
            {
                if (txtmobileCDForm.Text.Count() >= 3)
                {
                    feedbackobj.MobileCode = Server.HtmlEncode(txtmobileCDForm.Text);
                }
                else
                {
                    fbMobCodeEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                    return;
                }
            }
            else
            {
                if (txtmobileCDForm.Text.Count() > 4)
                {
                    feedbackobj.MobileCode = Server.HtmlEncode(txtmobileCDForm.Text);
                }
                else
                {
                    fbMobCodeEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                    return;
                }
            }

            if (txtmobileCDForm.Text.ToString().Substring(0, 1) != "0" || txtmobileCDForm.Text.ToString().Substring(1, 1) != "0")
            {
                fbMobCodeEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
            
            if (txtmobilehelp.Text.Count() > 6 && txtmobilehelp.Text.Count() < 16)
            {
                feedbackobj.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
            }
            else
            {
                fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            if (txtmobileCDForm.Text == "00962")
            {
                if (txtmobilehelp.Text.ToString().Substring(0, 2) == "07")
                {
                    feedbackobj.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
                }
                else
                {
                    fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                    return;
                }
            }
            if (txtmobileCDForm.Text == "00970")
            {
                if (txtmobilehelp.Text.ToString().Substring(0, 1) == "5")
                {
                    feedbackobj.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
                }
                else
                {
                    fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
                    return;
                }
            }

            //if (txtmobilehelp.Text.ToString().Substring(0, 1) != "0" || txtmobilehelp.Text.ToString().Substring(1, 1) == "0")
            //{
            // fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
            //return;
            //}
            //fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
        

        feedbackobj.MobileCode = Server.HtmlEncode(txtmobileCDForm.Text);
		feedbackobj.Name = Server.HtmlEncode(txtnamehelp.Text);
		feedbackobj.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
		feedbackobj.TicketId = ticketId;
		feedbackobj.RequestType = "Feedback";
		feedbackobj.TicketStatus = "O";
		feedbackobj.LinkTicket = linkWithId;
		feedbackobj.Feedback = Server.HtmlEncode(txtfeedback.Text);
		feedbackobj.Country = country;
		feedbackobj.DiscoveryBench = discoveryBench;
		feedbackobj.BrachNumber = branchNumber;
		feedbackobj.BranchName = branchName.TrimEnd();
		feedbackobj.branchId = Convert.ToInt32(branchId);
		feedbackobj.CountryId = Convert.ToInt32(countryId);
		feedbackobj.Status = "Y";
		if (lang == "EN")
		{
			feedbackobj.BrowsingLanguage = "en";
		}
		else
		{
			feedbackobj.BrowsingLanguage = "ar";
		}

		//////////////////////MailSenting///////////////////////////
		try
		{
            string baseurl = "";
            string withhyperlink = "";
            if (BusinessObject.Getbaseurl(out baseurl) >= 0)
            {
                string emaillink = baseurl + linkWithId;
                withhyperlink = "<a clicktracking=off href=" + "\"" + baseurl + linkWithId + "\" >" + emaillink + "</a>";
            }

            // Subject and multipart/alternative Body
            //string Subject = branchName.TrimEnd()+ ",Request Type:Feedback Form, Ticket Id:" + ticketId + ", Status:Open, Feedback Type: " + RadioButtonList1.Text;

            string Subject ="Smart Branch / "+ branchName.TrimEnd().Replace("Self Service Branch /", "") + ","+ RadioButtonList1.SelectedItem.Text + " Form / Customer Name:"+ feedbackobj.Name;

            //string html = @"<p>Test Mail</p>";
            //Ticket Id: " + ticketId +
            string html =
                 @"Submission Date/Time: " + BusinessObject.ConvertTimeToUtc() +
                 "<br/> Request Type: Feedback Form " +
                 "<br/> Feedback Type: " + RadioButtonList1.SelectedItem.Text +
                 "<br/> Country: " + country +
                 //"<br/> Product Name: FeedbackForm" +
                 //"<br/> Sub Product Name:"+ RadioButtonList1.SelectedItem.Text +

                 //"<br/> Selected Rate Option: " + emojiText.Text +
                 //"<br/> Discovery Bench: " + feedbackobj.DiscoveryBench +
                 "<br/> Branch Number: " + feedbackobj.BrachNumber +
                 "<br/> Branch Name: " + feedbackobj.BranchName.TrimEnd() +
                 "<br/> Browsing Language: " + feedbackobj.BrowsingLanguage +
                 "<br/> Customer Name: " + feedbackobj.Name +
                 "<br/> Mobile Country code: " + feedbackobj.MobileCode +
                 "<br/> Mobile: " + feedbackobj.Mobile +
                 "<br/> Feedback: " + Server.HtmlEncode(txtfeedback.Text);
                
				 //"<br/> Ticket Status: Open" +
				
            //"<br/> Link to be used while closing the ticket: " + withhyperlink;

            //if (BusinessObject.sendEmail(html.TrimEnd(), toMail.TrimEnd(), Subject, frmMail.TrimEnd()) < 0)
            //{
            //    return;
            //}
        }
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}

        //////////////////////////////////////////////////////////

		//if (feedbackobj.Insert() < 0)
		//{
		//	return;
		//}

		id04.Style.Add(HtmlTextWriterStyle.Display, "block");
		clearFeedbakForm();
	}
	
	protected void ClearButtonForm_Click(object sender, EventArgs e)
	{
		clearFeedbakForm();
	}
	public void clearFeedbakForm()
	{
		DataSet results = new DataSet();
        if (Request.QueryString["bNam"] != null && Request.QueryString["bNam"].ToString().Length < 50)
        {
            branchId = Server.HtmlEncode(Request.QueryString["bNam"]);
        }
        BusinessObject.getByBranchId(branchId, out DataSet value);
		results = value;
		country = Convert.ToString(results.Tables[0].Rows[0]["CountryName"]);
        if (country == "jo" || country == "Jordan" || country == "JO" || country == "JOR")
        {
            txtmobileCDForm.Text = "00962";
        }
        else if (country == "UAE" || country == "uae" || countryId == "2")
        {
            txtmobileCDForm.Text = "00971";
            txtmobilehelp.Attributes["placeholder"] = "5xxxxxxxx";
        }
        else if (country == "EGYPT" || country == "egypt" || countryId == "5")
        {
            txtmobileCDForm.Text = "002";
            txtmobilehelp.Attributes["placeholder"] = "01xxxxxxxx";
        }
        txtnamehelp.Text = "";
		txtmobilehelp.Text = "";
		txtfeedback.Text = "";
		emojiText.Text = "";
        fbMobCodeEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        lblPlsRateUs.Style.Add(HtmlTextWriterStyle.Display, "none");
        fbMobEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        fbNameEmptyFeildAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        lblfeedbackvalid.Style.Add(HtmlTextWriterStyle.Display, "none");
        lblRadioBtnAlert.Style.Add(HtmlTextWriterStyle.Display, "None");
    }
    
	
	protected void SuccessButton_Click(object sender, EventArgs e)
	{
		id04.Style.Add(HtmlTextWriterStyle.Display, "none");
	}
}