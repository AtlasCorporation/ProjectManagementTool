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
    pwMixer mixer = new pwMixer();
    private string salt;
    private string pw;
    private string hashedPassword;
    private string pwCheck;
    private byte[] pw_bytes;
    private string usernameParam;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void createAcc_Click(object sender, EventArgs e)
    {
        string usernameRegex = @"^[A-Za-z0-9]+$";
        if (Regex.IsMatch(username.Text, usernameRegex) && password.Text == repassword.Text)
        {
            salt = mixer.CreateSalt();
            pw = username.Text + salt + password.Text;
            pw_bytes = ASCIIEncoding.ASCII.GetBytes(pw);
            SHA512Managed sha512 = new SHA512Managed();
            usernameParam = username.Text;

            var hashed_byte_array = sha512.ComputeHash(pw_bytes);
            hashedPassword = Convert.ToBase64String(hashed_byte_array);

            //Username EXISTS tsekkaus
            string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
            string query = "select * from user where username=@username";
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", usernameParam);
                MySqlDataReader MyReader = cmd.ExecuteReader();
                if (MyReader.HasRows)
                {
                    lblMessages.Text = "Username exists already!";
                }// END OF USERNAME EXISTS
                else //Create new user if username is not taken and passwords match
                {
                    createUser();
                    Response.Redirect("Login.aspx?success=true");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
            }
        }
        else
        {
            lblMessages.Text = "Passwords doesn't match or username includes special characters.";
        }

    }
    private void createUser()
    {
        string ConnString = ConfigurationManager.ConnectionStrings["Mysli2"].ConnectionString;
        string insert = "Insert into user(username, password, salt) values(@username,@hashedPassword,@salt)";
        try
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand MyCommand = new MySqlCommand(insert, conn);
            MyCommand.Parameters.AddWithValue("@username", usernameParam);
            MyCommand.Parameters.AddWithValue("@hashedPassword", hashedPassword);
            MyCommand.Parameters.AddWithValue("@salt", salt);
            MySqlDataReader MyReader;
            conn.Open();
            MyReader = MyCommand.ExecuteReader();
            conn.Close();
        }
        catch (Exception ex)
        {
            lblMessages.Text = "Can't create user!";
        }
    }
}