using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
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
    protected void LogIn_Click(object sender, EventArgs e)
    {
        string ConnString = ConfigurationManager.ConnectionStrings["Mysli"].ConnectionString;
        string select = "Select password, salt from user where username='" + usernamelogin.Text + "'";
        DataTable dt = new DataTable();
        string check2 ="";
        string check = "";
        try
        {
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand MyCommand = new MySqlCommand(select, conn);

            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(MyCommand);
            
            da.Fill(dt);

            conn.Close();
            da.Dispose();
            check = Convert.ToString(dt.Rows[0]["password"]);
            check2 = Convert.ToString(dt.Rows[0]["salt"]);
        }
        catch (Exception ex)
        {
            lblMessages.Text = "Problems with credentials";
        }
        
        pw = usernamelogin.Text + check2 + passwordlogin.Text;
        pw_bytes2 = ASCIIEncoding.ASCII.GetBytes(pw);
        SHA512Managed sha512 = new SHA512Managed();

        var hashed_byte_array = sha512.ComputeHash(pw_bytes2);
        
        if(Convert.ToBase64String(hashed_byte_array) == check){
            Session["LoggedUser"] = usernamelogin.Text;
            lblMessages.Text = "Login success";
            Response.Redirect("Main.aspx");
        }
        else
        {
            lblMessages.Text = "Password or username is wrong";
        }

    }
}