using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;

public partial class GANTT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblFooter.Text = ex.Message;
        }
    }
}