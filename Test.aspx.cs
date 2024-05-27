using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arabbankdll;
using System.Configuration;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Data;
using System.Net.Mail;
//using EASendMail;
public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Get_SavingsRate();


        //DateTime localDateTime, univDateTime, convertDt;

        //string strDateTime = DateTime.Now.ToString();

        //localDateTime = DateTime.Parse(strDateTime);
        //univDateTime = localDateTime.ToUniversalTime();

        //convertDt = univDateTime.AddHours(3);

        //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
        //DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(univDateTime, cstZone);

        //Label1.Text = cstTime.ToString();


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = EncodeString(TextBox1.Text);
    }
    public static string EncodeString(string encryptString)
    {
        string EncryptionKey = BusinessObject.Getenckey();
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public static string DecodeString(string cipherText)
    {
        string EncryptionKey = BusinessObject.Getenckey();
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public void Get_SavingsRate()
    {
        string savingrate = "https://dapi.arabbank.com/product/v1/loans?apikey=TUs3e1AIBERXw9mPK9Pa106iWW54fSGl%20&country=AE";
        string json = get_web_content(savingrate);

        dynamic array = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        //DataSet dataSet = Newtonsoft.Json.JsonConvert.DeserializeObject<DataSet>(json);

        //dynamic Result = array.result;

        string min = array["data"].auto[0].country.types[0].currencies[0].amount.min;
        string max = array["data"].auto[0].country.types[0].currencies[0].amount.max;
        string tenorfrom = array["data"].auto[0].country.types[0].currencies[0].tenor.min;
        string tenorto = array["data"].auto[0].country.types[0].currencies[0].tenor.max;

        string min1 = array["data"].personal[0].country.types[0].currencies[0].amount.min;
        string max1 = array["data"].personal[0].country.types[0].currencies[0].amount.max;
        string tenorfrom1 = array["data"].personal[0].country.types[0].currencies[0].tenor.min;
        string tenorto1 = array["data"].personal[0].country.types[0].currencies[0].tenor.max;

        string min2 = array["data"].housing[0].country.types[0].currencies[0].amount.min;
        string max2 = array["data"].housing[0].country.types[0].currencies[0].amount.max;
        string tenorfrom2 = array["data"].housing[0].country.types[0].currencies[0].tenor.min;
        string tenorto2 = array["data"].housing[0].country.types[0].currencies[0].tenor.max;

        string interest= array["data"].housing[0].country.types[0].currencies[0].interestRate;

        string savingrate2 = "https://dapi.arabbank.com/product/v1/loans/auto/calculators/auto?apikey=TUs3e1AIBERXw9mPK9Pa106iWW54fSGl&amount=5000&tenor=10&country=AE&currency=AED&rate=2.96";
        string json2 = get_web_content(savingrate2);

        dynamic array2 = Newtonsoft.Json.JsonConvert.DeserializeObject(json2);

        string value = array2.data[0].amount;


        //int count = Result.Count;

        //string selectedMonth = depositDropDownList1.SelectedValue;

        //string sMonth;
        ////string mnth;
        //long fromAmount;
        //long toAmount;
        //double depositAmount = 0.0;
        //dynamic rate;

        //if (selectedMonth == "1")
        //{
        //    sMonth = selectedMonth + " Month";
        //}
        //else if (selectedMonth == "12")
        //{
        //    sMonth = "1 Year";
        //}
        //else
        //{
        //    sMonth = selectedMonth + " Months";
        //}
        //if (depositAmountTextBox.Text != "")
        //{
        //    depositAmount = Convert.ToDouble(Server.HtmlEncode(depositAmountTextBox.Text));
        //}


        //for (int i = 0; i < count; i++)
        //{
        //    //fromAmount = Result[i].range.from.amount;
        //    //toAmount = Result[i].range.to.amount;
        //    //rate = Result[i].rate;

        //    //if ((depositAmount >= fromAmount && depositAmount <= toAmount))
        //    //{
        //    //    rateAmount = rate;
        //    //    break;
        //    //}

        //    string sample = Result[i];

        //    //}
        //    //return rateAmount;
        //}
    }


    public string get_web_content(string url)
    {
        Uri uri = new Uri(url);
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
        request.Method = WebRequestMethods.Http.Get;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string output = reader.ReadToEnd();
        response.Close();

        return output;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Label1.Text = DecodeString(TextBox1.Text);
    }

    protected void Sendemail_Click(object sender, EventArgs e)
    {
        System.Net.Mail.MailMessage msg = new MailMessage();
        msg.To.Add(new MailAddress("cinideeptekno@gmail.com", "Test"));
        msg.From = new MailAddress("smartbranch@abwebapp.com", "Test");
        msg.Subject = "This is a Test Mail";
        msg.Body = "This is a test message using Exchange OnLine";
        msg.IsBodyHtml = true;

        SmtpClient client = new SmtpClient();
        client.Host = "smtp.office365.com";
        client.Port = 587;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("smartbranch@abwebapp.com", "Q@9of#bard");
        
       
        //client.DeliveryMethod = SmtpDeliveryMethod.Network;
       

        //client.TargetName = "STARTTLS/smtp.office365.com";
        
        client.Send(msg);


        //SmtpMail oMail = new SmtpMail("TryIt");

        //// Your Offic 365 email address
        //oMail.From = "smartbranch@abwebapp.com";
        //// Set recipient email address
        //oMail.To = "cinideeptekno@gmail.com";

        //// Set email subject
        //oMail.Subject = "test email from office 365 account";
        //// Set email body
        //oMail.TextBody = "this is a test email sent from c# project.";

        //// Your Office 365 SMTP server address,
        //// You should get it from outlook web access.
        //SmtpServer oServer = new SmtpServer("smtp.office365.com");

        //// user authentication should use your
        //// email address as the user name.
        //oServer.User = "smartbranch@abwebapp.com";
        //oServer.Password = "Q@9of#bard";

        //// Set 587 port
        //oServer.Port = 587;

        //// detect SSL/TLS connection automatically
        //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

        ////Console.WriteLine("start to send email over SSL...");




        try
        {
            //SmtpClient oSmtp = new SmtpClient();
            //oSmtp.SendMail(oServer, oMail);
            //client.Send(msg);
            Label1.Text = "Message Sent Succesfully";
        }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
        }
    }
}
