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
                // anna projektin ID getworkinghoursille
                if(Session["LoggedUser"] != null)
                {

                    //var data = Database.GetProjectWorkingHours(1);
                    // TODO: tarvitaan userin ID sessionista. Placeholderina nyt Prome-tilin id.
                    var data = Database.GetProjectWorkingHoursForUser(1, 7);
                    BindDataToGantt(data);

                    gvData.DataSource = data;
                    gvData.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        if (!IsPostBack && activeProject != null)
        {
            InitProjectHomePage();
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
            point = new DataPoint(0, item.Duration);
            point.AxisLabel = item.Text;
            //point.LegendText = item.Name;
            pieChart.Series["WorkHours"].Points.Add(point);
        }
    }
}