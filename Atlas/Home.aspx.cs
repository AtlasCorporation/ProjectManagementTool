using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }

        if (!IsPostBack)
        {
            InitProjectHomePage();
        }
    }

    /// <summary>
    /// Initializes home page.
    /// </summary>
    protected void InitProjectHomePage()
    {
        if (activeProject != null)
        {
            txtProjectName.Text = activeProject.name;
            txtProjectDesc.Text = activeProject.description;
        }
    }
}