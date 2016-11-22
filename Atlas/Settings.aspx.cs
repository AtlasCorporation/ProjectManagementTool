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
        // Check if some project is currently active from Session value
        if (Session["ActiveProject"] != null)
        {
            try
            {
                // Get the active project's data from DB
                activeProject = Database.GetProjectFromDb(Convert.ToInt32(Session["ActiveProject"]));
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        if (IsPostBack)
        {
            // Set focus back to ProjectDesc after postback
            Page.SetFocus(txtProjectDesc);
        }
        else
            InitProjectSettingsPage();
    }

    /// <summary>
    /// Initializes settings page.
    /// </summary>
    protected void InitProjectSettingsPage()
    {
        if (activeProject != null)
        {     
            txtProjectName.Text = activeProject.name;
            txtProjectDesc.Text = activeProject.description;
            txtGithubUser.Text = activeProject.github_username;
            if (!string.IsNullOrEmpty(activeProject.github_username))
            { 
                UpdateRepoList();
            }
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
    /// Changes existing project's properties.
    /// </summary>
    protected void SaveChanges(string projectName, string projectDesc, string githubUser, string githubRepo)
    {
        if (activeProject == null)
            return;
        try
        {
            using (var db = new atlasEntities())
            {
                project projectToChange = null;
                // Find the active project from DB
                foreach (project p in db.projects)
                {
                    if (p.id == activeProject.id)
                    {
                        projectToChange = p;
                        break;
                    }
                }

                // Update project's properties
                projectToChange.name = projectName;
                projectToChange.description = projectDesc;
                projectToChange.github_username = githubUser;
                projectToChange.github_reponame = githubRepo;

                // Change project to private/public
                user defaultUser = null;
                foreach (var u in db.users)
                {
                    if (u.username == "Default")
                    {
                        defaultUser = u;
                        break;
                    }
                }
                if (defaultUser != null)
                {
                    // Check if default-user has the project
                    bool found = false;
                    foreach (project p in defaultUser.projects)
                    {
                        if (p.id == projectToChange.id)
                            found = true;
                    }

                    // User wants project to be public -> add it to default-user (if it's not there already)
                    if (!cbPrivateProject.Checked)
                    {                      
                        if (!found)
                            defaultUser.projects.Add(projectToChange);
                    }
                    // User wants project to be private -> remove it from default-user (if it's there)
                    else
                    {
                        if (found)
                            defaultUser.projects.Remove(projectToChange);
                    }
                }
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
            using (var db = new atlasEntities())
            {
                project projectToDelete = null;
                // Find the active project from DB
                foreach (project p in db.projects)
                {
                    if (p.id == activeProject.id)
                    {
                        projectToDelete = p;
                        break;
                    }
                }

                // Delete project's foreign key from all users
                var users = db.users.ToList();
                foreach (var u in users)
                {
                    u.projects.Remove(projectToDelete);
                }

                // Delete project
                db.projects.Remove(projectToDelete);
                db.SaveChanges();
            }
            Session["ActiveProject"] = null;
            Response.Redirect("Home.aspx", true);
        }
        catch (Exception ex)
        {
            lblMessages.Text = ex.Message;
        }
    }
}