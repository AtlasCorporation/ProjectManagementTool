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

    }

    protected void btnCreateProject_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtProjectName.Text))
            CreateNewProject(txtProjectName.Text, txtProjectDesc.Text, txtGithubUser.Text, txtGithubRepo.Text);
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
}