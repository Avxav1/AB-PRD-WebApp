using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;

public partial class ProductsCards : System.Web.UI.Page
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
        string pageName = System.IO.Path.GetFileName(Request.Path);

        if(BusinessObject.getBehIdBySessionStart("ProductsCards.aspx", country, discoveryBench, branchNumber, branchName, lang)==0)
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

        if (country == "1")
        {
            liArabBankZainVisaCC.Visible = false;
            liArabBankVisaSilverCard.Visible = false;
            imgleaflet9.Visible = false;
            imgleaflet10.Visible = false;
            liArabBankRoyalJordanianVisaCC.Attributes.Add("class", "buttonm btn");
            liNashamaVisaCC.Attributes.Add("class", "buttonn btn");
            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnCards/VisaDebitCard/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnCards/InternetShoppingCard/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnCards/VisaSignatureCreditCard/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnCards/TogetherPlatinumCC/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnCards/VisaPlatinumCC/files/page/4.jpg";
                leaflet6.Src = "LeafletEN/Disc/EnCards/VisaBlackCC/files/page/4.jpg";
                leaflet7.Src = "LeafletEN/Disc/EnCards/MasterCardTitanium/files/page/4.jpg";
                leaflet8.Src = "LeafletEN/Disc/EnCards/VisaGoldCC/files/page/4.jpg";
                //leaflet9.Src = "demo_files/images/cardsArabic/visa-silver-CC-ara1.jpg";
                //leaflet10.Src = "demo_files/images/cardsArabic/zain-visa-cc-ara1.jpg";
                leaflet11.Src = "LeafletEN/Disc/EnCards/RJCreditCard/files/page/4.jpg";
                leaflet12.Src = "LeafletEN/Disc/EnCards/NashamaVisaCredit/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productCardsArabicVersion/VisaDebitCard/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productCardsArabicVersion/InternetCards/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productCardsArabicVersion/VisaSignatureCard/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/productCardsArabicVersion/TogetherPlatinum/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/productCardsArabicVersion/VisaPlatinum/files/page/4.jpg";
                leaflet6.Src = "LeafletAR/Disc/productCardsArabicVersion/VisaBlack/files/page/4.jpg";
                leaflet7.Src = "LeafletAR/Disc/productCardsArabicVersion/MasterCardTitanium/files/page/4.jpg";
                leaflet8.Src = "LeafletAR/Disc/productCardsArabicVersion/VisaGoldCard/files/page/4.jpg";
                //leaflet9.Src = "demo_files/images/cardsArabic/visa-silver-CC-ara1.jpg";
                //leaflet10.Src = "demo_files/images/cardsArabic/zain-visa-cc-ara1.jpg";
                leaflet11.Src = "LeafletAR/Disc/productCardsArabicVersion/RJCreditCard/files/page/4.jpg";
                leaflet12.Src = "LeafletAR/Disc/productCardsArabicVersion/Nashama/files/page/4.jpg";

                leaflet13.Src = "LeafletAR/Disc/productCardsArabicVersion/ShababVisaCreditCard/files/page/4.jpg";
                leaflet14.Src = "LeafletAR/Disc/productCardsArabicVersion/ShababVisaDebitCard/files/page/4.jpg";
                leaflet15.Src = "LeafletAR/Disc/productCardsArabicVersion/VisaTravelMateCC/files/page/4.jpg";
            }
        }

        if (lang == "EN")
		{
			txtLangHidden.Value= "";
		}
		else
		{
            txtLangHidden.Value = "ar-EG";//arabic link
            
        }

        if (country == "2")
        {
            lblArabBankVisaGoldCreditCard.Text = (string)base.GetLocalResourceObject("lblVisaGoldDubai.Text");
            lblArabBankVisaSilverCard.Text = (string)base.GetLocalResourceObject("lblVisaClassic.Text");
            lblArabBankMasterCardTitaniumCC.Text = (string)base.GetLocalResourceObject("lblMasterCardTitaniumDubai.Text");

            liArabBankZainVisaCC.Visible = false;
            liArabBankVisaBlackCC.Visible = false;
            liTogetherPlatinumCC.Visible = false;
            liArabBankRoyalJordanianVisaCC.Visible = false;
            liNashamaVisaCC.Visible = false;

            liShababVisaCreditCard.Visible = false;
            liShababVisaDebitCard.Visible = false;
            liVisaTravelMateCC.Visible = false;
            imgleaflet13.Visible = false;
            imgleaflet14.Visible = false;
            imgleaflet15.Visible = false;

            imgleaflet12.Visible = false;
            imgleaflet11.Visible = false;
            imgleaflet6.Visible = false;
            imgleaflet4.Visible = false;
            imgleaflet10.Visible = false;

            liArabBankVisaPlatinumCC.Attributes.Add("class", "buttonh btn");
            liArabBankMasterCardTitaniumCC.Attributes.Add("class", "buttoni btn");
            liArabBankVisaGoldCreditCard.Attributes.Add("class", "buttonj btn");
            liArabBankVisaSilverCard.Attributes.Add("class", "buttonk btn");
            liArabBankZainVisaCC.Attributes.Add("class", "buttonl btn");

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnCards/AEVisaDebitCard/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnCards/AEInternetShoppingCard/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnCards/AEVisaSignatureCreditCard/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnCards/AEVisaPlatinumCC/files/page/4.jpg";
                leaflet7.Src = "LeafletEN/Disc/EnCards/AEMTCInfo/files/page/4.jpg";
                leaflet8.Src = "LeafletEN/Disc/EnCards/AEVisaGoldCC/files/page/4.jpg";
                leaflet9.Src = "LeafletEN/Disc/EnCards/AEVisaSilver/files/page/4.jpg";
                //leaflet10.Src = "LeafletEN/Disc/EnCards/AEVisaElectron/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaDebitCard/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productCardsArabicVersion/AEInternetShoppingCard/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaSignatureCreditCard/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaPlatinumCC/files/page/4.jpg";
                leaflet7.Src = "LeafletAR/Disc/productCardsArabicVersion/AEMTCInfo/files/page/4.jpg";
                leaflet8.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaGoldCC/files/page/4.jpg";
                leaflet9.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaSilver/files/page/4.jpg";
                //leaflet10.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaElectron/files/page/4.jpg";
            }
        }
        if (country == "5")
        {
            //lblVisaDebitCard.Text= (string)base.GetLocalResourceObject("lblVisaBlackCredit.Text");
            lblArabBankVisaGoldCreditCard.Text = (string)base.GetLocalResourceObject("lblVisaGoldDubai.Text");
            lblArabBankVisaSilverCard.Text = (string)base.GetLocalResourceObject("lblVisaElectron.Text");
            lblArabBankMasterCardTitaniumCC.Text = (string)base.GetLocalResourceObject("lblMasterCardTitaniumDubai.Text");

            liVisaDebitCard.Visible = false;
            liArabBankZainVisaCC.Visible = false;
            //liArabBankVisaBlackCC.Visible = false;
            liTogetherPlatinumCC.Visible = false;
            liArabBankRoyalJordanianVisaCC.Visible = false;
            liNashamaVisaCC.Visible = false;

            imgleaflet1.Visible = false;
            imgleaflet12.Visible = false;
            imgleaflet11.Visible = false;
            //imgleaflet6.Visible = false;
            imgleaflet4.Visible = false;
            imgleaflet10.Visible = false;

            liShababVisaCreditCard.Visible = false;
            liShababVisaDebitCard.Visible = false;
            liVisaTravelMateCC.Visible = false;
            imgleaflet13.Visible = false;
            imgleaflet14.Visible = false;
            imgleaflet15.Visible = false;


            liInternetShoppingCards.Attributes.Add("class", "buttone btn");
            liArabBankVisaSignatureCC.Attributes.Add("class", "buttonf btn");
            liArabBankVisaPlatinumCC.Attributes.Add("class", "buttong btn");
            liArabBankVisaBlackCC.Attributes.Add("class", "buttonh btn");
            liArabBankMasterCardTitaniumCC.Attributes.Add("class", "buttoni btn");
            liArabBankVisaGoldCreditCard.Attributes.Add("class", "buttonj btn");
            liArabBankVisaSilverCard.Attributes.Add("class", "buttonk btn");
            //liArabBankZainVisaCC.Attributes.Add("class", "buttonl btn");

            if (lang == "EN")
            {
                //leaflet1.Src = "LeafletEN/Disc/EnCards/AEVisaDebitCard/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnCards/EGInternet shoppingCard/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnCards/EGVisaSignature/files/page/4.jpg";
                leaflet6.Src = "LeafletEN/Disc/EnCards/EGVisaBlackCreditcard/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnCards/EGVisaPlatinum/files/page/4.jpg";
                leaflet7.Src = "LeafletEN/Disc/EnCards/EGTMC/files/page/4.jpg";
                leaflet8.Src = "LeafletEN/Disc/EnCards/EGVisaGoldCreditCard/files/page/4.jpg";
                leaflet9.Src = "LeafletEN/Disc/EnCards/EGVisaElectron/files/page/4.jpg";
                //leaflet10.Src = "LeafletEN/Disc/EnCards/AEVisaElectron/files/page/4.jpg";
            }
            else
            {
                //leaflet1.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaDebitCard/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productCardsArabicVersion/EGInternet shoppingCard/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productCardsArabicVersion/EGVisaSignature/files/page/4.jpg";
                leaflet6.Src = "LeafletAR/Disc/productCardsArabicVersion/EGVisaBlackCreditcard/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/productCardsArabicVersion/EGVisaPlatinum/files/page/4.jpg";
                //leaflet6.Src = "LeafletAR/Disc/productCardsArabicVersion/EGVisaPlatinum/files/page/4.jpg";
                leaflet7.Src = "LeafletAR/Disc/productCardsArabicVersion/EGTMC/files/page/4.jpg";
                leaflet8.Src = "LeafletAR/Disc/productCardsArabicVersion/EGVisaGoldCreditCard/files/page/4.jpg";
                leaflet9.Src = "LeafletAR/Disc/productCardsArabicVersion/EGVisaElectron/files/page/4.jpg";
                //leaflet10.Src = "LeafletAR/Disc/productCardsArabicVersion/AEVisaElectron/files/page/4.jpg";
            }
        }
        if (country == "3")
        {
            lblVisaDebitCard.Text = (string)base.GetLocalResourceObject("lblInternetShoppingCards.Text");
            lblInternetShoppingCards.Text = (string)base.GetLocalResourceObject("lblArabBankMasterCardTitaniumCC.Text");
            lblArabBankVisaSignatureCC.Text = (string)base.GetLocalResourceObject("lblArabBankVisaBlackCC.Text");

            lblTogetherPlatinumCC.Text = (string)base.GetLocalResourceObject("lblVisaDebitCard.Text");
            lblArabBankVisaPlatinumCC.Text = (string)base.GetLocalResourceObject("lblArabBankVisaGoldCreditCard.Text");
            lblArabBankVisaBlackCC.Text = (string)base.GetLocalResourceObject("lblArabBankVisaPlatinumCC.Text");

            lblArabBankMasterCardTitaniumCC.Text = (string)base.GetLocalResourceObject("lblArabBankVisaSignatureCC.Text");
            lblArabBankVisaGoldCreditCard.Text = (string)base.GetLocalResourceObject("lblArabBankRoyalJordanianVisaPlatinumCC.Text");
            lblArabBankVisaSilverCard.Text = (string)base.GetLocalResourceObject("lblShababVisaCreditCard.Text");

            lblArabBankZainVisaCC.Text = (string)base.GetLocalResourceObject("lblArabBankVisaSilverCard.Text");
            lblArabBankRoyalJordanianVisaCC.Text = (string)base.GetLocalResourceObject("lblVisaTravelMateCC.Text");
            lblNashamaVisaCC.Text = (string)base.GetLocalResourceObject("lblWorldMaster.Text");

            liShababVisaCreditCard.Visible = false;
            liShababVisaDebitCard.Visible = false;
            liVisaTravelMateCC.Visible = false;

            imgleaflet13.Visible = false;
            imgleaflet15.Visible = false;
            imgleaflet14.Visible = false;



            //liInternetShoppingCards.Attributes.Add("class", "buttone btn");
            //liArabBankVisaSignatureCC.Attributes.Add("class", "buttonf btn");
            //liArabBankVisaPlatinumCC.Attributes.Add("class", "buttong btn");
            //liArabBankVisaBlackCC.Attributes.Add("class", "buttonh btn");
            //liArabBankMasterCardTitaniumCC.Attributes.Add("class", "buttoni btn");
            //liArabBankVisaGoldCreditCard.Attributes.Add("class", "buttonj btn");
            //liArabBankVisaSilverCard.Attributes.Add("class", "buttonk btn");
            //liArabBankZainVisaCC.Attributes.Add("class", "buttonl btn");

            if (lang == "EN")
            {
                leaflet1.Src = "LeafletEN/Disc/EnCards/PLInternetShoppingCard/files/page/4.jpg";
                leaflet2.Src = "LeafletEN/Disc/EnCards/PLTMC/files/page/4.jpg";
                leaflet3.Src = "LeafletEN/Disc/EnCards/PLVisaBlackCC/files/page/4.jpg";
                leaflet4.Src = "LeafletEN/Disc/EnCards/PLVisaDebitCard/files/page/4.jpg";
                leaflet5.Src = "LeafletEN/Disc/EnCards/PLVisaGoldCC/files/page/4.jpg";
                leaflet6.Src = "LeafletEN/Disc/EnCards/PLVisaPlatinumCC/files/page/4.jpg";
                leaflet7.Src = "LeafletEN/Disc/EnCards/PLVisaSignatureCreditCard/files/page/4.jpg";
                leaflet8.Src = "LeafletEN/Disc/EnCards/PLRJCreditCard/files/page/4.jpg";
                leaflet9.Src = "LeafletEN/Disc/EnCards/PLShababVisaCreditCard/files/page/4.jpg";
                leaflet10.Src = "LeafletEN/Disc/EnCards/PLVisaSilver/files/page/4.jpg";
                leaflet11.Src = "LeafletEN/Disc/EnCards/PLVisaTravelMateCC/files/page/4.jpg";
                leaflet12.Src = "LeafletEN/Disc/EnCards/PLWorldMaster/files/page/4.jpg";
            }
            else
            {
                leaflet1.Src = "LeafletAR/Disc/productCardsArabicVersion/PLInternetShoppingCard/files/page/4.jpg";
                leaflet2.Src = "LeafletAR/Disc/productCardsArabicVersion/PLTMC/files/page/4.jpg";
                leaflet3.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaBlackCC/files/page/4.jpg";
                leaflet4.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaDebitCard/files/page/4.jpg";
                leaflet5.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaGoldCC/files/page/4.jpg";
                leaflet6.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaPlatinumCC/files/page/4.jpg";
                leaflet7.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaSignatureCreditCard/files/page/4.jpg";
                leaflet8.Src = "LeafletAR/Disc/productCardsArabicVersion/PLRJCreditCard/files/page/4.jpg";
                leaflet9.Src = "LeafletAR/Disc/productCardsArabicVersion/PLShababVisaCreditCard/files/page/4.jpg";
                leaflet10.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaSilver/files/page/4.jpg";
                leaflet11.Src = "LeafletAR/Disc/productCardsArabicVersion/PLVisaTravelMateCC/files/page/4.jpg";
                leaflet12.Src = "LeafletAR/Disc/productCardsArabicVersion/PLWorldMaster/files/page/4.jpg";
            }
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