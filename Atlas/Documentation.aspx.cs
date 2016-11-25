using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Documentation : System.Web.UI.Page
{
    DocumentHandler dh = new DocumentHandler();

    /*
    KAIKKI SERVER.MAPPATH KORJATTAVA PROJEKTIN NIMILLÄ, KUN OLLAAN MUOKKAAMASSA KYSEISTÄ PROJEKTIA!
    */

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string path = Server.MapPath("~/Resources/TestiFilu.txt");
            ShowDocument.Text = dh.ReadFile(path);
            ModeText.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ShowDocument.Visible = true;
        ModeText.Visible = false;
        btnVisibility();
        string path = Server.MapPath("~/Resources/TestiFilu.txt");
        dh.SaveFile(path, ModeText);
       // ShowDocument.Text = dh.ReadFile(path);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ShowDocument.Visible = false;
        ModeText.Visible = true;
        btnVisibility();
        string path = Server.MapPath("~/Resources/TestiFilu.txt");
        ModeText.Text = dh.EditFile(path);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowDocument.Visible = true;
        btnVisibility();
        ModeText.Visible = false;
    }
    private void btnVisibility()
    {
        btnSave.Visible = !btnSave.Visible;
        btnEdit.Visible = !btnEdit.Visible;
        btnCancel.Visible = !btnCancel.Visible;
    }
}