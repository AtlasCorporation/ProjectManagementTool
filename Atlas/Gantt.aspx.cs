using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gantt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["ActiveProject"] != null)
        {
            mainDiv.Visible = true;
            reminderDiv.Visible = false;
        }
        else
        {
            mainDiv.Visible = false;
            reminderDiv.Visible = true;
        }
    }
    public string GetJsonData()
    {
        if (Session["ActiveProject"] != null)
        {
            return SiteLogic.GetTasksJson(Convert.ToInt32(Session["ActiveProject"]));
        }
        else return "";
    }
}