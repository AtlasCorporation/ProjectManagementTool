using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Task
/// </summary>
public class Task
{
    private int ID;

    public int id
    {
        get { return ID; }
        set { ID = value; }
    }

    private string Text;

    public string text
    {
        get { return Text; }
        set { Text = value; }
    }

    private string Start_date;

    public string start_date
    {
        get { return Start_date; }
        set { Start_date = value; }
    }

    private int Duration;

    public int duration
    {
        get { return Duration; }
        set { Duration = value; }
    }

    public int? parent { get; set; }


    public Task(int id, string name)
    {
        this.id = id;
        this.text = name;
    }

    public Task(int id, string name, string StartingDateTime, int WorkTime, int? Parent)
    {
        this.id = id;
        this.text = name;
        this.start_date = StartingDateTime;
        this.duration = WorkTime;
        this.parent = Parent;
    }
}