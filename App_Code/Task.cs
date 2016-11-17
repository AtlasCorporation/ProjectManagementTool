using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Task
/// </summary>
public class Task
{
    private int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    private string text;

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    private string startDate;

    public string StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    private int duration;

    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    public int? Parent { get; set; }

    private int ganttId;

    public int GanttId
    {
        get { return ganttId; }
        set { ganttId = value; }
    }

    private int ganttParentId;

    public int GanttParentId
    {
        get { return ganttParentId; }
        set { ganttParentId = value; }
    }

    public Task(int id, string name)
    {
        this.id = id;
        this.text = name;
    }

    public Task(int id, string name, string StartingDateTime, int WorkTime, int? parent, int ganttId)
    {
        this.id = id;
        this.text = name;
        this.startDate = StartingDateTime;
        this.duration = WorkTime;
        this.Parent = parent;
        this.ganttId = ganttId;
    }
}