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

    }
    public static string GetJsonData()
    {
        SiteLogic sl = new SiteLogic();
        return sl.GetTasksInJson(1);
    }
}