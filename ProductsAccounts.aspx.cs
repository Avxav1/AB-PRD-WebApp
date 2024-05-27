using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;
public partial class ProductsAccounts : System.Web.UI.Page
{
	string lang;
	string cultureName;
	string country;
	string discoveryBench;
	string branchNumber;
	string branchName;
	string tabletype;
	protected void Page_Load(object sender, EventArgs e)
	{
		lang = BusinessObject.GetCurrLangCode();

        if (Branchclass.Retrievebranchnumber(branchName, out branchNumber) < 0)
        {
            return;
        }

        if (Request.QueryString["ttype"] != null)
        {
            if (Request.QueryString["ttype"] == "c" || Request.QueryString["ttype"] == "con")
            {
                tabletype = "con";
                thebody.Attributes.Add("class", "blackversion");
            }
            else
            {
                tabletype = "dis";
                thebody.Attributes.Add("class", "blueversion");
            }
        }
        else
        {
            tabletype = "dis";
        }

        string pageName = System.IO.Path.GetFileName(Request.Path);
		if(BusinessObject.getBehIdBySessionStart("ProductsAccounts.aspx", country, discoveryBench, branchNumber, branchName, lang)==0)
        {
            return;
        }

		if (lang == "EN")
		{
			txtLangHidden.Value = "";
            leaflet8.Src = "LeafletEN/Disc/EnAccounts/eTawfeer/files/page/4.jpg";
        }
		else
		{
            txtLangHidden.Value = "ar-EG";//arabic link
			leaflet1.Src = "demo_files/images/accountsArabic/FD_JD_ara1.jpg";
			leaflet2.Src = "demo_files/images/accountsArabic/FD_Foreign_Ara1.jpg";
			leaflet3.Src = "demo_files/images/accountsArabic/Current_account_front_AR.jpg";
			leaflet4.Src = "demo_files/images/accountsArabic/savings-account-ara1.jpg"; 
			leaflet5.Src = "demo_files/images/accountsArabic/ElectronicDeposits_Ara1.jpg";
            leaflet8.Src = "LeafletAR/Disc/productAccountsArabicVersion/eTawfeer/files/page/4.jpg";
        }


        liNewbutton1.Visible = false;
        liNewbutton2.Visible = false;

        imgNewleafet1.Visible = false;
        imgNewleafet2.Visible = false;


        if (country == "2")
        {
            liNewbutton3.Visible = false;
            imgNewleafet3.Visible = false;

            lblfixedDepositJD.Text = (string)base.GetLocalResourceObject("lblfixedDepositAED.Text");
            lblElectronicDeposits.Text = (string)base.GetLocalResourceObject("lblCallAccount.Text");

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnAccounts/AEFixedDepositAED/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnAccounts/AECallAccount/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productAccountsArabicVersion/AEFixedDepositAED/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/productAccountsArabicVersion/AECallAccount/files/page/4.jpg";
            }
        }
        else if (country == "5")
        {

            //liNewbutton3.Visible = false;
            //imgNewleafet3.Visible = false;
            //liNewbutton2.Visible = false;
            //imgNewleafet2.Visible = false;

            //liNewbutton1.Visible = true;
            //imgNewleafet1.Visible = true;

            //liCurrentAccount.Visible = false;
            //liSavingAccounts.Visible = false;
            //imgleaflet3.Visible = false;
            //imgleaflet4.Visible = false;

            //imgleaflet.Visible = false;
            //liFixedDepositFC.Visible = false;

            liElectronicDeposits.Visible = false;
            liSavingAccounts.Visible = false;
            liNewbutton1.Visible = false;
            liNewbutton2.Visible = false;
            liNewbutton3.Visible = false;

            

            imgleaflet4.Visible = false;
            imgNewleafetelec.Visible = false;
            imgNewleafet1.Visible = false;
            imgNewleafet2.Visible = false;
            imgNewleafet3.Visible = false;


            lblfixedDepositJD.Text = (string)base.GetLocalResourceObject("3YearsFloatingCertificate.Text");
            lblFixedDepositFC.Text = (string)base.GetLocalResourceObject("CurrentAccounts.Text");
            //lblElectronicDeposits.Text = (string)base.GetLocalResourceObject("CCurrentaccountforAP.Text");
            lblCurrentAccount.Text = (string)base.GetLocalResourceObject("Savingaccount.Text");
            //lblSavingAccounts.Text = (string)base.GetLocalResourceObject("Fixed5yearscertificate.Text");
            //lblNewbutton1.Text = (string)base.GetLocalResourceObject("Fixed3yearscertificate.Text");
            //lblNewbutton2.Text = (string)base.GetLocalResourceObject("SavingAccountforElite.Text");

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnAccounts/EG3YearsFloatingCertificate/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnAccounts/EGCurrentAccounts/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnAccounts/EGSavingaccount/files/page/4.jpg";

                //leaflet5.Src = "LeafletEN/Disc/EnAccounts/EGCurrentaccountforAP/files/page/4.jpg";
                //leaflet4.Src = "LeafletEN/Disc/EnAccounts/EGFixed5yearscertificate/files/page/4.jpg";

                //leaflet6.Src = "LeafletEN/Disc/EnAccounts/EGFixed3yearscertificate/files/page/4.jpg";
                //leaflet7.Src = "LeafletEN/Disc/EnAccounts/EGSavingAccountforElite/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productAccountsArabicVersion/EG3YearsFloatingCertificate/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productAccountsArabicVersion/EGCurrentAccounts/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productAccountsArabicVersion/EGSavingaccount/files/page/4.jpg";
                //leaflet5.Src = "LeafletAR/Disc/productAccountsArabicVersion/EGCurrentaccountforAP/files/page/4.jpg";
                //leaflet4.Src = "LeafletAR/Disc/productAccountsArabicVersion/EGFixed5yearscertificate/files/page/4.jpg";
                //leaflet6.Src = "LeafletAR/Disc/productAccountsArabicVersion/EGFixed3yearscertificate/files/page/4.jpg";
                //leaflet7.Src = "LeafletAR/Disc/productAccountsArabicVersion/EGSavingAccountforElite/files/page/4.jpg";
            }
        }
        else if (country == "3")
        {
            liElectronicDeposits.Visible = false;
            liNewbutton1.Visible = false;
            liNewbutton2.Visible = false;
            liNewbutton3.Visible = false;


            imgNewleafetelec.Visible = false;
            imgNewleafet1.Visible = false;
            imgNewleafet2.Visible = false;
            imgNewleafet3.Visible = false;

            lblfixedDepositJD.Text = (string)base.GetLocalResourceObject("CurrentAccounts.Text");
            lblFixedDepositFC.Text = (string)base.GetLocalResourceObject("Savingaccount.Text");
            lblCurrentAccount.Text = (string)base.GetLocalResourceObject("DepositAccount.Text");
            lblSavingAccounts.Text = (string)base.GetLocalResourceObject("lbleTawfeer.Text");

            if (lang == "EN")
            {
         
                leaflet1.Src = "LeafletEN/Disc/EnAccounts/PLCurrentAccount/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnAccounts/PLSavingsAccount/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnAccounts/PLDepositAccount/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnAccounts/PLeTawfeer/files/page/4.jpg";
            }
            else
            {
              
                leaflet1.Src = "LeafletAR/Disc/productAccountsArabicVersion/PLCurrentAccount/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productAccountsArabicVersion/PLSavingsAccount/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productAccountsArabicVersion/PLDepositAccount/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/productAccountsArabicVersion/PLeTawfeer/files/page/4.jpg";
            }
        }
        else
        {
            liNewbutton3.Attributes.Add("class", "buttonv btn");
        }

        txtCountryHidden.Value =country;
		txtDiscoveryBenchHidden.Value = discoveryBench;
		txtBranchNumberHidden.Value = branchNumber;
		txtBranchNameHidden.Value = branchName;
		txttabletype.Value = tabletype;
	}
	protected override void InitializeCulture()
	{
        country = "1";
        discoveryBench = "1";
        branchName = "1";

        if (Request.QueryString["lang"] != null && (Request.QueryString["lang"].Length < 50))
        {
            cultureName = Server.HtmlEncode(Request.QueryString["lang"]);
        }
        if (Request.QueryString["cntry"] != null && (Request.QueryString["cntry"].Length < 50))
        {
            country = Server.HtmlEncode(Request.QueryString["cntry"]);
        }
        if (Request.QueryString["dBnch"] != null && (Request.QueryString["dBnch"].Length < 50))
        {
            discoveryBench = Server.HtmlEncode(Request.QueryString["dBnch"]);
        }
        if (Request.QueryString["bNam"] != null && (Request.QueryString["bNam"].Length < 50))
        {
            branchName = Server.HtmlEncode(Request.QueryString["bNam"]);
        }

        if (!string.IsNullOrEmpty(cultureName))
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		}
	}

    protected void btnLoan_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainPage.aspx");
    }
    protected void btnSession_Click(object sender, EventArgs e)
    {
        Session.Abandon();
    }
}