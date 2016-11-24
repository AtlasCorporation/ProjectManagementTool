using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TaskEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /*List<Task> tasks = SiteLogic.GetTasks(1);
            ddlTasks.DataSource = tasks;
            ddlTasks.DataTextField = "Text";
            ddlTasks.DataValueField = "Id";
            ddlTasks.DataBind();
            */
            InitControls();
        }
    }

    protected void InitControls()
    {
        // anna funktiolle projektin id
        List<TaskNode> nodes = SiteLogic.GetTaskNodes(1);

        foreach (TaskNode node in nodes)
        {
            twTasks.Nodes.Add(node);
        }
        twTasks.CollapseAll();

    }
}