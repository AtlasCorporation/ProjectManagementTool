using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Settings : System.Web.UI.Page
{
    protected project activeProject;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check login
        if (Session["LoggedUserId"] != null)
        {
            // Check if project is active
            if (Session["ActiveProject"] != null)
            {
                // Check if user is admin of this project
                if (Authorizer.CheckUserRoleForProject(Convert.ToInt32(Session["LoggedUserId"]), "admin", Convert.ToInt32(Session["ActiveProject"])))
                {
                    InitDivs(true);
                    try
                    {
                        // Get the active project's data from DB
                        activeProject = Database.GetProjectFromDb(Convert.ToInt32(Session["ActiveProject"]));
                        if (IsPostBack)
                        {
                            // Set focus back to ProjectDesc after postback
                            Page.SetFocus(txtProjectDesc);
                        }
                        else
                            InitProjectSettingsPage();
                    }
                    catch (Exception ex)
                    {
                        lblMessages.Text = ex.Message;
                    }
                }
                else
                {
                    InitDivs(false);
                    divAlert.InnerHtml = "<strong>Permission denied!</strong> You don't have permission to access this page.";
                }
            }
            else
            {
                InitDivs(false);
                divAlert.InnerHtml = "<strong>Select a project!</strong> Please select a project to change the settings.";
            }
        }
    }

    /// <summary>
    /// Shows/hides settings page.
    /// </summary>
    protected void InitDivs(bool showSettings)
    {
        divSettings.Visible = showSettings;
        divAlert.Visible = !showSettings;
    }

    /// <summary>
    /// Initializes settings page.
    /// </summary>
    protected void InitProjectSettingsPage()
    {
        if (activeProject != null)
        {     
            txtProjectName.Text = activeProject.name;
            if (!string.IsNullOrEmpty(activeProject.description))
                txtProjectDesc.Text = activeProject.description;        
            if (!string.IsNullOrEmpty(activeProject.github_username))
                txtGithubUser.Text = activeProject.github_username;
            if (!string.IsNullOrEmpty(activeProject.github_username) && !string.IsNullOrEmpty(activeProject.github_reponame))
                UpdateRepoList();
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtProjectName.Text))
            SaveChanges(txtProjectName.Text, txtProjectDesc.Text, txtGithubUser.Text, ddlGithubRepo.Text);
        else
            lblMessages.Text = "Please enter name for your project.";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx", true);
    }

    /// <summary>
    /// Saves changes to project's settings.
    /// </summary>
    protected void SaveChanges(string projectName, string projectDesc, string githubUser, string githubRepo)
    {
        if (activeProject == null)
            return;
        try
        {
            // Update project's properties to DB
            Database.UpdateProjectProperties(activeProject.id, projectName, projectDesc, githubUser, githubRepo);

            // Change project to private/public
            // Check if default-user has the project
            bool found = Database.UserHasProject("Default", activeProject.id);

            // User wants project to be public -> add it to default-user (if it's not there already)
            if (!cbPrivateProject.Checked)
            {
                if (!found)
                    Database.AddProjectToUser("Default", activeProject.id);
            }
            // User wants project to be private -> remove it from default-user (if it's there)
            else
            {
                if (found)
                    Database.RemoveProjectFromUser("Default", activeProject.id);
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
            UpdateRepoList();
        else
            ddlGithubRepo.Items.Clear();
    }

    /// <summary>
    /// Updates list of Github user's repositories when Github username is changed.
    /// </summary>
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

                // Select active project's repo if it was set
                ListItem listItem = ddlGithubRepo.Items.FindByText(activeProject.github_reponame);
                if (listItem != null)
                {
                    ddlGithubRepo.ClearSelection();
                    listItem.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessages.Text = ex.Message;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteProject();
    }

    protected void DeleteProject()
    {
        try
        {
            Database.DeleteProject(activeProject.id);
            Session["ActiveProject"] = null;
            Response.Redirect("Home.aspx", true);
        }    
        catch (Exception ex)
        {
            lblMessages.Text = ex.Message;
        }
    }
}