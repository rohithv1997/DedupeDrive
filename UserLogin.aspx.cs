using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

public partial class UserLogin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

        protected void Button1_Click(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("select Username from [DeDup].[dbo].[UserReg] where Username = '" + username.Value+ "'", con);
            SqlCommand cmd1 = new SqlCommand("select Password from [DeDup].[dbo].[UserReg] where Username = '" + username.Value + "'", con);
            string uname = (string)cmd.ExecuteScalar();
            string pwd = (string)cmd1.ExecuteScalar();
            con.Close();
        if (username.Value == "admin" && password.Value == "admin")
            {
 Session["ownername"] = username.Value;
            Response.Redirect("AdminHome.aspx");
        }
        string plain, hash; byte[] temp;
        plain = password.Value;
        SHA1 sha = new SHA1CryptoServiceProvider();
        temp = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < temp.Length; i++)
        {
            sb.Append(temp[i].ToString("x2"));
        }
        hash = sb.ToString();
        if (username.Value == uname && hash == pwd)
            {
                con.Open();
                SqlCommand cmdd = new SqlCommand("insert into [DeDup].[dbo].[LoginHistory] values('" + username.Value + "','" + DateTime.Now.ToString() + "')", con);
                cmdd.ExecuteNonQuery();
                con.Close();
                Session["ownername"] = username.Value;
                Response.Redirect("UserHome.aspx");
            }
            else
            {
            string script = "alert(\"Invalid Credentials!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }

        }
    }