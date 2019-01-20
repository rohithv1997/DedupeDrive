using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.Collections.Generic;

public partial class UserHome : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeConnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        username.InnerText = Session["ownername"].ToString();
        string un = Session["ownername"].ToString();
        SqlDataSource1.SelectParameters.Add("uname", un);
        con.Open();

        SqlCommand cmd = new SqlCommand("select Fid from [DeDup].[dbo].[FileUp] where oname=@on", con);
        cmd.Parameters.AddWithValue("@on", un);
        SqlDataReader DR = cmd.ExecuteReader();

        while (DR.Read())
        {
            ComboBox1.Items.Add(DR[0].ToString());

        }
        con.Close();
        int rowCount;
        rowCount = GridView1.Rows.Count;
        if(rowCount==0)
        {
            dall.Visible = false;
        }
	GridView1.Columns[0].HeaderStyle.Font.Size = GridView1.Columns[0].ItemStyle.Font.Size = 0;
    }
    protected void dall_Click(object sender, EventArgs e)
    {
        int rowCount;

       

        rowCount = GridView1.Rows.Count;
        if (rowCount != 0)
        {
            Directory.CreateDirectory(Server.MapPath("~/decrypt/") + username.InnerText);
            foreach (var item in ComboBox1.Items)
            {
                con.Open();
                int fileid = Convert.ToInt32(item.ToString());
                SqlCommand cmd = new SqlCommand("select filepath from [DeDup].[dbo].[FileUp] where Fid=@fid", con);
                cmd.Parameters.AddWithValue("@fid", fileid);
                string result = (string)cmd.ExecuteScalar();
                SqlCommand cmd2 = new SqlCommand("select filename from [DeDup].[dbo].[FileUp] where Fid=@fid", con);
                cmd2.Parameters.AddWithValue("@fid", fileid);
                SqlCommand cmd3 = new SqlCommand("select ftype from [DeDup].[dbo].[FileUp] where Fid=@fid", con);
                cmd3.Parameters.AddWithValue("@fid", fileid);
                string outfil2 = Server.MapPath("~/decrypt/" + username.InnerText + "/") + (string)cmd2.ExecuteScalar() + (string)cmd3.ExecuteScalar();
                DecryptFile(result, outfil2);
                con.Close();
            }
            using (var zip = new Ionic.Zip.ZipFile())
            {
                zip.AddDirectory(Server.MapPath("~/decrypt/" + username.InnerText + "/"));
                zip.Save(Server.MapPath("~/decrypt/") + username.InnerText + ".zip");
                FileStream fs = File.OpenRead(Server.MapPath("~/decrypt/") + username.InnerText + ".zip");
                byte[] buffer = new byte[(int)fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                fs.Close();
                SetResponse("Files", ".zip");
                HttpContext.Current.Response.BinaryWrite(buffer);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
                File.Delete(Server.MapPath("~/decrypt/") + username.InnerText + ".zip");
                DeleteFiles(Server.MapPath("~/decrypt/" + username.InnerText + "/"));
            }

        }
    }
    private void DeleteFiles(string folder)
    {
   
        string[] files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            File.Delete(file);
        }
        //then delete folder
        Directory.Delete(folder);

    }
 

    private static void SetResponse(string fileName, string filetype)
    {
        string attachment = "attachment; filename=" + fileName + filetype;
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "image/jpeg";
        HttpContext.Current.Response.AddHeader("Pragma", "public");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
		
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Fetch value of Name.
            int fileid = Convert.ToInt32(GridView1.Rows[rowIndex].Cells[0].Text);
	
            con.Open();
            SqlCommand cmd = new SqlCommand("select filepath from [DeDup].[dbo].[FileUp] where Fid=@fid", con);
            cmd.Parameters.AddWithValue("@fid", fileid);
            string result = (string)cmd.ExecuteScalar();
            SqlCommand cmd2 = new SqlCommand("select filename from [DeDup].[dbo].[FileUp] where Fid=@fid", con);
            cmd2.Parameters.AddWithValue("@fid", fileid);
            SqlCommand cmd3 = new SqlCommand("select ftype from [DeDup].[dbo].[FileUp] where Fid=@fid", con);
            cmd3.Parameters.AddWithValue("@fid", fileid);
            
            string outfil2 = Server.MapPath("~/decrypt/") + (string)cmd2.ExecuteScalar() + (string)cmd3.ExecuteScalar();
            DecryptFile(result, outfil2);
            FileStream fs = File.OpenRead(outfil2);
            byte[] buffer = new byte[(int)fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            fs.Close();
            SetResponse((string)cmd2.ExecuteScalar(), (string)cmd3.ExecuteScalar());
            HttpContext.Current.Response.BinaryWrite(buffer);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            File.Delete(outfil2);
            con.Close();
        }
        if (e.CommandName == "Delete")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Fetch value of Name.
            int fileid = Convert.ToInt32(GridView1.Rows[rowIndex].Cells[0].Text);
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select sha1 from [DeDup].[dbo].[FileUp] where Fid=" + fileid, con);
            string sha1fp = (string)cmd2.ExecuteScalar();
            SqlCommand cmd5 = new SqlCommand("select filepath from [DeDup].[dbo].[FileUp] where Fid=" + fileid, con);
            string dfilepath = (string)cmd5.ExecuteScalar();
            SqlCommand cmd3 = new SqlCommand("select count(*) from [DeDup].[dbo].[FileUp] where sha1='" + sha1fp + "'", con);
            int nor = (int)cmd3.ExecuteScalar();
            SqlCommand cmd4 = new SqlCommand("delete from [DeDup].[dbo].[FileUp] where Fid=" + fileid, con);
            cmd4.ExecuteNonQuery();
            con.Close();
            if (nor == 1)
            {
                File.Delete(dfilepath);
            }
            Response.Redirect("ViewFiles.aspx");
        }
    }
    private void DecryptFile(string inputFile, string outputFile)
    {

        {
            string password = @"myKey123";
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            RijndaelManaged RMCrypto = new RijndaelManaged();
            CryptoStream cs = new CryptoStream(fsCrypt,
            RMCrypto.CreateDecryptor(key, key),
            CryptoStreamMode.Read);
            FileStream fsOut = new FileStream(outputFile, FileMode.Create);
            int data;
            while ((data = cs.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);
            fsOut.Close();
            cs.Close();
            fsCrypt.Close();

        }
    }

    
}