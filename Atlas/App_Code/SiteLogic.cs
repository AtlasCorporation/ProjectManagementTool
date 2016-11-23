using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            //tasksJson += JsonConvert.SerializeObject(tasks.ElementAt(i));
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
}
