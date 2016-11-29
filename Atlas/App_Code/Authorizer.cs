using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Custom authorizer for roles.
/// </summary>
public class Authorizer
{
    /// <summary>
    /// Checks if given role is valid.
    /// </summary>
    public static bool isValidRole(string role)
    {
        if (role == "admin" || role == "user") // Valid role names here
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks if user has specified role in the project. Returns true if that is the case.
    /// </summary>
    public static bool CheckUserRoleForProject(int userID, string role, int projectID)
    {
        if (isValidRole(role))
        {
            string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
            string select = string.Format("SELECT role FROM user_project WHERE user_id=@userID AND project_id=@projectID");
            try
            {
                DataTable dt = new DataTable();
                MySqlConnection conn = new MySqlConnection(ConnString);
                MySqlCommand cmd = new MySqlCommand(select, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@projectID", projectID);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                string userRole = Convert.ToString(dt.Rows[0]["role"]);
                if (role == userRole)
                    return true; // User has the specified role for this project
            }
            catch (Exception)
            {
                throw;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if user has at least one of the roles from the list. Returns true if that is the case.
    /// This allows us to authorize features to multiple different roles.
    /// </summary>
    public static bool CheckUserRoleForProject(int userID, List<string> roles, int projectID)
    {
        // Check that all the roles in the list are valid
        foreach (string s in roles)
        {
            if (!isValidRole(s))
                return false;
        }

        string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
        string select = string.Format("SELECT role FROM user_project WHERE user_id=@userID AND project_id=@projectID");
        try
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand cmd = new MySqlCommand(select, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@projectID", projectID);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            da.Dispose();
            string userRole = Convert.ToString(dt.Rows[0]["role"]);
            foreach (string r in roles)
            { 
                if (r == userRole)
                    return true; // User has the specified role for this project
            }
        }
        catch (Exception)
        {
            throw;
        }
        return false;
    }
}