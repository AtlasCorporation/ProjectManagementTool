using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resources_ErrorSite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnBackToPrevious_Click(object sender, EventArgs e)
    {
        DefaultHttpHandler dhh = new DefaultHttpHandler();
        string prevPage = Request.QueryString["aspxerrorpath"];
        Response.Redirect(prevPage);
    }
}