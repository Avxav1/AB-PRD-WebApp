using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using Arabbankdll;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class Googlemap : System.Web.UI.Page
{
    //private bool _isError = false;
    DataTable dt = null;
    string lang = "";
    string cntr = "";
    string branchState = "";
    string branchArabicState = "";
    string ATMState = "";
    string ATMArabicState = "";
    string ITMState = "";
    string ITMArabicState = "";
    string KioskState = "";
    string KioskArabicState = "";
    string ATMPlusState = "";
    string ATMPlusArabicState = "";
    string SmartBranchState = "";
    string SmartBranchArabicState = "";
    protected override void InitializeCulture()
    {
        string cultureName = Request.QueryString["lang"];
        if (!string.IsNullOrEmpty(cultureName))
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }
    }
    protected async void Page_Load(object sender, EventArgs e)
    {
        lang = BusinessObject.GetCurrLangCode();
        Session["lang"] = lang;
        string countrid = Server.HtmlEncode(Request.QueryString["cntry"]);
        cntr = Request.QueryString["cntry"];
        if (cntr == null)
        {
            cntr = "JO";
        }
        else if (cntr == "1")
        {
            cntr = "JO";
        }
        else if (cntr == "2")
        {
            cntr = "AE";
        }
        else if (cntr == "5")
        {
            cntr = "EG";

        }
        else if (cntr == "3")
        {
            cntr = "PS";

        }
        if (!Page.IsPostBack)
        {
            await LoadCity(countrid);
            await LoadBranch();
            await Defaultloading();
            await LoadATM();
            await LoadITM();
            await GETKiosk();
            await GETATMPlus();
            await GETSmartBranch();

        }
    }




    private async Task GetLabels()
    {
        try
        {
            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync("https://api.arabbank.com/location/v1/metadata?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4");
            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<Rootobject>(str);
            var dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("Services");
            dt.Columns.Add("Type");
            dt.Columns.Add("City");
            dt.Columns.Add("En");
            dt.Columns.Add("Code");

            foreach (var arr in rootobject.data)
            {
                if (arr.type == "Branch")
                {
                    if (lang == "EN")
                    {
                        lblChooseLocal.Text = "Select " + arr.description.en;
                        branchState = lblChooseLocal.Text;


                    }
                    else
                    {
                        lblChooseLocal.Text = "اختر" + " " + arr.description.ar;
                        branchState = lblChooseLocal.Text;

                    }
                }
                if (arr.type == "ATM")
                {
                    if (lang == "EN")
                    {
                        Label2.Text = "Select " + arr.description.en;
                        ATMState = Label2.Text;
                    }
                    else
                    {
                        Label2.Text = "اختر" + " " + arr.description.ar;
                        ATMState = Label2.Text;
                    }
                }
                if (arr.type == "ITM")
                {
                    if (lang == "EN")
                    {
                        Label1.Text = "Select " + arr.description.en;
                        ITMState = Label1.Text;
                    }
                    else
                    {
                        Label1.Text = "اختر" + " " + arr.description.ar;
                        ITMState = Label1.Text;
                    }
                }
                if (arr.type == "ATMplus")
                {
                    if (lang == "EN")
                    {
                        lblATMPlus.Text = "Select " + arr.description.en;
                        ATMPlusState = lblATMPlus.Text;
                    }
                    else
                    {
                        lblATMPlus.Text = "اختر" + " " + arr.description.ar;
                        ATMPlusState = lblATMPlus.Text;
                    }
                }
                if (arr.type == "SmartBranch")
                {
                    if (lang == "EN")
                    {
                        lblSelfService.Text = "Select " + arr.description.en;
                        SmartBranchState = lblSelfService.Text;
                    }
                    else
                    {
                        lblSelfService.Text = "اختر" + " " + arr.description.ar;
                        SmartBranchState = lblSelfService.Text;
                    }
                }
                if (arr.type == "KIOSK")
                {
                    if (lang == "EN")
                    {
                        lblKiosk.Text = "Select " + arr.description.en;
                        KioskState = lblKiosk.Text;
                    }
                    else
                    {
                        lblKiosk.Text = "اختر" + " " + arr.description.ar;
                        KioskState = lblKiosk.Text;
                    }
                }
            }




        }

        catch (WebException ex) when (ex.Status == WebExceptionStatus.NameResolutionFailure)
        {

            var er = ex;



        }




    }








    protected async void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        await GetLabels();
        await LoadBranch();
        await Defaultloading();
        await LoadATM();
        await LoadITM();
        await GETATMPlus();
        await GETKiosk();
        await GETSmartBranch();
    }
    private async Task Defaultloading()
    {
        if (ddlBranch.Items.Count > 1)
        {
            ddlBranch.SelectedIndex = 1;
        }
        await ReadXML("branch");
    }
    private void createxml(string typearab)
    {
        string branchname = "";
        if (typearab == "branch")
        {
            branchname = ddlBranch.SelectedValue;
        }
        else if (typearab == "atm")
        {
            branchname = ddlATM.SelectedValue;
        }
        else if (typearab == "itm")
        {
            branchname = ddlITM.SelectedValue;
        }


        string branchxml = "locator/" + branchname + typearab + ".xml";

        if (!File.Exists(Server.MapPath(branchxml)))
        {
            using (XmlWriter xWr = XmlWriter.Create(Server.MapPath(branchxml)))
            {


                int cityid = Convert.ToInt32(ddlCity.SelectedValue);


                xWr.WriteStartDocument();

                xWr.WriteStartElement("Library");

                //if (Branchclass.Retrievenearest(out dt, branchname, cityid, typearab) >= 0)
                //{
                foreach (DataRow row in dt.Rows)
                {

                    xWr.WriteStartElement("List");

                    // ADD FEW ELEMENTS.
                    xWr.WriteElementString("BranchId", row["BranchId"].ToString());
                    xWr.WriteElementString("Latitude", row["Latitude"].ToString());
                    xWr.WriteElementString("Longitude", row["Longitude"].ToString());
                    xWr.WriteElementString("Distance", row["Distance"].ToString());

                    xWr.WriteElementString("Typenew", row["Typenew"].ToString());
                    xWr.WriteElementString("BranchName", row["BranchName"].ToString());
                    xWr.WriteElementString("BranchAddress", row["BranchAddress"].ToString());
                    xWr.WriteElementString("BranchNameAR", row["BranchNameAR"].ToString());
                    xWr.WriteElementString("BranchAddressAR", row["BranchAddressAR"].ToString());

                    xWr.WriteElementString("WorkingHours", row["WorkingHours"].ToString());
                    xWr.WriteElementString("Phone", row["Phone"].ToString());
                    xWr.WriteElementString("EliteCenter", row["EliteCenter"].ToString());
                    xWr.WriteElementString("CorporateService", row["CorporateService"].ToString());

                    xWr.WriteElementString("ForeignCurrency", row["ForeignCurrency"].ToString());
                    xWr.WriteElementString("OnlineDeposit", row["OnlineDeposit"].ToString());
                    xWr.WriteElementString("ChequeDeposit", row["ChequeDeposit"].ToString());

                    xWr.WriteElementString("QrCode", GenerateCode("http://maps.google.com/maps?q=" + row["Latitude"] + "," + row["Longitude"]));

                    xWr.WriteEndElement();          // CLOSE LIST.

                }
                //}

                xWr.WriteEndElement();          // CLOSE LIBRARY.
                xWr.WriteEndDocument();         // END DOCUMENT.

                // FLUSH AND CLOSE.
                //xWr.Flush();
                xWr.Close();

                // SHOW A MESSAGE IN A DIV.
                //div_xml.InnerText = "File created.";
            }
        }
    }
    public async Task ReadXML(string typenew)
    {
        string branchname = "";

        if (typenew == "branch")
        {
            branchname = ddlBranch.SelectedValue;
        }
        else if (typenew == "atm")
        {
            branchname = ddlATM.SelectedValue;
        }
        else if (typenew == "itm")
        {
            branchname = ddlITM.SelectedValue;
        }
        else if (typenew == "kiosk")
        {
            branchname = ddlKiosk.SelectedValue;
        }
        else if (typenew == "atmPlus")
        {
            branchname = ddlATMPlus.SelectedValue;
        }
        else if (typenew == "smartBranch")
        {
            branchname = ddlSelfService.SelectedValue;
        }

        try
        {
            string uri = "https://api.arabbank.com/location/v1/locations/" + branchname + "?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4";
            WebClient webClient = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webResponse = await webClient.DownloadStringTaskAsync(uri);
            string str = webResponse.ToString();
            var rootobjects = JsonConvert.DeserializeObject<Rootobjects>(str);
            var data = rootobjects.data;
            var mapinfo = new DataTable();
            mapinfo.Columns.Add("Id");
            mapinfo.Columns.Add("Latitude");
            mapinfo.Columns.Add("Longitude");
            mapinfo.Columns.Add("FormattedAddress");
            mapinfo.Columns.Add("BranchName");
            mapinfo.Columns.Add("WorkingHoursOpen");
            mapinfo.Columns.Add("WorkingHoursClose");
            mapinfo.Columns.Add("Phone");
            mapinfo.Columns.Add("Phone2");
            mapinfo.Columns.Add("BranchServices");
            mapinfo.Columns.Add("Type");
            mapinfo.Columns.Add("Description");

            // Bind Data to dropdownlist  
            StringBuilder sb = new StringBuilder();


            if (lang == "EN")
            {
                sb.Append(@"<script async defer src = " + "\" https://maps.googleapis.com/maps/api/js?key=AIzaSyAR8OVOZBMzLL_EIzO83NDkj1vZGklDENI \"" + "></script>");
                //sb.Append(@"<script async defer src = " + "\"lib/js/googlemap.js\"" + "></script>");
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.Append(@"<script async defer src = " + "\" https://maps.googleapis.com/maps/api/js?key=AIzaSyAR8OVOZBMzLL_EIzO83NDkj1vZGklDENI&language=ar&region=JO \"" + "></script>");
                //sb.Append(@"<script async defer src = " + "\"googlemaplangcountry.js\"" + "></script>");
                sb.Append(Environment.NewLine);
            }

            sb.Append(@"<script>");
            sb.Append(Environment.NewLine);
            sb.Append(@"function initMap() {");
            sb.Append(Environment.NewLine);
            sb.Append(@"var map;");
            sb.Append(Environment.NewLine);
            sb.Append(@"var bounds = new google.maps.LatLngBounds();");
            sb.Append(Environment.NewLine);
            sb.Append(@"var mapOptions = {");
            sb.Append(Environment.NewLine);
            sb.Append(@"mapTypeId: 'roadmap'");
            sb.Append(Environment.NewLine);
            sb.Append(@"};");
            sb.Append(Environment.NewLine);
            sb.Append(@"map = new google.maps.Map(document.getElementById(" + "\"mapCanvas\"" + "), mapOptions);");
            sb.Append(Environment.NewLine);
            sb.Append(@"map.setTilt(50);");
            sb.Append(Environment.NewLine);
            sb.Append(@"var markers = [");
            sb.Append(Environment.NewLine);

            decimal dist = 0;
            int markerid = 0;


            var rowDesc = mapinfo.NewRow();
            rowDesc["Id"] = data.id;
            rowDesc["Latitude"] = data.geographic_location.location.lat;
            rowDesc["Longitude"] = data.geographic_location.location.lng;



            sb.Append(@"['" + rowDesc["Id"].ToString().Trim() + "', " + rowDesc["Latitude"].ToString().Trim() + ", " + rowDesc["Longitude"].ToString().Trim() + "],");
            sb.Append(Environment.NewLine);



            sb.Append(@"];");
            sb.Append(Environment.NewLine);
            sb.Append(@"var infoWindowContent = [");
            sb.Append(Environment.NewLine);



            if (lang == "EN")
            {
                sb.Append(@"['<div dir=" + "\"ltr\"" + " class=" + "\"info_content\"" + " style =" + "\"color: black; height: 265px; width: 350px\"" + " > ' +");
                sb.Append(Environment.NewLine);
                sb.Append(@"'<div style=" + "\"float:left;  width: 194px; \"" + " > ' +");
                sb.Append(Environment.NewLine);



                if (data.name != null)
                {
                    rowDesc["BranchName"] = data.name.en;
                    sb.Append(@"'<h3>" + rowDesc["BranchName"].ToString().Trim() + "</h3>' +");
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    rowDesc["BranchName"] = "";
                    sb.Append(@"'<h3>" + rowDesc["BranchName"].ToString().Trim() + "</h3>' +");
                    sb.Append(Environment.NewLine);
                }

                if (data.address.formatted_address != null)
                {
                    string replace = data.address.formatted_address.en;
                    replace = Regex.Replace(replace, "'", "`");
                    rowDesc["FormattedAddress"] = replace;
                    sb.Append(@"'<p>" + rowDesc["FormattedAddress"].ToString().Trim() + "</p>' + ");
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    rowDesc["FormattedAddress"] = "";
                    sb.Append(@"'<p>" + rowDesc["FormattedAddress"].ToString().Trim() + "</p>' + ");
                    sb.Append(Environment.NewLine);
                }


                if (typenew == "atm")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = data.type;
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.en;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }


                if (typenew == "itm")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = data.type;
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.en;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }

                if (typenew == "kiosk")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = data.type;
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.en;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }

                if (typenew == "atmPlus")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = data.type;
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.en;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }
            }
            else
            {
                sb.Append(@"['<div dir=" + "\"rtl\"" + " class=" + "\"info_content\"" + " style =" + "\"color: black; height: 265px; width: 350px\"" + " > ' +");
                sb.Append(Environment.NewLine);
                sb.Append(@"'<div style=" + "\"float:right; position: absolute; width: 194px; \"" + " > ' +");
                sb.Append(Environment.NewLine);



                if (data.name != null)
                {
                    rowDesc["BranchName"] = data.name.ar;
                    sb.Append(@"'<h3>" + rowDesc["BranchName"].ToString().Trim() + "</h3>' +");
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    rowDesc["BranchName"] = "";
                    sb.Append(@"'<h3>" + rowDesc["BranchName"].ToString().Trim() + "</h3>' +");
                    sb.Append(Environment.NewLine);
                }

                if (data.address.formatted_address != null)
                {
                    rowDesc["FormattedAddress"] = data.address.formatted_address.ar;
                    sb.Append(@"'<p>" + rowDesc["FormattedAddress"].ToString().Trim() + "</p>' + ");
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    rowDesc["FormattedAddress"] = "";
                    sb.Append(@"'<p>" + rowDesc["FormattedAddress"].ToString().Trim() + "</p>' + ");
                    sb.Append(Environment.NewLine);
                }

                if (typenew == "atm")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = "ماكينة الصراف الآلي";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.ar;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }

                if (typenew == "itm")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = "أجهزة الصراف الآلي التفاعلية";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.ar;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }

                if (typenew == "kiosk")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = "كشك";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {

                            rowDesc["Description"] = data.services[i].description.ar;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }

                if (typenew == "atmPlus")
                {

                    if (data.type != null)
                    {
                        rowDesc["Type"] = "أجهزة الصراف الآلي زائد";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        rowDesc["Type"] = "";
                        sb.Append(@"'<h3>" + rowDesc["Type"].ToString().Trim() + "</h3>' + ");
                        sb.Append(Environment.NewLine);
                    }
                    if (data.services != null)
                    {

                        for (int i = 0; i < data.services.Length; i++)
                        {
                            rowDesc["Description"] = data.services[i].description.ar;
                            sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                            sb.Append(Environment.NewLine);
                        }

                    }
                    else
                    {
                        rowDesc["Description"] = "";
                        sb.Append(@"'<Label>" + rowDesc["Description"].ToString().Trim() + "</Label>' + ");
                        sb.Append(Environment.NewLine);
                    }


                }


            }
            if (typenew == "branch")
            {
                if (lang == "EN")
                {
                    string workingHours = "";
                    if (data.opening_hours != null)
                    {
                        rowDesc["WorkingHoursOpen"] = data.opening_hours[0].opens;
                        rowDesc["WorkingHoursClose"] = data.opening_hours[0].closes;
                        workingHours = rowDesc["WorkingHoursOpen"].ToString() + " to " + rowDesc["WorkingHoursClose"].ToString();
                    }
                    else
                    {
                        rowDesc["WorkingHoursOpen"] = "";
                        rowDesc["WorkingHoursClose"] = "";
                        workingHours = "";
                    }
                    sb.Append(@"'<h3>Working Hours:</h3><label>" + workingHours.ToString() + "</label>' +");
                    sb.Append(Environment.NewLine);
                    sb.Append(@"'<h3>Phone:</h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:left; \"" + " ></label>' + ");
                    sb.Append(Environment.NewLine);
                    if (data.contact_info != null)
                    {
                        for (int i = 0; i < data.contact_info.Length; i++)
                        {
                            rowDesc["Phone"] = data.contact_info[i].phone;
                            sb.Append(@"'<h3></h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:left; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        rowDesc["Phone"] = "";
                        sb.Append(@"'<h3></h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:left; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");
                        sb.Append(Environment.NewLine);
                    }




                }
                else
                {
                    if (data.opening_hours != null)
                    {
                        rowDesc["WorkingHoursOpen"] = data.opening_hours[0].opens;
                        rowDesc["WorkingHoursClose"] = data.opening_hours[0].closes;
                    }
                    else
                    {
                        rowDesc["WorkingHoursOpen"] = "";
                        rowDesc["WorkingHoursClose"] = "";
                    }

                    sb.Append(@"'<h3>أوقات العمل:</h3><label>" + rowDesc["WorkingHoursOpen"].ToString() + " " + rowDesc["WorkingHoursClose"].ToString() + "</label>' +");
                    sb.Append(Environment.NewLine);
                    sb.Append(@"'<h3>الهاتف:</h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:right; \"" + " ></label>' + ");
                    sb.Append(Environment.NewLine);

                    if (data.contact_info != null)
                    {
                        for (int i = 0; i < data.contact_info.Length; i++)
                        {
                            rowDesc["Phone"] = data.contact_info[i].phone;
                            sb.Append(@"'<h3></h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:right; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        rowDesc["Phone"] = "";
                        sb.Append(@"'<h3>الهاتف:</h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:right; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");

                        sb.Append(Environment.NewLine);
                    }
                }
            }
            if (typenew == "branch")
            {
                if (lang == "EN")
                {
                    sb.Append(@"'<h3>" + "Branch" + "</h3>' + ");
                }
                else
                {
                    sb.Append(@"'<h3>" + "الفرع" + " </h3>' + ");
                }
                if (lang == "EN")
                {

                    if (data.services != null)
                    {
                        for (int i = 0; i < data.services.Length; i++)
                        {
                            rowDesc["BranchServices"] = data.services[i].description.en;
                            string bService = rowDesc["BranchServices"].ToString();
                            sb.Append(@"'<label>" + bService + "</label></br>' +");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        string bService = "";
                        sb.Append(@"'<label>" + bService + "</label></br>' +");
                        sb.Append(Environment.NewLine);
                    }


                }
                else
                {

                    if (data.services != null)
                    {
                        for (int i = 0; i < data.services.Length; i++)
                        {
                            rowDesc["BranchServices"] = data.services[i].description.ar;
                            string bService = rowDesc["BranchServices"].ToString();
                            sb.Append(@"'<label>" + bService + "</label></br>' +");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        string bService = "";
                        sb.Append(@"'<label>" + bService + "</label></br>' +");
                        sb.Append(Environment.NewLine);
                    }




                }
            }
            if (typenew == "smartBranch")
            {
                if (lang == "EN")
                {
                    string workingHours = "";
                    if (data.opening_hours != null)
                    {
                        rowDesc["WorkingHoursOpen"] = data.opening_hours[0].opens;
                        rowDesc["WorkingHoursClose"] = data.opening_hours[0].closes;
                        workingHours = rowDesc["WorkingHoursOpen"].ToString() + " to " + rowDesc["WorkingHoursClose"].ToString();
                    }
                    else
                    {
                        rowDesc["WorkingHoursOpen"] = "";
                        rowDesc["WorkingHoursClose"] = "";
                        workingHours = "";
                    }
                    sb.Append(@"'<h3>Working Hours:</h3><label>" + workingHours.ToString() + "</label>' +");
                    sb.Append(Environment.NewLine);
                    sb.Append(@"'<h3>Phone:</h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:left; \"" + " ></label>' + ");
                    sb.Append(Environment.NewLine);
                    if (data.contact_info != null)
                    {
                        for (int i = 0; i < data.contact_info.Length; i++)
                        {
                            rowDesc["Phone"] = data.contact_info[i].phone;
                            sb.Append(@"'<h3></h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:left; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        rowDesc["Phone"] = "";
                        sb.Append(@"'<h3></h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:left; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");
                        sb.Append(Environment.NewLine);
                    }
                }
                else
                {
                    if (data.opening_hours != null)
                    {
                        rowDesc["WorkingHoursOpen"] = data.opening_hours[0].opens;
                        rowDesc["WorkingHoursClose"] = data.opening_hours[0].closes;
                    }
                    else
                    {
                        rowDesc["WorkingHoursOpen"] = "";
                        rowDesc["WorkingHoursClose"] = "";
                    }

                    sb.Append(@"'<h3>أوقات العمل:</h3><label>" + rowDesc["WorkingHoursOpen"].ToString() + " " + rowDesc["WorkingHoursClose"].ToString() + "</label>' +");
                    sb.Append(Environment.NewLine);
                    sb.Append(@"'<h3>الهاتف:</h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:right; \"" + " ></label>' + ");
                    sb.Append(Environment.NewLine);

                    if (data.contact_info != null)
                    {
                        for (int i = 0; i < data.contact_info.Length; i++)
                        {
                            rowDesc["Phone"] = data.contact_info[i].phone;
                            sb.Append(@"'<h3></h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:right; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");

                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        rowDesc["Phone"] = "";
                        sb.Append(@"'<h3>الهاتف:</h3><label style=" + "\"direction: ltr; unicode-bidi: bidi-override;float:right; \"" + " >" + rowDesc["Phone"].ToString().Trim() + "</label></br>' + ");

                        sb.Append(Environment.NewLine);
                    }
                }
            }
            if (typenew == "smartBranch")
            {
                if (lang == "EN")
                {
                    sb.Append(@"'<h3>" + "Smart Branch" + "</h3>' + ");
                }
                else
                {
                    sb.Append(@"'<h3>" + "الفرع" + " </h3>' + ");
                }

                if (lang == "EN")
                {

                    if (data.services != null)
                    {
                        for (int i = 0; i < data.services.Length; i++)
                        {
                            rowDesc["BranchServices"] = data.services[i].description.en;
                            string bService = rowDesc["BranchServices"].ToString();
                            sb.Append(@"'<label>" + bService + "</label></br>' +");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        string bService = "";
                        sb.Append(@"'<label>" + bService + "</label></br>' +");
                        sb.Append(Environment.NewLine);
                    }


                }
                else
                {

                    if (data.services != null)
                    {
                        for (int i = 0; i < data.services.Length; i++)
                        {
                            rowDesc["BranchServices"] = data.services[i].description.ar;
                            string bService = rowDesc["BranchServices"].ToString();
                            sb.Append(@"'<label>" + bService + "</label></br>' +");
                            sb.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        string bService = "";
                        sb.Append(@"'<label>" + bService + "</label></br>' +");
                        sb.Append(Environment.NewLine);
                    }




                }


            }
            sb.Append(@"'</div>' +");
            sb.Append(Environment.NewLine);
            string Qrcode = GenerateCode("http://maps.google.com/maps?q=" + rowDesc["Latitude"] + "," + rowDesc["Longitude"]);
            if (lang == "EN")
            {
                sb.Append(@"'<div style=" + "\"float:right; width: 48 % \"" + " > <img style=" + "\"width: 150px \"" + " src=" + "\"" + Qrcode.ToString().Trim() + "\"" + " /></div ></div > '],");
            }
            else
            {
                sb.Append(@"'<div style=" + "\"float:left; width: 48 % \"" + " > <img style=" + "\"width: 150px \"" + " src=" + "\"" + Qrcode.ToString().Trim() + "\"" + " /></div ></div > '],");
            }

            sb.Append(Environment.NewLine);



            sb.Append(@"];");
            sb.Append(Environment.NewLine);

            sb.Append(@"var infoWindow = new google.maps.InfoWindow(), marker, i;");
            sb.Append(Environment.NewLine);
            sb.Append(@"for (i = 0; i < markers.length; i++)");
            sb.Append(Environment.NewLine);
            sb.Append(@"{");
            sb.Append(Environment.NewLine);
            sb.Append(@"var position = new google.maps.LatLng(markers[i][1], markers[i][2]);");
            sb.Append(Environment.NewLine);
            sb.Append(@"bounds.extend(position);");
            sb.Append(Environment.NewLine);
            sb.Append(@"marker = new google.maps.Marker({");
            sb.Append(Environment.NewLine);
            sb.Append(@"position: position,");
            sb.Append(Environment.NewLine);
            sb.Append(@"map: map,");
            sb.Append(Environment.NewLine);
            sb.Append(@"title: markers[i][0]");
            sb.Append(Environment.NewLine);
            sb.Append(@"});");
            sb.Append(Environment.NewLine);
            sb.Append(@"google.maps.event.addListener(marker, 'click', (function (marker, i) {");
            sb.Append(Environment.NewLine);
            sb.Append(@"return function()");
            sb.Append(Environment.NewLine);
            sb.Append(@"{");
            sb.Append(Environment.NewLine);
            sb.Append(@"infoWindow.setContent(infoWindowContent[i][0]);");
            sb.Append(Environment.NewLine);
            sb.Append(@"infoWindow.open(map, marker);");
            sb.Append(Environment.NewLine);
            sb.Append(@" }");
            sb.Append(Environment.NewLine);
            sb.Append(@"})(marker, i));");

            sb.Append(@"if(i==" + markerid + ")");
            sb.Append(@"{");
            sb.Append(@"infoWindow.setContent(infoWindowContent[i][0]);");
            sb.Append(Environment.NewLine);
            sb.Append(@"infoWindow.open(map, marker);");
            sb.Append(Environment.NewLine);
            sb.Append(@" }");

            sb.Append(Environment.NewLine);
            sb.Append(@"map.fitBounds(bounds);");
            sb.Append(Environment.NewLine);
            sb.Append(@"}");
            sb.Append(Environment.NewLine);


            sb.Append(@"var listener = google.maps.event.addListener(map, " + "\"idle\"" + ", function()");
            sb.Append(Environment.NewLine);
            sb.Append("{");

            decimal di = dist;


            sb.Append("map.setZoom(18);");

            sb.Append("google.maps.event.removeListener(listener);");
            sb.Append("  });");

            sb.Append(@"}");
            sb.Append(Environment.NewLine);
            sb.Append(@"google.maps.event.addDomListener(window, 'load', initMap);");
            sb.Append(Environment.NewLine);
            sb.Append(@"</script>");

            if (!ClientScript.IsStartupScriptRegistered("JSScript"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
            }

        }


        catch (Exception ex)
        {

        }


    }
    private async Task LoadCity(string countryid)
    {

        await GetCity("https://api.arabbank.com/location/v1/metadata?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4");

        if (ddlCity.Items.Count > 1)
        {
            ddlCity.SelectedIndex = 0;
        }
    }
    private async Task LoadBranch()
    {
        await GetBranch();
    }
    private async Task LoadATM()
    {
        await GetATM();
    }
    private async Task LoadITM()
    {
        await GetITM();
    }
    protected async void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            await ReadXML("branch");
            ddlATM.SelectedIndex = 0;
            ddlITM.SelectedIndex = 0;
            ddlKiosk.SelectedIndex = 0;
            ddlATMPlus.SelectedIndex = 0;
            ddlSelfService.SelectedIndex = 0;
        }
        else
        {
            await Defaultloading();
        }
    }
    protected async void ddlATM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlATM.SelectedIndex > 0)
        {
            await ReadXML("atm");
            ddlBranch.SelectedIndex = 0;
            ddlITM.SelectedIndex = 0;
            ddlKiosk.SelectedIndex = 0;
            ddlSelfService.SelectedIndex = 0;
            ddlATMPlus.SelectedIndex = 0;
        }
        else
        {
            await Defaultloading();
        }

    }
    protected async void ddlITM_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlITM.SelectedIndex > 0)
        {
            await ReadXML("itm");
            ddlATM.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlKiosk.SelectedIndex = 0;
            ddlSelfService.SelectedIndex = 0;
            ddlATMPlus.SelectedIndex = 0;
        }
        else
        {
            await Defaultloading();
        }


    }
    protected async void ddlKiosk_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlKiosk.SelectedIndex > 0)
        {
            await ReadXML("kiosk");
            ddlBranch.SelectedIndex = 0;
            ddlITM.SelectedIndex = 0;
            ddlATM.SelectedIndex = 0;
            ddlATMPlus.SelectedIndex = 0;
            ddlSelfService.SelectedIndex = 0;
        }
        else
        {
            await Defaultloading();
        }
    }
    protected async void ddlSelfService_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSelfService.SelectedIndex > 0)
        {
            await ReadXML("smartBranch");
            ddlATM.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlITM.SelectedIndex = 0;
            ddlATMPlus.SelectedIndex = 0;
            ddlKiosk.SelectedIndex = 0;
        }
        else
        {
            await Defaultloading();
        }
    }
    protected async void ddlATMPlus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlATMPlus.SelectedIndex > 0)
        {
            await ReadXML("atmPlus");
            ddlBranch.SelectedIndex = 0;
            ddlITM.SelectedIndex = 0;
            ddlKiosk.SelectedIndex = 0;
            ddlSelfService.SelectedIndex = 0;
            ddlATM.SelectedIndex = 0;
        }
        else
        {
            await Defaultloading();
        }
    }
    private string GenerateCode(string name)
    {
        var writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        var result = writer.Write(name);

        string temppath = "images/QRImage" + DateTime.Now.Ticks + ".jpg";
        string path = Server.MapPath("~/" + temppath);
        var barcodeBitmap = new Bitmap(result);
        string barcodeimage = null;
        using (MemoryStream memory = new MemoryStream())
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        //imgQRCode.Visible = true;
        //imgQRCode.ImageUrl = "~/images/QRImage.jpg";
        barcodeimage = temppath;
        return barcodeimage;

    }
    private async Task GetCity(string uri)
    {
        try
        {
            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync(uri);
            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<Rootobject>(str);
            var dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("Services");
            dt.Columns.Add("Type");
            dt.Columns.Add("City");
            dt.Columns.Add("En");
            dt.Columns.Add("Code");


            foreach (var arr in rootobject.data)
            {
                if (arr.type == "Branch")
                {

                    if (lang == "EN")
                    {
                        lblChooseLocal.Text = "Select " + arr.description.en;
                        branchState = lblChooseLocal.Text;

                        for (int j = 0; j < arr.city.Length; j++)
                        {
                            if (arr.city[j].countryCode == cntr)
                            {
                                var rowDesc = dt.NewRow();
                                rowDesc["Description"] = arr.city[j].description.en;
                                rowDesc["Code"] = arr.city[j].countryCode;
                                dt.Rows.Add(rowDesc);
                            }
                        }
                    }
                    else
                    {
                        lblChooseLocal.Text = "اختر" + " " + arr.description.ar;
                        branchState = lblChooseLocal.Text;
                        for (int j = 0; j < arr.city.Length; j++)
                        {
                            if (arr.city[j].countryCode == cntr)
                            {
                                var rowDesc = dt.NewRow();
                                rowDesc["Description"] = arr.city[j].description.ar;
                                rowDesc["Code"] = arr.city[j].description.en;
                                dt.Rows.Add(rowDesc);
                            }
                        }
                    }




                    if (dt.Rows[0]["Description"].ToString() == "")
                    {
                        if (lang == "en")
                        {
                            lblChooseCity.Text = "Select City";
                            ddlCity.Items.Insert(0, new ListItem("No Data", "No Data"));
                        }
                        else
                        {
                            lblChooseCity.Text = "اختر مدينة";
                            string city = lblChooseCity.Text;
                            ddlCity.Items.Insert(0, new ListItem(city, "NoData"));
                        }

                    }
                    else
                    {

                        if (lang == "EN")
                        {
                            lblChooseCity.Text = "Select City";
                            ddlCity.DataSource = dt;
                            ddlCity.DataTextField = "Description";
                            ddlCity.DataValueField = "Description";
                            ddlCity.DataBind();
                        }
                        else
                        {
                            lblChooseCity.Text = "اختر المدينة";
                            ddlCity.DataSource = dt;
                            ddlCity.DataTextField = "Description";
                            ddlCity.DataValueField = "Code";
                            ddlCity.DataBind();
                        }
                    }

                }
                if (arr.type == "ATM")
                {
                    if (lang == "EN")
                    {
                        Label2.Text = "Select " + arr.description.en;
                        ATMState = Label2.Text;
                    }
                    else
                    {
                        Label2.Text = "اختر" + " " + arr.description.ar;
                        ATMState = Label2.Text;
                    }
                }
                if (arr.type == "ITM")
                {
                    if (lang == "EN")
                    {
                        Label1.Text = "Select " + arr.description.en;
                        ITMState = Label1.Text;
                    }
                    else
                    {
                        Label1.Text = "اختر" + " " + arr.description.ar;
                        ITMState = Label1.Text;
                    }
                }
                if (arr.type == "ATMplus")
                {
                    if (lang == "EN")
                    {
                        lblATMPlus.Text = "Select " + arr.description.en;
                        ATMPlusState = lblATMPlus.Text;
                    }
                    else
                    {
                        lblATMPlus.Text = "اختر" + " " + arr.description.ar;
                        ATMPlusState = lblATMPlus.Text;
                    }
                }
                if (arr.type == "SmartBranch")
                {
                    if (lang == "EN")
                    {
                        lblSelfService.Text = "Select " + arr.description.en;
                        SmartBranchState = lblSelfService.Text;
                    }
                    else
                    {
                        lblSelfService.Text = "اختر" + " " + arr.description.ar;
                        SmartBranchState = lblSelfService.Text;
                    }
                }
                if (arr.type == "KIOSK")
                {
                    if (lang == "EN")
                    {
                        lblKiosk.Text = "Select " + arr.description.en;
                        KioskState = lblKiosk.Text;
                    }
                    else
                    {
                        lblKiosk.Text = "اختر" + " " + arr.description.ar;
                        KioskState = lblKiosk.Text;
                    }
                }
            }




        }

        catch (WebException ex) when (ex.Status == WebExceptionStatus.NameResolutionFailure)
        {

            var er = ex;



        }
    }
    private async Task GetBranch()
    {
        string ddlCityText = "";
        if (lang == "EN")
        {
            ddlCityText = ddlCity.SelectedItem.Text;
        }
        else
        {
            ddlCityText = ddlCity.SelectedValue;
        }


        try
        {



            string uri = "https://api.arabbank.com/location/v1/locations?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&origins=31.951907%2C35.93312&categories=Branch&country=" + cntr + "&city=" + ddlCityText + "";



            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync(uri);
            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<BranchObject>(str);
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("nameArabic");


            if (lang == "EN")
            {
                int i = 0;
                foreach (var arr in rootobject.data)
                {
                    if (arr.type == "Branch")
                    {
                        var rowDesc = dt.NewRow();
                        rowDesc["id"] = arr.id;
                        rowDesc["name"] = arr.name.en;
                        dt.Rows.Add(rowDesc);
                        i++;
                    }
                }
            }
            else
            {
                int i = 0;
                foreach (var arr in rootobject.data)
                {
                    if (arr.type == "Branch")
                    {
                        var rowDesc = dt.NewRow();
                        rowDesc["id"] = arr.id;
                        rowDesc["nameArabic"] = arr.name.ar;
                        dt.Rows.Add(rowDesc);
                        i++;
                    }
                }
            }

            if (dt.Rows.Count == 0)
            {
                if (lang == "en")
                {
                    ddlBranch.Items.Insert(0, new ListItem("Select Branch", "No Item"));
                }
                else
                {
                    ddlBranch.Items.Insert(0, new ListItem(branchState, "No Item"));
                }
            }
            else
            {

                if (lang == "EN")
                {
                    ddlBranch.DataSource = dt;
                    ddlBranch.DataTextField = "name";
                    ddlBranch.DataValueField = "id";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem(branchState, "0"));
                }
                else
                {
                    ddlBranch.DataSource = dt;
                    ddlBranch.DataTextField = "nameArabic";
                    ddlBranch.DataValueField = "id";
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem(branchState, "0"));
                }

            }


        }



        catch (Exception ex)
        {

        }


    }
    private async Task GetATM()
    {
        string branchText;
        if (lang == "EN")
        {
            branchText = ddlCity.SelectedItem.Text;
        }
        else
        {
            branchText = ddlCity.SelectedValue;
        }


        try
        {



            string uri = "https://api.arabbank.com/location/v1/locations?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&origins=31.951907%2C35.93312&categories=ATM&country=" + cntr + "&city=" + branchText + "";
            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync(uri);

            string str = webRequest.ToString();

            var rootobject = JsonConvert.DeserializeObject<BranchObject>(str);


            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("type");
            dt.Columns.Add("coordinates");
            dt.Columns.Add("distance");
            dt.Columns.Add("name");
            dt.Columns.Add("nameArabic");
            int i = 0;
            foreach (var arr in rootobject.data)
            {
                if (arr.type == "ATM")
                {
                    var rowDesc = dt.NewRow();
                    rowDesc["id"] = arr.id;
                    rowDesc["name"] = arr.name.en;
                    rowDesc["nameArabic"] = arr.name.ar;
                    dt.Rows.Add(rowDesc);
                    i++;
                }
            }

            if (dt.Rows.Count != 0)
            {
                if (lang == "EN")
                {
                    ddlATM.DataSource = dt;
                    ddlATM.DataTextField = "name";
                    ddlATM.DataValueField = "id";
                    ddlATM.DataBind();
                    ddlATM.Items.Insert(0, new ListItem(ATMState, "0"));

                }
                else
                {
                    ddlATM.DataSource = dt;
                    ddlATM.DataTextField = "nameArabic";
                    ddlATM.DataValueField = "id";
                    ddlATM.DataBind();
                    ddlATM.Items.Insert(0, new ListItem(ATMState, "0"));
                }
            }
            else
            {
                if (lang == "EN")
                {
                    ddlATM.Items.Insert(0, new ListItem(ATMState, "NoATMAvailable"));
                }
                else
                {
                    ddlATM.Items.Insert(0, new ListItem(ATMState, "NoATMAvailable"));
                }
            }
        }



        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }



    }
    private async Task GetITM()
    {
        string branchText;
        if (lang == "EN")
        {
            branchText = ddlCity.SelectedItem.Text;
        }
        else
        {
            branchText = ddlCity.SelectedValue;
        }

        try
        {






            string uri = "https://api.arabbank.com/location/v1/locations?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&origins=31.951907%2C35.93312&categories=ITM&country=" + cntr + "&city=" + branchText + "";

            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync(uri);
            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<BranchObject>(str);
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("type");
            dt.Columns.Add("coordinates");
            dt.Columns.Add("distance");
            dt.Columns.Add("name");
            dt.Columns.Add("nameArabic");
            int i = 0;
            foreach (var arr in rootobject.data)
            {
                if (arr.type == "ITM")
                {
                    var rowDesc = dt.NewRow();
                    rowDesc["id"] = arr.id;
                    rowDesc["name"] = arr.name.en;
                    rowDesc["nameArabic"] = arr.name.ar;
                    dt.Rows.Add(rowDesc);
                    i++;
                }
            }

            if (dt.Rows.Count != 0)
            {

                if (lang == "EN")
                {
                    ddlITM.DataSource = dt;
                    ddlITM.DataTextField = "name";
                    ddlITM.DataValueField = "id";
                    ddlITM.DataBind();
                    if (lang == "EN")
                    {
                        ddlITM.Items.Insert(0, new ListItem(ITMState, "0"));
                    }
                }
                else
                {
                    ddlITM.DataSource = dt;
                    ddlITM.DataTextField = "nameArabic";
                    ddlITM.DataValueField = "id";
                    ddlITM.DataBind();
                    ddlITM.Items.Insert(0, new ListItem(ITMState, "0"));
                }

            }
            else
            {
                ddlITM.DataSource = dt;
                ddlITM.DataTextField = "name";
                ddlITM.DataValueField = "id";
                ddlITM.DataBind();
                if (lang == "EN")
                {
                    ddlITM.Items.Insert(0, new ListItem("No ITM Available", "0"));
                }
                else
                {
                    ddlITM.Items.Insert(0, new ListItem(ITMState, "0"));
                }
            }




        }


        catch (Exception ex)
        {

        }

    }
    private async Task GETATMPlus()
    {
        string atmPlus;
        if (lang == "EN")
        {
            atmPlus = ddlCity.SelectedItem.Text;
        }
        else
        {
            atmPlus = ddlCity.SelectedValue;
        }

        try
        {
            //cntr = Request.QueryString["cntry"];
            //if (cntr == "1")
            //{
            //    cntr = "JO";
            //}
            //else if (cntr == "2")
            //{
            //    cntr = "AE";
            //}
            //else if (cntr == "3")
            //{
            //    cntr = "EG";
            //}



            //GetBranchByID getcountry = new GetBranchByID();
            //DataTable ds = new DataTable();
            //ds=  getcountry.GetCountry(int.Parse(Request.QueryString["cntry"]));               
            //string tempurl = ds.Rows[0]["GoogleMapSubAPI"].ToString();
            //tempurl = Regex.Replace(tempurl, "none", "ATMplus");
            //tempurl = Regex.Replace(tempurl, "xxxx", "" + cntr + "");
            //tempurl = Regex.Replace(tempurl, "yyyy", "" + atmPlus + "");

            //string uri = tempurl;




            string uri = "https://api.arabbank.com/location/v1/locations?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&origins=31.951907%2C35.93312&categories=ATMplus&country=" + cntr + "&city=" + atmPlus + "";
            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync(uri);
            //var webResponse = (HttpWebResponse)webRequest.GetResponse();
            //if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
            //{
            //    var reader = new StreamReader(webResponse.GetResponseStream());
            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<BranchObject>(str);
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("type");
            dt.Columns.Add("coordinates");
            dt.Columns.Add("distance");
            dt.Columns.Add("name");
            dt.Columns.Add("nameArabic");
            int i = 0;
            foreach (var arr in rootobject.data)
            {
                if (arr.type == "ATMplus")
                {
                    var rowDesc = dt.NewRow();
                    rowDesc["id"] = arr.id;
                    rowDesc["name"] = arr.name.en;
                    rowDesc["nameArabic"] = arr.name.ar;
                    dt.Rows.Add(rowDesc);
                    i++;
                }
            }
            if (dt.Rows.Count != 0)
            {
                if (lang == "EN")
                {
                    ddlATMPlus.DataSource = dt;
                    ddlATMPlus.DataTextField = "name";
                    ddlATMPlus.DataValueField = "id";
                    ddlATMPlus.DataBind();
                    ddlATMPlus.Items.Insert(0, new ListItem(ATMPlusState, "0"));
                }
                else
                {
                    ddlATMPlus.DataSource = dt;
                    ddlATMPlus.DataTextField = "nameArabic";
                    ddlATMPlus.DataValueField = "id";
                    ddlATMPlus.DataBind();
                    ddlATMPlus.Items.Insert(0, new ListItem(ATMPlusState, "0"));
                }
            }
            else
            {
                ddlATMPlus.DataSource = dt;
                ddlATMPlus.DataTextField = "name";
                ddlATMPlus.DataValueField = "id";
                ddlATMPlus.DataBind();
                if (lang == "EN")
                {
                    ddlATMPlus.Items.Insert(0, new ListItem("No ATM Plus Available", "0"));
                }
                else
                {
                    ddlATMPlus.Items.Insert(0, new ListItem(ATMPlusState, "0"));
                }
            }

        }
        catch (Exception exs)
        {
            string ex = exs.ToString();
        }
    }
    private async Task GETKiosk()
    {
        string kiosk;
        if (lang == "EN")
        {
            kiosk = ddlCity.SelectedItem.Text;
        }
        else
        {
            kiosk = ddlCity.SelectedValue;
        }
        try
        {
            //cntr = Request.QueryString["cntry"];
            //if (cntr == "1")
            //{
            //    cntr = "JO";
            //}
            //else if (cntr == "2")
            //{
            //    cntr = "AE";
            //}
            //else if(cntr == "3")
            //{
            //    cntr = "EG";
            //}


            // GetBranchByID getcountry = new GetBranchByID();
            // DataTable ds = new DataTable();
            //ds= getcountry.GetCountry(int.Parse(Request.QueryString["cntry"]));

            // string tempurl = ds.Rows[0]["GoogleMapSubAPI"].ToString();
            // tempurl = Regex.Replace(tempurl, "none", "KIOSK");
            // tempurl = Regex.Replace(tempurl, "xxxx", "" + cntr + "");
            // tempurl = Regex.Replace(tempurl, "yyyy", "" + kiosk + "");

            // string uri = tempurl;







            string uri = "https://api.arabbank.com/location/v1/locations?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&origins=31.951907%2C35.93312&categories=KIOSK&country=" + cntr + "&city=" + kiosk + "";
            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };
            var webRequest = await web.DownloadStringTaskAsync(uri);
            //var webResponse = (HttpWebResponse)webRequest.GetResponse();
            //if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
            //{
            //    var reader = new StreamReader(webResponse.GetResponseStream());
            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<BranchObject>(str);
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("nameArabic");
            int i = 0;
            foreach (var arr in rootobject.data)
            {
                if (arr.type == "KIOSK")
                {
                    var rowDesc = dt.NewRow();
                    rowDesc["id"] = arr.id;
                    rowDesc["name"] = arr.name.en;
                    rowDesc["nameArabic"] = arr.name.ar;
                    dt.Rows.Add(rowDesc);
                    i++;
                }
            }
            if (dt.Rows.Count != 0)
            {
                if (lang == "EN")
                {
                    ddlKiosk.DataSource = dt;
                    ddlKiosk.DataTextField = "name";
                    ddlKiosk.DataValueField = "id";
                    ddlKiosk.DataBind();
                    ddlKiosk.Items.Insert(0, new ListItem(KioskState, "0"));
                }
                else
                {
                    ddlKiosk.DataSource = dt;
                    ddlKiosk.DataTextField = "nameArabic";
                    ddlKiosk.DataValueField = "id";
                    ddlKiosk.DataBind();
                    ddlKiosk.Items.Insert(0, new ListItem(KioskState, "0"));
                }
            }
            else
            {
                ddlKiosk.DataSource = dt;
                ddlKiosk.DataTextField = "name";
                ddlKiosk.DataValueField = "id";
                ddlKiosk.DataBind();
                if (lang == "EN")
                {
                    ddlKiosk.Items.Insert(0, new ListItem("No Kiosk Available", "0"));
                }
                else
                {
                    ddlKiosk.Items.Insert(0, new ListItem(KioskState, "0"));
                }
            }

        }
        catch (Exception exs)
        {
            string ex = exs.ToString();
        }
    }
    private async Task GETSmartBranch()
    {
        string smartBranch;
        if (lang == "EN")
        {
            smartBranch = ddlCity.SelectedItem.Text;
        }
        else
        {
            smartBranch = ddlCity.SelectedValue;
        }
        try
        {






            string uri = "https://api.arabbank.com/location/v1/locations?apikey=UnvKNEDs2pBD0zGnsbdTli1xpCG0QKj4&origins=31.951907%2C35.93312&categories=SmartBranch&country=" + cntr + "&city=" + smartBranch + "";
            WebClient web = new WebClient { Encoding = System.Text.Encoding.UTF8 };

            var webRequest = await web.DownloadStringTaskAsync(uri);

            string str = webRequest.ToString();
            var rootobject = JsonConvert.DeserializeObject<BranchObject>(str);
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("nameArabic");
            int i = 0;
            foreach (var arr in rootobject.data)
            {
                if (arr.type == "SmartBranch")
                {
                    var rowDesc = dt.NewRow();
                    rowDesc["id"] = arr.id;
                    rowDesc["name"] = arr.name.en;
                    rowDesc["nameArabic"] = arr.name.ar;
                    dt.Rows.Add(rowDesc);
                    i++;
                }
            }
            if (dt.Rows.Count != 0)
            {
                if (lang == "EN")
                {
                    ddlSelfService.DataSource = dt;
                    ddlSelfService.DataTextField = "name";
                    ddlSelfService.DataValueField = "id";
                    ddlSelfService.DataBind();
                    ddlSelfService.Items.Insert(0, new ListItem(SmartBranchState, "0"));
                }
                else
                {
                    ddlSelfService.DataSource = dt;
                    ddlSelfService.DataTextField = "nameArabic";
                    ddlSelfService.DataValueField = "id";
                    ddlSelfService.DataBind();
                    ddlSelfService.Items.Insert(0, new ListItem(SmartBranchState, "0"));
                }
            }
            else
            {
                ddlSelfService.DataSource = dt;
                ddlSelfService.DataTextField = "name";
                ddlSelfService.DataValueField = "id";
                ddlSelfService.DataBind();
                if (lang == "EN")
                {
                    ddlSelfService.Items.Insert(0, new ListItem("No Smart Branch Available", "0"));
                }
                else
                {
                    ddlSelfService.Items.Insert(0, new ListItem(SmartBranchState, "0"));
                }
            }

        }
        catch (Exception exs)
        {
            string ex = exs.ToString();
        }














    }
    //City Class
    public class Rootobject
    {
        public Datum[] data { get; set; }
    }
    public class Datum
    {
        public City[] city { get; set; }
        public Service[] services { get; set; }
        public Country[] country { get; set; }
        public Description description { get; set; }
        public string type { get; set; }
    }
    public class Description
    {
        public string en { get; set; }
        public string ar { get; set; }
    }
    public class City
    {
        public Description1 description { get; set; }
        public string code { get; set; }
        public string countryCode { get; set; }
    }
    public class Description1
    {
        public string en { get; set; }
        public string ar { get; set; }
    }
    public class Service
    {
        public Description2 description { get; set; }
        public string code { get; set; }
    }
    public class Description2
    {
        public string en { get; set; }
        public string ar { get; set; }
    }
    public class Country
    {
        public Description3 description { get; set; }
        public string code { get; set; }
    }
    public class Description3
    {
        public string en { get; set; }
        public string ar { get; set; }
    }

    //Branch Class
    public class BranchObject
    {
        public Metadata metadata { get; set; }
        public BranchData[] data { get; set; }
    }
    public class Metadata
    {
        public Queryparameters queryParameters { get; set; }
        public string type { get; set; }
        public bool hasMore { get; set; }
        public int count { get; set; }
    }
    public class Queryparameters
    {
        public string origins { get; set; }
    }
    public class BranchData
    {
        public string id { get; set; }
        public string type { get; set; }
        public Coordinates coordinates { get; set; }
        public Distance distance { get; set; }
        public Name name { get; set; }

    }
    public class Coordinates
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }
    public class Name
    {
        public string en { get; set; }
        public string ar { get; set; }
    }



    //Third API
    public class Rootobjects
    {
        public MetadataA metadata { get; set; }
        public Data data { get; set; }
    }
    public class MetadataA
    {
        public Queryparameter queryParameters { get; set; }
        public string type { get; set; }
        public bool hasMore { get; set; }
        public int count { get; set; }
    }
    public class Queryparameter
    {

    }
    public class Data
    {
        public string terminal_id { get; set; }
        public Address address { get; set; }
        public Contact_Info[] contact_info { get; set; }
        public Geographic_Location geographic_location { get; set; }
        public string timezone { get; set; }
        public Opening_Hours[] opening_hours { get; set; }
        public CityA[] city { get; set; }
        public NameAPI name { get; set; }
        public CountryA[] country { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public ServiceA[] services { get; set; }
    }
    public class Address
    {
        public En[] en { get; set; }
        public Ar[] ar { get; set; }
        public Formatted_Address formatted_address { get; set; }
    }
    public class Formatted_Address
    {
        public string en { get; set; }
        public string ar { get; set; }
    }
    public class Contact_Info
    {
        public string phone { get; set; }
    }
    public class En
    {
        public string short_name { get; set; }
        public string[] types { get; set; }
        public string long_name { get; set; }
    }
    public class Ar
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
    public class Geographic_Location
    {
        public Location location { get; set; }
    }
    public class Opening_Hours
    {
        public string day { get; set; }
        public string opens { get; set; }
        public string closes { get; set; }
    }
    public class Location
    {
        public float lat { get; set; }
        public Viewport viewport { get; set; }
        public float lng { get; set; }
    }
    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }
    public class Northeast
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
    public class Southwest
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
    public class NameAPI
    {
        public string en { get; set; }
        public string ar { get; set; }
    }
    public class CityA
    {
        public string countryCode { get; set; }
        public DescriptionCity description { get; set; }
        public string code { get; set; }
    }
    public class DescriptionCity
    {
        public string ar { get; set; }
        public string en { get; set; }
    }
    public class CountryA
    {
        public Description1Country description { get; set; }
        public string code { get; set; }
    }
    public class Description1Country
    {
        public string ar { get; set; }
        public string en { get; set; }
    }
    public class ServiceA
    {
        public Description2Services description { get; set; }
        public string code { get; set; }
    }
    public class Description2Services
    {
        public string ar { get; set; }
        public string en { get; set; }
    }


}