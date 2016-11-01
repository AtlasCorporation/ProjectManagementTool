using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

public partial class Main : System.Web.UI.Page
{

    pwMixer mixer = new pwMixer();
    private string salt;
    private string pw;
    private byte[] pw_bytes;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void createAcc_Click(object sender, EventArgs e)
    {
        result.Text = password.Text;
        salt = mixer.CreateSalt();
        string pw = username.Text + salt + password.Text;
        pw_bytes = ASCIIEncoding.ASCII.GetBytes(pw);
        SHA512Managed sha512 = new SHA512Managed();

        var hashed_byte_array = sha512.ComputeHash(pw_bytes);
        hashResult.Text = Convert.ToBase64String(hashed_byte_array);
    }
}