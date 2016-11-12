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

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int hours;

    public int Hours
    {
        get { return hours; }
        set { hours = value; }
    }


    public Task(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}