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
                if (Session["LoggedUserId"] != null)
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
        // TODO: tarvitaan userin ID sessionista.
        var data = Database.GetProjectWorkingHoursForUser(Convert.ToInt32(Session["ActiveProject"]), Convert.ToInt32(Session["LoggedUserId"]));

        ChartArea chartArea = new ChartArea("ChartArea");
        userPieChart.ChartAreas.Add(chartArea);
        userPieChart.ChartAreas["ChartArea"].Area3DStyle.Enable3D = true;
        userPieChart.Series.Clear();
        //userPieChart.Palette = ChartColorPalette.Chocolate;
        userPieChart.Titles.Add("Hours spent on project by " + Session["LoggedUser"]);
        userPieChart.Series.Add("WorkHours");
        userPieChart.Series["WorkHours"].ChartType = SeriesChartType.Pie;
        DataPoint point;
        
        foreach (Task item in data)
        {
            point = new DataPoint(0, item.Duration);
            //point.AxisLabel = item.Text;
            point.IsValueShownAsLabel = true;
            point.IsVisibleInLegend = true;
            point.LegendText = item.Text;
            userPieChart.Series["WorkHours"].Points.Add(point);
            userPieChart.Legends.Add(new Legend(item.Text));
        }
    }

    protected void InitMainPieChart()
    {
        var data = Database.GetProjectWorkingHours(Convert.ToInt32(Session["ActiveProject"]));

        ChartArea chartArea = new ChartArea("ChartArea");
        usersPieChart.ChartAreas.Add(chartArea);
        usersPieChart.ChartAreas["ChartArea"].Area3DStyle.Enable3D = true;
        usersPieChart.Series.Clear();
        //usersPieChart.Palette = ChartColorPalette.Pastel;
        usersPieChart.Titles.Add("Total hours spent on project");
        usersPieChart.Series.Add("WorkHours");
        usersPieChart.Series["WorkHours"].ChartType = SeriesChartType.Pie;
        DataPoint point;

        foreach (Task item in data)
        {
            point = new DataPoint(0, item.Duration);
            //point.AxisLabel = item.Text;
            point.IsValueShownAsLabel = true;
            point.IsVisibleInLegend = true;
            point.LegendText = item.Text;
            usersPieChart.Series["WorkHours"].Points.Add(point);
            usersPieChart.Legends.Add(new Legend(item.Text));
        }
    }

    /// <summary>
    /// Initializes home page.
    /// </summary>
    protected void InitProjectHomePage()
    {
        lblProjectName.Text = activeProject.name;
        if (!string.IsNullOrEmpty(activeProject.description))
            lblProjectDesc.Text = activeProject.description;
        if (!string.IsNullOrEmpty(activeProject.github_username) && !string.IsNullOrEmpty(activeProject.github_reponame))
            InitGithub();
    }

    /// <summary>
    /// Initializes stuff from Github (commits etc).
    /// </summary>
    protected async void InitGithub()
    {
        divCommitFeed.InnerHtml = "";
        try
        {
            List<GitHubCommit> commits = await Github.GetCommits(activeProject.github_username, activeProject.github_reponame);
            if (commits != null && commits.Count > 0)
            {
                foreach (GitHubCommit c in commits)
                {
                    divCommitFeed.InnerHtml += string.Format("<div class='feed-item'><div class='date'>{0}<br/>{1} pushed a commit:</div><div class='text'>&nbsp;&nbsp;<a href='{2}'>{3}</a></div></div>",
                                                            c.Commit.Author.Date.DateTime.ToShortDateString(), c.Commit.Author.Name, c.HtmlUrl, c.Commit.Message);
                }
            }
        }
        catch (Exception)
        {
            lblMessages.Text = "Failed loading commits from Github!";
        }
    }
}