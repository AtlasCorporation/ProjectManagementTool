using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Documentation : System.Web.UI.Page
{
    DocumentHandler dh = new DocumentHandler();
    protected string activeProjectName;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEdit.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        ddlSelectTemplate.Visible = false;
        lblTemplate.Visible = false;
        btnSave.Visible = false;

        if (!IsPostBack)
        {
            dh.readTemplates(ddlSelectTemplate, Server.MapPath("~/Resources"));
        }

        if (Session["ActiveProject"] != null)
        {
            divDocumentation.Visible = true;
            divAlert.Visible = false;

            // Roles "user" and "admin" are authorized to edit documentation
            List<string> authorizedRolesToEditDocs = new List<string> { "user", "admin" };
            if (Session["LoggedUserId"] != null)
            {
                if (Authorizer.CheckUserRoleForProject(Convert.ToInt32(Session["LoggedUserId"]), authorizedRolesToEditDocs, Convert.ToInt32(Session["ActiveProject"]))) { 
                    btnEdit.Visible = true; // Allow edit if logged-in user has role "user" or "admin"
                    ddlSelectTemplate.Visible = true;
                    lblTemplate.Visible = true;
                }
            }

            // Create folder for active project if it doesn't exist already
            activeProjectName = Database.GetProjectFromDb(Convert.ToInt32(Session["ActiveProject"])).name;
            string projectFolder = Server.MapPath("~/Resources/" + activeProjectName);
            if (!Directory.Exists(projectFolder))
                Directory.CreateDirectory(projectFolder);

            // Create txt-file for active project it it doesn't exist already
            string txtFile = projectFolder + "/Doc.txt";
            if (!File.Exists(txtFile))
                File.Create(txtFile);
            ShowDocument.Text = dh.ReadFile(txtFile);
            ModeText.Visible = false;
        }
        else
        {
            divDocumentation.Visible = false;
            divAlert.Visible = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ShowDocument.Visible = true;
        ModeText.Visible = false;
        btnEdit.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        ddlSelectTemplate.Visible = true;
        lblTemplate.Visible = true;
        string path = Server.MapPath("~/Resources/" + activeProjectName + "/Doc.txt");
        dh.SaveFile(path, ModeText);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ShowDocument.Visible = false;
        ModeText.Visible = true;
        btnEdit.Visible = false;
        btnSave.Visible = true;
        btnCancel.Visible = true;
        ddlSelectTemplate.Visible = false;
        lblTemplate.Visible = false;
        string path = "";
        if (ddlSelectTemplate.SelectedIndex > 0)
        {
            path = Server.MapPath("~/Resources/" + ddlSelectTemplate.Items[ddlSelectTemplate.SelectedIndex].Value.ToString());
        }
        else {
            path = Server.MapPath("~/Resources/" + activeProjectName + "/Doc.txt");
        }
        ModeText.Text = dh.EditFile(path);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowDocument.Visible = true;
        btnEdit.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        ModeText.Visible = false;
        ddlSelectTemplate.Visible = true;
        lblTemplate.Visible = true;
    }
    private void btnVisibility()
    {
        btnSave.Visible = !btnSave.Visible;
        btnEdit.Visible = !btnEdit.Visible;
        btnCancel.Visible = !btnCancel.Visible;
    }
}