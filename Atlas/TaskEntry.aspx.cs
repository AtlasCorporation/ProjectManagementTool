using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TaskEntry : System.Web.UI.Page
{
    protected string selectedTaskName;
    protected string selectedTaskId;
    protected DateTime selectedDate;
    protected int userId;
    List<string> hours;
    List<string> minutes;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControls();
            if (Session["ActiveProject"] != null)
            {
                
            }
        }
        lblConfirmSave.Text = "";
        if(Session["LoggedUser"] != null)
        {
            //userId = Convert.ToInt32(Session["LoggedUser"]);
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
        hours = new List<string>();
        for(int i = 0;i<24;i++)
        {
            if(i<10){ hours.Add("0" + i); }
            else { hours.Add("" + i);}            
        }
        minutes = new List<string>();
        for(int i = 0;i<60;i++)
        {
            if (i < 10){ minutes.Add("0" + i); }
            else { minutes.Add("" + i); }
        }
        ddlWorkTime.DataSource = hours;
        ddlWorkTime.DataBind();
        ddlHours.DataSource = hours;
        ddlHours.DataBind();
        ddlHours.SelectedIndex = 8;
        ddlMinutes.DataSource = minutes;
        ddlMinutes.DataBind();
        calendar.SelectedDate = DateTime.Now;
    }

    protected void twTasks_SelectedNodeChanged(object sender, EventArgs e)
    {
        selectedTaskName = twTasks.SelectedNode.Text;
        selectedTaskId = twTasks.SelectedValue;
        lblHelp.Text = "Selected task:";
        lblSelectedTask.Text = selectedTaskName;
    }

    protected void btnShowAddTask_Click(object sender, EventArgs e)
    {
        addTaskDiv.Visible = true;
        btnShowAddTask.Enabled = false;
    }

    protected void btnAddTask_Click(object sender, EventArgs e)
    {
        if (Session["LoggedUser"] != null)
        {
            if (selectedTaskName != null && selectedTaskName != string.Empty && selectedTaskId != null && selectedTaskId != string.Empty)
            {
                if (tbTaskName.Text != null && tbTaskName.Text != string.Empty)
                {

                }
                else
                {
                    lblHelp.Text = "Give a task name!";
                    lblSelectedTask.Text = string.Empty;
                }

            }
            else
            {
                lblHelp.Text = "Select a task!";
                lblSelectedTask.Text = string.Empty;
            }
        }
        else
        {
            lblHelp.Text = "Login first!";
            lblSelectedTask.Text = string.Empty;
        }
        

    }

    protected void btnCancelAddTask_Click(object sender, EventArgs e)
    {
        btnShowAddTask.Enabled = true;
        addTaskDiv.Visible = false;
    }

    protected void btnLogHours_Click(object sender, EventArgs e)
    {
        if (Session["LoggedUser"] != null)
        {
            if(Session["ActiveProject"] != null)
            {
                if (selectedTaskName != null && selectedTaskName != string.Empty && selectedTaskId != null && selectedTaskId != string.Empty)
                {
                    if (twTasks.SelectedNode.ChildNodes.Count == 0)
                    {
                        int workingHours = Convert.ToInt32(ddlWorkTime.Text);
                        DateTime dateTime = new DateTime(calendar.SelectedDate.Year, calendar.SelectedDate.Month, calendar.SelectedDate.Day, Convert.ToInt32(ddlHours.Text), Convert.ToInt32(ddlMinutes.Text), 0);

                        /*int result = Database.AddDonetask(Convert.ToInt32(selectedTaskId), userId, workingHours, dateTime);
                        if (result != 0)
                        {
                            lblConfirmSave.Text = "Saved! Task: " + selectedTaskName + ", user: " + Session["LoggedUser"] + ", hours: " + ddlWorkTime.Text + ", started at: " + dateTime;
                        }
                        else lblConfirmSave.Text = "Tallennus epäonnistui!";*/
                    }
                    else
                    {
                        lblHelp.Text = "Select a task with no children!";
                        lblSelectedTask.Text = string.Empty;
                    }
                }
                else
                {
                    lblHelp.Text = "Select a task!";
                    lblSelectedTask.Text = string.Empty;
                }
            }
            else
            {
                lblHelp.Text = "Select a project first!";
                lblSelectedTask.Text = string.Empty;
            }            
        }
        else
        {
            lblHelp.Text = "Login first!";
            lblSelectedTask.Text = string.Empty;
        }        
    }

    protected void btnDeleteTask_Click(object sender, EventArgs e)
    {

    }

    protected void btnCalendarBack_Click(object sender, EventArgs e)
    {
        if (calendar.VisibleDate == DateTime.MinValue)
        {
            calendar.VisibleDate = DateTime.Now;
        }
        calendar.VisibleDate = calendar.VisibleDate.AddYears(-1);
    }

    protected void btnCalendarForward_Click(object sender, EventArgs e)
    {
        if (calendar.VisibleDate == DateTime.MinValue)
        {
            calendar.VisibleDate = DateTime.Now;
        }

        calendar.VisibleDate = calendar.VisibleDate.AddYears(1);
    }
}