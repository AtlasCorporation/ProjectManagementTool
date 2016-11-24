using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        if (!string.IsNullOrEmpty(txtProjectName.Text))
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
            project newProject = new project
            {
                name = projectName,
                description = projectDesc,
                github_username = githubUser,
                github_reponame = githubRepo
            };

            // Add project to DB
            if (!Database.AddProject(newProject))
                lblMessages.Text = "Project named '" + newProject.name + "' already exists!";

            // Add project to currently logged in user
            if (Session["LoggedUser"] != null)
            {
                string loggedUser = Session["LoggedUser"].ToString();
                Database.AddProjectToUser(loggedUser, newProject.id);
            }

            // User wants project to be public -> add it to default-user
            if (!cbPrivateProject.Checked)
            {
                Database.AddProjectToUser("Default", newProject.id);
            }

            Session["ActiveProject"] = newProject.id;          
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
             UpdateRepoList();
        else
            ddlGithubRepo.Items.Clear();
    }

    protected async void UpdateRepoList()
    {
        ddlGithubRepo.Items.Clear();
        lblMessages.Text = "";
        try
        {
            List<string> repos = await Github.GetReposForUser(txtGithubUser.Text);
            if (repos != null && repos.Count > 0)
            {
                ddlGithubRepo.DataSource = repos;
                ddlGithubRepo.DataBind();
            }
        }
        catch (Exception)
        {
            lblMessages.Text = "User '" + txtGithubUser.Text + "' does not exist.";
        }
    }
}