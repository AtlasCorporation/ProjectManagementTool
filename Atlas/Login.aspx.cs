using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Display message after successful account creation
        string s = Request.QueryString["success"];
        if (s != null)
            divAccountCreated.Style["display"] = "block";
        else
            divAccountCreated.Style["display"] = "none";
    }

    protected void LogIn_Click(object sender, EventArgs e)
    {
        string username = usernamelogin.Text;
        string password = passwordlogin.Text;
        try
        {
            if (ValidateUser(username, password))
            {
                Session.Remove("ActiveProject");
                FormsAuthentication.RedirectFromLoginPage(username, false);
            }
            else
            {
                lblMessages.Text = "Incorrect Credentials.";
            }
        }
        catch (Exception)
        {
            lblMessages.Text = "Login Failed.";
        }
    }

    protected bool ValidateUser(string username, string password)
    {
        string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
        string select = string.Format("Select id, password, salt from user where username=@username");
        DataTable dt = new DataTable();
        string checkSALT = "";
        string checkPW = "";
        int userId = 0;

        //Select with parameter
        string usernameRegex = @"^[A-Za-z0-9]+$";
        if (Regex.IsMatch(username, usernameRegex))
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnString);
                MySqlCommand MyCommand = new MySqlCommand(select, conn);
                MyCommand.Parameters.AddWithValue("@username", username);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(MyCommand);
                da.Fill(dt);
                conn.Close();
                da.Dispose();
                checkPW = Convert.ToString(dt.Rows[0]["password"]);
                checkSALT = Convert.ToString(dt.Rows[0]["salt"]);
                userId = Convert.ToInt32(dt.Rows[0]["id"]);
            }
            catch (Exception)
            {
                throw;
            }

            //Generates check pattern from user inputs to check if Hash matches one in database
            string pw = username + checkSALT + password;
            byte[] pw_bytes = ASCIIEncoding.ASCII.GetBytes(pw);
            SHA512Managed sha512 = new SHA512Managed();

            var hashed_byte_array = sha512.ComputeHash(pw_bytes);

            if (Convert.ToBase64String(hashed_byte_array) == checkPW)
            {
                Session["LoggedUser"] = username;
                Session["LoggedUserId"] = userId;
                return true;
            }
        }
        return false;
    }
}