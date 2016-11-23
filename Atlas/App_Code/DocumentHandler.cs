using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string text = File.ReadAllText(path).Replace("\n", "<br />").Replace("#", "<h1>").Replace("¤", "</h1>");
            return text;
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
}