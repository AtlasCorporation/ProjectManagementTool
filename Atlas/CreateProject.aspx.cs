using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateProject : System.Web.UI.Page
{
    protected static AtlasEntities ctx;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            ctx = new AtlasEntities();
        }
    }

    protected void btnCreateProject_Click(object sender, EventArgs e)
    {
        lblMessages.Text = "";
        var result = from p in ctx.projects
                     orderby p.name
                     select new { p.name };
        foreach (var p in result)
        {
            lblMessages.Text += p.name + "<br>";
        }
    }
}