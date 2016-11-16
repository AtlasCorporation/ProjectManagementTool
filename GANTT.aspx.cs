using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using System.Data;
using System.Text;
using System.Web.UI.DataVisualization.Charting;

public partial class GANTT : System.Web.UI.Page
{
    //DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

            Atlas.Database db = new Atlas.Database();

            // anna projektin ID getworkinghoursille
            var data = db.GetProjectWorkingHours(1);
            BindDataToGantt(data);

            gvData.DataSource = data;
            gvData.DataBind();

    }    

    protected void BindDataToGantt(IEnumerable<Task> tasks)
    {        
        ChartArea chartArea = new ChartArea("ChartArea");
        pieChart.ChartAreas.Add(chartArea);
        pieChart.ChartAreas["ChartArea"].Area3DStyle.Enable3D = true;
        pieChart.Series.Clear();
        //pieChart.Palette = ChartColorPalette.EarthTones;
        pieChart.Titles.Add("Hours spent on project");
        pieChart.Series.Add("WorkHours");
        pieChart.Series["WorkHours"].ChartType = SeriesChartType.Pie;
        DataPoint point;

        foreach (Task item in tasks)
        {
            point = new DataPoint(0,item.duration);
            point.AxisLabel = item.text;
            //point.LegendText = item.Name;
            pieChart.Series["WorkHours"].Points.Add(point);                     
        }        
    }

    
}

