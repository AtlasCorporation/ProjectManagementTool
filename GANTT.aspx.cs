using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using System.Data;
using System.Text;


public partial class GANTT : System.Web.UI.Page
{
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // testihaku projektin id:llä
            dt = Atlas.Database.GetGanttData(1);
            chartGANTT.DataSource = dt;
            chartGANTT.Series["Dokumentaatio"].YValueMembers = "dokumentaatiotunnit";
            chartGANTT.Series["Ohjelmointi"].YValueMembers = "ohjelmointitunnit";
            chartGANTT.DataBind();

            gvData.DataSource = dt;
            gvData.DataBind();
        }
        catch (Exception ex)
        {
            lblFooter.Text = ex.Message;
        }
    }    
}

