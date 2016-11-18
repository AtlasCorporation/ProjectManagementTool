using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DocumentReader
/// </summary>
public class DocumentHandler
{
    public DocumentHandler()
    {
        
    }
    public string ReadFile(string path)
    {
        //reading helpfile in
        try
        {
            string text = File.ReadAllText(path).Replace("\n", "<br />");
            return text;
        }
        catch (Exception)
        {
           return "Tiedosto puuttuu";
        }
    }
    public void SaveFile(string path, TextBox tb)
    {
        if (!File.Exists(path)) { 
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