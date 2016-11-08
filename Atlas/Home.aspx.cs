using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if some project is currently active from Session value
            if (Session["ActiveProject"] != null)
            {
                // Get the active project's data from DB and display it
                project activeProject = GetProjectFromDb(Session["ActiveProject"].ToString());
                InitProjectHomePage(activeProject);
            }
        }
    }

    /// <summary>
    /// Gets project object from DB with the given project name.
    /// </summary>
    protected project GetProjectFromDb(string projectName)
    {
        try
        {
            using (var db = new atlasEntities())
            {
                var ret = from p in db.projects
                          where p.name == projectName
                          select p;
                var project = ret.FirstOrDefault();
                if (project != null)
                    return project;
            }
        }
        catch (Exception ex)
        {
            lblMessages.Text = ex.Message;
        }
        return null;
    }

    /// <summary>
    /// Initializes home page's values with the given project.
    /// </summary>
    protected void InitProjectHomePage(project p)
    {
        txtProjectName.Text = p.name;
        txtProjectDesc.Text = p.description;
    }
}