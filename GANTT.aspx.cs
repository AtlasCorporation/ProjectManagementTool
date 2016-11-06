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
    //DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // testihaku projektin id:llä
            /*  dt = Atlas.Database.GetGanttData(1);
              chartGANTT.DataSource = dt;
              chartGANTT.Series["Dokumentaatio"].YValueMembers = "dokumentaatiotunnit";
              chartGANTT.Series["Ohjelmointi"].YValueMembers = "ohjelmointitunnit";
              chartGANTT.DataBind();

              gvData.DataSource = dt;
              gvData.DataBind();*/

            // lblFooter.Text = ""+Atlas.Database.EFTEST(1);
            atlasEntities ctx = new atlasEntities();
            var worktime = (from a in ctx.tasks
                           join fulfilledTask in ctx.donetasks on a.id equals fulfilledTask.task_id
                           where a.project_id == 1
                           select new { a.name, fulfilledTask.worktime, a.task_id }).ToList();

            int? x = 0;
            int hours = 0;

            foreach(var item in worktime)
            {
                
            }


            gvData.DataSource = worktime;
            gvData.DataBind();

        }
        catch (Exception ex)
        {
            lblFooter.Text = ex.Message;
        }
    }    

    
}

