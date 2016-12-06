using MarkedNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DocumentHandler
/// </summary>
public class DocumentHandler
{
    public DocumentHandler()
    {

    }
    public string ReadFile(string path)
    {
        //reading file in
        try
        {
            string text = File.ReadAllText(path);

            var marked = new Marked();
            var html = marked.Parse(text);

            return html;
        }
        catch (Exception)
        {
            return "Tiedosto puuttuu";
        }
    }
    public string EditFile(string path)
    {
        //reading file in
        try
        {
            string text = File.ReadAllText(path);
            return text;
        }
        catch (Exception)
        {
            return "Tiedosto puuttuu";
        }
    }
    public void SaveFile(string path, TextBox tb)
    {
        if (!File.Exists(path))
        {
            TextWriter tw = new StreamWriter(path, true);
            tw.WriteLine("The next line!");
            tw.Close();
        }
        try
        {
            File.WriteAllText(path, tb.Text);
        }
        catch (Exception)
        {

        }
    }
    public void readTemplates(DropDownList ddl, string path)
    {
        DirectoryInfo d = new DirectoryInfo(path);//Checking resource folder
        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
        string temp = "";
        ddl.Items.Insert(0, string.Empty);
        foreach (FileInfo file in Files)
        {
            temp = file.Name.Replace(".txt", "");
            ddl.Items.Add(new ListItem(temp, file.Name));
        }
    }
}