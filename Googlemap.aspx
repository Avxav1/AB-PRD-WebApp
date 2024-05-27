<%--<%@ OutputCache Duration="360" VaryByParam="*" Location="ServerAndClient" %>--%>
<%@ Page Language="C#" Async="true" UICulture="auto" AutoEventWireup="true" CodeFile="Googlemap.aspx.cs" Inherits="Googlemap" meta:resourcekey="PageResource1" %>
<%--<%@ Register TagPrefix="cc1" Namespace="ComboImg" Assembly="ComboImg" %>--%>

<!DOCTYPE html>
<% 
    //Response.Buffer = false;
    Response.CacheControl = "private";
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Locator</title>
    
    <link rel="stylesheet" href="css/sample.css" />
    <script src="js/jquery/jquery-1.9.0.min.js"></script>    
    <!-- <msdropdown> -->
    <link rel="stylesheet" type="text/css" href="css/msdropdown/dd.css" />
    <script src="js/msdropdown/jquery.dd.js"></script>
    <!-- </msdropdown> -->
    <link rel="stylesheet" type="text/css" href="css/msdropdown/skin2.css" />
    <link rel="stylesheet" type="text/css" href="css/msdropdown/flags.css" />
    <link href="Content/ABDS/locator.css" rel="stylesheet" />
   <%-- <script src="lib/js/googlemap.js"></script>--%>
     <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAR8OVOZBMzLL_EIzO83NDkj1vZGklDENI"></script>
    <script>
        $(document).ready(function (e) {
            //no use
            try {
                var pages = $("#pages").msDropdown({
                    on: {
                        change: function (data, ui) {
                            var val = data.value;
                            if (val != "")
                                window.location = val;
                        }
                    }
                }).data("dd");

                var pagename = document.location.pathname.toString();
                pagename = pagename.split("/");
                pages.setIndexByValue(pagename[pagename.length - 1]);
                $("#ver").html(msBeautify.version.msDropdown);
            } catch (e) {
                //console.log(e);	
            }

            $("#ver").html(msBeautify.version.msDropdown);

            //convert
            $("select").msDropdown({ roundedBorder: false });
            //createByJson();
            $("#tech").data("dd");
        });
        function showValue(h) {
            console.log(h.name, h.value);
        }
        $("#tech").change(function () {
            console.log("by jquery: ", this.value);
        })

    </script>
</head>
<body>  
    <form id="form1" runat="server">

      
            <div id="container">
            <div class="row">
                <div id="columnleft" runat="server" class="column left" dir='<%$ Resources: Resource,TextDirection%>'>
                    <div class="productlist">
                        <br />
                        <div class="alltext">
                            <asp:Label ID="lblChooseCity" runat="server" ></asp:Label>
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="dropcalc" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div style="height:15px;"></div>
                           
                            <asp:Label ID="lblChooseLocal" runat="server" ></asp:Label>
                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropcalc" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                           <div style="height:15px;"></div>
                            <asp:Label ID="Label2" runat="server" ></asp:Label>
                            <asp:DropDownList ID="ddlATM" runat="server" CssClass="dropcalc" AutoPostBack="True" OnSelectedIndexChanged="ddlATM_SelectedIndexChanged">
                            </asp:DropDownList>
                          <div style="height:15px;"></div>
                            <asp:Label ID="Label1" runat="server" ></asp:Label>
                            <asp:DropDownList ID="ddlITM" runat="server" CssClass="dropcalc" AutoPostBack="True" OnSelectedIndexChanged="ddlITM_SelectedIndexChanged">
                            </asp:DropDownList>
                           <div style="height:15px;"></div>
                            <asp:Label ID="lblKiosk" runat="server" ></asp:Label>
                            <asp:DropDownList runat="server" CssClass="dropcalc" ID="ddlKiosk" AutoPostBack="true" OnSelectedIndexChanged="ddlKiosk_SelectedIndexChanged"></asp:DropDownList>
                          <div style="height:15px;"></div>
                             <asp:Label ID="lblSelfService" runat="server" ></asp:Label>
                            <asp:DropDownList runat="server" CssClass="dropcalc" ID="ddlSelfService" AutoPostBack="true" OnSelectedIndexChanged="ddlSelfService_SelectedIndexChanged"></asp:DropDownList>
                          <div style="height:15px;"></div>
                             <asp:Label ID="lblATMPlus" runat="server" ></asp:Label>
                            <asp:DropDownList runat="server" CssClass="dropcalc" ID="ddlATMPlus" AutoPostBack="true" OnSelectedIndexChanged="ddlATMPlus_SelectedIndexChanged"></asp:DropDownList>
                           
                            
                        </div>
                    </div>
                </div>
                <div id="columnright" runat="server" class="column right">
                    <div runat="server" id="mapCanvas" class="googlemapstyle"></div>
                </div>
            </div>
        </div>
      
    </form>
                
           
</body>
</html>