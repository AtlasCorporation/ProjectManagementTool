﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gantt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string GetJsonData()
    {
        // tarvitsee projektin id:n
        // Session["ActiveProject"] ei ole vielä int vaan object. tarvitaan int.

        if (Session["ActiveProject"] != null)
        {
            return SiteLogic.GetTasksInJson(Convert.ToInt32(Session["ActiveProject"]));
        }
        else return "";
        
        
    }
}