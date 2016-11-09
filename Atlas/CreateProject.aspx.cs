using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Octokit;
using System.Threading.Tasks;

public partial class CreateProject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set focus back to ProjectDesc after postback
        if (IsPostBack)
            Page.SetFocus(txtProjectDesc);
    }

    protected void btnCreateProject_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtProjectName.Text))
            CreateNewProject(txtProjectName.Text, txtProjectDesc.Text, txtGithubUser.Text, ddlGithubRepo.Text);
        else
            lblMessages.Text = "Please enter name for your project.";
    }

    /// <summary>
    /// Creates a new project with given properties.
    /// Adds the new project to the database and sets it as active project.
    /// </summary>
    protected void CreateNewProject(string projectName, string projectDesc, string githubUser, string githubRepo)
    {
        try
        {
            using (var db = new atlasEntities())
            {
                var projects = db.Set<project>();
                project p = new project
                {
                    name = projectName,
                    description = projectDesc,
                    github_username = githubUser,
                    github_reponame = githubRepo
                };
                projects.Add(p);
                Session["ActiveProject"] = p.name;
                db.SaveChanges();
            }         
            Response.Redirect("Home.aspx", true);
        }
        catch (Exception ex)
        {
            lblMessages.Text = ex.Message;
        }
    }

    protected void txtGithubUser_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtGithubUser.Text))
            RegisterAsyncTask(new PageAsyncTask(GetReposForUser));
        else
            ddlGithubRepo.Items.Clear();
    }

    /// <summary>
    /// Gets Github repositories for given user.
    /// </summary>
    public async Task GetReposForUser()
    {
        try
        {
            ddlGithubRepo.Items.Clear();
            lblMessages.Text = "";
            var client = new GitHubClient(new ProductHeaderValue("atlas"));
            var repos = await client.Repository.GetAllForUser(txtGithubUser.Text);
            if (repos != null && repos.Count > 0)
            {
                foreach (var r in repos)
                {
                    ddlGithubRepo.Items.Add(new ListItem(r.Name, r.Name));
                }
            }
            else
            {
                lblMessages.Text = "Could not find any repositories for user '" + txtGithubUser.Text + "'.";
            }
        }
        catch (Exception)
        {
            lblMessages.Text = "User '" + txtGithubUser.Text + "' does not exist.";
        }
    }
}