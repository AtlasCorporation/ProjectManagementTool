using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class Main : System.Web.UI.Page
{

    pwMixer mixer = new pwMixer();
    private string salt;
    private string pw;
    private string pwCheck;
    private byte[] pw_bytes;
    private byte[] pw_bytes2;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void createAcc_Click(object sender, EventArgs e)
    {
        string usernameRegex = @"^[A-Za-z0-9]+$";
        if (Regex.IsMatch(username.Text, usernameRegex) && password.Text == repassword.Text)
        {
            result.Text = password.Text + " and salt: " + salt;
            salt = mixer.CreateSalt();
            pw = username.Text + salt + password.Text;
            pw_bytes = ASCIIEncoding.ASCII.GetBytes(pw);
            SHA512Managed sha512 = new SHA512Managed();

            var hashed_byte_array = sha512.ComputeHash(pw_bytes);
            string hashedPassword = Convert.ToBase64String(hashed_byte_array);
            hashResult.Text = hashedPassword;
            //
            //
            //
            //Username EXISTS tsekkaus
            string ConnString = ConfigurationManager.ConnectionStrings["Mysli"].ConnectionString;
            string insert = "Insert into user(username, password, salt) values('" + username.Text + "','" + hashedPassword + "','" + salt + "')";
            string query = "select * from user where username='" + username.Text + "'";
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader MyReader = cmd.ExecuteReader();
                if (MyReader.HasRows)
                {
                    Label5.Text = "löytyy jo";
                }
                else
                {
                    Label5.Text = "ei löydy";
                }
                conn.Close();
            }
            catch (Exception)
            {

            } // END OF USERNAME EXISTS

            //ADDING USER
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnString);
                MySqlCommand MyCommand = new MySqlCommand(insert, conn);
                MySqlDataReader MyReader;
                conn.Open();
                MyReader = MyCommand.ExecuteReader();
                conn.Close();
                Label5.Text = "Palaute lähetetty onnistuneesti.";
            }
            catch (Exception ex)
            {
                Label6.Text = ex.Message;
            }
            // END OF ADDING USER
            //
            //
            //
        }
        else
        {
            result.Text = "Something went wrong";
        }

        
    }

    protected void LogIn_Click(object sender, EventArgs e)
    {
        result.Text = passwordlogin.Text;
        salt = "fiuf3"; //mixer.CreateSalt();
        pw = usernamelogin.Text + salt + passwordlogin.Text;
        pw_bytes2 = ASCIIEncoding.ASCII.GetBytes(pw);
        SHA512Managed sha512 = new SHA512Managed();

        var hashed_byte_array = sha512.ComputeHash(pw_bytes2);
        Label5.Text = passwordlogin.Text + " and salt: " + salt;
        Label6.Text = Convert.ToBase64String(hashed_byte_array);

    }
}