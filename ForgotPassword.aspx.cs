using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Web.UI;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void forgotpassword(object sender, EventArgs e)
     {
        string ranpass=Membership.GeneratePassword(9, 1);
        string plain, hash; byte[] temp;
        plain = ranpass;
        SHA1 sha = new SHA1CryptoServiceProvider();
        temp = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < temp.Length; i++)
        {
            sb.Append(temp[i].ToString("x2"));
        }
        hash = sb.ToString();
        con.Open();
        SqlCommand cmd5 = new SqlCommand("update [DeDup].[dbo].[UserReg] set Password ='"+hash+"' where Username ='"+username.Value+"'", con);
        cmd5.ExecuteNonQuery();
        SqlCommand cmd3 = new SqlCommand("select count(*) from [DeDup].[dbo].[UserReg] where Username = '" + username.Value + "'", con);
         int no = (int)cmd3.ExecuteScalar();
         con.Close();
         if (no != 0)
         {

             con.Open();
             SqlCommand cmd = new SqlCommand("select Email from [DeDup].[dbo].[UserReg] where Username = '" + username.Value + "'", con);
             string emailaddress = (string)cmd.ExecuteScalar();
             SqlCommand cmd2 = new SqlCommand("select Password from [DeDup].[dbo].[UserReg] where Username = '" + username.Value + "'", con);
             string password = (string)cmd2.ExecuteScalar();
             con.Close();
             Sendm(emailaddress, username.Value, ranpass);
             string script = "alert(\"Your Password has been sent to your email!\");";
             ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
             username.Value = "";
        }
         else
         {
            string script = "alert(\"Enter valid Username!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),"ServerControlScript", script, true);
            username.Value = "";
        }
     }
     public void Sendm(string emailid, string un, string pw)
     {
         var client = new SmtpClient("smtp.gmail.com", 587)
         {
             Credentials = new System.Net.NetworkCredential("pragadeeshdrive@gmail.com", "pragadeeshdrive2018"),
             EnableSsl = true
         };
         client.Send("pragadeeshdrive@gmail.com", emailid, "Your Login Credentials", "Username: " + un + "\n" + "Password: " + pw);



     }
}
