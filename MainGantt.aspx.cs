using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainGantt : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public static string GetJsonData()
    {
        Atlas.SiteLogic sl = new Atlas.SiteLogic();
        return sl.GetTasksInJson(1);
    }


}