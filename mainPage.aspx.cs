using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Globalization;
//using System.Web.Mail;
using System.Xml;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Net.Mime;
using System.Data;
using Arabbankdll;
public partial class Personalloan : System.Web.UI.Page
{
    private bool _isError = false;
    public double rateAmount;
    string lang;
    string country;
    string discoveryBench;
    string branchNumber;
    string branchName;
    double interestRate;
    string pName;
    string prdctName;
    string subPrdctName;
    string countryId;
    string branchId;
    string ttype;
    //string cultureName;
    ConfigCls configObj = new ConfigCls();

    protected override void InitializeCulture()
    {
        string cultureName = "";
        if (Request.QueryString["lang"] != null && Request.QueryString["lang"].Length < 50)
        {
            cultureName = Server.HtmlEncode(Request.QueryString["lang"]);
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

        string productName = "loan";
        string subProductName = "personal";

        if (!IsPostBack)
        {
            countryId = "1";
            branchId = "1";
            discoveryBench = "1";
            ttype = "dis";

            if (PreviousPage != null)
            {
                System.Web.UI.HtmlControls.HtmlInputHidden SourceProduct =
                    (System.Web.UI.HtmlControls.HtmlInputHidden)PreviousPage.FindControl("txtProduct");
                if (SourceProduct != null && SourceProduct.Value.Length < 50)
                {
                    productName = Server.HtmlEncode(SourceProduct.Value);
                    if (BusinessObject.checksafety(productName))
                    {
                        hproductName.Value = productName;
                    }
                }

                System.Web.UI.HtmlControls.HtmlInputHidden SourceSubProduct =
                    (System.Web.UI.HtmlControls.HtmlInputHidden)PreviousPage.FindControl("txtSubProduct");
                if (SourceSubProduct != null && SourceSubProduct.Value.Length < 50)
                {
                    subProductName = Server.HtmlEncode(SourceSubProduct.Value);
                    if (BusinessObject.checksafety(subProductName))
                    {
                        hsubProductName.Value = subProductName;
                    }
                }
            }
            else
            {
                hproductName.Value = "loan";
                hsubProductName.Value = "personal";
            }

            if (PreviousPage != null)
            {
                System.Web.UI.HtmlControls.HtmlInputHidden SourceCountry =
                    (System.Web.UI.HtmlControls.HtmlInputHidden)PreviousPage.FindControl("txtCountryHidden");
                if (SourceCountry != null && SourceCountry.Value.Length < 50)
                {
                    countryId = Server.HtmlEncode(SourceCountry.Value);
                    if (BusinessObject.checksafety(countryId))
                    {
                        hcountryId.Value = countryId;
                    }
                }

                System.Web.UI.HtmlControls.HtmlInputHidden SourceBench =
                    (System.Web.UI.HtmlControls.HtmlInputHidden)PreviousPage.FindControl("txtDiscoveryBenchHidden");
                if (SourceBench != null && SourceBench.Value.Length < 50)
                {
                    discoveryBench = Server.HtmlEncode(SourceBench.Value);
                    if (BusinessObject.checksafety(discoveryBench))
                    {
                        hdiscoveryBench.Value = discoveryBench;
                    }
                }

                System.Web.UI.HtmlControls.HtmlInputHidden SourceBranch =
                    (System.Web.UI.HtmlControls.HtmlInputHidden)PreviousPage.FindControl("txtBranchNameHidden");
                if (SourceBranch != null && SourceBranch.Value.Length < 50)
                {
                    branchId = Server.HtmlEncode(SourceBranch.Value);
                    if (BusinessObject.checksafety(branchId))
                    {
                        hbranchId.Value = branchId;
                    }
                }

                System.Web.UI.HtmlControls.HtmlInputHidden SourceType =
                    (System.Web.UI.HtmlControls.HtmlInputHidden)PreviousPage.FindControl("txttabletype");
                if (SourceType != null && SourceType.Value.Length < 50)
                {
                    ttype = Server.HtmlEncode(SourceType.Value);
                    if (BusinessObject.checksafety(ttype))
                    {
                        httype.Value = ttype;
                    }
                }

            }
            else
            {
                hcountryId.Value = "1";
                hdiscoveryBench.Value = "1";
                hbranchId.Value = "1";
                httype.Value = "dis";
            }

            DataSet results = new DataSet();
            if (BusinessObject.getByBranchId(branchId, out DataSet value) >= 0)
            {
                results = value;
                DataRow branchdet = results.Tables[0].Rows[0];

                country = Convert.ToString(branchdet["CountryName"]);
                hcountry.Value = country;
                branchNumber = Convert.ToString(branchdet["BranchNumber"]);
                hbranchNumber.Value = branchNumber;
                branchName = Convert.ToString(branchdet["BranchName"]);
                hbranchName.Value = branchName;
            }


            ///////////////////////////////////////////////////////////////////////////
            ////////////////////////getting value from queryString/////////////////////

            prdctName = hproductName.Value;
            subPrdctName = hsubProductName.Value;
            productName = hproductName.Value;
            subProductName = hsubProductName.Value;

            /////////////////////////////////////////////////////////////////////////

            if (ttype == "con")
            {
                btnamInterested.Visible = false;
                btnIneedhelp.Visible = false;
                btnIwillthink.Visible = false;
                backBtnImg.ImageUrl = "~/source/img/rightconsult.png";
                thebody.Attributes.Add("class", "blackversion");
            }
            else
            {
                thebody.Attributes.Add("class", "blueversion");
            }
            RadioButtonListcurrency.Visible = false;

            if (productName == "loan" && subProductName == "personal")
            {
                calculateButton.Click += (from, ea) => getFormData();
                loadPersonalLoan();
            }
            else if (productName == "loan" && subProductName == "AutoLoan")
            {
                calculateButton.Click += (from, ea) => getFormData();
                loadAutoLoan();
            }
            else if (productName == "loan" && subProductName == "housing")
            {
                calculateButton.Click += (from, ea) => getFormData();
                loadHousingLoan();
            }
            else if (productName == "loan" && subProductName == "nonResidentJordan")
            {
                calculateButton.Click += (from, ea) => getFormData();
                loadResidentJordan();
            }


            else if (productName == "accounts" && subProductName == "FixedDepositJD")
            {
                depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadFixedDepositJD();
            }

            else if (productName == "accounts" && subProductName == "FixedDepositForeign")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadFixedDepositForeign();
            }
            else if (productName == "accounts" && subProductName == "Current")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadCurrentAccount();
            }
            else if (productName == "accounts" && subProductName == "Savings")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadSavingsAccount();
            }
            else if (productName == "accounts" && subProductName == "ElectronicDeposits")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadElectronicDepositsAccount();
            }
            else if (productName == "accounts" && subProductName == "NewButton1")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadNewButton1();
            }
            else if (productName == "accounts" && subProductName == "NewButton2")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadNewButton2();
            }
            else if (productName == "accounts" && subProductName == "NewButton3")
            {
                //depositCalculateButton.Click += (from, ea) => getDepositFormData();
                loadNewButton3();
            }

            else if (productName == "Cards" && subProductName == "VisaDebitCard")
            {
                loadVisaDebitCard();
            }
            else if (productName == "Cards" && subProductName == "InternetShoppingCard")
            {
                loadInternetShoppingCard();
            }
            else if (productName == "Cards" && subProductName == "VisaSilver")
            {
                loadVisaSilver();
            }
            else if (productName == "Cards" && subProductName == "VisaGoldCC")
            {
                loadVisaGoldCC();
            }
            else if (productName == "Cards" && subProductName == "VisaBlackCC")
            {
                loadVisaBlackCC();
            }
            else if (productName == "Cards" && subProductName == "ZainVisa")
            {
                loadZainVisa();
            }
            else if (productName == "Cards" && subProductName == "RJCreditCard")
            {
                loadRJCreditCard();
            }
            else if (productName == "Cards" && subProductName == "MasterCardTitanium")
            {
                loadMasterCardTitanium();
            }
            else if (productName == "Cards" && subProductName == "VisaPlatinumCC")
            {
                loadVisaPlatinumCC();
            }
            else if (productName == "Cards" && subProductName == "TogetherPlatinumCC")
            {
                loadTogetherPlatinumCC();
            }
            else if (productName == "Cards" && subProductName == "NashamaVisaCredit")
            {
                loadNashamaVisaCredit();
            }
            else if (productName == "Cards" && subProductName == "VisaSignatureCreditCard")
            {
                loadVisaSignatureCreditCard();
            }
            else if (productName == "Cards" && subProductName == "VisaTravelMateCC")
            {
                loadVisaTravelMateCC();
            }
            else if (productName == "Cards" && subProductName == "ShababVisaDebitCard")
            {
                loadShababVisaDebitCard();
            }
            else if (productName == "Cards" && subProductName == "ShababVisaCreditCard")
            {
                loadShababVisaCreditCard();
            }
            else if (productName == "Bancassurance" && subProductName == "LammaYekFlyer")
            {
                loadLammaYek_flyer();
            }
            else if (productName == "Bancassurance" && subProductName == "JanaElOmor")
            {
                loadJanaElOmor();
            }
            else if (productName == "Bancassurance" && subProductName == "AaelatyBiAman")
            {
                loadAaelatyBiAman();
            }
            else if (productName == "Bancassurance" && subProductName == "HattaYedersou")
            {
                loadHattaYedersou();
            }
            else if (productName == "Bancassurance" && subProductName == "RahetEIBal")
            {
                loadRahetEIBal();
            }
            else if (productName == "Bancassurance" && subProductName == "CriticalIllness")
            {
                loadCritical_Illness();
            }

            else if (productName == "RelationShipProgram" && subProductName == "Elite")
            {
                loadElite();
            }
            else if (productName == "RelationShipProgram" && subProductName == "ArabiPremium")
            {
                loadArabiPremium();
            }
            else if (productName == "RelationShipProgram" && subProductName == "ArabiCrossBoarders")
            {
                loadArabiCrossBoarders();
            }
            else if (productName == "RelationShipProgram" && subProductName == "ArabiExtra")
            {
                loadArabiExtra();
            }
            else if (productName == "RelationShipProgram" && subProductName == "Shabab")
            {
                loadShabab();
            }
            else if (productName == "RelationShipProgram" && subProductName == "JeelAlArabi")
            {
                loadJeelAlArabi();
            }
            else if (productName == "RelationShipProgram" && subProductName == "TabeebPlus")
            {
                loadTabeebPlus();
            }
            else
            {
                loadPersonalLoan();
            }
            if (lang == "EN")
            {
                backBtnImg.ImageUrl = "~/source/img/previous1.png";///////////////////////////Back button direction - Right
            }
            else
            {
                backBtnImg.ImageUrl = "~/source/img/next1.png";///////////////////////////////Back button direction - left
                lblPopMayBeLatrMobile.CssClass = "arabiclabel";
                lblPopupIamInterestedMobile.CssClass = "arabiclabel";
                lblPopupIamNeedHelpMobile.CssClass = "arabiclabel";
            }
            
        }

        RadioButtonListcurrency.Items[0].Text = (string)base.GetLocalResourceObject("RadioCurrencyJOD.Text");
        RadioButtonListcurrency.Items[1].Text = (string)base.GetLocalResourceObject("RadioCurrencyUSD.Text");
        
        if (lang == "EN")
        {
            //RadioButtonListcurrency.TextAlign = TextAlign.Right;
            radcontainer.Attributes.Add("dir", "ltr");
            iwillthinkleft.Attributes.Add("dir", "ltr");
            iwillthinkright.Attributes.Add("dir", "ltr");
            iaminterestedleft.Attributes.Add("dir", "ltr");
            iaminterestedright.Attributes.Add("dir", "ltr");
            ineedhelpleft.Attributes.Add("dir", "ltr");
            ineedhelpright.Attributes.Add("dir", "ltr");
        }
        else
        {
            ///RadioButtonListcurrency.TextAlign = TextAlign.Left;
            radcontainer.Attributes.Add("dir", "rtl");
            iwillthinkleft.Attributes.Add("dir", "rtl");
            iwillthinkright.Attributes.Add("dir", "rtl");
            iaminterestedleft.Attributes.Add("dir", "rtl");
            iaminterestedright.Attributes.Add("dir", "rtl");
            ineedhelpleft.Attributes.Add("dir", "rtl");
            ineedhelpright.Attributes.Add("dir", "rtl");
        }


        productName = hproductName.Value;
        subProductName = hsubProductName.Value;

        if (productName == "loan" && subProductName == "personal")
        {
            calculateButton.Click += (from, ea) => getFormData();
        }
        else if (productName == "loan" && subProductName == "AutoLoan")
        {
            calculateButton.Click += (from, ea) => getFormData();
        }
        else if (productName == "loan" && subProductName == "housing")
        {
            calculateButton.Click += (from, ea) => getFormData();
        }
        else if (productName == "loan" && subProductName == "nonResidentJordan")
        {
            calculateButton.Click += (from, ea) => getFormData();
        }
        else if (productName == "accounts" && subProductName == "FixedDepositJD")
        {
            depositCalculateButton.Click += (from, ea) => getDepositFormData();
        }
    }

    public void loanparameter(string country, string loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate, out string curen,out string curar)
    {
        string apistring = "https://api.arabbank.com/product/v1/loans?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&country=JO";

        if (country == "1")
        {
            apistring = "https://api.arabbank.com/product/v1/loans?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&country=JO";
        }
        else if (country == "2")
        {
            apistring = "https://api.arabbank.com/product/v1/loans?apikey=TUs3e1AIBERXw9mPK9Pa106iWW54fSGl%20&country=AE";
        }
        else if (country == "5")
        {
            apistring = "https://api.arabbank.com/product/v1/loans?apikey=TUs3e1AIBERXw9mPK9Pa106iWW54fSGl%20&country=EG";
        }
        else if (country == "3")
        {
            apistring = "https://api.arabbank.com/product/v2/loans?apikey=lmJcuCh1IGnVUJnBnCaiV2JiFaXeooqG&country=PS";
        }

        string json = get_web_content(apistring);

        dynamic array = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        //DataSet dataSet = Newtonsoft.Json.JsonConvert.DeserializeObject<DataSet>(json);
        //dynamic Result = array.result;

        minamt = "";
        maxamt = "";
        minmonth = "";
        maxmonth = "";
        interestrate = "";
        curen = "";
        curar = "";

        if (loantype == "a")
        {
            minamt = array["data"].auto[0].country.types[0].currencies[0].amount.min;
            maxamt = array["data"].auto[0].country.types[0].currencies[0].amount.max;
            minmonth = array["data"].auto[0].country.types[0].currencies[0].tenor.min;
            maxmonth = array["data"].auto[0].country.types[0].currencies[0].tenor.max;
            interestrate = array["data"].auto[0].country.types[0].currencies[0].interestRate;
            curen = array["data"].auto[0].country.types[0].currencies[0].code;
            curar = array["data"].auto[0].country.types[0].currencies[0].descriptions.ar;
            if (country == "3" && RadioButtonListcurrency.SelectedIndex == 1)
            {
                minamt = array["data"].auto[0].country.types[0].currencies[1].amount.min;
                maxamt = array["data"].auto[0].country.types[0].currencies[1].amount.max;
                minmonth = array["data"].auto[0].country.types[0].currencies[1].tenor.min;
                maxmonth = array["data"].auto[0].country.types[0].currencies[1].tenor.max;
                interestrate = array["data"].auto[0].country.types[0].currencies[1].interestRate;
                curen = array["data"].auto[0].country.types[0].currencies[1].code;
                curar = array["data"].auto[0].country.types[0].currencies[1].descriptions.ar;
            }
        }
        else if (loantype == "p")
        {
            minamt = array["data"].personal[0].country.types[0].currencies[0].amount.min;
            maxamt = array["data"].personal[0].country.types[0].currencies[0].amount.max;
            minmonth = array["data"].personal[0].country.types[0].currencies[0].tenor.min;
            maxmonth = array["data"].personal[0].country.types[0].currencies[0].tenor.max;
            interestrate = array["data"].personal[0].country.types[0].currencies[0].interestRate;
            curen = array["data"].auto[0].country.types[0].currencies[0].code;
            curar = array["data"].auto[0].country.types[0].currencies[0].descriptions.ar;
            if (country == "3" && RadioButtonListcurrency.SelectedIndex == 1)
            {
                minamt = array["data"].personal[0].country.types[0].currencies[1].amount.min;
                maxamt = array["data"].personal[0].country.types[0].currencies[1].amount.max;
                minmonth = array["data"].personal[0].country.types[0].currencies[1].tenor.min;
                maxmonth = array["data"].personal[0].country.types[0].currencies[1].tenor.max;
                interestrate = array["data"].personal[0].country.types[0].currencies[1].interestRate;
                curen = array["data"].auto[0].country.types[0].currencies[1].code;
                curar = array["data"].auto[0].country.types[0].currencies[1].descriptions.ar;
            }
        }
        else if (loantype == "h")
        {
            minamt = array["data"].housing[0].country.types[0].currencies[0].amount.min;
            maxamt = array["data"].housing[0].country.types[0].currencies[0].amount.max;
            minmonth = array["data"].housing[0].country.types[0].currencies[0].tenor.min;
            maxmonth = array["data"].housing[0].country.types[0].currencies[0].tenor.max;
            interestrate = array["data"].housing[0].country.types[0].currencies[0].interestRate;
            curen = array["data"].auto[0].country.types[0].currencies[0].code;
            curar = array["data"].auto[0].country.types[0].currencies[0].descriptions.ar;
            if (country == "3" && RadioButtonListcurrency.SelectedIndex == 1)
            {
                minamt = array["data"].housing[0].country.types[0].currencies[1].amount.min;
                maxamt = array["data"].housing[0].country.types[0].currencies[1].amount.max;
                minmonth = array["data"].housing[0].country.types[0].currencies[1].tenor.min;
                maxmonth = array["data"].housing[0].country.types[0].currencies[1].tenor.max;
                interestrate = array["data"].housing[0].country.types[0].currencies[1].interestRate;
                curen = array["data"].auto[0].country.types[0].currencies[1].code;
                curar = array["data"].auto[0].country.types[0].currencies[1].descriptions.ar;
            }
        }
        else if (loantype == "n")
        {
            minamt = array["data"].auto[0].country.types[0].currencies[0].amount.min;
            maxamt = array["data"].auto[0].country.types[0].currencies[0].amount.max;
            minmonth = array["data"].auto[0].country.types[0].currencies[0].tenor.min;
            maxmonth = array["data"].auto[0].country.types[0].currencies[0].tenor.max;
            interestrate = array["data"].auto[0].country.types[0].currencies[0].interestRate;
            curen = array["data"].auto[0].country.types[0].currencies[0].code;
            curar = array["data"].auto[0].country.types[0].currencies[0].descriptions.ar;
            if (country == "3" && RadioButtonListcurrency.SelectedIndex == 1)
            {
                minamt = array["data"].auto[0].country.types[0].currencies[1].amount.min;
                maxamt = array["data"].auto[0].country.types[0].currencies[1].amount.max;
                minmonth = array["data"].auto[0].country.types[0].currencies[1].tenor.min;
                maxmonth = array["data"].auto[0].country.types[0].currencies[1].tenor.max;
                interestrate = array["data"].auto[0].country.types[0].currencies[1].interestRate;
                curen = array["data"].auto[0].country.types[0].currencies[1].code;
                curar = array["data"].auto[0].country.types[0].currencies[1].descriptions.ar;
            }
        }
        else if (loantype == "u")
        {
            minamt = array["data"].auto[0].country.types[1].currencies[0].amount.min;
            maxamt = array["data"].auto[0].country.types[1].currencies[0].amount.max;
            minmonth = array["data"].auto[0].country.types[1].currencies[0].tenor.min;
            maxmonth = array["data"].auto[0].country.types[1].currencies[0].tenor.max;
            interestrate = array["data"].auto[0].country.types[1].currencies[0].interestRate;
            curen = array["data"].auto[0].country.types[0].currencies[0].code;
            curar = array["data"].auto[0].country.types[0].currencies[0].descriptions.ar;
            if (country == "3" && RadioButtonListcurrency.SelectedIndex == 1)
            {
                minamt = array["data"].auto[0].country.types[1].currencies[1].amount.min;
                maxamt = array["data"].auto[0].country.types[1].currencies[1].amount.max;
                minmonth = array["data"].auto[0].country.types[1].currencies[1].tenor.min;
                maxmonth = array["data"].auto[0].country.types[1].currencies[1].tenor.max;
                interestrate = array["data"].auto[0].country.types[1].currencies[1].interestRate;
                curen = array["data"].auto[0].country.types[0].currencies[1].code;
                curar = array["data"].auto[0].country.types[0].currencies[1].descriptions.ar;
            }
        }
    }

    public void loancalculator(string amount,string month,string country,string currency,string rate, string loantype, out string emiamt)
    {
        string firstvar = loantype;
        string secondvar = loantype;
        if(loantype=="new" || loantype == "used")
        {
            firstvar = "auto";
        }
        if (loantype == "housing" )
        {
            secondvar = "ready";
        }

        string savingrate2 = "https://api.arabbank.com/product/v1/loans/"+firstvar+"/calculators/"+secondvar+ "?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&amount=" + amount+"&tenor="+month+"&country="+country+"&currency="+currency+"&rate="+rate;
        string json2 = get_web_content(savingrate2);

        dynamic array2 = Newtonsoft.Json.JsonConvert.DeserializeObject(json2);

        string value = array2.data[0].monthlyInstallment;
        emiamt = value;
    }


    private void loadPersonalLoan()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        if (configObj.RetrieveCountry(Convert.ToInt64(hcountryId.Value)) < 0)
        {
            return;
        }

        //string FrmJD = configObj.PersonalLoanFrmJD.ToString();
        //string ToJD = configObj.PersonalLoanToJD.ToString();
        //string frmMnth = configObj.PersonalLoanFrmMnth.ToString();
        //string toMnth = configObj.PersonalLoanToMnth.ToString();
        //string PLinterest = configObj.PersonalLoanInterest.ToString();
        string loantype = "p";
        countryId = hcountryId.Value;
        loanparameter(countryId,loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate, out string curren, out string currar);

        string FrmJD = minamt;
        string ToJD = maxamt;
        string frmMnth = minmonth;
        string toMnth = maxmonth;
        string PLinterest = interestrate;


        Page.Header.Title = "Personal Loan";//Title

        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblPersonalLoan.Text");//Page Title Name

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Personal%20Loan%20-%20Refresher.jpg";//Slide Image1


        productName.Text = "Loan";//to know which popup shows
        subProductName.Text = "1";//to know which popup shows

        //countryId = Request.QueryString["cntry"];
        //branchId = Request.QueryString["bNam"];
        //discoveryBench = Request.QueryString["dBnch"];


        if (lang == "EN")
        {

            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnLoans/PersonalLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnLoans/PersonalLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            lblPoppLoanCalcuAmntLimit.Text = "(From "+curren+" " + FrmJD + " To "+curren+" " + ToJD + ")";//setting amount limit
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/PersonalLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/PersonalLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            lblPoppLoanCalcuAmntLimit.Text = "(من " + FrmJD + " دينار  الى " + ToJD + " دينار)";//setting amount limit
        }

        if (countryId == "2")
        {
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/AEPersonalLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/AEPersonalLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From AED " + FrmJD + " To AED " + ToJD + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/AEPersonalLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/AEPersonalLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " + FrmJD + " د.أ  الى " + ToJD + " د.أ)";//setting amount limit
            }
        }
        else if (countryId == "5")
        {
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/EGPersonalLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/EGPersonalLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From EGP " + FrmJD + " To EGP " + ToJD + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/EGPersonalLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/EGPersonalLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " + FrmJD + " جنيه مصري  الى " + ToJD + " جنيه مصري)";//setting amount limit
            }
        }
        else if (countryId == "3")
        {
            RadioButtonListcurrency.Visible = true;
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/PLPersonalLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/PLPersonalLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From EGP " + FrmJD + " To EGP " + ToJD + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/PLPersonalLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/PLPersonalLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " + FrmJD + " جنيه مصري  الى " + ToJD + " جنيه مصري)";//setting amount limit
            }
        }

        pName = "personalLoan";
        hpName.Value = pName;
        interestRate = Convert.ToDouble(PLinterest);
        hinterestRate.Value = interestRate.ToString();
        //////////////setting slider limit////////////////////////////////
        loanMonthSlider.Attributes["min"] = frmMnth;
        loanMonthSlider.Attributes["max"] = toMnth;
        lblLoanPoppMinVal.Text = frmMnth;
        lblLoanPoppMaxVal.Text = toMnth;
        ///////////////////////////////////////////////////////////////////

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "block");
    }
    private void loadAutoLoan()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        if (configObj.RetrieveCountry(Convert.ToInt64(hcountryId.Value)) < 0)
        {
            return;
        }

        //int FrmJD = Convert.ToInt32(configObj.autoLoanFrmJD);
        //int ToJD = Convert.ToInt32(configObj.autoLoanToJD);
        //string frmMnth = configObj.AutoLoanFrmMnth.ToString();
        //string toMnth = configObj.AutoLoanToMnth.ToString();


        Page.Header.Title = "Auto Loan";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblAutoLoan.Text");//Page Title Name

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Auto Loan - Existing Family in Car.jpg";//Slide Image1

        productName.Text = "Loan";//to know which popup shows
        subProductName.Text = "3";//to know which popup shows

        string loantype = "n";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate,out string curen, out string curar);

        string FrmJD = minamt;
        string ToJD = maxamt;
        string frmMnth = minmonth;
        string toMnth = maxmonth;
        string ALinterest = interestrate;

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnLoans/AutoLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnLoans/AutoLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            lblPoppLoanCalcuAmntLimit.Text = "(From JOD " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To JOD " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/Autoloan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/Autoloan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            lblPoppLoanCalcuAmntLimit.Text = "(من " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " دينار  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " دينار)";//setting amount limit
        }

        

        if (countryId == "2")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblAutoLoanDubai.Text");//Page Title Name
            loantype = "a";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen,out curar);

            FrmJD = minamt;
            ToJD = maxamt;
            frmMnth = minmonth;
            toMnth = maxmonth;
            ALinterest = interestrate;

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/AEAutoLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/AEAutoLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From AED " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To AED " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/AEAutoLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/AEAutoLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " د.أ  الى " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " د.أ)";//setting amount limit
            }
        }


        if (countryId == "5")
        {
            //ritSideImage1.Attributes["src"] = "";
            //productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblAutoLoanDubai.Text");//Page Title Name
            loantype = "a";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen, out curar);

            FrmJD = minamt;
            ToJD = maxamt;
            frmMnth = minmonth;
            toMnth = maxmonth;
            ALinterest = interestrate;

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/EGAutoLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/EGAutoLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From EGP " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To EGP " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/EGAutoLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/EGAutoLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " د.أ  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " د.أ)";//setting amount limit
            }
        }

        if (countryId == "3")
        {
            RadioButtonListcurrency.Visible = true;
            //ritSideImage1.Attributes["src"] = "";
            //productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblAutoLoanDubai.Text");//Page Title Name
            loantype = "a";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen, out curar);

            FrmJD = minamt;
            ToJD = maxamt;
            frmMnth = minmonth;
            toMnth = maxmonth;
            ALinterest = interestrate;

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/PLAutoLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/PLAutoLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From EGP " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To EGP " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/PLAutoLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/PLAutoLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " د.أ  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " د.أ)";//setting amount limit
            }
        }

        pName = "autoLoan";
        hpName.Value = pName;
        //////////////setting slider limit////////////////////////////////
        loanMonthSlider.Attributes["min"] = frmMnth;
        loanMonthSlider.Attributes["max"] = toMnth;
        lblLoanPoppMinVal.Text = frmMnth;
        lblLoanPoppMaxVal.Text = toMnth;
        ///////////////////////////////////////////////////////////////////
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "block");

    }
    private void loadHousingLoan()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        if (configObj.RetrieveCountry(Convert.ToInt64(hcountryId.Value)) < 0)
        {
            return;
        }


        //int FrmJD = Convert.ToInt32(configObj.housingLoanFrmJD);
        //int ToJD = Convert.ToInt32(configObj.housingLoanToJD);
        //string frmMnth = configObj.HousingLoanFrmMnth.ToString();
        //string toMnth = configObj.HousingLoanToMnth.ToString();
        //string HLinterest = configObj.HousingLoanInterest.ToString();

        string loantype = "h";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate, out string curen, out string curar);

        string FrmJD = minamt;
        string ToJD = maxamt;
        string frmMnth = minmonth;
        string toMnth = maxmonth;
        string HLinterest = interestrate;


        Page.Header.Title = "Housing Loan";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblHousingLoan.Text");

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Housing Loan - Refresh.jpg";//Slide Image1

        productName.Text = "Loan";//to know which popup shows
        subProductName.Text = "2";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnLoans/HousingLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnLoans/HousingLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            lblPoppLoanCalcuAmntLimit.Text = "(From JOD " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To JOD " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/HousingLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/HousingLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            lblPoppLoanCalcuAmntLimit.Text = "(من " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " دينار  الى " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " دينار)";//setting amount limit
        }

        if (countryId == "2")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblHousingLoanDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/AEHousingLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/AEHousingLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From AED " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To AED " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/AEHousingLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/AEHousingLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " د.أ  الى " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " د.أ)";//setting amount limit
            }
        }

        if (countryId == "3")
        {
            RadioButtonListcurrency.Visible = true;
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblHousingLoan.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/PLHousingLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/PLHousingLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(From AED " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To AED " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/PLHousingLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/PLHousingLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
                lblPoppLoanCalcuAmntLimit.Text = "(من " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " د.أ  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " د.أ)";//setting amount limit
            }
        }

        interestRate = Convert.ToDouble(HLinterest);
        hinterestRate.Value = interestRate.ToString();
        pName = "housingLoan";
        hpName.Value = pName;
        //////////////setting slider limit////////////////////////////////
        loanMonthSlider.Attributes["min"] = frmMnth;
        loanMonthSlider.Attributes["max"] = toMnth;
        lblLoanPoppMinVal.Text = frmMnth;
        lblLoanPoppMaxVal.Text = toMnth;
        ///////////////////////////////////////////////////////////////////

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "block");

    }
    private void loadResidentJordan()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;
        productName.Text = "Loan";//to know which popup shows
        subProductName.Text = "5";//to know which popup shows

        Page.Header.Title = "Non Resident Jordan Loan";//Title
        

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Non%20Residence%20Mortgage.jpg";//Slide Image1


        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblNonResidentJordanLoan.Text");//Page Title Name
        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnLoans/NonresidentJordanLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnLoans/NonresidentJordanLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/NonresidentJordanianLoan/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/NonresidentJordanianLoan/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }

        if(countryId=="2")
        {
            subProductName.Text = "35";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblNonResidentArabLoan.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnLoans/AENonresidentArabLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnLoans/AENonresidentArabLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productLoanArabicVersion/AENonresidentArabLoan/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productLoanArabicVersion/AENonresidentArabLoan/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/Products.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }

        }


        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");

    }

    //Accounts//////////////////////////////////////////////////////
    private void loadFixedDepositJD()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Fixed Deposit JD";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblFixedDepositJD.Text");//Page Title Name
                                                                                                       //"Fixed Deposit JOD";//Title Above SWF


        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Fixed%20Deposit%20JOD.jpg";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "6";//to know which popup shows


        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/FixedDepositJD/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/FixedDepositJD/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/FixedDepositJD/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/FixedDepositJD/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            subProductName.Text = "37";
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblfixedDepositAED.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/AEFixedDepositAED/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/AEFixedDepositAED/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/AEFixedDepositAED/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/AEFixedDepositAED/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        else if (countryId == "5")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/3YearsFloatingCertificateofDepositwithadailyinterest.png";//Slide Image1

            subProductName.Text = "43";
            productNameLbl.Text = (string)base.GetLocalResourceObject("3YearsFloatingCertificate.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EG3YearsFloatingCertificate/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EG3YearsFloatingCertificate/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EG3YearsFloatingCertificate/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EG3YearsFloatingCertificate/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        else if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Accounts/CurrentAccount.jpg";//Slide Image1

            subProductName.Text = "9";
            Page.Header.Title = "Current Account";//Title
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblCurrentAccount.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/PLCurrentAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/PLCurrentAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/PLCurrentAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/PLCurrentAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5" || countryId == "3")
        {
            btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
        else
        {
            btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadFixedDepositForeign()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

      

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Fixed%20Deposit%20foreign%20currency.jpg";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "8";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/FixedDepositForeign/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/FixedDepositForeign/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/FixedDepositForeignCurrencies/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/FixedDepositForeignCurrencies/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
           
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/AEFixedDepositForeign/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/AEFixedDepositForeign/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/AEFixedDepositForeign/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/AEFixedDepositForeign/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            //ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            //ritSideImage1.Attributes["src"] = "../images/CurrentaccountforAPElite1.png";//Slide Image1

            //productNameLbl.Text = (string)base.GetLocalResourceObject("CurrentaccountforAP.Text");
            //subProductName.Text = "44";
            //if (lang == "EN")
            //{
            //    if (ttype == "con")
            //    {
            //        swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGCurrentaccountforAP/book.swf";//Flash
            //    }
            //    else
            //    {
            //        swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGCurrentaccountforAP/book.swf";//Flash
            //    }
            //    backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            //}
            //else
            //{
            //    if (ttype == "con")
            //    {
            //        swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGCurrentaccountforAP/book.swf";//Flash
            //    }
            //    else
            //    {
            //        swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGCurrentaccountforAP/book.swf";//Flash
            //    }
            //    backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            //}
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/Current_account.jpg";//Slide Image1

            subProductName.Text = "9";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblCurrentAccount.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGCurrentAccounts/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGCurrentAccounts/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGCurrentAccounts/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGCurrentAccounts/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }

        }
        if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Accounts/SavingsAccount.jpg";//Slide Image1

            Page.Header.Title = "Savings Account";//Title
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblSavingsAccount.Text");//Page Title Name
            subProductName.Text = "10";
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/PLSavingsAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/PLSavingsAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/PLSavingsAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/PLSavingsAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadCurrentAccount()
    {
        

        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Current Account";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblCurrentAccount.Text");//Page Title Name
                                                                                                       //"Current Account";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Accounts.jpg";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "9";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/CurrentAccount/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/CurrentAccount/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/CurrentAccount/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/CurrentAccount/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/AECurrentAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/AECurrentAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/AECurrentAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/AECurrentAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            //ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            //ritSideImage1.Attributes["src"] = "../images/lpf3y.jpg";//Slide Image1
            //subProductName.Text = "45";
            //productNameLbl.Text = (string)base.GetLocalResourceObject("Fixed3yearscertificate.Text");//Page Title Name
            //if (lang == "EN")
            //{
            //    if (ttype == "con")
            //    {
            //        swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGFixed3yearscertificate/book.swf";//Flash
            //    }
            //    else
            //    {
            //        swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGFixed3yearscertificate/book.swf";//Flash
            //    }
            //    backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            //}
            //else
            //{
            //    if (ttype == "con")
            //    {
            //        swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGFixed3yearscertificate/book.swf";//Flash
            //    }
            //    else
            //    {
            //        swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGFixed3yearscertificate/book.swf";//Flash
            //    }
            //    backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            //}

            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/savingsaccount.jpg";//Slide Image1
            countryId = hcountryId.Value;
            branchId = hbranchId.Value;
            discoveryBench = hdiscoveryBench.Value;
            branchNumber = hbranchNumber.Value;
            ttype = httype.Value;

            Page.Header.Title = "Electronic Deposits";//Title
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblSavingsAccount.Text");//Page Title Name




            productName.Text = "Accounts";//to know which popup shows
            subProductName.Text = "10";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGSavingaccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGSavingaccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGSavingaccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGSavingaccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }



            btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
            btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Accounts/DepositAccount.jpg";//Slide Image1
            subProductName.Text = "53";
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblFixedDeposit.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/PLDepositAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/PLDepositAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/PLDepositAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/PLDepositAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadSavingsAccount()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Savings Account";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblSavingsAccount.Text");//Page Title Name
                                                                                                       //"Savings Account";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Savings Accounts.jpg";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "10";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/SavingsAccount/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/SavingsAccount/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/Savings/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/Savings/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/AESavingsAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/AESavingsAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/AESavingsAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/AESavingsAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/fixedfiveyears.png";//Slide Image1

            subProductName.Text = "46";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("Fixed5yearscertificate.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGFixed5yearscertificate/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGFixed5yearscertificate/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGFixed5yearscertificate/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGFixed5yearscertificate/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            subProductName.Text = "48";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lbleTawfeer.Text");//Page Title Name
            if (lang == "EN")
            {
                ritSideImage1.Attributes["src"] = "../images/palestine/Accounts/EtawfeerLeftImage.png";//Slide Image1
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/PLeTawfeer/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/PLeTawfeer/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                ritSideImage1.Attributes["src"] = "../images/palestine/Accounts/EtawfeerLeftImage.png";//Slide Image1
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/PLeTawfeer/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/PLeTawfeer/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadElectronicDepositsAccount()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Electronic Deposits";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblElectronicDeposits.Text");//Page Title Name
        

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/onlineDeposit.jpg";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "10020";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/ElectronicDeposit/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/ElectronicDeposit/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/ElectronicDeposit/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/ElectronicDeposit/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/Call Account.jpg";//Slide Image1

            subProductName.Text = "36";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblCallAccount.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/AECallAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/AECallAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/AECallAccount/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/AECallAccount/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/Current_account.jpg";//Slide Image1

            subProductName.Text = "9";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblCurrentAccount.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGCurrentAccounts/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGCurrentAccounts/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGCurrentAccounts/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGCurrentAccounts/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    private void loadNewButton1()
    {
        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/savingsaccount.jpg";//Slide Image1
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Electronic Deposits";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblSavingsAccount.Text");//Page Title Name


       

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "10";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGSavingaccount/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGSavingaccount/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGSavingaccount/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGSavingaccount/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    private void loadNewButton2()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Electronic Deposits";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("SavingAccountforElite.Text");//Page Title Name


        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/savingaccountforelitearabipremium.png";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "47";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/EGSavingAccountforElite/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/EGSavingAccountforElite/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/EGSavingAccountforElite/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/EGSavingAccountforElite/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }



        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    private void loadNewButton3()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "eTawfeer";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("lbleTawfeer.Text");//Page Title Name


        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        if (lang == "EN")
            ritSideImage1.Attributes["src"] = "../images/etawen.jpg";//Slide Image1
        else
            ritSideImage1.Attributes["src"] = "../images/etawar.jpg";//Slide Image1

        productName.Text = "Accounts";//to know which popup shows
        subProductName.Text = "48";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnAccounts/eTawfeer/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnAccounts/eTawfeer/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productAccountsArabicVersion/eTawfeer/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productAccountsArabicVersion/eTawfeer/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsAccounts.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }



        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    //Cards//////////////////////////////////////////////////////////
    private void loadVisaDebitCard()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Visa Debit Card";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaDebitCard.Text");//Page Title Name
                                                                                                      //"Visa Debit Card";//Title Above SWF


        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/visa_debitcard_jornew.jpg";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "11";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaDebitCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaDebitCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaDebitCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaDebitCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaDebitCarddubai.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaidebit.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEVisaDebitCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEVisaDebitCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEVisaDebitCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEVisaDebitCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }


        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblInternetShoppingCard.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/InternetShoppingCard.jpg";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "12";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLInternetShoppingCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLInternetShoppingCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLInternetShoppingCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLInternetShoppingCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }


    private void loadShababVisaCreditCard()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Shabab Visa Credit Card";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblShababVisaCreditCard.Text");//Page Title Name
                                                                                                      //"Visa Debit Card";//Title Above SWF
        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/shababvisacreditcard_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "51";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/ShababVisaCreditCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/ShababVisaCreditCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/ShababVisaCreditCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/ShababVisaCreditCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    private void loadShababVisaDebitCard()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Shabab Visa Debit Card";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblShababVisaDebitCard.Text");//Page Title Name
                                                                                                             //"Visa Debit Card";//Title Above SWF
        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/shababvisadebitcard_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "50";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/ShababVisaDebitCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/ShababVisaDebitCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/ShababVisaDebitCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/ShababVisaDebitCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    private void loadVisaTravelMateCC()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Visa Travel Mate Credit Card";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaTravelMateCC.Text");//Page Title Name
                                                                                                             //"Visa Debit Card";//Title Above SWF
        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/visatravelmatecreditcard_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "52";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaTravelMateCC/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaTravelMateCC/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaTravelMateCC/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaTravelMateCC/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }


    private void loadInternetShoppingCard()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Internet Shopping Card";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblInternetShoppingCard.Text");//Page Title Name
                                                                                                             //"Internet Shopping Card";//Title Above SWF


        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/internetshoppingcard_jor_new.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "12";//to know which popup shows


        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/InternetShoppingCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/InternetShoppingCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/InternetCards/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/InternetCards/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaiinternet card.png";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEInternetShoppingCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEInternetShoppingCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEInternetShoppingCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEInternetShoppingCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "5")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_Internetshopping.png";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGInternet shoppingCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGInternet shoppingCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGInternet shoppingCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGInternet shoppingCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/TMC.png";//Slide Image1
            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "19";//to know which popup shows
            Page.Header.Title = "Internet Shopping Card";//Title
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblMasterCardTitanium.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLTMC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLTMC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLTMC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLTMC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadVisaSilver()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Visa Silver";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaSilver.Text");//Page Title Name
                                                                                                   //"Arab Bank Visa Silver Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Silver%20Credit%20Card.jpg";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "13";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaSilver/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaSilver/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaSilver/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaSilver/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
        if (countryId == "2")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaSilverDubai.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaisilver.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEVisaSilver/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEVisaSilver/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEVisaSilver/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEVisaSilver/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "5")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblVisaElectron.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_visaElectron.jpg";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGVisaElectron/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGVisaElectron/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGVisaElectron/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGVisaElectron/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblShababVisaCreditCard.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/ShababVisaCreditCard.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "51";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLShababVisaCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLShababVisaCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLShababVisaCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLShababVisaCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadVisaGoldCC()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Visa Gold CC";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaGoldCC.Text");//Page Title Name
                                                                                                   //"Arab Bank Visa Gold Credit Card";//Title Above SWF


        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/visagold_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "15";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaGoldCC/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaGoldCC/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaGoldCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaGoldCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaGoldCCdubai.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaigold.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEVisaGoldCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEVisaGoldCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEVisaGoldCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEVisaGoldCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaGoldCCdubai.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_gold.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGVisaGoldCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGVisaGoldCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGVisaGoldCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGVisaGoldCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblRJPlatinumCreditCard.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/RJCreditCard.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "18";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLRJCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLRJCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLRJCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLRJCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadVisaBlackCC()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Visa Black CC";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaBlackCC.Text");//Page Title Name
                                                                                                    //"Arab Bank Visa Black Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/visablack_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "14";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaBlackCC/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaBlackCC/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaBlack/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaBlack/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "5")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_visablackleft.png";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGVisaBlackCreditcard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGVisaBlackCreditcard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGVisaBlackCreditcard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGVisaBlackCreditcard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaPlatinumCCVisaPlatinumCC.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaPlatinum.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "20";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaPlatinumCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaPlatinumCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaPlatinumCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaPlatinumCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadZainVisa()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "ZainVisa";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblZainVisa.Text");//Page Title Name
                                                                                                 //"Arab Bank - Zain Visa Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/AB-Zain%20Cobranded%20Card.jpg";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "17";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/ZainVisa/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/ZainVisa/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/ZainVisa/book.swf";//Flashh
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/ZainVisa/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if(countryId=="2")
        {
            subProductName.Text = "38";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblVisaElectron.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEVisaElectron/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEVisaElectron/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEVisaElectron/book.swf";//Flashh
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEVisaElectron/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        else if (countryId == "3")
        {
            subProductName.Text = "13";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaSilver.Text");//Page Title Name

            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaSilver.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaSilver/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaSilver/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaSilver/book.swf";//Flashh
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaSilver/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadRJCreditCard()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "RJCreditCard";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblRJCreditCard.Text");//Page Title Name
                                                                                                     //"Arab Bank - Royal Jordanian Visa Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/rjcreditcard_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "18";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/RJCreditCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/RJCreditCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/RJCreditCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/RJCreditCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaTravelMateCC.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaTravelMateCreditCard.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "52";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaTravelMateCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaTravelMateCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaTravelMateCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaTravelMateCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadMasterCardTitanium()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "MasterCardTitanium";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblMasterCardTitanium.Text");//Page Title Name
                                                                                                           //"Arab Bank MasterCard Titanium Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/mastercardtitanium_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "19";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/MasterCardTitanium/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/MasterCardTitanium/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/MasterCardTitanium/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/MasterCardTitanium/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
        if (countryId == "2")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblMasterCardTitaniumDubai.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaititanium.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEMTCInfo/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEMTCInfo/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEMTCInfo/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEMTCInfo/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblMasterCardTitaniumDubai.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_titan.jpg";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGTMC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGTMC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGTMC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGTMC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaSignatureCreditCard.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaSignature.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "23";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaSignatureCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaSignatureCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaSignatureCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaSignatureCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadVisaPlatinumCC()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "VisaPlatinumCCVisaPlatinumCC";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaPlatinumCCVisaPlatinumCC.Text");//Page Title Name
                                                                                                                     //"Arab Bank Visa Platinum Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/visaplatinum_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "20";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaPlatinumCC/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaPlatinumCC/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaPlatinum/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaPlatinum/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaiplatinum.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEVisaPlatinumCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEVisaPlatinumCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEVisaPlatinumCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEVisaPlatinumCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "5")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_visaplatinum.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGVisaPlatinum/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGVisaPlatinum/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGVisaPlatinum/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGVisaPlatinum/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaGold.png";//Slide Image1
            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "15";//to know which popup shows
            Page.Header.Title = "Visa Gold Credit Card";//Title
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaGoldCC.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaGoldCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaGoldCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaGoldCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaGoldCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadTogetherPlatinumCC()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Together Platinum CC";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblTogetherPlatinumCC.Text");//Page Title Name
                                                                                                           //"Together Platinum Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/togetherplatinum_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "21";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/TogetherPlatinumCC/book.swf";
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/TogetherPlatinumCC/book.swf";
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/TogetherPlatinum/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/TogetherPlatinum/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
        if (countryId == "3")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaDebitCard.jpg";//Slide Image1
            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "11";//to know which popup shows
            Page.Header.Title = "Visa Debit Card";//Title
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaDebitCard.Text");//Page Title Name
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaDebitCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaDebitCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaDebitCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaDebitCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadNashamaVisaCredit()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "NashamaVisaCredit";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblNashamaVisaCredit.Text");//Page Title Name
                                                                                                          //"Nashama Visa Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/nashamavisa creditcard_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "22";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/NashamaVisaCredit/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/NashamaVisaCredit/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/Nashama/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/Nashama/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblWorldMaster.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/WorldMaster.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "57";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLWorldMaster/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLWorldMaster/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLWorldMaster/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLWorldMaster/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadVisaSignatureCreditCard()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "VisaSignatureCreditCard";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaSignatureCreditCard.Text");//Page Title Name
                                                                                                                //"Arab Bank Visa Signature Credit Card";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/visasignature_jornew.png";//Slide Image1

        productName.Text = "Cards";//to know which popup shows
        subProductName.Text = "23";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnCards/VisaSignatureCreditCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnCards/VisaSignatureCreditCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/VisaSignatureCard/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/VisaSignatureCard/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/dubaisignature.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/AEVisaSignatureCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/AEVisaSignatureCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/AEVisaSignatureCreditCard/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/AEVisaSignatureCreditCard/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
        }
        if (countryId == "5")
        {
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/eg_visasignature.png";//Slide Image1

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/EGVisaSignature/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/EGVisaSignature/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/EGVisaSignature/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/EGVisaSignature/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblVisaBlackCC.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/Cards/VisaBlack.png";//Slide Image1

            productName.Text = "Cards";//to know which popup shows
            subProductName.Text = "14";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnCards/PLVisaBlackCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnCards/PLVisaBlackCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/productCardsArabicVersion/PLVisaBlackCC/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/productCardsArabicVersion/PLVisaBlackCC/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsCards.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }


    //Bancassurance///////////////////////////////////////////////////
    private void loadLammaYek_flyer()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Lamma Yek_flyer";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblLammaYek_flyer.Text");//Page Title Name
                                                                                                       //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Bancassurance-%20Lama%20Yikbaro.jpg";//Slide Image1

        productName.Text = "Bancassurance";//to know which popup shows
        subProductName.Text = "24";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/LammaYek_flyer/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/LammaYek_flyer/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/lamma/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/lamma/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblHasadElOmor.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/BancAssurance/HasadElOmor.png";//Slide Image1

            productName.Text = "Bancassurance";//to know which popup shows
            subProductName.Text = "55";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/PLHasadElOmor/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/PLHasadElOmor/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/PLHasadElOmor/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/PLHasadElOmor/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }


        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadJanaElOmor()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "JanaElOmor";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblJanaElOmor.Text");//Page Title Name
                                                                                                   //"Jana El Omr - Al-Arabi for Retirement Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Bancassurance-%20Jana%20al%20omor.jpg";//Slide Image1

        productName.Text = "Bancassurance";//to know which popup shows
        subProductName.Text = "25";//to know which popup shows


        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/JanaElOmor/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/JanaElOmor/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/janal/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/janal/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblHattaYenjaho.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/BancAssurance/HattaYenjaho.png";//Slide Image1

            productName.Text = "Bancassurance";//to know which popup shows
            subProductName.Text = "56";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/PLHattaYenjaho/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/PLHattaYenjaho/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/PLHattaYenjaho/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/PLHattaYenjaho/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadAaelatyBiAman()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "AaelatyBiAman";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblAaelatyBiAman.Text");//Page Title Name
                                                                                                      //"Aaelaty Bi Aman - Al-Arabi for Term Life Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Bancassurance-%20Ailt%20be%20Aman.jpg";//Slide Image1

        productName.Text = "Bancassurance";//to know which popup shows
        subProductName.Text = "26";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/AaelatyBiAman/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/AaelatyBiAman/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/aelaty/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/aelaty/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblAutoInsurance.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/BancAssurance/LeftImage.png";//Slide Image1

            productName.Text = "Bancassurance";//to know which popup shows
            subProductName.Text = "58";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/PLAutoInsurance/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/PLAutoInsurance/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/PLAutoInsurance/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/PLAutoInsurance/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadHattaYedersou()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Hatta Yedersou";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblHattaYedersou.Text");//Page Title Name
                                                                                                      //"Hatta Yedersou - Al-Arabi for Schooling Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Bancassurance-%20hatta%20yodros.jpg";//Slide Image1

        productName.Text = "Bancassurance";//to know which popup shows
        subProductName.Text = "33";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/HattaYedersou/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/HattaYedersou/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/hatta/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/hatta/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadRahetEIBal()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Rahet EIBal";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblRahetEIBal.Text");//Page Title Name
                                                                                                   //"Rahet El-Bal - Al-Arabi for Personal Accident Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Bancassurance-%20Rahit%20al%20bal.jpg";//Slide Image1

        productName.Text = "Bancassurance";//to know which popup shows
        subProductName.Text = "31";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/RahetEIBal/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/RahetEIBal/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/rahet/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/rahet/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblTravelAccidentInsurance.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/BancAssurance/Tranvelinsurance.png";//Slide Image1

            productName.Text = "Bancassurance";//to know which popup shows
            subProductName.Text = "59";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/PLTravelAccidentInsurance/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/PLTravelAccidentInsurance/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/PLTravelAccidentInsurance/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/PLTravelAccidentInsurance/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadCritical_Illness()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Critical Illness";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblCriticalIllness.Text");//Page Title Name
                                                                                                        //"Critical Illness";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Bancassurance-%20Critical%20Illness.jpg";//Slide Image1

        productName.Text = "Bancassurance";//to know which popup shows
        subProductName.Text = "34";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/Critical_Illness/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/Critical_Illness/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/Critical_Illness/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/Critical_Illness/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "3")
        {
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblBalakMerta.Text");//Page Title Name
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/BancAssurance/BalakMerta7.png";//Slide Image1

            productName.Text = "Bancassurance";//to know which popup shows
            subProductName.Text = "54";//to know which popup shows

            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnBanacasurance/PLBalakMerta/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnBanacasurance/PLBalakMerta/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/prdctBanacasuranceArbcVer/PLBalakMerta/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/prdctBanacasuranceArbcVer/PLBalakMerta/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/ProductsBancassurance.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    //RelationShipProgram///////////////////////////////////////////////////

    private void loadElite()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Elite";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblElite.Text");//Page Title Name
                                                                                              //Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Elitecar.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "10013";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/Elite/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/Elite/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/Elite/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/Elite/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if(countryId=="2")
        {
            subProductName.Text = "39";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblEliteDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/AEElite/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/AEElite/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/AEElite/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/AEElite/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        else if (countryId == "5")
        {
            subProductName.Text = "39";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblEliteDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/EGElite/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/EGElite/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/EGElite/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/EGElite/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        else if (countryId == "3")
        {
            subProductName.Text = "41";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiCrossBoardersDubai.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/CrossBorder.jpg";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLCrossBorder/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLCrossBorder/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLCrossBorder/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLCrossBorder/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadArabiPremium()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "ArabiPremium";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblArabiPremium.Text");//Page Title Name
                                                                                                     //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Arabi PremiumRefresher.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "10015";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/Arabi Premium/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/Arabi Premium/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/ArabicPremium/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/ArabicPremium/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            subProductName.Text = "40";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/AEArabiPremium/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/AEArabiPremium/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
        else
        {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/AEArabiPremium/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/AEArabiPremium/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        else if (countryId == "5")
        {
            subProductName.Text = "40";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/EGArabipremium/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/EGArabipremium/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/EGArabipremium/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/EGArabipremium/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        else if (countryId == "3")
        {
            subProductName.Text = "42";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiExtraDubai.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/ArabiExtraProgram.jpg";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLArabiExtraProgram/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLArabiExtraProgram/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLArabiExtraProgram/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLArabiExtraProgram/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadArabiCrossBoarders()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "ArabiCrossBoarders";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblArabiCrossBoarders.Text");//Page Title Name
                                                                                                           //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/CrossBorder.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "10016";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/CrossBorder/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/CrossBorder/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/CrossBorder/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/CrossBorder/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "2")
        {
            subProductName.Text = "41";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiCrossBoardersDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/AECrossBorder/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/AECrossBorder/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/AECrossBorder/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/AECrossBorder/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        else if (countryId == "5")
        {
            subProductName.Text = "41";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiCrossBoardersDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/EGArabiCrossBorder/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/EGArabiCrossBorder/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/EGArabiCrossBorder/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/EGArabiCrossBorder/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
        }

        else if (countryId == "3")
        {
            subProductName.Text = "40";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblArabiPremium.Text");

            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/ArabiPremium.jpg";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLArabicPremium/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLArabicPremium/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLArabicPremium/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLArabicPremium/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadArabiExtra()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "ArabiExtra";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblArabiExtra.Text");//Page Title Name
                                                                                                   //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/ArabiExtraAccount.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "10017";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/ArabiExtraProgram/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/ArabiExtraProgram/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/ArabiExtraProgram/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/ArabiExtraProgram/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
        if (countryId == "2")
        {
            subProductName.Text = "42";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiExtraDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/AEArabiExtraProgram/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/AEArabiExtraProgram/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/AEArabiExtraProgram/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/AEArabiExtraProgram/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        else if (countryId == "5")
        {
            subProductName.Text = "42";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("lblArabiExtraDubai.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/EGArabiExtraProgram/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/EGArabiExtraProgram/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/EGArabiExtraProgram/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/EGArabiExtraProgram/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        else if (countryId == "3")
        {
            subProductName.Text = "10013";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblElite.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/Elite.jpg";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLElite/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLElite/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLElite/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLElite/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadShabab()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Shabab";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblShabab.Text");//Page Title Name
                                                                                               //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/Shababslefie.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "10018";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/Shahab/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/Shahab/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/Shahab/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/Shahab/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "5")
        {
            subProductName.Text = "10018";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblShabab.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/EGShabab/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/EGShabab/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/EGShabab/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/EGShabab/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "3")
        {
            subProductName.Text = "49";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblArabiJunior.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/ArabiJunior.jpg";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLArabiJunior/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLArabiJunior/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLArabiJunior/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLArabiJunior/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    private void loadJeelAlArabi()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Arabi Junior";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblArabiJunior.Text");//Page Title Name
                                                                                                    //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/LeftImage.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "48";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/ArabiJunior/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/ArabiJunior/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/ArabiJunior/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/ArabiJunior/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }

        if (countryId == "5")
        {
            ritSideImage1.Attributes["src"] = "../images/JAARefresherKitchen.jpg";//Slide Image1
            subProductName.Text = "10019";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblJeelAlArabi.Text");
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/EGJeelAlArabi/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/EGJeelAlArabi/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/EGJeelAlArabi/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/EGJeelAlArabi/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }
        if (countryId == "3")
        {
            ritSideImage1.Attributes["src"] = "../images/JAARefresherKitchen.jpg";//Slide Image1
            subProductName.Text = "10018";//to know which popup shows
            productNameLbl.Text = (string)base.GetLocalResourceObject("productNameLblShabab.Text");
            ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
            ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/Shabab.jpg";//Slide Image1
            if (lang == "EN")
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLShahab/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLShahab/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
            }
            else
            {
                if (ttype == "con")
                {
                    swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLShahab/book.swf";//Flash
                }
                else
                {
                    swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLShahab/book.swf";//Flash
                }
                backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

            }
        }

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    private void loadTabeebPlus()
    {
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        Page.Header.Title = "Tabeeb Plus";//Title
        productNameLbl.Text = (string)base.GetLocalResourceObject("lblTabeebPlus.Text");//Page Title Name
                                                                                                    //"Lamma Yekbarou - Al-Arabi for Education Insurance";//Title Above SWF

        ritSideImage1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");//showing slide image1
        ritSideImage1.Attributes["src"] = "../images/palestine/RelationshipPrograms/TabeebPlus.jpg";//Slide Image1

        productName.Text = "RelationShipProgram";//to know which popup shows
        subProductName.Text = "60";//to know which popup shows

        if (lang == "EN")
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletEN/Cons/EnrelationShip/PLTabeebPlus/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletEN/Disc/EnrelationShip/PLTabeebPlus/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton
        }
        else
        {
            if (ttype == "con")
            {
                swfNAme.Text = "../LeafletAR/Cons/relationShipArbicVersion/PLTabeebPlus/book.swf";//Flash
            }
            else
            {
                swfNAme.Text = "../LeafletAR/Disc/relationShipArbicVersion/PLTabeebPlus/book.swf";//Flash
            }
            backBtnImg.NavigateUrl = "~/RelationShipProgram.aspx?lang=ar-EG&cntry=" + countryId + "&dBnch=" + discoveryBench + "&bNam=" + branchId + "&ttype=" + ttype + "";//BackButton

        }
       

        btnhousingDepositcalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
        btnhousingloancalculator.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    //For Loan calculator///////////////////////////////////////////////////////////////////
    private void getFormData()
    {
        this.Form.DefaultButton = this.calculateButton.UniqueID;
        lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        calcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        countryId = hcountryId.Value;
        branchId = hbranchId.Value;
        discoveryBench = hdiscoveryBench.Value;
        branchNumber = hbranchNumber.Value;
        ttype = httype.Value;

        if (configObj.RetrieveCountry(Convert.ToInt64(hcountryId.Value)) < 0)
        {
            return;
        }

        string loantype = "p";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate, out string curen, out string curar);

        //string frmMnth = minmonth;
        //string toMnth = maxmonth;
        //string ALinterest = interestrate;

        int personalLoanFrmJD = Convert.ToInt32(minamt);
        int personalLoanToJD = Convert.ToInt32(maxamt);

        if (countryId == "2")
        {
            loantype = "a";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen,out curar);
        }
        else if (countryId == "1")
        {
            loantype = "n";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen,out curar);
        }
        else if (countryId == "5")
        {
            //loantype = "n";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen, out curar);
        }

        if (hpName.Value == "autoLoan")
        {
            if (countryId == "3")
            {
                loantype = "a";
                loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen, out curar);
            }
        }

        int autoloanFrmJD = Convert.ToInt32(minamt);
        int autoloanToJD = Convert.ToInt32(maxamt);


        if (loanNewUsedBtn.Checked == true)
        {
            loantype = "u";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen, out curar);

            autoloanFrmJD = Convert.ToInt32(minamt);
            autoloanToJD = Convert.ToInt32(maxamt);
        }
        int housingLoanFrmJD = 0;
        int housingLoanToJD = 0;

        if (hpName.Value == "housingLoan")
        {
            loantype = "h";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate, out curen, out curar);

            housingLoanFrmJD = Convert.ToInt32(minamt);
            housingLoanToJD = Convert.ToInt32(maxamt);
        }

        if (loanAmountTextBox.Text != "")
        {
            calcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            int LoanTxtValue = Convert.ToInt32(Server.HtmlEncode(loanAmountTextBox.Text));
            if (hpName.Value == "personalLoan")
            {
                interestRate = Convert.ToDouble(hinterestRate.Value);
                if (LoanTxtValue >= personalLoanFrmJD && LoanTxtValue <= personalLoanToJD)
                {
                    lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                    loanPaymentAmountLabel.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else
                {
                    loanPaymentAmountLabel.Style.Add(HtmlTextWriterStyle.Display, "none");
                    lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "block");

                    if (lang == "EN")
                    {
                        lblPoppLCalcuLimitAlert.Text = "Amount should be from JOD " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to JOD " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US"));
                    }
                    else
                    {
                        lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  دينار أردني الى  " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US")) + "  دينار أردني";
                    }
                    
                    if (countryId == "2")
                    {
                        if (lang == "EN")
                        {
                            
                            lblPoppLCalcuLimitAlert.Text = "Amount should be from AED " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to AED " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  د.أ الى  " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US")) + "  د.أ";
                        }
                    }
                    if (countryId == "5")
                    {
                        if (lang == "EN")
                        {

                            lblPoppLCalcuLimitAlert.Text = "Amount should be from EGP " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to EGP " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  د.أ الى  " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US")) + "  د.أ";
                        }
                    }

                    if (countryId == "3")
                    {
                        if (RadioButtonListcurrency.SelectedIndex == 1)
                        {
                            if (lang == "EN")
                            {

                                lblPoppLCalcuLimitAlert.Text = "Amount should be from USD " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to USD " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US"));
                            }
                            else
                            {
                                lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(personalLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  دولار الى  " + Convert.ToDouble(personalLoanToJD).ToString("N", new CultureInfo("en-US")) + "  دولار";
                            }
                        }
                    }

                    return;
                }

            }
            else if (hpName.Value == "autoLoan")
            {
                if (LoanTxtValue >= autoloanFrmJD && LoanTxtValue <= autoloanToJD)
                {
                    lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                    loanPaymentAmountLabel.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else
                {
                    loanPaymentAmountLabel.Style.Add(HtmlTextWriterStyle.Display, "none");
                    lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "block");

                    if (lang == "EN")
                    {
                        lblPoppLCalcuLimitAlert.Text = "Amount should be from JOD " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + " to JOD " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US"));
                    }
                    else
                    {
                        lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + "  دينار أردني الى  " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US")) + "  دينار أردني";
                    }
                    if (countryId == "2")
                    {
                        if (lang == "EN")
                        {
                            lblPoppLCalcuLimitAlert.Text = "Amount should be from AED " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + " to AED " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + "  د.أ الى  " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US")) + "  د.أ";
                        }
                    }
                    else if (countryId == "5")
                    {
                        if (lang == "EN")
                        {
                            lblPoppLCalcuLimitAlert.Text = "Amount should be from EGP " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + " to EGP " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + "  د.أ الى  " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US")) + "  د.أ";
                        }
                    }
                    if (countryId == "3")
                    {
                        if (RadioButtonListcurrency.SelectedIndex == 1)
                        {
                            if (lang == "EN")
                            {

                                lblPoppLCalcuLimitAlert.Text = "Amount should be from USD " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + " to USD " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US"));
                            }
                            else
                            {
                                lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(autoloanFrmJD).ToString("N", new CultureInfo("en-US")) + "  دولار الى  " + Convert.ToDouble(autoloanToJD).ToString("N", new CultureInfo("en-US")) + "  دولار";
                            }
                        }
                    }
                    return;
                }

            }
            else
            {
                interestRate = Convert.ToDouble(hinterestRate.Value);
                if (LoanTxtValue >= housingLoanFrmJD && LoanTxtValue <= housingLoanToJD)
                {
                    lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                    loanPaymentAmountLabel.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
                else
                {
                    loanPaymentAmountLabel.Style.Add(HtmlTextWriterStyle.Display, "none");
                    lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "block");


                    if (lang == "EN")
                    {
                        lblPoppLCalcuLimitAlert.Text = "Amount should be from JOD " + Convert.ToDouble(housingLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to JOD " + Convert.ToDouble(housingLoanToJD).ToString("N", new CultureInfo("en-US"));
                    }
                    else
                    {
                        lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(housingLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  دينار أردني الى  " + Convert.ToDouble(housingLoanToJD).ToString("N", new CultureInfo("en-US")) + "  دينار أردني";
                    }
                    if (countryId == "2")
                    {
                        if (lang == "EN")
                        {
                            lblPoppLCalcuLimitAlert.Text = "Amount should be from AED " + Convert.ToDouble(housingLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to AED " + Convert.ToDouble(housingLoanToJD).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(housingLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  د.أ الى  " + Convert.ToDouble(housingLoanToJD).ToString("N", new CultureInfo("en-US")) + "  د.أ";
                        }
                    }
                    if (countryId == "3")
                    {
                        if (RadioButtonListcurrency.SelectedIndex == 1)
                        {
                            if (lang == "EN")
                            {

                                lblPoppLCalcuLimitAlert.Text = "Amount should be from USD " + Convert.ToDouble(housingLoanFrmJD).ToString("N", new CultureInfo("en-US")) + " to USD " + Convert.ToDouble(housingLoanToJD).ToString("N", new CultureInfo("en-US"));
                            }
                            else
                            {
                                lblPoppLCalcuLimitAlert.Text = "يجب أن يكون مبلغ القرض من  " + Convert.ToDouble(housingLoanFrmJD).ToString("N", new CultureInfo("en-US")) + "  دولار الى  " + Convert.ToDouble(housingLoanToJD).ToString("N", new CultureInfo("en-US")) + "  دولار";
                            }
                        }
                    }
                    return;
                }

            }
            //Method variables used to store converted hidden field data.
            double loanAmount = 0.0;
            //double interestRate = 8.75;
            int numberOfPayments = 0;

            //Resetting the required fields.
            loanAmountRequiredLabel.Visible = false;
            interestRateRequiredLabel.Visible = false;
            numberOfPaymentsRequiredLabel.Visible = false;

            //Checking to make sure something was typed in the loan amount textbox.
            if (loanAmountTextBox.Text.Trim().Length > 0)
            {
                //Saving the loan amount textbox entry to the hidden field value.
                loanAmountHiddenField.Value = Server.HtmlEncode(loanAmountTextBox.Text);

                //Trying to convert the hidden field value. If not notifying the user.
                if (!double.TryParse(loanAmountHiddenField.Value, out loanAmount))
                {
                    loanAmountRequiredLabel.Visible = true;
                    _isError = true;
                }
            }
            //Displaying the error user notification if nothing is in the loan amount textbox.
            else
            {
                loanAmountRequiredLabel.Visible = true;
                _isError = true;
            }

            //interestRateHiddenField.Value = "7.25";

            if (sliderOuputTxtBx.Text.Length > 0)
            {
                //Saving the textbox entry to the hidden field value.
                numberOfPaymentsHiddenField.Value = sliderOuputTxtBx.Text;

                //Trying to convert the hidden field value. If not notify the user.
                if (!int.TryParse(numberOfPaymentsHiddenField.Value, out numberOfPayments))
                {
                    numberOfPaymentsRequiredLabel.Visible = true;
                    _isError = true;
                }
            }

            //Displaying the error user notification if nothing is in the number of payments textbox.
            else
            {
                numberOfPaymentsRequiredLabel.Visible = true;
                _isError = true;
            }

            //If there are no errors from the form entries the proceed with the calculations.
            if (!_isError)
            {
                //Calculating the loan payment amount, rounding at the last 2 digits, and storing the value in the hidden field.

                if (hpName.Value == "personalLoan")
                {
                    loanPaymentAmountHiddenField.Value = Math.Round(calculatePaymentForPersonal(interestRate, loanAmount, numberOfPayments), 2).ToString();
                }
                else if (hpName.Value == "autoLoan")
                {
                    loanPaymentAmountHiddenField.Value = Math.Round(calculatePaymentForAuto(interestRate, loanAmount, numberOfPayments), 2).ToString();
                }
                else
                {
                    loanPaymentAmountHiddenField.Value = Math.Round(calculatePaymentForHousing(interestRate, loanAmount, numberOfPayments), 2).ToString();
                }

                //loanPaymentAmountHiddenField.Value = Math.Round(calculatePayment(interestRate, loanAmount, numberOfPayments), 2).ToString();
                //Displaying the loan payment amount from the hidden field value.

                loanPaymentAmountLabel.Visible = true;
                if (lang == "EN")
                {
                  
                    loanPaymentAmountLabel.Text = "Monthly Payment Amount: JOD " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                }
                else
                {
                    loanPaymentAmountLabel.Text = "مبلغ الدفعه الشهريه " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                }

                if(hcountryId.Value=="2")
                {
                    if (lang == "EN")
                    {
                        loanPaymentAmountLabel.Text = "Monthly Payment Amount: AED " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                    }
                    else
                    {
                        loanPaymentAmountLabel.Text = "مبلغ الدفعه الشهريه " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                    }
                }

                if (hcountryId.Value == "5")
                {
                    if (lang == "EN")
                    {
                        loanPaymentAmountLabel.Text = "Monthly Payment Amount: EGP " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                    }
                    else
                    {
                        loanPaymentAmountLabel.Text = "مبلغ الدفعه الشهريه " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                    }
                }
                if (hcountryId.Value == "3")
                {
                    if (RadioButtonListcurrency.SelectedIndex == 0)
                    {
                        if (lang == "EN")
                        {
                            loanPaymentAmountLabel.Text = "Monthly Payment Amount: JOD " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            loanPaymentAmountLabel.Text = "مبلغ الدفعه الشهريه " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                    }
                    else
                    {
                        if (lang == "EN")
                        {
                            loanPaymentAmountLabel.Text = "Monthly Payment Amount: USD " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            loanPaymentAmountLabel.Text = "مبلغ الدفعه الشهريه " + Convert.ToDouble(loanPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                    }

                }

            }
        }
        else
        {
            calcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
            return;
        }
    }

    //For Deposit calculator//////////////////////////////////////////
    public double Get_Rate()
    {
        int count = 0;
        dynamic Result = "";
        try
        {
            string gettermsrate = BusinessObject.GetrateAPI()+ "&type=term";
            string json = get_web_content(gettermsrate);

            dynamic array = JsonConvert.DeserializeObject(json);
            Result = array.result;
            count = Result.Count;
        }
        catch
        {
            return 0;
        }

        string selectedMonth = depositDropDownList1.SelectedValue;

        string sMonth;
        string mnth;
        long fromAmount;
        long toAmount;
        double loanAmount = 0.0;
        dynamic rate;

        if (selectedMonth == "1")
        {
            sMonth = selectedMonth + " Month";
        }
        else if (selectedMonth == "12")
        {
            sMonth = "1 Year";
        }
        else
        {
            sMonth = selectedMonth + " Months";
        }
        if (depositAmountTextBox.Text != "")
        {
            loanAmount = Convert.ToDouble(Server.HtmlEncode(depositAmountTextBox.Text));
        }


        for (int i = 0; i < count; i++)
        {
            fromAmount = Result[i].range.from.amount;
            toAmount = Result[i].range.to.amount;
            rate = Result[i].rate;
            mnth = Result[i].duration;

            if ((loanAmount >= fromAmount && loanAmount <= toAmount) && sMonth == mnth)
            {
                rateAmount = rate;
                break;
            }
        }
        return rateAmount;
    }
    public double Get_SavingsRate()
    {
        string savingrate = BusinessObject.GetrateAPI()+ "&type=saving";
        string json = get_web_content(savingrate);

        dynamic array = JsonConvert.DeserializeObject(json);
        dynamic Result = array.result;
        int count = Result.Count;

        string selectedMonth = depositDropDownList1.SelectedValue;

        string sMonth;
        //string mnth;
        long fromAmount;
        long toAmount;
        double depositAmount = 0.0;
        dynamic rate;

        if (selectedMonth == "1")
        {
            sMonth = selectedMonth + " Month";
        }
        else if (selectedMonth == "12")
        {
            sMonth = "1 Year";
        }
        else
        {
            sMonth = selectedMonth + " Months";
        }
        if (depositAmountTextBox.Text != "")
        {
            depositAmount = Convert.ToDouble(Server.HtmlEncode(depositAmountTextBox.Text));
        }


        for (int i = 0; i < count; i++)
        {
            fromAmount = Result[i].range.from.amount;
            toAmount = Result[i].range.to.amount;
            rate = Result[i].rate;
           
            if ((depositAmount >= fromAmount && depositAmount <= toAmount))
            {
                rateAmount = rate;
                break;
            }

        }
        return rateAmount;
    }
    public string get_web_content(string url)
    {
        string output="";
        try
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
             output = reader.ReadToEnd();
            response.Close();
        }
        catch(Exception ex)
        {

        }
    

        return output;
    }
    private void getDepositFormData()
    {
        this.Form.DefaultButton = this.depositCalculateButton.UniqueID;
        depositPaymentAmountLabel.Visible = false;
        lblDepositamountvalidation.Visible = false;
        dpstCalcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        if (depositAmountTextBox.Text != "")
        {
            string subPName = Request.QueryString["subProductName"];
            double rAmount;
            if (subPName == "Savings")
            {
                rAmount = Get_SavingsRate();//getting interestRate by comparing depositSavingsAmount & selectedMonth
            }
            else
            {
                rAmount = Get_Rate();//getting interestRate by comparing depositAmount & selectedMonth
            }

            if (Get_Rate() == 0)
            {
                if (lang == "EN")
                {
                    depositPaymentAmountLabel.Text = "Sorry, your request can not  processed now. Please try again";
                }
                else
                {
                    depositPaymentAmountLabel.Text = "نأسف لم يتم تنفيذ طلبك، يرجى المحاولة مرة أخرى";
                }

                return;
            }

            double depositAmount = 0.0;
            //Method variables used to store converted hidden field data.
            if (depositAmountTextBox.Text != "") { depositAmount = Convert.ToDouble(Server.HtmlEncode(depositAmountTextBox.Text)); };
            double interestRate = rAmount;
            int numberOfPayments = 0;

            //Resetting the required fields.
            loanAmountRequiredLabel.Visible = false;
            interestRateRequiredLabel.Visible = false;
            depositNumberOfPaymentsRequiredLabel.Visible = false;

            //Checking to make sure something was typed in the loan amount textbox.
            if (depositAmountTextBox.Text.Length > 0)
            {
                //Saving the loan amount textbox entry to the hidden field value.
                depositAmountHiddenField.Value = Server.HtmlEncode(depositAmountTextBox.Text);

                //Trying to convert the hidden field value. If not notifying the user.
                if (!double.TryParse(depositAmountHiddenField.Value, out depositAmount))
                {
                    loanAmountRequiredLabel.Visible = true;
                    _isError = true;
                }
            }
            //Displaying the error user notification if nothing is in the loan amount textbox.
            else
            {
                loanAmountRequiredLabel.Visible = true;
                _isError = true;
            }

            //Checking to make sure something was typed in number of payments textbox.
            if (depositDropDownList1.Text.Length > 0 && depositDropDownList1.Text.Length < 255)
            {
                //Saving the textbox entry to the hidden field value.
                depositNumberOfPaymentsHiddenField.Value = depositDropDownList1.Text;

                //Trying to convert the hidden field value. If not notify the user.
                if (!int.TryParse(depositNumberOfPaymentsHiddenField.Value, out numberOfPayments))
                {
                    depositNumberOfPaymentsRequiredLabel.Visible = true;
                    _isError = true;
                }
            }
            
            //Displaying the error user notification if nothing is in the number of payments textbox.
            else
            {
                depositNumberOfPaymentsRequiredLabel.Visible = true;
                _isError = true;
            }

            //If there are no errors from the form entries the proceed with the calculations.
            if (!_isError)
            {
                if (hcountryId.Value == "2")
                {
                    if (Convert.ToDouble(depositAmountTextBox.Text) >= 5000 && Convert.ToDouble(depositAmountTextBox.Text) <= 100000000)
                    {
                        lblDepositamountvalidation.Visible = false;
                        string apistringdeposit = "https://api.arabbank.com/product/v1/deposits/" + depositAmount + "/calculators/" + numberOfPayments + "?apikey=qLWUhMgBHgZyKSGIFhSj8DRc2G2XdCr5&country=AE&currency=AED&type=term&dealType=FAR";
                        string jsondeposit = get_web_content(apistringdeposit);

                        dynamic array = Newtonsoft.Json.JsonConvert.DeserializeObject(jsondeposit);

                        depositPaymentAmountHiddenField.Value = array["data"][0].totalInterest;
                        depositPaymentAmountLabel.Visible = true;
                        if (lang == "EN")
                        {
                            depositPaymentAmountLabel.Text = "Interest Amount: AED " + Convert.ToDouble(depositPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            depositPaymentAmountLabel.Text = "مبلغ الفائدة د.إ " + Convert.ToDouble(depositPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                    }
                    else
                    {
                        lblDepositamountvalidation.Visible = true;
                    }
                }
                if (hcountryId.Value == "5")
                {
                    if (Convert.ToDouble(depositAmountTextBox.Text) >= 5000 && Convert.ToDouble(depositAmountTextBox.Text) <= 100000000)
                    {
                        lblDepositamountvalidation.Visible = false;
                        string apistringdeposit = "https://api.arabbank.com/product/v1/deposits/" + depositAmount + "/calculators/" + numberOfPayments + "?apikey=qLWUhMgBHgZyKSGIFhSj8DRc2G2XdCr5&country=AE&currency=AED&type=term&dealType=FAR";
                        string jsondeposit = get_web_content(apistringdeposit);

                        dynamic array = Newtonsoft.Json.JsonConvert.DeserializeObject(jsondeposit);

                        depositPaymentAmountHiddenField.Value = array["data"][0].totalInterest;
                        depositPaymentAmountLabel.Visible = true;
                        if (lang == "EN")
                        {
                            depositPaymentAmountLabel.Text = "Interest Amount: EGP " + Convert.ToDouble(depositPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                        else
                        {
                            depositPaymentAmountLabel.Text = "مبلغ الفائدة د.إ " + Convert.ToDouble(depositPaymentAmountHiddenField.Value).ToString("N", new CultureInfo("en-US"));
                        }
                    }
                    else
                    {
                        lblDepositamountvalidation.Visible = true;
                    }
                }
                if (hcountryId.Value == "1")
                {
                    //Calculating the loan payment amount, rounding at the last 2 digits, and storing the value in the hidden field.
                    depositPaymentAmountHiddenField.Value = Math.Round(depositCalculatePayment(interestRate, depositAmount, numberOfPayments), 2).ToString();

                    //Displaying the loan payment amount from the hidden field value.
                    depositPaymentAmountLabel.Visible = true;
                    if (lang == "EN")
                    {
                        depositPaymentAmountLabel.Text = "Interest Amount: JOD " + depositPaymentAmountHiddenField.Value;
                    }
                    else
                    {
                        depositPaymentAmountLabel.Text = "مبلغ الفائدة " + depositPaymentAmountHiddenField.Value;
                    }
                }
            }

        }
        else
        {
            dpstCalcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
            return;
        }
    }
    ///////////////////////////////////////////////////////////////////

    //Method used to calculate the payment amount.
    private double depositCalculatePayment(double interestRate, double depositAmount, int numberOfMonthlyPayments)
    {
        //Making sure we have valid data.
        if (interestRate > 0 && depositAmount > 0 && numberOfMonthlyPayments > 0)
        {
            //Checking the interest rate format, and divide into monthly percentage rate accordingly.
            if (interestRate > 0)
                interestRate /= 100;
            else
                interestRate /= 12;

            return ((depositAmount * (numberOfMonthlyPayments * 30) * interestRate) / 365);

        }
        else
        {
            return 0;
        }
    }
    private double calculatePayment(double interestRate, double loanAmount, int numberOfMonthlyPayments)
    {
        //Making sure we have valid data.
        if (interestRate > 0 && loanAmount > 0 && numberOfMonthlyPayments > 0)
        {
            //Calculate and return the loan payment amount.
            return ((loanAmount + ((loanAmount * (interestRate / 100) * (numberOfMonthlyPayments)) / 12)) / (numberOfMonthlyPayments));
        }
        else
            return 0;
    }
    private double calculatePaymentForPersonal(double interestRate, double loanAmount, int numberOfMonthlyPayments)//for personalLoan
    {
        //Making sure we have valid data.
       // interestRate = Convert.ToDouble(configObj.PersonalLoanInterest);

        //if (interestRate > 0 && loanAmount > 0 && numberOfMonthlyPayments > 0)
        //{
        //    //Calculate and return the loan payment amount.
        //    return ((loanAmount * (interestRate / (12 * 100))) / (1 - (1 / (Math.Pow((1 + (interestRate / (12 * 100))), (numberOfMonthlyPayments))))));
        //}
        //else
        //{
        //    return 0;
        //}

        string loantype = "p";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate,out string curen,out string curar);
        string country = "";
        string currency = "";

        if(countryId=="1")
        {
            country = "JO";
            currency = "JOD";
        }
        else if(countryId == "2")
        {
            country = "AE";
            currency = "AED";
        }
        else if(countryId == "5")
        {
            country = "EG";
            currency = "EGP";
        }
        else if (countryId == "3")
        {
            country = "PS";
            currency = "USD";
        }
        loantype = "personal";
        loancalculator(loanAmount.ToString(), numberOfMonthlyPayments.ToString(), country, currency, interestrate, loantype, out string emiamt);
        return Convert.ToDouble(emiamt);

    }
    private double calculatePaymentForAuto(double interestRate, double loanAmount, int numberOfMonthlyPayments)//for autoLoan
    {
        //if (configObj.RetrieveCountry(Convert.ToInt64(hcountryId.Value)) < 0)
        //{
        //    return -1;
        //}

        //double ALinterestN = Convert.ToDouble(configObj.AutoLoanInterestNew);
        //double ALinterestU = Convert.ToDouble(configObj.AutoLoanInterestUsed);

        //bool isChecked = loanNewRdBtn.Checked;
        //if (isChecked)
        //    interestRate = ALinterestN;
        //else
        //    interestRate = ALinterestU;
        ////Making sure we have valid data.
        //if (interestRate > 0 && loanAmount > 0 && numberOfMonthlyPayments > 0)
        //{

        //    return ((loanAmount + ((loanAmount * (interestRate / 100) * (numberOfMonthlyPayments)) / 12)) / (numberOfMonthlyPayments));
        //}
        //else
        //    return 0;

        string loantype = "p";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate,out string curen,out string curar);
        string country = "";
        string currency = "";

        if (countryId == "1")
        {
            country = "JO";
            currency = "JOD";
        }
        else if(countryId == "2")
        {
            country = "AE";
            currency = "AED";
        }
        else if(countryId == "5")
        {
            country = "EG";
            currency = "EGP";
        }
        else if (countryId == "3")
        {
            country = "PS";
            if (RadioButtonListcurrency.SelectedIndex == 0)
            {
                currency = "JOD";

            }
            else
            {
                currency = "USD";
            }
        }

        if (countryId == "2" || countryId == "3")
        {
            loantype = "auto";
        }
        else
        {
            bool isChecked = loanNewRdBtn.Checked;
            if (isChecked)
                loantype ="new";
            else
                loantype = "used";
        }

        loancalculator(loanAmount.ToString(), numberOfMonthlyPayments.ToString(), country, currency, interestrate, loantype, out string emiamt);
        return Convert.ToDouble(emiamt);
    }
    private double calculatePaymentForHousing(double interestRate, double loanAmount, int numberOfMonthlyPayments)//for housingLoan
    {
        //Making sure we have valid data.
        //interestRate = Convert.ToDouble(configObj.HousingLoanInterest);

        //if (interestRate > 0 && loanAmount > 0 && numberOfMonthlyPayments > 0)
        //{
        //    //return ((loanAmount + ((loanAmount * (interestRate / 100) * (numberOfMonthlyPayments)) / 12)) / (numberOfMonthlyPayments));
        //    return ((loanAmount * (interestRate / (12 * 100))) / (1 - (1 / (Math.Pow((1 + (interestRate / (12 * 100))), (numberOfMonthlyPayments))))));
        //}
        //else
        //{
        //    return 0;
        //}

        string loantype = "h";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate, out string curen,out string curar);
        string country = "";
        string currency = "";

        if (countryId == "1")
        {
            country = "JO";
            currency = "JOD";
        }
        else if (countryId == "2")
        {
            country = "AE";
            currency = "AED";
        }
        else if (countryId == "3")
        {
            country = "PS";
            currency = "USD";
        }
        loantype = "housing";
        loancalculator(loanAmount.ToString(), numberOfMonthlyPayments.ToString(), country, currency, interestrate, loantype, out string emiamt);
        return Convert.ToDouble(emiamt);

    }

    protected void btnIwillthink_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.SubmitButton.UniqueID;
        id03.Style.Add(HtmlTextWriterStyle.Display, "none");
        id02.Style.Add(HtmlTextWriterStyle.Display, "none");
        id04.Style.Add(HtmlTextWriterStyle.Display, "none");
        id05.Style.Add(HtmlTextWriterStyle.Display, "none");
        id01.Style.Add(HtmlTextWriterStyle.Display, "block");
        id06.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
        nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
        mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");

        if (hcountry.Value == "jo" || hcountry.Value == "Jordan" || hcountry.Value == "JO" || hcountry.Value == "JOR")
        {
            txtMobileCd.Text = "00962";
        }
        else if (hcountry.Value == "UAE" || hcountry.Value == "uae")
        {
            txtMobileCd.Text = "00971";
            lblMobile.Text = (string)base.GetLocalResourceObject("lblMobileAE.Text");
        }
        else if (hcountry.Value == "egypt" || hcountry.Value == "EGYPT" || hcountryId.Value == "5")
        {
            txtMobileCd.Text = "002";
            lblMobile.Text = (string)base.GetLocalResourceObject("lblMobileEGYPT.Text");
            lblMobileCode.Text = "eg: 002";
        }
        else if (hcountry.Value == "Palastine" || hcountry.Value == "palastine" || hcountry.Value == "PALASTINE" || hcountryId.Value == "3")
        {
            txtMobileCd.Text = "00970";
            lblMobile.Text = (string)base.GetLocalResourceObject("lblMobilePalastine.Text");
            lblMobileCode.Text = "eg: 00970";
        }
        else
        {
            txtMobileCd.Text = "";
        }
        txtMobile.Text = "";
        txtName.Text = "";
    }

    protected void btnamInterested_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.Button3.UniqueID;
        id03.Style.Add(HtmlTextWriterStyle.Display, "none");
        id01.Style.Add(HtmlTextWriterStyle.Display, "none");
        id05.Style.Add(HtmlTextWriterStyle.Display, "none");
        id02.Style.Add(HtmlTextWriterStyle.Display, "block");
        id06.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
        alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
        mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
        mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");

        if (hcountry.Value == "jo" || hcountry.Value == "Jordan" || hcountry.Value == "JO")
        {
            txtmobileinterestedCd.Text = "00962";
        }
        else if (hcountry.Value == "UAE" || hcountry.Value == "uae" || hcountryId.Value == "2")
        {
            txtmobileinterestedCd.Text = "00971";
            lblMobile1.Text = (string)base.GetLocalResourceObject("lblMobileAE.Text");
        }
        else if (hcountry.Value == "egypt" || hcountry.Value == "EGYPT" || hcountryId.Value == "5")
        {
            txtmobileinterestedCd.Text = "002";
            lblMobile1.Text = (string)base.GetLocalResourceObject("lblMobileEGYPT.Text");
            lblMobileCode1.Text = "eg: 002";
        }
        else if (hcountry.Value == "Palastine" || hcountry.Value == "palastine" || hcountry.Value == "PALASTINE" || hcountryId.Value == "3")
        {
            txtmobileinterestedCd.Text = "00970";
            lblMobile1.Text = (string)base.GetLocalResourceObject("lblMobilePalastine.Text");
            lblMobileCode1.Text = "eg: 00970";
        }

        
        else
        {
            txtmobileinterestedCd.Text = "";
        }
        txtmobileinterested.Text = "";
        txtnameInterested.Text = "";
    }

    protected void btnIneedhelp_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.Button5.UniqueID;
        id03.Style.Add(HtmlTextWriterStyle.BackgroundColor, "linear-gradient(to bottom left, #009AFC 0%, #0138B9 100%);");
        id01.Style.Add(HtmlTextWriterStyle.Display, "none");
        id02.Style.Add(HtmlTextWriterStyle.Display, "none");
        id05.Style.Add(HtmlTextWriterStyle.Display, "none");
        id03.Style.Add(HtmlTextWriterStyle.Display, "block");
        id06.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
        alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
        mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
        mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
        mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");

        if (hcountry.Value == "jo" || hcountry.Value == "Jordan" || hcountry.Value == "JO")
        {
            txtmobilehelpCd.Text = "00962";
        }
        else if (hcountry.Value == "UAE" || hcountry.Value == "uae" || hcountryId.Value=="2")
        {
            txtmobilehelpCd.Text = "00971";
            lblMobile2.Text = (string)base.GetLocalResourceObject("lblMobileAE.Text");
            
        }
        else if (hcountry.Value == "egypt" || hcountry.Value == "EGYPT" || hcountryId.Value == "5")
        {
            txtmobilehelpCd.Text = "002";
            lblMobile2.Text = (string)base.GetLocalResourceObject("lblMobileEGYPT.Text");
            lblMobileCode2.Text = "eg: 002";
        }
        else if (hcountry.Value == "Palastine" || hcountry.Value == "palastine" || hcountry.Value == "PALASTINE" || hcountryId.Value == "3")
        {
            txtmobilehelpCd.Text = "00970";
            lblMobile2.Text = (string)base.GetLocalResourceObject("lblMobilePalastine.Text");
            lblMobileCode2.Text = "eg: 00970";
        }
        else
        {
            txtmobilehelpCd.Text = "";
        }
        txtmobilehelp.Text = "";
        txtnamehelp.Text = "";
    }

    protected void loanradiobutton_Click(object sender, EventArgs e)
    {
        loaninitialdata();
    }

    private void depositinitialdata()
    {
        if (lang == "EN")
        {
            lblDeposithint.Text = "(from JOD 5,000 to JOD 100,000,000)";//setting amount limit
            lblDepositamountvalidation.Text = "Deposit Amount should be from JOD 5,000 to JOD 100,000,000";
        }
        else
        {
            lblDeposithint.Text = "(من 5000 الى 100,000,000)";//setting amount limit
            lblDepositamountvalidation.Text = "من 5000 الى 100,000,000";
        }
        if (hcountryId.Value == "2")
        {
            if (lang == "EN")
            {
                lblDeposithint.Text = "(from AED 5,000 to AED 100,000,000)";//setting amount limit
                lblDepositamountvalidation.Text = "Deposit Amount should be from AED 5,000 to AED 100,000,000";
            }
            else
            {
                lblDeposithint.Text = "(من 5,000 د.أ الى 100,000,000 د.أ)";//setting amount limit
                lblDepositamountvalidation.Text = "يجب أن يكون مبلغ الوديعة من 5,000 د.أ الى 100,000,000 د.أ";
            }
        }
        else if (hcountryId.Value == "5")
        {
            if (lang == "EN")
            {
                lblDeposithint.Text = "(from EGP 5,000 to AED 100,000,000)";//setting amount limit
                lblDepositamountvalidation.Text = "Deposit Amount should be from EGP 5,000 to EGP 100,000,000";
            }
            else
            {
                lblDeposithint.Text = "(من 5,000 د.أ الى 100,000,000 د.أ)";//setting amount limit
                lblDepositamountvalidation.Text = "يجب أن يكون مبلغ الوديعة من 5,000 د.أ الى 100,000,000 د.أ";
            }
        }
    }

    private void loaninitialdata()
    {
        this.Form.DefaultButton = this.calculateButton.UniqueID;

        if (configObj.RetrieveCountry(Convert.ToInt64(hcountryId.Value)) < 0)
        {
            return;
        }

        string loantype = "n";
        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate,out string curen,out string curar);

        string FrmJD = minamt;
        string ToJD = maxamt;
        string frmMnth = minmonth;
        string toMnth = maxmonth;
        string ALinterest = interestrate;

        //int FrmJD = Convert.ToInt32(configObj.autoLoanFrmJD);
        //int ToJD = Convert.ToInt32(configObj.autoLoanToJD);
        //string frmMnth = configObj.AutoLoanFrmMnth.ToString();
        //string toMnth = configObj.AutoLoanToMnth.ToString();

        if (hsubProductName.Value == "AutoLoan")
        {
            if (loanNewUsedBtn.Checked)
            {
                loantype = "u";
                loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate,out curen,out curar);

                FrmJD = minamt;
                ToJD = maxamt;
                frmMnth = minmonth;
                toMnth = maxmonth;
            }
        }

        if (hsubProductName.Value == "personal")
        {
            loantype = "p";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate,out curen,out curar);

            FrmJD = minamt;
            ToJD = maxamt;
            frmMnth = minmonth;
            toMnth = maxmonth;
        }
        if (hsubProductName.Value == "housing")
        {
            loantype = "h";
            loanparameter(countryId, loantype, out minamt, out maxamt, out minmonth, out maxmonth, out interestrate,out curen,out curar);

            FrmJD = minamt;
            ToJD = maxamt;
            frmMnth = minmonth;
            toMnth = maxmonth;
        }
       

        if (lang == "EN")
        {
            lblPoppLoanCalcuAmntLimit.Text = "(From JOD " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To JOD " +Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
        }
        else
        {
            lblPoppLoanCalcuAmntLimit.Text = "(من " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " دينار  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " دينار)";//setting amount limit
        }
        if(hcountryId.Value=="2")
        {
            if (lang == "EN")
            {
                lblPoppLoanCalcuAmntLimit.Text = "(From AED " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To AED " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                lblPoppLoanCalcuAmntLimit.Text = "(من " +Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " د.أ  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " د.أ)";//setting amount limit
            }
        }

        if (hcountryId.Value == "5")
        {
            if (lang == "EN")
            {
                lblPoppLoanCalcuAmntLimit.Text = "(From EGP " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To EGP " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {
                lblPoppLoanCalcuAmntLimit.Text = "(من " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " جنيه مصري  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " جنيه مصري)";//setting amount limit
            }
        }

        loanMonthSlider.Attributes["min"] = frmMnth;
        loanMonthSlider.Attributes["max"] = toMnth;
        lblLoanPoppMinVal.Text = frmMnth;
        lblLoanPoppMaxVal.Text = toMnth;

        loanPaymentAmountLabel.Visible = false;
        loanAmountTextBox.Text = "";
        lblPoppLCalcuLimitAlert.Text = "";

        this.Form.DefaultButton = this.calculateButton.UniqueID;
    }

    protected void btnloancalcu_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.calculateButton.UniqueID;
        loanNewUsedBtn.Checked = false;
        loanNewRdBtn.Checked = true;

        loaninitialdata();

        if (hsubProductName.Value != "AutoLoan" || hcountryId.Value=="2" || hcountryId.Value == "3")
        {
            autoLoanRdBtn.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        else
        {
            autoLoanRdBtn.Style.Add(HtmlTextWriterStyle.Display, "block");
        }

        id01.Style.Add(HtmlTextWriterStyle.Display, "none");
        id02.Style.Add(HtmlTextWriterStyle.Display, "none");
        id03.Style.Add(HtmlTextWriterStyle.Display, "none");
        id05.Style.Add(HtmlTextWriterStyle.Display, "block");
        id06.Style.Add(HtmlTextWriterStyle.Display, "none");

        loanPaymentAmountLabel.Visible = false;
        loanAmountTextBox.Text = "";
        lblPoppLCalcuLimitAlert.Text = "";
        calcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        calcuNumberOnlyAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        
        //Label12.Visible = false;
    }

    protected void btnDepositcalcu_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.depositCalculateButton.UniqueID;
        id01.Style.Add(HtmlTextWriterStyle.Display, "none");
        id02.Style.Add(HtmlTextWriterStyle.Display, "none");
        id03.Style.Add(HtmlTextWriterStyle.Display, "none");
        id05.Style.Add(HtmlTextWriterStyle.Display, "none");
        id06.Style.Add(HtmlTextWriterStyle.Display, "block");
        depositPaymentAmountLabel.Visible = false;
        depositAmountTextBox.Text = "";
        lblDepositamountvalidation.Visible = false;
        depositinitialdata();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string json = "";
        string APIResponse = "Success";
        string failurereason = "";
        /////////////////////////apiWrite///////////////////////////////////////////////
        jsonPostClass jsonObj = new jsonPostClass();
        List<jsonPostClass> lstJson = new List<jsonPostClass>();
        //DateTime jsonDT = DateTime.Parse(Convert.ToString(BusinessObject.ConvertTimeToUtc()), CultureInfo.CreateSpecificCulture("en-US"));
        DateTime jsonDT = BusinessObject.getlocaltime(Convert.ToInt32(hcountryId.Value));

        string abcountr = hcountry.Value;
        if (hcountryId.Value == "5")
        {
            abcountr = "EG";
        }

            lstJson.Add(new jsonPostClass()
            {
            name = "CustomerLanguage",
            value = lang,
            type = "",
            displayname = "",
            masked = ""
            });
        lstJson.Add(new jsonPostClass()
        {
            name = "AB_Country",
            value = abcountr,
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "AB_CName",
            value = Server.HtmlEncode(txtName.Text),
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "ProductName",
            value = hproductName.Value,
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "SubProduct",
            value = hsubProductName.Value,
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "RequestDateTime",
            value = Convert.ToString(jsonDT.ToString("dd'-'MM'-'yyyy HH:mm:ss")),
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "Bench_Code",
            value = hdiscoveryBench.Value,
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "Branch_code",
            value = hbranchNumber.Value,
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "Branch_name",
            value = hbranchName.Value,
            type = "",
            displayname = "",
            masked = ""
        });
        lstJson.Add(new jsonPostClass()
        {
            name = "AccountNumber",
            value = "",
            type = "",
            displayname = "",
            masked = ""
        });
        string serverwritingapi = BusinessObject.Getserverwritingapi();
        string listnamevar = "SmartBranchCampaign";
        string countryvar = "JO";

        if (hcountryId.Value=="2")
        {
            serverwritingapi = BusinessObject.Getserverwritingapiuae();
            listnamevar = "CL_SmartBranch_AE";
            countryvar = "AE";
        }
        if (hcountryId.Value == "5")
        {
            serverwritingapi = BusinessObject.Getserverwritingapiegypt();
            listnamevar = "CL_SmartBranch_EG";
            countryvar = "EG";
        }
        if (hcountryId.Value == "3")
        {
            serverwritingapi = BusinessObject.Getserverwritingapipalestine();
            listnamevar = "CL_SmartBranch_PS";
            countryvar = "PS";
        }

        string datetimeurl = DateTime.Now.ToLongDateString();
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(serverwritingapi);

        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        HttpWebResponse httpResponse = null;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        try
        {
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                json = new JavaScriptSerializer().Serialize(new
                {
                    listname = listnamevar,
                    mobile = Server.HtmlEncode(txtMobile.Text),
                    countrycode = Server.HtmlEncode(txtMobileCd.Text),
                    country = countryvar,
                    attributes = lstJson
                });

                if (hcountryId.Value == "2")
                {
                    json = new JavaScriptSerializer().Serialize(new
                    {
                        listname = listnamevar,
                        mobile = Server.HtmlEncode(txtMobileCd.Text + txtMobile.Text),
                        countrycode = Server.HtmlEncode("00962"),
                        country = countryvar,
                        attributes = lstJson
                    });
                }
                else if (hcountryId.Value == "5")
                {
                    json = new JavaScriptSerializer().Serialize(new
                    {
                        listname = listnamevar,
                        mobile = Server.HtmlEncode(txtMobileCd.Text + txtMobile.Text),
                        countrycode = Server.HtmlEncode("962"),
                        country = countryvar,
                        attributes = lstJson
                    });
                }
                else if (hcountryId.Value == "3")
                {
                    json = new JavaScriptSerializer().Serialize(new
                    {
                        listname = listnamevar,
                        mobile = Server.HtmlEncode(txtMobileCd.Text + txtMobile.Text),
                        countrycode = Server.HtmlEncode("970"),
                        country = countryvar,
                        attributes = lstJson
                    });
                }

                streamWriter.Write(json);
            }

           
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        }

        catch (Exception h)
        {
            failurereason = h.Message;
            APIResponse = "Failure";
        }

        //HttpWebResponse httpResponse = null;
        //try
        //{
        //    httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //}
        //catch(Exception f)
        //{
        //    failurereason = f.Message;
         //   APIResponse = "Failure";
        //}

        if (APIResponse != "Failure")
        {
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result2 = streamReader.ReadToEnd();
            }
        }

        Iwillthinkclass Iwillthinkclassobj = new Iwillthinkclass();
        if (txtName.Text != "")
        {
            Iwillthinkclassobj.CustName = Server.HtmlEncode(txtName.Text);
        }
        else
        {
            nameAlert.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (hcountryId.Value=="5")
        {
            if (txtMobileCd.Text != "" && txtMobileCd.Text.Count() >= 3)
            {
                Iwillthinkclassobj.MobileCd = Server.HtmlEncode(txtMobileCd.Text);
            }
            else
            {
                nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
        }
        else
        {
            if (txtMobileCd.Text != "" && txtMobileCd.Text.Count() > 4)
            {
                Iwillthinkclassobj.MobileCd = Server.HtmlEncode(txtMobileCd.Text);
            }
            else
            {
                nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
        }
        if (txtMobileCd.Text.ToString().Substring(0, 1) != "0" || txtMobileCd.Text.ToString().Substring(1, 1) != "0")
        {
            nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtMobile.Text != "")
        {
            Iwillthinkclassobj.Mobile = Server.HtmlEncode(txtMobile.Text);
        }
        else
        {
            nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "block");
            mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }
        if (txtMobile.Text.Count() > 6 && txtMobile.Text.Count() < 16)
        {
            Iwillthinkclassobj.Mobile = Server.HtmlEncode(txtMobile.Text);
        }
        else
        {
            nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtMobileCd.Text == "00962")
        {
            if (txtMobile.Text.ToString().Substring(0, 2) == "07")
            {
                Iwillthinkclassobj.Mobile = Server.HtmlEncode(txtMobile.Text);
            }
            else
            {
                nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
        }

        if (txtMobileCd.Text == "00970")
        {
            if (txtMobile.Text.ToString().Substring(0, 1) == "5")
            {
                Iwillthinkclassobj.Mobile = Server.HtmlEncode(txtMobile.Text);
            }
            else
            {
                nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
                mob07tAlertLbl.Text = (string)base.GetLocalResourceObject("mobStart05AlertLbl.Text");
                return;
            }
        }

        //if (txtMobile.Text.ToString().Substring(0, 1) != "0" || txtMobile.Text.ToString().Substring(1, 1) == "0")
        // {
        // nameAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        // mobCDLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "none");
        // mobLimitAlertLbl.Style.Add(HtmlTextWriterStyle.Display, "block");
        // return;
        // }

        Iwillthinkclassobj.BranchName = hbranchName.Value;
        Iwillthinkclassobj.BranchNumber = hbranchNumber.Value;

        Iwillthinkclassobj.DiscoveryBench = hdiscoveryBench.Value;
        if (lang == "EN")
        {
            Iwillthinkclassobj.BrowsingLanguage = "en";
        }
        else
        {
            Iwillthinkclassobj.BrowsingLanguage = "ar";
        }
        Iwillthinkclassobj.ProductName = prdctNameCntrlTxt.Text;
        var subPrdctId = suPrdctNameCntrlTxt.Text;
        Iwillthinkclassobj.subProductId = Convert.ToInt32(subPrdctId);
        Iwillthinkclassobj.Status = "Y";
        Iwillthinkclassobj.branchId = Convert.ToInt32(hbranchId.Value);
        Iwillthinkclassobj.CountryId = Convert.ToInt32(hcountryId.Value);
        Iwillthinkclassobj.APIResponse = APIResponse;
        Iwillthinkclassobj.FailureReason = failurereason;

        if (Iwillthinkclassobj.Insert() < 0)
        {
            return;
        }

        if (lang == "EN")
        {
            lblAlertThankYou.Text = "Thank you";
        }
        else
        {
            lblAlertThankYou.Text = "شكرا";
        }

        id01.Style.Add(HtmlTextWriterStyle.Display, "none");
        id04.Style.Add(HtmlTextWriterStyle.Display, "block");

    }
    protected void SuccessButton_Click(object sender, EventArgs e)
    {
        id04.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        id01.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected void SubmitButtoninterested_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.Button3.UniqueID;
        if (configObj.RetrieveBranch(Convert.ToInt64(hbranchId.Value)) < 0)
        {
            return;
        }

        string toMail = configObj.ToMail.ToString();
        string frmMail = configObj.FromMail.ToString();

        generateAutoNumber generateAutoObj = new generateAutoNumber();

        string ticketId = generateAutoObj.generateAutoNumbers_ImIntersted();

        string encryptedticketid = BusinessObject.EncodeString(ticketId);
        string encryptedformname = BusinessObject.EncodeString("ABIaminterested");

        string linkWithId = "/TicketStatus.aspx?tId=" + encryptedticketid + "&form="+ encryptedformname;

        Iaminterestedclass Iaminterestedobject = new Iaminterestedclass();
        if (txtnameInterested.Text != "")
        {
            Iaminterestedobject.CustomerName = Server.HtmlEncode(txtnameInterested.Text);
        }
        else
        {
            alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
            mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (hcountryId.Value=="5")
        {
            if (txtmobileinterestedCd.Text != "" && txtmobileinterestedCd.Text.Count() >= 3)
            {
                Iaminterestedobject.MobileCd = Server.HtmlEncode(txtmobileinterestedCd.Text);
            }
            else
            {
                alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
        }
        else
        {
            if (txtmobileinterestedCd.Text != "" && txtmobileinterestedCd.Text.Count() > 4)
            {
                Iaminterestedobject.MobileCd = Server.HtmlEncode(txtmobileinterestedCd.Text);
            }
            else
            {
                alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
        }
        if (txtmobileinterestedCd.Text.ToString().Substring(0, 1) != "0" || txtmobileinterestedCd.Text.ToString().Substring(1, 1) != "0")
        {
            alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
            mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }
        if (txtmobileinterested.Text != "")
        {
            Iaminterestedobject.Mobile = Server.HtmlEncode(txtmobileinterested.Text);
        }
        else
        {
            alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "block");
            mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtmobileinterested.Text.Count() > 6 && txtmobileinterested.Text.Count() < 16)
        {
            Iaminterestedobject.Mobile = Server.HtmlEncode(txtmobileinterested.Text);
        }
        else
        {
            alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
            mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtmobileinterestedCd.Text == "00962")
        {
            if (txtmobileinterested.Text.ToString().Substring(0, 2) == "07")
            {
                Iaminterestedobject.Mobile = Server.HtmlEncode(txtmobileinterested.Text);
            }
            else
            {
                alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
        }
        if (txtmobileinterestedCd.Text == "00970")
        {
            if (txtmobileinterested.Text.ToString().Substring(0, 1) == "5")
            {
                Iaminterestedobject.Mobile = Server.HtmlEncode(txtmobileinterested.Text);
            }
            else
            {
                alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty.Style.Add(HtmlTextWriterStyle.Display, "none");
                mob07tAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
                mob07tAlertLbl1.Text = (string)base.GetLocalResourceObject("mobStart05AlertLbl.Text");
                return;
            }
        }
        // if (txtmobileinterested.Text.ToString().Substring(0, 1) != "0" || txtmobileinterested.Text.ToString().Substring(1, 1) == "0")
        //{
        //  alertNameInterested.Style.Add(HtmlTextWriterStyle.Display, "none");
        // mobCDLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "none");
        // mobLimitAlertLbl1.Style.Add(HtmlTextWriterStyle.Display, "block");
        // return;
        //}

        Iaminterestedobject.BranchName = hbranchName.Value;
        Iaminterestedobject.BranchNumber = hbranchNumber.Value;
        Iaminterestedobject.ProductName = prdctNameCntrlTxt1.Text;
        Iaminterestedobject.ProductSubName = suPrdctNameCntrlTxt1.Text;
        Iaminterestedobject.DiscoveryBench = hdiscoveryBench.Value;
        if (lang == "EN")
        {
            Iaminterestedobject.BrowsingLanguage = "en";
        }
        else
        {
            Iaminterestedobject.BrowsingLanguage = "ar";
        }
        var subPrdctId = suPrdctNameCntrlTxt.Text;
        Iaminterestedobject.subProductId = Convert.ToInt32(subPrdctId);
        Iaminterestedobject.TicketId = ticketId;
        Iaminterestedobject.TicketStatus = "O";
        Iaminterestedobject.LinkTicket = linkWithId;
        Iaminterestedobject.Country = hcountry.Value;
        Iaminterestedobject.Status = "Y";
        Iaminterestedobject.RequestType = "I am Interested";
        Iaminterestedobject.branchId = Convert.ToInt32(hbranchId.Value);
        Iaminterestedobject.CountryId = Convert.ToInt32(hcountryId.Value);

        if(BusinessObject.getSubproductname(subPrdctId,out string subproductnamedb)<0)
        {
            return;
        }

        //////////////////////MailSending///////////////////////////
        try
        {
            string baseurl = "";
            string withhyperlink = "";
            if (BusinessObject.Getbaseurl(out baseurl) >= 0)
            {
                string emaillink = baseurl + linkWithId;
                withhyperlink = "<a clicktracking=off href=" + "\"" + baseurl + linkWithId + "\" >Click here</a>";
            }

            // Subject and multipart/alternative Body
            string Subject = Iaminterestedobject.BranchName.Trim()+"-Request Type: I am Interested - Ticket Id:" + ticketId + " - Status:Open";

            string html =
                 @"Ticket Id: " + ticketId +
                 "<br/> Request Type: I am Interested " +
                 "<br/> Country: " + hcountry.Value +
                 "<br/> Product Name: " + hproductName.Value +
                 "<br/> Sub Product Name: " + subproductnamedb +
                 "<br/> Customer Name: " + Iaminterestedobject.CustomerName +
                 "<br/> Mobile Country code: " + Iaminterestedobject.MobileCd +
                 "<br/> Mobile: " + Iaminterestedobject.Mobile +
                 "<br/> Discovery Bench: " + Iaminterestedobject.DiscoveryBench +
                 "<br/> Branch Number: " + Iaminterestedobject.BranchNumber +
                 "<br/> Branch Name: " + Iaminterestedobject.BranchName +
                 "<br/> Browsing Language: " + Iaminterestedobject.BrowsingLanguage +
                 "<br/> Submission Date/Time: " + BusinessObject.getlocaltime(Convert.ToInt32(hcountryId.Value)) +
                 "<br/> Ticket Status: Open" +
            "<br/> Link to be used while closing the ticket: "+withhyperlink;

            if (BusinessObject.sendEmail(html, toMail, Subject, frmMail) < 0)
            {
                return;
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        //////////////////////////////////////////////////////////

        if (Iaminterestedobject.Insert() < 0)
        {
            return;
        }
        id02.Style.Add(HtmlTextWriterStyle.Display, "none");
        id04.Style.Add(HtmlTextWriterStyle.Display, "block");
    }
    protected void CancelButtoninterested_Click(object sender, EventArgs e)
    {
        id02.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void SubmitButtonhelp_Click(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.Button5.UniqueID;
        if (configObj.RetrieveBranch(Convert.ToInt64(hbranchId.Value)) < 0)
        {
            return;
        }

        string toMail = configObj.ToMail.ToString();
        string frmMail = configObj.FromMail.ToString();

        generateAutoNumber generateAutoObj = new generateAutoNumber();
        string ticketId = generateAutoObj.generateAutoNumbers_INeedHlp();

        string encryptedticketid = BusinessObject.EncodeString(ticketId);
        string encryptedformname = BusinessObject.EncodeString("ABIneedhelp");

        string linkWithId = "/TicketStatus.aspx?tId=" + encryptedticketid + "&form="+ encryptedformname;


        Ineedhelpclass Ineedhelpobject = new Ineedhelpclass();
        if (txtnamehelp.Text != "")
        {
            Ineedhelpobject.CustomerName = Server.HtmlEncode(txtnamehelp.Text);
        }
        else
        {
            alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (hcountryId.Value == "5")
        {
            if (txtmobilehelpCd.Text != "" && txtmobilehelpCd.Text.Count() >= 3)
            {
                Ineedhelpobject.MobileCode = Server.HtmlEncode(txtmobilehelpCd.Text);
            }
            else
            {
                alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
        }
        else
        {
            if (txtmobilehelpCd.Text != "" && txtmobilehelpCd.Text.Count() > 4)
            {
                Ineedhelpobject.MobileCode = Server.HtmlEncode(txtmobilehelpCd.Text);
            }
            else
            {
                alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                return;
            }
        }
        if (txtmobilehelpCd.Text.ToString().Substring(0, 1) != "0" || txtmobilehelpCd.Text.ToString().Substring(1, 1) != "0")
        {
            alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtmobilehelp.Text != "")
        {
            Ineedhelpobject.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
        }
        else
        {
            alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtmobilehelp.Text.Count() > 6 && txtmobilehelp.Text.Count() < 16)
        {
            Ineedhelpobject.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
        }
        else
        {
            alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
            mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
            mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
            return;
        }

        if (txtmobilehelpCd.Text == "00962")
        {
            if (txtmobilehelp.Text.ToString().Substring(0, 2) == "07")
            {
                Ineedhelpobject.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
            }
            else
            {
                alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
        }
        if (txtmobilehelpCd.Text == "00970")
        {
            if (txtmobilehelp.Text.ToString().Substring(0, 1) == "5")
            {
                Ineedhelpobject.Mobile = Server.HtmlEncode(txtmobilehelp.Text);
            }
            else
            {
                alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobAlertempty2.Style.Add(HtmlTextWriterStyle.Display, "none");
                mobStart07AlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
                mobStart07AlertLbl2.Text = (string)base.GetLocalResourceObject("mobStart05AlertLbl.Text");
                return;
            }
        }

        // if (txtmobilehelp.Text.ToString().Substring(0, 1) != "0" || txtmobilehelp.Text.ToString().Substring(1, 1) == "0")
        // {
        //   alertNamehelp.Style.Add(HtmlTextWriterStyle.Display, "none");
        //   mobCDLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "none");
        //   mobLimitAlertLbl2.Style.Add(HtmlTextWriterStyle.Display, "block");
        //   return;
        // }

        Ineedhelpobject.BranchName = hbranchName.Value;
        Ineedhelpobject.BranchNumber = hbranchNumber.Value;
        Ineedhelpobject.ProductName = prdctNameCntrlTxt2.Text;
        Ineedhelpobject.ProductSubName = suPrdctNameCntrlTxt2.Text;
        Ineedhelpobject.DiscoveryBench = hdiscoveryBench.Value;

        if (lang == "EN")
        {
            Ineedhelpobject.BrowsingLanguage = "en";
        }
        else
        {
            Ineedhelpobject.BrowsingLanguage = "ar";
        }

        var subPrdctId = suPrdctNameCntrlTxt.Text;
        Ineedhelpobject.subProductId = Convert.ToInt32(subPrdctId);
        Ineedhelpobject.TicketId = ticketId;
        Ineedhelpobject.LinkTicket = linkWithId;
        Ineedhelpobject.TicketStatus = "O";
        Ineedhelpobject.Country = hcountry.Value;
        Ineedhelpobject.Status = "Y";
        Ineedhelpobject.RequestType = "I need help";
        Ineedhelpobject.branchId = Convert.ToInt32(hbranchId.Value);
        Ineedhelpobject.CountryId = Convert.ToInt32(hcountryId.Value);

        if (BusinessObject.getSubproductname(subPrdctId, out string subproductnamedb) < 0)
        {
            return;
        }

        //////////////////////MailSending///////////////////////////
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
            string Subject = Ineedhelpobject.BranchName.Trim()+"-Request Type: I need help - Ticket Id:" + ticketId + " - Status:Open";

            string html =
                 @"Ticket Id: " + ticketId +
                 "<br/> Request Type: I need Help " +
                 "<br/> Country: " + Ineedhelpobject.Country +
                 "<br/> Product Name: " + hproductName.Value +
                 "<br/> Sub Product Name: " + subproductnamedb +
                 "<br/> Customer Name: " + Ineedhelpobject.CustomerName +
                 "<br/> Mobile Country code: " + Ineedhelpobject.MobileCode +
                 "<br/> Mobile: " + Ineedhelpobject.Mobile +
                 "<br/> Discovery Bench: " + Ineedhelpobject.DiscoveryBench +
                 "<br/> Branch Number: " + Ineedhelpobject.BranchNumber +
                 "<br/> Branch Name: " + Ineedhelpobject.BranchName +
                 "<br/> Browsing Language: " + Ineedhelpobject.BrowsingLanguage +
                 "<br/> Submission Date/Time: " + BusinessObject.getlocaltime(Convert.ToInt32(hcountryId.Value)) +
                 "<br/> Ticket Status: Open" +
                 "<br/> Link to be used while closing the ticket: " + withhyperlink;

            if (BusinessObject.sendEmail(html, toMail, Subject, frmMail) < 0)
            {
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        //////////////////////////////////////////////////////////

        if (Ineedhelpobject.Insert() < 0)
        {
            return;
        }
        id03.Style.Add(HtmlTextWriterStyle.Display, "none");
        id04.Style.Add(HtmlTextWriterStyle.Display, "block");
    }
    protected void CancelButtonhelp_Click(object sender, EventArgs e)
    {
        id03.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void loancancelButton_Click(object sender, EventArgs e)
    {
        id05.Style.Add(HtmlTextWriterStyle.Display, "none");
        id06.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void loanclearButton_Click(object sender, EventArgs e)
    {
        clearloanfields();
    }

    private void clearloanfields()
    {
        loanAmountTextBox.Text = "";
        depositAmountTextBox.Text = "";
        lblDepositamountvalidation.Visible = false;
        loanPaymentAmountLabel.Visible = false;
        depositPaymentAmountLabel.Visible = false;
        calcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        dpstCalcuEmptyFieldAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
        lblPoppLCalcuLimitAlert.Style.Add(HtmlTextWriterStyle.Display, "none");
    }

    protected void btnSession_Click(object sender, EventArgs e)
    {
        Session.Abandon();
    }

    protected void RadioButtonListcurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearloanfields();
        //RadioButtonListcurrency.
        string currency = "USD";
        string currencyar = "";
        string loantype = "p";
        if (RadioButtonListcurrency.SelectedIndex==0)
        {
            currency = "JOD";
            currencyar = "";
        }

        if(hpName.Value == "personalLoan")
        {
            loantype = "p";
        }
        else if (hpName.Value == "housingLoan")
        {
            loantype = "h";
        }
        else if (hpName.Value == "autoLoan")
        {
            //if (loanNewUsedBtn.Checked == true)
            //{
            //    loantype = "u";
            //}
            //else
            //{
            //    loantype = "n";
            //}
            loantype = "a";
        }

        countryId = hcountryId.Value;
        loanparameter(countryId, loantype, out string minamt, out string maxamt, out string minmonth, out string maxmonth, out string interestrate, out string curren, out string currar);

        string FrmJD = minamt;
        string ToJD = maxamt;
        string frmMnth = minmonth;
        string toMnth = maxmonth;
        string PLinterest = interestrate;

        if (currency == "JOD")
        {
            if (lang == "EN")
            {

                lblPoppLoanCalcuAmntLimit.Text = "(From JOD " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To JOD " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {

                lblPoppLoanCalcuAmntLimit.Text = "(من " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " دينار أردني  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " دينار أردني)";//setting amount limit
            }
        }
        else
        {
            if (lang == "EN")
            {

                lblPoppLoanCalcuAmntLimit.Text = "(From USD " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " To USD " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + ")";//setting amount limit
            }
            else
            {

                lblPoppLoanCalcuAmntLimit.Text = "(من " + Convert.ToDouble(FrmJD).ToString("N", new CultureInfo("en-US")) + " دولار  الى " + Convert.ToDouble(ToJD).ToString("N", new CultureInfo("en-US")) + " دولار)";//setting amount limit
            }
        }
       
        interestRate = Convert.ToDouble(PLinterest);
        hinterestRate.Value = interestRate.ToString();
        //////////////setting slider limit////////////////////////////////
        loanMonthSlider.Attributes["min"] = frmMnth;
        loanMonthSlider.Attributes["max"] = toMnth;
        lblLoanPoppMinVal.Text = frmMnth;
        lblLoanPoppMaxVal.Text = toMnth;
    }
}