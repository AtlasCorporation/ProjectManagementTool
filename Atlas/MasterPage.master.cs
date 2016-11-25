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
        CheckLogin();
        if (!IsPostBack)
            InitProjects();
    }

    /// <summary>
    /// Initializes project selection listbox.
    /// </summary>
    protected void InitProjects()
    {
        // Always add "Create new" as first item (index = 0)
        lbProjects.Items.Add(new ListItem("Create new project"));

        // List of all projects user has
        List<project> userProjects = new List<project>();

        // Get all user's projects from DB
        try
        {
            if(Session["ActiveProject"] != null)
            {
                string activeProject = Session["ActiveProject"].ToString();
                lblProject.Text = activeProject;
            }
            // Check if some user is logged in
            if (Session["LoggedUser"] != null)
            {
                string loggedUser = Session["LoggedUser"].ToString();
                lblUser.Text = loggedUser;
                // Get logged in user's projects
                userProjects = Database.GetAllProjectsForUser(loggedUser);
            }
            // Not logged in, get Default-user's projects
            else
            {
                userProjects = Database.GetAllProjectsForUser("Default");
            }

            // Add all user's projects to listbox
            if (userProjects != null)
            {
                foreach (project p in userProjects)
                {
                    lbProjects.Items.Add(new ListItem(p.name, p.id.ToString()));
                }
            }
        }
        catch (Exception)
        {
        }
    }

    /// <summary>
    /// Changes link to logout if you're currently logged in and vice versa.
    /// </summary>
    protected void CheckLogin()
    {
        if (Session["LoggedUser"] != null)
        {
            aLogin.HRef = ResolveUrl("~/Logout.aspx");
            aLogin.InnerHtml = "<span class='glyphicon glyphicon-log-out'></span> Logout";
        }
        else
        {
            aLogin.HRef = ResolveUrl("~/Login.aspx");
            aLogin.InnerHtml = "<span class='glyphicon glyphicon-log-in'></span> Login";
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
