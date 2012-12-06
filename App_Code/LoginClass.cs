﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonClass
/// </summary>
public class LoginClass
{
    public static string getUserId()
    {
        try
        {
            return HttpContext.Current.Session["UserId"].ToString();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Redirect("../../Default.aspx");
        }
        return null;
    }

    public static string getUserIdOrTempUserId()
    {
        try
        {
            if (HttpContext.Current.Request.QueryString.Count == 0)
            {
                HttpContext.Current.Session["TempUserId"] = null;
                return LoginClass.getUserId();
            }
            else
            {
                HttpContext.Current.Session["TempUserId"] = HttpContext.Current.Request.QueryString.Get(0);
                return HttpContext.Current.Request.QueryString.Get(0);                
            }

        }
        catch (Exception ex) 
        { 
            HttpContext.Current.Response.Redirect("../../Default.aspx");            
        }
        return null;
    }

}