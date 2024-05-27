using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;
using System.Text.RegularExpressions;
public partial class Products : System.Web.UI.Page
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
		if(BusinessObject.getBehIdBySessionStart("RelationShipProgram.aspx", country, discoveryBench, branchNumber, branchName, lang)==0)
        {
            return;
        }
        lblJeelAlArabi.Text = (string)base.GetLocalResourceObject("lblArabiJunior.Text");
        liTabeebPlus.Visible = false;
        imgleaflet7.Visible = false;
        if (lang == "EN")
		{
			txtLangHidden.Value= "";
            leaflet6.Src = "LeafletEN/Disc/EnrelationShip/ArabiJunior/files/page/4.jpg";
        }
		else
		{
			txtLangHidden.Value= "ar-EG";//arabic link
			leaflet1.Src = "demo_files/images/relationshipArabicVer/elite arabic_NN.jpg";
			leaflet2.Src = "demo_files/images/relationshipArabicVer/arabi_premium_arabic.jpg";
			leaflet3.Src = "demo_files/images/relationshipArabicVer/cros_border_arabic.jpg";
			leaflet4.Src = "demo_files/images/relationshipArabicVer/arabi_extra_program_Arabic NN.jpg";
			leaflet5.Src = "demo_files/images/relationshipArabicVer/Shabab arabic_NN.jpg";
			leaflet6.Src = "LeafletAR/Disc/relationShipArbicVersion/ArabiJunior/files/page/4.jpg";
		}

        if (country == "2")
        {
            lblElite.Text = (string)base.GetLocalResourceObject("lblEliteDubai.Text");
            lblArabiPremium.Text = (string)base.GetLocalResourceObject("lblArabiDubai.Text");
            lblArabiCrossBoarders.Text = (string)base.GetLocalResourceObject("lblArabiCrossBoardersDubai.Text");
            lblArabiExtra.Text = (string)base.GetLocalResourceObject("lblArabiExtraDubai.Text");

            liJeelAlArabi.Visible = false;
            liShabab.Visible = false;
         
            imgleaflet5.Visible = false;
            imgleaflet6.Visible = false;

            if (lang == "EN")
            {

                leaflet1.Src = "LeafletEN/Disc/EnrelationShip/AEElite/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnrelationShip/AEArabiPremium/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnrelationShip/AECrossBorder/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnrelationShip/AEArabiExtraProgram/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/relationShipArbicVersion/AEElite/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/relationShipArbicVersion/AEArabiPremium/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/relationShipArbicVersion/AECrossBorder/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/relationShipArbicVersion/AEArabiExtraProgram/files/page/4.jpg";
            }
        }

        if (country == "5")
        {
            lblElite.Text = (string)base.GetLocalResourceObject("lblEliteDubai.Text");
            lblArabiPremium.Text = (string)base.GetLocalResourceObject("lblArabiDubai.Text");
            lblArabiCrossBoarders.Text = (string)base.GetLocalResourceObject("lblArabiCrossBoardersDubai.Text");
            lblArabiExtra.Text = (string)base.GetLocalResourceObject("lblArabiExtraDubai.Text");
            lblShabab.Text = (string)base.GetLocalResourceObject("lblShababEgypt.Text");
            lblJeelAlArabi.Text = (string)base.GetLocalResourceObject("lblJeelAlArabi.Text");

            liJeelAlArabi.Visible = false;
            liShabab.Visible = false;

            imgleaflet5.Visible = false;
            imgleaflet6.Visible = false;

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnrelationShip/EGElite/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnrelationShip/EGArabiPremium/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnrelationShip/EGArabiCrossBorder/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnrelationShip/EGArabiExtraProgram/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnrelationShip/EGShabab/files/page/4.jpg";
                leaflet6.Src = "LeafletEN/Disc/EnrelationShip/EGJeelAlArabi/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/relationShipArbicVersion/EGElite/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/relationShipArbicVersion/EGArabiPremium/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/relationShipArbicVersion/EGArabiCrossBorder/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/relationShipArbicVersion/EGArabiExtraProgram/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/relationShipArbicVersion/EGShabab/files/page/4.jpg";
                leaflet6.Src = "LeafletAR/Disc/relationShipArbicVersion/EGJeelAlArabi/files/page/4.jpg";
            }
        }

        if (country == "3")
        {
            liTabeebPlus.Visible = true;
            imgleaflet7.Visible = true;



            lblElite.Text = (string)base.GetLocalResourceObject("lblArabiCrossBoardersDubai.Text");
            lblArabiPremium.Text = (string)base.GetLocalResourceObject("lblArabiExtraDubai.Text");
            lblArabiCrossBoarders.Text = (string)base.GetLocalResourceObject("lblArabiDubai.Text");
            lblArabiExtra.Text = (string)base.GetLocalResourceObject("lblElite.Text");
            lblShabab.Text = (string)base.GetLocalResourceObject("lblArabiJunior.Text");
            lblJeelAlArabi.Text = (string)base.GetLocalResourceObject("lblShabab.Text");
            lblTabeebPlus.Text = (string)base.GetLocalResourceObject("lblTabeebPlus.Text");

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnrelationShip/PLCrossBorder/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnrelationShip/PLArabiExtraProgram/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnrelationShip/PLArabicPremium/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnrelationShip/PLElite/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnrelationShip/PLArabiJunior/files/page/4.jpg";
                leaflet6.Src = "LeafletEN/Disc/EnrelationShip/PLShahab/files/page/4.jpg";
                leaflet7.Src = "LeafletEN/Disc/EnrelationShip/PLTabeebPlus/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/relationShipArbicVersion/PLCrossBorder/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/relationShipArbicVersion/PLArabiExtraProgram/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/relationShipArbicVersion/PLArabicPremium/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/relationShipArbicVersion/PLElite/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/relationShipArbicVersion/PLArabiJunior/files/page/4.jpg";
                leaflet6.Src = "LeafletAR/Disc/relationShipArbicVersion/PLShahab/files/page/4.jpg";
                leaflet7.Src = "LeafletAR/Disc/relationShipArbicVersion/PLTabeebPlus/files/page/4.jpg";
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
        Server.Transfer("mainPage.aspx");
    }
    protected void btnSession_Click(object sender, EventArgs e)
    {
        Session.Abandon();
    }
}