using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;

public partial class Products : System.Web.UI.Page
{
	string lang;
	string cultureName;
	string country;
	string discoveryBench;
	string branchNumber;
	string branchName;
	string pageName;
	string tabletype;
	//long behvrId;
	protected override void InitializeCulture()
	{
		pageName = System.IO.Path.GetFileName(Request.Path);

        country = "1";
        discoveryBench = "1";
        branchName = "1";

        if (Request.QueryString["lang"] != null)
        {
            cultureName = Server.HtmlEncode(Request.QueryString["lang"]);
        }
        if (Request.QueryString["cntry"] != null && (Request.QueryString["cntry"].Length<50))
        {
            country = Server.HtmlEncode(Request.QueryString["cntry"]);
        }
        if (Request.QueryString["dBnch"] != null && (Request.QueryString["dBnch"].Length < 50))
        {
            discoveryBench = Server.HtmlEncode( Request.QueryString["dBnch"]);
        }
        if (Request.QueryString["bNam"] != null && (Request.QueryString["bNam"].Length < 50))
        {
            branchName = Server.HtmlEncode( Request.QueryString["bNam"]);
        }
		
		if (!string.IsNullOrEmpty(cultureName))
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
		}
        
    }
	protected void Page_Load(object sender, EventArgs e)
	{
		lang = BusinessObject.GetCurrLangCode();

        if (Branchclass.Retrievebranchnumber(branchName, out branchNumber) < 0)
        {
            return;
        }

        if(BusinessObject.getBehIdBySessionStart("Products.aspx", country, discoveryBench, branchNumber, branchName, lang)==0)
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

        if (lang == "EN")
		{
            txtLangHidden.Value = "";
		}
		else
		{
            txtLangHidden.Value = "ar-EG";//arabic link
            leaflet1.Src = "demo_files/images/loanArabic/Personal-loan-ara1.jpg";
			leaflet2.Src = "demo_files/images/loanArabic/AutoLoan_Arabic.jpg";
			leaflet3.Src = "demo_files/images/loanArabic/housing-loan-Ara1.jpg";
			leaflet4.Src = "demo_files/images/loanArabic/nrjLoan_Arabic.jpg";
		}

        if (country == "2")
        {
            lblNonRJHomeLoan.Text = (string)base.GetLocalResourceObject("lblNonRJHomeLoanDubai.Text");
            lblAutoLoan.Text = (string)base.GetLocalResourceObject("lblAutoLoanDubai.Text");
            lblHousingLoan.Text = (string)base.GetLocalResourceObject("lblHousingLoanDubai.Text");
            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnLoans/AEPersonalLoan/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnLoans/AEAutoLoan/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnLoans/AEHousingLoan/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnLoans/AENonresidentArabLoan/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productLoanArabicVersion/AEPersonalLoan/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productLoanArabicVersion/AEAutoLoan/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productLoanArabicVersion/AEHousingLoan/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/productLoanArabicVersion/AENonresidentArabLoan/files/page/4.jpg";
            }

        }
        else if (country == "5")
        {

            liHousingLoan.Visible = false;
            liNonRJHomeLoan.Visible = false;

            imgHousingloan.Visible = false;
            imgnrjhomeloan.Visible = false;
          

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnLoans/EGPersonalLoan/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnLoans/EGAutoLoan/files/page/4.jpg";
              
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productLoanArabicVersion/EGPersonalLoan/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productLoanArabicVersion/EGAutoLoan/files/page/4.jpg";
               
            }

        }
        else if (country == "3")
        {

            liNonRJHomeLoan.Visible = false;
            imgnrjhomeloan.Visible = false;


            if (lang == "EN")
            {
                leaflet2.Src = "LeafletEN/Disc/EnLoans/PLAutoLoan/files/page/4.jpg";
                leaflet1.Src = "LeafletEN/Disc/EnLoans/PLPersonalLoan/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnLoans/PLHousingLoan/files/page/4.jpg";

            }
            else
            {
                leaflet2.Src = "LeafletAR/Disc/productLoanArabicVersion/PLAutoLoan/files/page/4.jpg";
                leaflet1.Src = "LeafletAR/Disc/productLoanArabicVersion/PLPersonalLoan/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productLoanArabicVersion/PLHousingLoan/files/page/4.jpg";

            }

        }

        txtCountryHidden.Value = country;
		txtDiscoveryBenchHidden.Value = discoveryBench;
		txtBranchNumberHidden.Value = branchNumber;
		txtBranchNameHidden.Value = branchName;
		txttabletype.Value = tabletype;
	}
    protected void btnLoan_Click(object sender, EventArgs e)
    {
        Server.Transfer("mainPage.aspx?lang="+ txtLangHidden.Value);
    }
    protected void btnSession_Click(object sender, EventArgs e)
    {
        Session.Abandon();
    }
}