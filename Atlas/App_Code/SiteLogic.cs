﻿using System;
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

    public static string GetTasksJson(int projectId)
    {
        IEnumerable<donetask> donetasks;
        IEnumerable<task> result = Database.GetProjectTasks(projectId);
        List<task> tasks = result.ToList();
        List<Task> parsedTasks = new List<Task>();


            for(int i = 0;i<tasks.Count();i++)
            {
                if(tasks.ElementAt(i).task_id == null)
                {
                    parsedTasks.Add(new Task(tasks.ElementAt(i).id, tasks.ElementAt(i).name));
                    tasks.RemoveAt(i);
                    i--;
                }
            }

            bool hasChild = false;
        

            while (tasks.Count > 0 )
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    for(int j = 0; j<parsedTasks.Count;j++)
                    {
                        // onko vanhempaa
                        if (tasks.ElementAt(i).task_id == parsedTasks.ElementAt(j).ID)
                        {
                            for(int k = 0;k<tasks.Count;k++)
                            {
                                // onko lapsia
                                if(tasks.ElementAt(i).id == tasks.ElementAt(k).task_id)
                                {
                                    // on, siirretään task parsittuihin ilman tuntitietoja
                                    parsedTasks.Add(new Task(tasks.ElementAt(i).id, tasks.ElementAt(i).name, parsedTasks.ElementAt(j).ID));
                                    tasks.RemoveAt(i);
                                    if (i == tasks.Count) { i--; }
                                    if (tasks.Count == 0) { break; }
                                    hasChild = true;
                                    break;
                                }
                            }
                            if (tasks.Count == 0) { break; }
                            if (!hasChild)
                            {
                                // ei lapsia, lisätään parsittuihin donetaskeina
                                donetasks = Database.GetDonetasks(tasks.ElementAt(i).id);
                                foreach(donetask dt in donetasks)
                                {
                                    parsedTasks.Add(new Task(tasks.ElementAt(i).id, tasks.ElementAt(i).name, dt.date.Value.Day + "-" + dt.date.Value.Month + "-" + dt.date.Value.Year, dt.worktime, parsedTasks.ElementAt(j).ID));
                                }
                                tasks.RemoveAt(i);
                                if(i == tasks.Count) { i--; }
                                if (tasks.Count == 0) { break; }
                            }
                            hasChild = false;
                        }
                    }
                    if(i < -1)
                    {
                        i = -1;
                    }
                    if (tasks.Count == 0) { break; }
                }
            }
            // lisätään tilapäiset id:t ganttia varten
            int x = 1;
            foreach(Task item in parsedTasks)
            {
                item.GanttId = x;
                x++;
            }

            foreach(Task item in parsedTasks)
            {
                if(item.Parent != null)
                {
                    item.GanttParentId = (from c in parsedTasks where item.Parent == c.ID select c.GanttId).FirstOrDefault();
                }
            }

            return ParseTasksIntoJson(parsedTasks);        
        

    }

    protected static string ParseTasksIntoJson(List<Task> tasks)
    {
        try
        {
            Task temptask;
            string json = "{data:[";
            for(int i = 0;i<tasks.Count;i++)
            {
                temptask = tasks.ElementAt(i);
                if (temptask.StartDate == null)
                {
                    json += "{id:" + temptask.GanttId + @", text:""" + temptask.Text;
                    if(temptask.Parent != null)
                    {
                        json += @""",parent:" + temptask.GanttParentId + "}";
                    }
                    else
                    {
                        json += @"""}";
                    }
                    
                }
                else
                {
                    json += "{id:" + temptask.GanttId + @", text:""" + temptask.Text + @""",start_date:""" + temptask.StartDate + @""",duration:" + temptask.Duration + ", parent:" + temptask.GanttParentId + "}";
                }
                if(i != tasks.Count -1)
                {
                    json += ",";
                }
            }
            json += "]}";
            return json;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public static List<donetask> GetDonetasks(int taskId, int userId)
    {
        IEnumerable<donetask> result = Database.GetDonetasks(taskId, userId);
        List<donetask> donetasks = result.ToList();
        return donetasks;
    }


    public static List<TaskNode> GetTaskNodes(int projectID)
    {
        var result = Database.GetProjectTasks(projectID);
        List<task> tasks = result.ToList();
        List<TaskNode> nodes = new List<TaskNode>();
        TaskNode tempNode;
        bool added;

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

        
        // arrange rest of the list into nodetrees
        while (tasks.Count > 0)
        {
            int initialCount = tasks.Count;
            for (int i = 0; i < tasks.Count; i++)
            {
                for (int x = 0; x < nodes.Count; x++)
                {
                    System.Diagnostics.Debug.WriteLine("i:" + i + "x:" + x);
                    System.Diagnostics.Debug.WriteLine("tasks.Count:" +  tasks.Count);
                    tempNode = new TaskNode(tasks.ElementAt(i).id.ToString(), tasks.ElementAt(i).name, tasks.ElementAt(i).task_id.ToString());
                    added = nodes.ElementAt(x).TryAddToParent(tempNode);
                    if (added)
                    {
                        tasks.RemoveAt(i);
                        if(i == tasks.Count) { i--; }
                        if (tasks.Count == 0) { break; }
                    }
                }
                if (tasks.Count == 0) { break; }
            }
            // make sure the loop is not infinite
            if (tasks.Count == initialCount)
            {
                throw new ApplicationException("GetTaskNodes can not arrange tasks into nodes");
            }
        }

        return nodes;
    }

    #region backups
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
    }
    
       public static string GetTasksInJson(int projectID)
    {
        string tasksJson = "{data:[";
        List<Task> tasks = Database.GetTasks(projectID);
        Task tempTask;
        for (int i = 0; i < tasks.Count; i++)
        {
            tempTask = tasks.ElementAt(i);
            tasksJson += "{id:" + tempTask.GanttId;
            if (tempTask.Parent != null)
            {
                tasksJson += @", text:""" + tempTask.Text + @""", start_date:""" + tempTask.StartDate + @""", duration:" + tempTask.Duration + ", parent:" + tempTask.GanttParentId + "}";
            }
            else
            {
                tasksJson += "}";
            }
            if (i != tasks.Count - 1)
            {
                tasksJson += ",";
            }
        }
        
        tasksJson += "]}";
        
        return tasksJson;
    }

     
     */
    #endregion
}