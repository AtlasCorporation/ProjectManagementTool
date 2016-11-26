using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TaskEntry : System.Web.UI.Page
{
    protected DateTime selectedDate;
    protected int userId;
    List<string> hours;
    List<string> minutes;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControls();
        }
        
        if(twTasks.SelectedNode != null)
        {
            lblSelectedTask.Text = twTasks.SelectedNode.Text;
        }

        if(Session["LoggedUser"] != null)
        {
            //userId = Convert.ToInt32(Session["LoggedUser"]);
        }
    }

    protected void InitControls()
    {
        // anna funktiolle projektin id
        BuildTaskTree();
        twTasks.CollapseAll();

        hours = new List<string>();
        for(int i = 0;i<24;i++)
        {
            if(i<10){ hours.Add("0" + i); }
            else { hours.Add("" + i);}            
        }

        ddlHours.DataSource = hours;
        ddlHours.DataBind();
        ddlHours.SelectedIndex = 8;

        minutes = new List<string>();

        for(int i = 0;i<60;i++)
        {
            if (i < 10){ minutes.Add("0" + i); }
            else { minutes.Add("" + i); }
        }

        ddlMinutes.DataSource = minutes;
        ddlMinutes.DataBind();

        hours = new List<string>();

        for (int i = 1; i <= 24; i++)
        {
            hours.Add("" + i);
        }

        ddlWorkTime.DataSource = hours;
        ddlWorkTime.DataBind();

        calendar.VisibleDate = DateTime.Now;
        calendar.SelectedDate = DateTime.Now;
    }
        
    protected void BuildTaskTree()
    {
        List<TaskNode> nodes = SiteLogic.GetTaskNodes(Convert.ToInt32(Session["ActiveProject"]));

        foreach (TaskNode node in nodes)
        {
            twTasks.Nodes.Add(node);
        }            

        if(twTasks.Nodes.Count == 0)
        {
            virginDiv.Visible = true;
            taskControlDiv.Visible = false;
        }
        else
        {
            virginDiv.Visible = false;
            taskControlDiv.Visible = true;
        }
    }

    protected void twTasks_SelectedNodeChanged(object sender, EventArgs e)
    {
        lblSelectedTask.Text = twTasks.SelectedNode.Text;
    }

    protected void btnShowAddTask_Click(object sender, EventArgs e)
    {
        addTaskDiv.Visible = true;
        btnShowAddTask.Enabled = false;
        removeTaskDiv.Visible = false;
        btnShowDeleteTask.Enabled = true;
        lblHelp.Text = string.Empty;
    }

    protected void btnVirginTask_Click(object sender, EventArgs e)
    {
        if (Session["ActiveProject"] != null)
        {
            if (Session["LoggedUser"] != null)
            {
                if(tbVirginTask.Text != string.Empty && tbVirginTask.Text != null)
                {
                    int result = Database.AddTask(null, Convert.ToInt32(Session["ActiveProject"]), tbVirginTask.Text);
                    if (result > 0)
                    {
                        lblHelp.Text = "Save successful!";
                        virginDiv.Visible = false;
                        taskControlDiv.Visible = true;
                        twTasks.Nodes.Clear();
                        BuildTaskTree();
                    }
                    else { lblHelp.Text = "Save failed!"; }
                }
                else
                {
                    lblHelp.Text = "Type a name!";
                }
            }
            else
            {
                lblHelp.Text = "Login first!";
                lblSelectedTask.Text = string.Empty;
            }
        }
        else
        {
            lblHelp.Text = "Choose a project first!";
            lblSelectedTask.Text = string.Empty;
        }
    }

    protected void btnAddTask_Click(object sender, EventArgs e)
    {       

        if(Session["ActiveProject"] != null)
        {
            if (Session["LoggedUser"] != null)
            {
                
                if (tbTaskName.Text != null && tbTaskName.Text != string.Empty)
                {
                    if(cbIsRoot.Checked)
                    {
                        int result = Database.AddTask(null, Convert.ToInt32(Session["ActiveProject"]), tbTaskName.Text);
                        if(result > 0)
                        {
                            lblHelp.Text = "Save successful!";
                            addTaskDiv.Visible = false;
                            twTasks.Nodes.Clear();
                            BuildTaskTree();
                        }
                        else
                        {
                            lblHelp.Text = "Save failed!";
                        }
                    }
                    else 
                    {
                        if (twTasks.SelectedNode != null)
                        {
                            int? taskId = Convert.ToInt32(twTasks.SelectedValue);
                            int result = Database.AddTask(taskId, Convert.ToInt32(Session["ActiveProject"]), tbTaskName.Text);
                            if (result > 0)
                            {
                                lblHelp.Text = "Save successful!";
                                addTaskDiv.Visible = false;
                                twTasks.Nodes.Clear();
                                BuildTaskTree();
                            }
                            else
                            {
                                lblHelp.Text = "Save failed!";
                            }
                        }
                        else
                        {
                            lblHelp.Text = "Select a task as parent or check the 'Create root task' -box!";
                            lblSelectedTask.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    lblHelp.Text = "Name the new task!";
                    lblSelectedTask.Text = string.Empty;
                }
            }
            else
            {
                lblHelp.Text = "Login first!";
                lblSelectedTask.Text = string.Empty;
            }
        }
        else
        {
            lblHelp.Text = "Choose a project first!";
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
                if (twTasks.SelectedNode != null)
                {
                    if (twTasks.SelectedNode.ChildNodes.Count == 0)
                    {
                        int workingHours = Convert.ToInt32(ddlWorkTime.Text);
                        DateTime dateTime = new DateTime(calendar.SelectedDate.Year, calendar.SelectedDate.Month, calendar.SelectedDate.Day, Convert.ToInt32(ddlHours.Text), Convert.ToInt32(ddlMinutes.Text), 0);

                       /* int result = Database.AddDonetask(Convert.ToInt32(twTasks.SelectedNode.Value), Session["LoggedUser"], workingHours, dateTime);
                        if (result != 0)
                        {
                            lblConfirmSave.Text = "Saved! Task: " + twTasks.SelectedNode.Text + ", user: " + Session["LoggedUser"] + ", hours: " + ddlWorkTime.Text + ", started at: " + dateTime;
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

    protected void btnConfirmDelete_Click(object sender, EventArgs e)
    {
        int taskId = Convert.ToInt32(twTasks.SelectedNode.Value);
        int result = Database.RemoveTask(taskId);
        lblHelp.Text = result + " rows deleted!";
        twTasks.Nodes.Clear();
        BuildTaskTree();
        removeTaskDiv.Visible = false;
        btnShowDeleteTask.Enabled = true;
    }

    protected void btnCancelDelete_Click(object sender, EventArgs e)
    {        
        removeTaskDiv.Visible = false;
        btnShowDeleteTask.Enabled = true;
    }

    protected void btnShowDeleteTask_Click(object sender, EventArgs e)
    {
        if (twTasks.SelectedNode != null)
        {
            if (twTasks.SelectedNode.ChildNodes.Count == 0)
            {
                removeTaskDiv.Visible = true;
                addTaskDiv.Visible = false;
                btnShowDeleteTask.Enabled = false;
                btnShowAddTask.Enabled = true;
                lblHelp.Text = string.Empty;
            }
            else
            {
                lblHelp.Text = "Can not delete a task with subtasks";
            }
        }
        else
        {
            lblHelp.Text = "Select a task to delete!";
        }
    }
}