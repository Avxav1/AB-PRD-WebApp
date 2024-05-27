using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;
public partial class SuccessPage : System.Web.UI.Page
{
	string ticketId;
	string formName;
	string url;
	//XMLClass xmlObj = new XMLClass();
	TicketStatusclass ticketObj = new TicketStatusclass();
    ConfigCls configObj = new ConfigCls();
  
	protected override void InitializeCulture()
	{
        string cultureName = "";
        if (Request.QueryString["lang"] != null && Request.QueryString["lang"].Length<255)
        {
            cultureName = Request.QueryString["lang"];
        }
		if (!string.IsNullOrEmpty(cultureName))
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		}

	}
	protected void Page_Load(object sender, EventArgs e)
	{
        if (Request.QueryString["tId"] != null && Request.QueryString["tId"].Length < 255)
        {
            string ticketIdbefore = Request.QueryString["tId"];
            ticketId = BusinessObject.DecodeString(ticketIdbefore);
        }
        else
        {
            return;
        }

        if (Request.QueryString["form"] != null && Request.QueryString["form"].Length < 255)
        {
            string formNamebefore = Request.QueryString["form"];
            formName = BusinessObject.DecodeString(formNamebefore);
        }
        else
        {
            return;
        }

		check_ticketStatus();

		url = HttpContext.Current.Request.Url.AbsoluteUri;

	}
	protected void ticketClosBtn_click(object sender, EventArgs e)
	{
		check_ticketStatus();

        if (Request.QueryString["tId"] != null && Request.QueryString["tId"].Length < 255)
        {
            string ticketIdbefore = Request.QueryString["tId"];
            ticketId = BusinessObject.DecodeString(ticketIdbefore);
        }
        else
        {
            return;
        }

        if (Request.QueryString["form"] != null && Request.QueryString["form"].Length < 255)
        {
            string formNamebefore = Request.QueryString["form"];
            formName = BusinessObject.DecodeString(formNamebefore);
        }
        else
        {
            return;
        }


        if (formName == "ABIaminterested" || formName == "ABIneedhelp" || formName == "ABFeedback")
        {
            if (BusinessObject.updateticketdetails(ticketId, formName) < 0)
            {
                succesMsg.Text = "Failed !";
                return;
            }
            else
            {
                succesMsg.Text = "Successfully Closed";
                ticketClosBtn.Enabled = false;
            }
        }
        else
        {
            ticketClosBtn.Enabled = false;
            succesMsg.Text = "Invalid !";
        }

        mailSent();

    }
	public void check_ticketStatus()
	{
        if (formName == "ABIaminterested" || formName == "ABIneedhelp" || formName == "ABFeedback")
        {
            if(ticketObj.get_ticketStatus(formName, ticketId, out string result)<0)
            {
                return;
            }

            if (result == "C" || result == "")
            {
                ticketClosBtn.Enabled = false;
                succesMsg.Text = "The link expired !";
                return;
            }
            else
            {
                succesMsg.Text = "";
                ticketClosBtn.Enabled = true;
            }
        }
        else
        {
            ticketClosBtn.Enabled = false;
            succesMsg.Text = "Invalid !";
        }
	}


	public void mailSent()
	{
		//////////////////////MailSenting///////////////////////////
		string ticketStatus = "";
		string RequestType = "";
		string Country = "";
		string CustomerName = "";
		string Mobile = "";
		string Feedback = "";
		string FeedbackType = "";
		string EmojiRating = "";
		string DiscoveryBench = "";
		string BrachNumber = "";
		string BranchName = "";
		string BrowsingLanguage = "";
		string SubmissionDateTime = "";
		string TicketStatus = "";
		string LinkTicket = "";
		string Status = "";
		string MobileCode = "";
		string productName = "";
		string subProductName = "";
		string countryId = "";
		string branchId = "";
		DataRow dr = null;

        if(formName== "ABIaminterested")
        {
            if(Iaminterestedclass.Retrievebranchidcountryid(ticketId,out countryId,out branchId)<0)
            {
                return;
            }
        }
        else if (formName == "ABIneedhelp")
        {
            if (Ineedhelpclass.Retrievebranchidcountryid(ticketId, out countryId, out branchId) < 0)
            {
                return;
            }
        }
        else if (formName == "ABFeedback")
        {
            if (Feedbackclass.Retrievebranchidcountryid(ticketId, out countryId, out branchId) < 0)
            {
                return;
            }
        }

        if (configObj.RetrieveBranch(Convert.ToInt64(branchId)) < 0)
        {
            return;
        }


        string toMail = "";

        string frmMail = configObj.FromMail;

		if (formName == "ABFeedback")
		{
           
			ticketObj.getdetailsByTicketId_FeedBckForm(formName, ticketId, out dr);

            

            if (dr["Feedback"].ToString().Trim() == "Complaint")
            {
                toMail = configObj.ToMail_fb2.ToString();
            }
            else
            {
                toMail = configObj.ToMail_fb.ToString();
            }

            //toMail = configObj.ToMail_fb;

            ticketStatus = dr["TicketStatus"].ToString();
			RequestType = dr["RequestType"].ToString();
			Country = dr["Country"].ToString();
			CustomerName = dr["CustomerName"].ToString();
			Mobile = dr["Mobile"].ToString();
			Feedback = dr["Feedback"].ToString();
			FeedbackType = dr["FeedbackType"].ToString();
			EmojiRating = dr["EmojiRating"].ToString();
			DiscoveryBench = dr["DiscoveryBench"].ToString();
			BrachNumber = dr["BrachNumber"].ToString();
			BranchName = dr["BranchName"].ToString();
			BrowsingLanguage = dr["BrowsingLanguage"].ToString();
			SubmissionDateTime = dr["SubmissionDateTime"].ToString();
			TicketStatus = dr["TicketStatus"].ToString();
			LinkTicket = dr["LinkTicket"].ToString();
			Status = dr["Status"].ToString();
			MobileCode = dr["MobileCode"].ToString();
			productName = "Feedback Form ";
			subProductName = "Feedback Form ";
			try
			{
                // Subject and multipart/alternative Body
                string Subject = BranchName.Trim()+"-Request Type: " + RequestType + " - Ticket Id:" + ticketId + " - Status:Closed";
                //string html = @"<p>Test Mail</p>";
                string html =
                     @"Ticket Id: " + ticketId +
                     "<br/> Request Type: " + RequestType +
                     "<br/> Country: " + Country +
                     "<br/> Product Name: " + productName +
                     "<br/> Sub Product Name: " + FeedbackType +
                     "<br/> Customer Name: " + CustomerName +
                     "<br/> Mobile Country code: " + MobileCode +
                     "<br/> Mobile: " +BusinessObject.DecodeString(Mobile) +
                     "<br/> Feedback: " + Feedback +
                     "<br/> FeedbackType: " + FeedbackType +
                     "<br/> EmojiRating: " + EmojiRating +
                     "<br/> Discovery Bench: " + DiscoveryBench +
                     "<br/> Branch Number: " + BrachNumber +
                     "<br/> Branch Name: " + BranchName +
                     "<br/> Browsing Language: " + BrowsingLanguage +
                     "<br/> Submission Date/Time: " + BusinessObject.ConvertTimeToUtc() +
                     "<br/> Ticket Status: Closed";
                    //"<br/> Closed ticket link: " + withhyperlink;

                if (BusinessObject.sendEmail(html, toMail, Subject, frmMail) < 0)
                {
                    return;
                }

            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		else
		{

            toMail = configObj.ToMail.ToString();

            ticketObj.getdetailsByTicketId(formName, ticketId, out dr);
			ticketStatus = dr["TicketStatus"].ToString();
			RequestType = dr["RequestType"].ToString();
			Country = dr["Country"].ToString();
			CustomerName = dr["CustomerName"].ToString();
			Mobile = dr["Mobile"].ToString();
			DiscoveryBench = dr["DiscoveryBench"].ToString();
			BrachNumber = dr["BrachNumber"].ToString();
			BranchName = dr["BranchName"].ToString();
			BrowsingLanguage = dr["BrowsingLanguage"].ToString();
			SubmissionDateTime = dr["SubmissionDateTime"].ToString();
			TicketStatus = dr["TicketStatus"].ToString();
			LinkTicket = dr["LinkTicket"].ToString();
			Status = dr["Status"].ToString();
			MobileCode = dr["MobileCode"].ToString();
			productName = dr["productName"].ToString();
			subProductName = dr["subProductName"].ToString();
			try
			{
				// Subject and multipart/alternative Body
				string Subject = BranchName.Trim()+" - Request Type: " + RequestType + " - Ticket Id:" + ticketId + " - Status:Closed";
				
                //string html = @"<p>Test Mail</p>";
                string html =
                     @"Ticket Id: " + ticketId +
                     "<br/> Request Type: " + RequestType +
                     "<br/> Country: " + Country +
                     "<br/> Product Name: " + productName +
                     "<br/> Sub Product Name: " + subProductName +
                     "<br/> Customer Name: " + CustomerName +
                     "<br/> Mobile Country code: " + MobileCode +
                     "<br/> Mobile: " + BusinessObject.DecodeString(Mobile) +
                     "<br/> Discovery Bench: " + DiscoveryBench +
                     "<br/> Branch Number: " + BrachNumber +
                     "<br/> Branch Name: " + BranchName +
                     "<br/> Browsing Language: " + BrowsingLanguage +
                     "<br/> Submission Date/Time: " + BusinessObject.ConvertTimeToUtc() +
                     "<br/> Ticket Status: Closed";

                if (BusinessObject.sendEmail(html, toMail, Subject, frmMail) < 0)
                {
                    return;
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		//////////////////////////////////////////////////////////
	}

}