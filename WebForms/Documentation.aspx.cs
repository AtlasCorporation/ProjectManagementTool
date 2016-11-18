using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Documentation : System.Web.UI.Page
{
    DocumentHandler dh = new DocumentHandler();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string path = Server.MapPath("~/Resources/TestiFilu.txt");
           ShowDocument.Text =  dh.ReadFile(path);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/Resources/TxtFilu.txt");
        dh.SaveFile(path, ModeText);
    }
}