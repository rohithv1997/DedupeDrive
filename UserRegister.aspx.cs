using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
using System.Text;


public partial class UserRegister : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd2 = new SqlCommand("select count(*) from [DeDup].[dbo].[UserReg] where Username = '" + username.Value + "'", con);
        int no=(int)cmd2.ExecuteScalar();
        con.Close();
        if(username.Value == "admin") { no = 2; }
        if (no != 0 )
        {
            string script = "alert(\"User name already exists!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
        }
        else
        {
            string selectedGender = "";
            if (Request.Form["optionsRadios"] != null)
            {
                selectedGender = Request.Form["optionsRadios"].ToString();
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

            con.Open();
            SqlCommand cmd = new SqlCommand("insert into [DeDup].[dbo].[UserReg](Username,Password,Gender,Email) values('" + username.Value + "','" + hash + "','" + selectedGender + "','" + emailid.Value + "')", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registered Successfully....')", true);
            con.Close();
            Sendm(emailid.Value, username.Value, password.Value);
        	Response.Redirect("UserLogin.aspx");
        }
       
    }
    
    public void Sendm(string emailid, string un, string pw)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new System.Net.NetworkCredential("pragadeeshdrive@gmail.com", "pragadeeshdrive2018"),
            EnableSsl = true
        };
        client.Send("pragadeeshdrive@gmail.com", emailid, "Welcome to Pragadeesh Drive!", "Please find your account details below:\n\n"+"Username: " + un + "\n" + "Password: " + pw);




    }
}
