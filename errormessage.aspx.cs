using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Arabbankdll;
public partial class errormessage : System.Web.UI.Page
{
    string lang;

    protected void Page_Load(object sender, EventArgs e)
    {
        //lang = BusinessObject.GetCurrLangCode();
        lang = Session["lang"].ToString();

        if (lang == "EN")
        {
            lblErrorMessage.Text = "Sorry, your request can not  processed now. Please try again";
        }
        else
        {
            lblErrorMessage.Text = "نأسف لم يتم تنفيذ طلبك، يرجى المحاولة مرة أخرى";
        }
    }
}