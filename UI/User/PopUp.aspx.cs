﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BuinessLayer;
using ObjectLayer;

public partial class popup_calendar : System.Web.UI.Page
{

    private static object _lock = new object();
    string userid;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            userid = Session["UserId"].ToString();

        }
        catch (Exception ex) { Response.Redirect("../../Default.aspx"); }
    }

    protected void btnSaveUpload_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (FileUpload1.HasFile)
            {
                HttpPostedFile myFile = FileUpload1.PostedFile;
                int size = myFile.ContentLength / 1024;
                if (size < 4000)
                {
                    string fullName = FileUpload1.FileName;
                    string nameWithoutExtension = fullName.Split('.')[0];

                    string temp = nameWithoutExtension + DateTime.Now.ToString() + userid;
                    temp = temp.Replace(@"/", "");
                    temp = temp.Replace(@" ", "");
                    temp = temp.Replace(@":", "");

                    try
                    {
                        WallBLL wallBll = new WallBLL();
                        
                        FileUpload1.SaveAs(Server.MapPath(Global.USER_PHOTOS) + temp + ".jpg");

                    }
                    catch (Exception ex)
                    {
                        //  lblMsg.Text += ex;
                        lblMsg.Text = ex.Message;
                        lblMsg.Visible = true;
                    }
                }
                else
                {
                    lblMsg.Text = "File Size Less than 4MB";
                    lblMsg.Visible = true;
                }
            }

        }
    }
}