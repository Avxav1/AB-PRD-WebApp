<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["LANG"] = "EN";

        // insert query id and time
    }

    void Session_End(object sender, EventArgs e)
    {
        Arabbankdll.Behaviorclass behavrObj = new Arabbankdll.Behaviorclass();
        var behId = Session["behId"];
        long BehaviorId = Convert.ToInt64(behId);

        if(Arabbankdll.BusinessObject.updateBehIdBySessionEnd(BehaviorId)<0)
        {
            return;
        }

        System.Data.DataSet ds = null;
        string CountryId = "0";
        string BranchId = "0";
        string BenchId = "0";

        if(Arabbankdll.Behaviorclass.getbranchdetails(Convert.ToInt64(behId),out ds)>=0)
        {
            if(ds.Tables[0].Rows.Count>0)
            {
                System.Data.DataRow row = ds.Tables[0].Rows[0];

                CountryId = row["Country"].ToString();
                BranchId = row["BranchName"].ToString();
                BenchId = row["DiscoveryBench"].ToString();

                behavrObj.Country = CountryId;
                behavrObj.BranchName = BranchId;
                behavrObj.DiscoveryBench = BenchId;

                if(behavrObj.UpdateStatistics()>=0)
                {

                }

            }
        }



    }

</script>
