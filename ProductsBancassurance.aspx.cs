using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;
using System.Text.RegularExpressions;
public partial class ProductsBancassurance : System.Web.UI.Page
{
	string lang;
	string cultureName;
	string country;
	string discoveryBench;
	string branchNumber;
	string branchName;
	string tabletype;
    protected override void InitializeCulture()
    {
        country = "1";
        discoveryBench = "1";
        branchName = "1";

        if (Request.QueryString["lang"] != null && (Request.QueryString["lang"].Length < 50))
        {
            cultureName = Server.HtmlEncode(Request.QueryString["lang"]);
        }
        if (Request.QueryString["cntry"] != null && (Request.QueryString["cntry"].Length < 50) && Regex.IsMatch(Request.QueryString["cntry"],
                      @"^[1-9][0-9]{0,2}$"))
        {
            country = Server.HtmlEncode(Request.QueryString["cntry"]);
        }
        if (Request.QueryString["dBnch"] != null && (Request.QueryString["dBnch"].Length < 50) && Regex.IsMatch(Request.QueryString["dBnch"],
                      @"^[1-9][0-9]{0,2}$"))
        {
            discoveryBench = Server.HtmlEncode(Request.QueryString["dBnch"]);
        }
        if (Request.QueryString["bNam"] != null && (Request.QueryString["bNam"].Length < 50) && Regex.IsMatch(Request.QueryString["bNam"],
                      @"^[1-9][0-9]{0,2}$"))
        {
            branchName = Server.HtmlEncode(Request.QueryString["bNam"]);
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
		if(BusinessObject.getBehIdBySessionStart("ProductsBancassurance.aspx", country, discoveryBench, branchNumber, branchName, lang)==0)
        {
            return;
        }

		if (lang == "EN")
		{
			txtLangHidden.Value = "";
		}
		else
		{
			txtLangHidden.Value = "ar-EG";//arabic link
			leaflet1.Src = "demo_files/images/banacasurancArabic/critical-illness_flyer--a1_new.jpg";
			leaflet2.Src = "demo_files/images/banacasurancArabic/na-el-omor-Ara1_new.jpg";
			leaflet3.Src = "demo_files/images/banacasurancArabic/Aelaty-Bi-Aman-Ara1_new.jpg";
			leaflet4.Src = "demo_files/images/banacasurancArabic/Rahet-El-Bal-Ara1_new.jpg";
			leaflet5.Src = "demo_files/images/banacasurancArabic/Hatta-Yedersou-Ara1_new.jpg";
			leaflet6.Src = "demo_files/images/banacasurancArabic/Lamma-Yek_Ara1_new.jpg";

		}

        if (country == "3")
        {
            //liAaelatyBiAman.Visible = false;
            //liRahetElBal.Visible = false;
            liHattaYedersou.Visible = false;
            //imgleaf4.Visible = false;
            //imgleaf5.Visible = false;
            imgleaf6.Visible = false;

            lblCriticalIllness.Text = (string)base.GetLocalResourceObject("lblBalakMerta7.Text");
            lblLammaYekbarou.Text = (string)base.GetLocalResourceObject("lblHasadElOmor.Text");
            lblJanaElOmr.Text = (string)base.GetLocalResourceObject("lblHattaYenjaho.Text");
            lblAaelatyBiAman.Text = (string)base.GetLocalResourceObject("lblAutoInsurance.Text");
            lblRahetElBal.Text = (string)base.GetLocalResourceObject("lblTravelAccidentInsurance.Text");

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnBanacasurance/PLBalakMerta/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnBanacasurance/PLHattaYenjaho/files/page/4.jpg";
                leaflet6.Src = "LeafletEN/Disc/EnBanacasurance/PLHasadElOmor/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnBanacasurance/PLAutoInsurance/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnBanacasurance/PLTravelAccidentInsurance/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/prdctBanacasuranceArbcVer/PLBalakMerta/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/prdctBanacasuranceArbcVer/PLHattaYenjaho/files/page/4.jpg";
                leaflet6.Src = "LeafletAR/Disc/prdctBanacasuranceArbcVer/PLHasadElOmor/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/prdctBanacasuranceArbcVer/PLAutoInsurance/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/prdctBanacasuranceArbcVer/PLTravelAccidentInsurance/files/page/4.jpg";
            }
        }

        txtCountryHidden.Value = country;
		txtDiscoveryBenchHidden.Value =discoveryBench;
		txtBranchNumberHidden.Value = branchNumber;
		txtBranchNameHidden.Value = branchName;
		txttabletype.Value =tabletype;
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