using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitProjects();
        }
    }

    /// <summary>
    /// Initializes project selection listbox.
    /// </summary>
    protected void InitProjects()
    {
        // Always add "Create new" as first item (index = 0)
        lbProjects.Items.Add(new ListItem("Create new project"));

        // Get all user's projects from DB and add them to listbox
        try
        {
            List<project> userProjects = Database.GetAllProjectsForUser("Default"); // Change this if logged in (once login-system is done). Default-user has projects that are displayed to anyone (= not logged in).
            foreach (project p in userProjects)
            {
                lbProjects.Items.Add(new ListItem(p.name, p.id.ToString()));
            }
        }
        catch (Exception)
        {
        }
    }

    protected void lbProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbProjects.SelectedIndex == 0)
        {
            // Go to project creation page
            Response.Redirect("CreateProject.aspx", true);
        }
        else
        {
            // Change currently active project
            Session["ActiveProject"] = lbProjects.SelectedValue;
            Response.Redirect("Home.aspx", true);
        }
    }
}
