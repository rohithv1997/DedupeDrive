using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

public partial class UserHome : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DeConnection"].ConnectionString);
    string f, ml, ty, kk, nww, st, p1, mon, fullpath, ep;
    string file1, filepath, status = "0", fsize1, files1;
    int fileid;
    protected void Page_Load(object sender, EventArgs e)
    {
        username.InnerText = Session["ownername"].ToString();
        autoid();
    }
    private void autoid()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select max(Fid) from [DeDup].[dbo].[FileUp] ", con);
        object result = cmd.ExecuteScalar();
        int ID;
        if (result.GetType() != typeof(DBNull))
        {
            ID = Convert.ToInt32(result);
        }
        else
        {
            ID = 0;
        }
        ID = ID + 1;
        fileid = ID;
        //Label14.Text = ID.ToString();

        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        FileUpload1.SaveAs(Server.MapPath("~/temp/") + fileid+ Path.GetExtension(FileUpload1.FileName));
        ml = Server.MapPath("~/temp/");
        f = fileid + Path.GetExtension(FileUpload1.FileName);
        kk = ml + f;
        ep = Server.MapPath("~/encrypt/") +f;
	 
        FileStream fs = new FileStream(kk, FileMode.Open, FileAccess.ReadWrite);
        string chksum = BitConverter.ToString(System.Security.Cryptography.SHA1.Create().ComputeHash(fs));
        FileInfo fz = new FileInfo(kk);
        long s1 = fz.Length;
        string fsize = s1.ToString();
        fs.Close();
        string dat = DateTime.Now.ToString();
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from [DeDup].[dbo].[FileUp] where sha1=@chksum", con);
        cmd.Parameters.AddWithValue("@chksum", chksum);
        int RecordCount = Convert.ToInt32(cmd.ExecuteScalar());
        
        if (RecordCount != 0)
        {
            SqlCommand cmd1 = new SqlCommand("select * from [DeDup].[dbo].[FileUp] where sha1=@chksum and oname=@oname", con);
            cmd1.Parameters.AddWithValue("@chksum", chksum);
            cmd1.Parameters.AddWithValue("@oname", username.InnerText);
            int RecordCount1 = Convert.ToInt32(cmd1.ExecuteScalar());
            if (RecordCount1 == 0)
            {
                
              
                SqlCommand cmd3 = new SqlCommand("select filepath from [DeDup].[dbo].[FileUp] where sha1=@chksum", con);
                cmd3.Parameters.AddWithValue("@chksum", chksum);
                string fp2 = (string)cmd3.ExecuteScalar();
                SqlCommand cmd2 = new SqlCommand("insert into [DeDup].[dbo].[FileUp] (Fid,filename,sha1,ftype,fsize,filepath,oname,date) values(@Fid,@filename,@sha1,@ftype,@fsize,@filepath,@oname,@date)", con);
                cmd2.Parameters.AddWithValue("@Fid", fileid);
                cmd2.Parameters.AddWithValue("@filename", Path.GetFileNameWithoutExtension(FileUpload1.FileName));
                cmd2.Parameters.AddWithValue("@sha1",chksum);
                cmd2.Parameters.AddWithValue("@ftype", Path.GetExtension(FileUpload1.FileName));
                cmd2.Parameters.AddWithValue("@fsize", fsize);
                cmd2.Parameters.AddWithValue("@filepath", fp2);
                cmd2.Parameters.AddWithValue("@oname", username.InnerText);
                cmd2.Parameters.AddWithValue("@date", dat);
                cmd2.ExecuteNonQuery();
                con.Close();
               

                string script = "alert(\"File uploaded successfully!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                File.Delete(Server.MapPath("~/temp/") + fileid + Path.GetExtension(FileUpload1.FileName));
 		autoid();
            }
            else
            {
                
                SqlCommand cmdf = new SqlCommand(" select * FROM [dedup].[dbo].[FileUp] where oname=@oname and filename=@filename and ftype=@ftype", con);
                cmdf.Parameters.AddWithValue("@oname", username.InnerText);
                cmdf.Parameters.AddWithValue("@filename", Path.GetFileNameWithoutExtension(FileUpload1.FileName));
                cmdf.Parameters.AddWithValue("@ftype", Path.GetExtension(FileUpload1.FileName));
                int check1 = Convert.ToInt32(cmdf.ExecuteScalar());
                con.Close();
                if (check1 != 0)
                {
                    string script = "alert(\"File already exists!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                else
                {
                    string script = "alert(\"Same file already exists with a different file name!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                }
                File.Delete(Server.MapPath("~/temp/") + fileid + Path.GetExtension(FileUpload1.FileName));
            }
        }
        else
        {
            SqlCommand cmdf = new SqlCommand(" select count(*) FROM [dedup].[dbo].[FileUp] where oname=@oname and filename like @filename and ftype=@ftype", con);
            cmdf.Parameters.AddWithValue("@oname", username.InnerText);
            cmdf.Parameters.AddWithValue("@filename", Path.GetFileNameWithoutExtension(FileUpload1.FileName)+"%");
            cmdf.Parameters.AddWithValue("@ftype", Path.GetExtension(FileUpload1.FileName));
            int check1 = Convert.ToInt32(cmdf.ExecuteScalar());
            string nfilename =Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            if (check1!=0)
            {
                nfilename = nfilename + "("+check1.ToString()+")";
            }
            SqlCommand cmd1 = new SqlCommand("insert into [DeDup].[dbo].[FileUp] (Fid,filename,sha1,ftype,fsize,filepath,oname,date) values(@Fid,@filename,@sha1,@ftype,@fsize,@filepath,@oname,@date)", con);
            cmd1.Parameters.AddWithValue("@Fid", fileid);
            cmd1.Parameters.AddWithValue("@filename", nfilename);
            cmd1.Parameters.AddWithValue("@sha1", chksum);
            cmd1.Parameters.AddWithValue("@ftype", Path.GetExtension(FileUpload1.FileName));
            cmd1.Parameters.AddWithValue("@fsize", fsize);
            cmd1.Parameters.AddWithValue("@filepath", ep);
            cmd1.Parameters.AddWithValue("@oname", username.InnerText);
            cmd1.Parameters.AddWithValue("@date", dat);

            cmd1.ExecuteNonQuery();
            con.Close();
            string outfil = Server.MapPath("~/encrypt/") + fileid + Path.GetExtension(FileUpload1.FileName);
            EncryptFile(kk, outfil);
            File.Delete(Server.MapPath("~/temp/") + fileid + Path.GetExtension(FileUpload1.FileName));
            string script = "alert(\"File uploaded successfully!\");";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            autoid();
        }

    }
    







    private void EncryptFile(string inputFile, string outputFile)
    {

        try
        {
            string password = @"myKey123";
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = UE.GetBytes(password);

            string cryptFile = outputFile;
            FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt,
                RMCrypto.CreateEncryptor(key, key),
                CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            int data;
            while ((data = fsIn.ReadByte()) != -1)
                cs.WriteByte((byte)data);


            fsIn.Close();
            cs.Close();
            fsCrypt.Close();
        }
        catch
        {
            //MessageBox.Show("Encryption failed!", "Error");
        }
    }
}
    