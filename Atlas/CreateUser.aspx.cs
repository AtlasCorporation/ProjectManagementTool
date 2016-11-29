using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void createAcc_Click(object sender, EventArgs e)
    {
        if (createUser(username.Text, password.Text, repassword.Text))
        {
            Response.Redirect("Login.aspx?success=true");
        }
    }

    private bool createUser(string username, string password, string repassword)
    {
        string usernameRegex = @"^[A-Za-z0-9]+$";
        // Check that username has no special characters and passwords match
        if (Regex.IsMatch(username, usernameRegex) && password == repassword)
        {
            // Check if username exists already
            string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
            string query = "select * from user where username=@username";
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnString);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                MySqlDataReader MyReader = cmd.ExecuteReader();
                if (MyReader.HasRows)
                {
                    conn.Close();
                    lblMessages.Text = "Username exists already!";
                }
                else // Create new user if username is not taken
                {
                    conn.Close();
                    return AddUserToDB(username, password);
                }
            }
            catch (Exception)
            {
            }
        }
        else
        {
            lblMessages.Text = "Passwords do not match or username includes special characters.";
        }
        return false;
    }

    private bool AddUserToDB(string username, string password)
    {
        // Hash password and salt it
        string salt = pwMixer.CreateSalt();
        string pw = username + salt + password;
        byte[] pw_bytes = ASCIIEncoding.ASCII.GetBytes(pw);
        SHA512Managed sha512 = new SHA512Managed();

        var hashed_byte_array = sha512.ComputeHash(pw_bytes);
        string hashedPassword = Convert.ToBase64String(hashed_byte_array);

        // Add new user to DB
        string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
        string insert = "Insert into user(username, password, salt) values(@username,@hashedPassword,@salt)";
        try
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand cmd = new MySqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword);
            cmd.Parameters.AddWithValue("@salt", salt);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
                return true; // Added successfully
        }
        catch (Exception ex)
        {
            lblMessages.Text = ex.Message;
        }
        return false;
    }
}