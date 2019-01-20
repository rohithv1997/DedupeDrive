using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        username.InnerText= Session["ownername"].ToString();
    }
    protected void changepassword(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select password from [DeDup].[dbo].[UserReg] where Username = '" + username.InnerText + "'", con);
        string passhash = (string)cmd.ExecuteScalar();
        string plain, hash; byte[] temp;
        plain = TextBox1.Text;
        SHA1 sha = new SHA1CryptoServiceProvider();
        temp = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < temp.Length; i++)
        {
            sb.Append(temp[i].ToString("x2"));
        }
        hash = sb.ToString();
        //new password        
        if (hash==passhash)
        {
            plain = TextBox2.Text;
            SHA1 sha2 = new SHA1CryptoServiceProvider();
            temp = sha2.ComputeHash(Encoding.UTF8.GetBytes(plain));
            StringBuilder sb2 = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                sb2.Append(temp[i].ToString("x2"));
            }
            hash = sb2.ToString();
            SqlCommand cmd2 = new SqlCommand("update [DeDup].[dbo].[UserReg] set Password ='" + hash + "' where Username ='" + username.InnerText + "'", con);
            cmd2.ExecuteNonQuery();
            string script = "alert(\"Your Password has been changed!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
        else
        {
            string script = "alert(\"Invalid Password!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
}