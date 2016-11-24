using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for TaskNode
/// </summary>
public class TaskNode : TreeNode
{

    public string id { get; set; }
    public string name { get; set; }
    public string parentId { get; set; }

    public TaskNode()
    {

    }
    public TaskNode(string Id, string Name)
    {
        id = Id;
        Value = Id;
        Text = Name;
    }
    public TaskNode(string Id, string Name, string ParentId)
    {
        id = Id;
        Value = Id;
        parentId = ParentId;
        Text = Name;
    }

    public bool TryAddToParent(TaskNode node)
    {
        bool result;

        if (this.id == node.parentId)
        {
            this.ChildNodes.Add(node);
            return true;
        }

        foreach (TaskNode taskNode in this.ChildNodes)
        {
            result = taskNode.TryAddToParent(node);
            if (result)
            {
                return true;
            }
        }
        return false;

    }
}