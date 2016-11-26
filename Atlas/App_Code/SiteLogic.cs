using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for SiteLogic
/// </summary>

public class SiteLogic
{

    public SiteLogic()
    {

    }

    public static string GetTasksInJson(int projectID)
    {
        string tasksJson = "{data:[";
        List<Task> tasks = Database.GetTasks(projectID);
        Task tempTask;
        for (int i = 0; i < tasks.Count; i++)
        {
            tempTask = tasks.ElementAt(i);
            tasksJson += "{id:" + tempTask.GanttId + @", text:""" + tempTask.Text + @""", start_date:""" + tempTask.StartDate + @""", duration:" + tempTask.Duration;

            if (tempTask.Parent != null)
            {
                tasksJson += ", parent:" + tempTask.GanttParentId + "}";
            }
            else
            {
                tasksJson += "}";
            }
            if (i != tasks.Count - 1)
            {
                tasksJson += ",";
            }
            else
            {
                tasksJson += "]}";
            }
        }

        return tasksJson;
    }



    public static List<TaskNode> GetTaskNodes(int projectID)
    {
        var result = Database.GetProjectTasks(projectID);
        List<task> tasks = result.ToList();
        List<TaskNode> nodes = new List<TaskNode>();
        TaskNode tempNode;
        bool added;

        System.Diagnostics.Debug.WriteLine("Before removing roots:" + tasks.Count);

        // get rootnodes from tasks
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks.ElementAt(i).task_id == null)
            {
                tempNode = new TaskNode(tasks.ElementAt(i).id.ToString(), tasks.ElementAt(i).name);
                nodes.Add(tempNode);
                tasks.RemoveAt(i);
                i--;
            }
        }

        System.Diagnostics.Debug.WriteLine("After removing roots:" + tasks.Count);

        // arrange rest of the list into nodetrees
        while (tasks.Count > 0)
        {
            int initialCount = tasks.Count;
            for (int i = 0; i < tasks.Count; i++)
            {
                for (int x = 0; x < nodes.Count; x++)
                {
                    tempNode = new TaskNode(tasks.ElementAt(i).id.ToString(), tasks.ElementAt(i).name, tasks.ElementAt(i).task_id.ToString());
                    added = nodes.ElementAt(x).TryAddToParent(tempNode);
                    if (added)
                    {
                        tasks.RemoveAt(i);
                        if (tasks.Count == 0) { break; }
                    }
                }
            }
            // make sure the loop is not infinite
            if (tasks.Count == initialCount)
            {
                throw new ApplicationException("GetTaskNodes can not arrange tasks into nodes");
            }
        }

        return nodes;
    }

    #region backupit
    /*
    public static List<Task> GetTasks(int projectID)
    {
        List<Task> tasks = new List<Task>();
        IEnumerable<task> query = Database.GetProjectTasks(projectID);
        Task tempTask;

        string tempString;
        foreach (var item in query)
        {
            tempString = "";
            for (int i = 0; i < item.tier; i++)
            {
                tempString += "-";
            }
            tempTask = new Task(item.id, tempString + item.name);
            tasks.Add(tempTask);
        }
        return tasks;
    }*/
    #endregion
}
