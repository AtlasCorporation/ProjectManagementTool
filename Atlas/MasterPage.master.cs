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

    protected void InitProjects()
    {
        // Always add "Create new" as first item (index = 0)
        lbProjects.Items.Add(new ListItem("Create new project"));

        // TODO: Get all user's projects from database here!
        lbProjects.Items.Add(new ListItem("Projekti 1", "yy"));
        lbProjects.Items.Add(new ListItem("Projekti 2", "kaa"));
        lbProjects.Items.Add(new ListItem("Projekti 3", "koo"));
        lbProjects.Items.Add(new ListItem("Projekti 4", "nee"));
        lbProjects.Items.Add(new ListItem("Projekti 5", "vii"));
        lbProjects.Items.Add(new ListItem("Projekti 6", "kuu"));
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
            // TODO: Change currently active project
            Response.Redirect("Index.aspx", true);
        }
    }
}
