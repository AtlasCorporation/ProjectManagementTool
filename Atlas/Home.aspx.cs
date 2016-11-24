using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Octokit;
using System.Web.UI.DataVisualization.Charting;

public partial class Home : System.Web.UI.Page
{
    protected project activeProject;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if some project is currently active from Session value
        if (Session["ActiveProject"] != null)
        {
            // Get the active project's data from DB
            try
            {                
                activeProject = Database.GetProjectFromDb(Convert.ToInt32(Session["ActiveProject"]));

                // Pie chartit
                InitMainPieChart();
                if (Session["LoggedUser"] != null)
                {
                    InitUserPieChart();
                }
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Clearing piecharts");
            userPieChart.Series.Clear();
            usersPieChart.Series.Clear();
        }

        if (!IsPostBack && activeProject != null)
        {
            InitProjectHomePage();
        }
    }

    protected void InitUserPieChart()
    {
        // TODO: tarvitaan projektin ja userin ID sessionista.
        var data = Database.GetProjectWorkingHoursForUser(1, 7);

        ChartArea chartArea = new ChartArea("ChartArea");
        userPieChart.ChartAreas.Add(chartArea);
        userPieChart.ChartAreas["ChartArea"].Area3DStyle.Enable3D = true;
        userPieChart.Series.Clear();
        //pieChart.Palette = ChartColorPalette.EarthTones;
        userPieChart.Titles.Add("Hours spent on project by " + Session["LoggedUser"]);
        userPieChart.Series.Add("WorkHours");
        userPieChart.Series["WorkHours"].ChartType = SeriesChartType.Pie;
        DataPoint point;

        foreach (Task item in data)
        {
            point = new DataPoint(0, item.Duration);
            point.AxisLabel = item.Text;
            //point.LegendText = item.Name;
            userPieChart.Series["WorkHours"].Points.Add(point);
        }
    }

    protected void InitMainPieChart()
    {
        // TODO: tarvitaan projektin id sessionista
        var data = Database.GetProjectWorkingHours(1);

        ChartArea chartArea = new ChartArea("ChartArea");
        usersPieChart.ChartAreas.Add(chartArea);
        usersPieChart.ChartAreas["ChartArea"].Area3DStyle.Enable3D = true;
        usersPieChart.Series.Clear();
        //pieChart.Palette = ChartColorPalette.EarthTones;
        usersPieChart.Titles.Add("Total hours spent on project");
        usersPieChart.Series.Add("WorkHours");
        usersPieChart.Series["WorkHours"].ChartType = SeriesChartType.Pie;
        DataPoint point;

        foreach (Task item in data)
        {
            point = new DataPoint(0, item.Duration);
            point.AxisLabel = item.Text;
            //point.LegendText = item.Name;
            usersPieChart.Series["WorkHours"].Points.Add(point);
        }
    }

    /// <summary>
    /// Initializes home page.
    /// </summary>
    protected void InitProjectHomePage()
    {
        lblProjectName.Text = activeProject.name;
        lblProjectDesc.Text = activeProject.description;
        //InitGithub();
    }

    /// <summary>
    /// Initializes stuff from Github (commits etc).
    /// </summary>
    protected async void InitGithub()
    {
        lblCommitFeed.Text = "";
        List<GitHubCommit> commits = await Github.GetCommits(activeProject.github_username, activeProject.github_reponame);
        foreach (GitHubCommit c in commits)
        {
            if (c.Committer.Login != null && c.Commit.Message != null)
                lblCommitFeed.Text += "- " + c.Committer.Login + " -- " + c.Commit.Message + "<br/>";
        }
    }
}