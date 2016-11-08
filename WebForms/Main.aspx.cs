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
using System.Data;

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

}