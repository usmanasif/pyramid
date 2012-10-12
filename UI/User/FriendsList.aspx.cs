﻿using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BuinessLayer;
using ObjectLayer;
using System.Globalization;
public partial class FriendsList : System.Web.UI.Page
{
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {

        
        try
        {
            if (Request.QueryString.Count == 0)
            {
                 userid = Session["UserId"].ToString();
                 Session["TempUserId"] = null;
            }           
            else
            {
                userid = Request.QueryString.Get(0);
                Session["TempUserId"] = userid;
            }

        }
        catch (Exception ex) { Response.Redirect("../../Default.aspx"); }

        ((Label)Master.FindControl("lblTitle")).Text = "View Friends";
        if (!IsPostBack)
        {

            LoadFriendsList();
            
          
        }

        if (GridViewFriendsListRequest.Rows.Count == 0)
            pendinglabel.Visible = false;
        else
            pendinglabel.Visible = true;

        Panel1.DefaultButton = "SearchButton";
    }


    protected void LoadFriendsList()
    {


        GridViewFriendsList.DataSource = FriendsBLL.getAllFriendsListName(userid, Global.CONFIRMED);
        GridViewFriendsList.DataBind();

        GridViewFriendsListRequest.DataSource = FriendsBLL.getAllFriendsListName(userid, Global.PENDING);
        GridViewFriendsListRequest.DataBind();
        if (userid != Session["UserId"].ToString())
            GridViewFriendsList.Columns[2].Visible = false;

    }


   /* protected void GridViewFriendsList_SelectedIndexChanged(object sender, EventArgs e)
    {
         FriendsBLL.deleteFriends(GridViewFriendsList.DataKeys[GridViewFriendsList.SelectedIndex].Value.ToString());
       LoadFriendsList();
       // Response.Write(GridViewFriendsList.DataKeys[GridViewFriendsList.SelectedIndex].Value);
    }
    protected void GridViewFriendsListRequest_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FriendsBLL.deleteFriends(GridViewFriendsListRequest.DataKeys[GridViewFriendsListRequest.SelectedIndex].Value.ToString());
        FriendsBO objClass=new FriendsBO();
        objClass.Status=true;
        objClass.Id = GridViewFriendsListRequest.DataKeys[GridViewFriendsListRequest.SelectedIndex].Value.ToString();
        FriendsBLL.confirmRequest(objClass);
        LoadFriendsList();
    }*/

    protected void GridViewFriendsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FriendsBLL.deleteFriends(GridViewFriendsList.DataKeys[GridViewFriendsList.SelectedIndex].Value.ToString());
        LoadFriendsList();
        // Response.Write(GridViewFriendsList.DataKeys[GridViewFriendsList.SelectedIndex].Value);
    }

    protected void GridViewFriendsListRequest_SelectedIndexChanged(object sender, EventArgs e)
    {
        string friendid = GridViewFriendsListRequest.DataKeys[GridViewFriendsListRequest.SelectedIndex].Value.ToString();

        Response.Redirect("FriendofFriendsList.aspx?UserId=" + friendid + "&Type=Mutual");
        // FriendsBLL.deleteFriends(GridViewFriendsListRequest.DataKeys[GridViewFriendsListRequest.SelectedIndex].Value.ToString());
        //FriendsBO objClass=new FriendsBO();
        //objClass.Status=Global.CONFIRMED;
        //objClass.Id = GridViewFriendsListRequest.DataKeys[GridViewFriendsListRequest.SelectedIndex].Value.ToString();
        //FriendsBLL.confirmRequest(objClass);
        //LoadFriendsList();
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridViewFriendsList.DataSource = FriendsBLL.getAllFriendsListName(userid, Global.CONFIRMED);
        //GridViewFriendsList.DataBind();
        GridViewFriendsList.PageIndex = GridViewFriendsList.PageIndex + 1;
       
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string fieldValue = SearchTextBox.Text;
        string CheckBox = SearchDropDownList.Text;

        if (CheckBox == "Name")
        {
            GridViewFriendsList.DataSource = FriendsBLL.FindByListName(userid, fieldValue, Global.CONFIRMED);
            GridViewFriendsList.DataBind();
        }

        if (CheckBox == "CurrrentCity")
        {
            GridViewFriendsList.DataSource = FriendsBLL.FindByListCurrent(userid, fieldValue, Global.CONFIRMED);
            GridViewFriendsList.DataBind();
            //   Response.Write("current");
        }

        if (CheckBox == "HomeTown")
        {
            GridViewFriendsList.DataSource = FriendsBLL.FindByListHomeTown(userid, fieldValue, Global.CONFIRMED);
            GridViewFriendsList.DataBind();
            //   Response.Write("current");
        }

        if (CheckBox == "School")
        {
            GridViewFriendsList.DataSource = FriendsBLL.FindByListSchool(userid, fieldValue, Global.CONFIRMED);
            GridViewFriendsList.DataBind();
            //   Response.Write("current");
        }

        if (CheckBox == "WorkPlace")
        {
            GridViewFriendsList.DataSource = FriendsBLL.FindByListWorkPlace(userid, fieldValue, Global.CONFIRMED);
            GridViewFriendsList.DataBind();

        }



    }

    protected void GridViewFriendsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewFriendsList.PageIndex = e.NewPageIndex;
        LoadFriendsList();
    }
}