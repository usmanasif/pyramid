﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BuinessLayer;
using ObjectLayer;
public partial class ContactInfo : System.Web.UI.Page
{
    string userid;
    protected void Page_Load(object sender, EventArgs e)
    {

        imgSave.Visible = false;
        lblSave.Visible = false;
        
        try
        {
            userid = Session["UserId"].ToString();

        }
        catch (Exception ex) { Response.Redirect("../../Default.aspx"); }
        ((Label)Master.FindControl("lblTitle")).Text = "Contact Information";
        if (!IsPostBack)
        {

            LoadContactInfo();
            LoadPhoneNumber();
            LoadOthersEmail();
            LoadWebsites();
           
        }
    }
    protected void LoadGridView()
    {

        LoadPhoneNumber();
        LoadOthersEmail();
        LoadWebsites();
        }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BasicInfoBO objBasicInfo = new BasicInfoBO();

        objBasicInfo.UserId = userid;
        objBasicInfo.CityTown = txtTownCity.Text;
        objBasicInfo.Address = txtAddress.Text;
        objBasicInfo.ZipCode = txtZipCode.Text;
        objBasicInfo.Neighbourhood = txtNeighbourhood.Text;

        BasicInfoBLL.updateContactInfoPage(objBasicInfo);
        UpdateEmail();
        LoadContactInfo();
        WallPost("Changed Contact Information");
        imgSave.Visible = true;
        lblSave.Visible = true;

    }
    protected void WallPost(string post)
    {
        UserBO objUser = new UserBO();
        objUser = UserBLL.getUserByUserId(userid);

        WallBO objWall = new WallBO();
        objWall.PostedByUserId = userid;
        objWall.WallOwnerUserId = userid;
        objWall.FirstName = objUser.FirstName;
        objWall.LastName = objUser.LastName;
        objWall.Post = post;
        objWall.AddedDate = DateTime.Now;
        objWall.Type = Global.PROFILE_CHANGE;
        string wid=WallBLL.insertWall(objWall);

        ////////////////////////////////////TICKER CODE //////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////
        List<UserFriendsBO> listtag = FriendsBLL.getAllFriendsListName(Session["UserId"].ToString(), Global.CONFIRMED);
        //get the education,hometown and employer of people in list
        foreach (UserFriendsBO Useritem in listtag)
        {
            TickerBO objTicker = new TickerBO();


            objTicker.PostedByUserId = objWall.PostedByUserId;
            objTicker.TickerOwnerUserId = Useritem.FriendUserId;
            objTicker.FirstName = objWall.FirstName;
            objTicker.LastName = objWall.LastName;
            objTicker.Post = objWall.Post;
            objTicker.Title = objWall.Post;
            objTicker.AddedDate = DateTime.UtcNow;
            objTicker.Type = objWall.Type;
            objTicker.EmbedPost = objWall.EmbedPost;
            objTicker.WallId = wid;
            TickerBLL.insertTicker(objTicker);

        }
        TickerBO objTickerUserTag = new TickerBO();


        objTickerUserTag.PostedByUserId = Session["UserId"].ToString();
        objTickerUserTag.TickerOwnerUserId = Session["UserId"].ToString();
        objTickerUserTag.FirstName = objUser.FirstName;
        objTickerUserTag.LastName = objUser.LastName;
        objTickerUserTag.Post = objWall.Post;
        objTickerUserTag.Title = objWall.Post;
        objTickerUserTag.AddedDate = DateTime.UtcNow;
        objTickerUserTag.Type = objWall.Type;
        objTickerUserTag.EmbedPost = objWall.EmbedPost;
        objTickerUserTag.WallId = wid;
        TickerBLL.insertTicker(objTickerUserTag);

        ////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////
    }
     protected void UpdateEmail()
    {
        UserBO userObj = new UserBO();
        userObj.Email = txtPrimaryEmail.Text;
         userObj.Id = userid;
       UserBLL.updateEmail(userObj);
       LoadContactInfo();
     }

    protected void LoadContactInfo()
    {
        BasicInfoBO objBasicInfo = new BasicInfoBO();
        objBasicInfo = BasicInfoBLL.getBasicInfoByUserId(userid);


        txtTownCity.Text = objBasicInfo.CityTown;
        txtAddress.Text = objBasicInfo.Address;
        txtZipCode.Text = objBasicInfo.ZipCode;
        txtNeighbourhood.Text = objBasicInfo.Neighbourhood;
        UserBO userObj= new UserBO();
        userObj = UserBLL.getUserByUserId(userid);
        txtPrimaryEmail.Text = userObj.Email;
        
    }
    protected void lbtnAddEmail_Click(object sender, EventArgs e)
    {

         ContactInfoBO objContactInfo = new ContactInfoBO();
        objContactInfo.Name=txtEmail.Text;
        objContactInfo.UserId = userid;
        objContactInfo.Type="Email";
        ContactInfoBLL.insertContactInfo(objContactInfo);
        LoadGridView();
    }
    protected void lbtnAddPhoneNumber_Click(object sender, EventArgs e)
    {
        ContactInfoBO objContactInfo = new ContactInfoBO();
        objContactInfo.Name = lstCountryCode.SelectedValue +"-"+ txtPhoneNumber.Text;
        objContactInfo.UserId = userid;
        objContactInfo.Type = "PhoneNumber";
        ContactInfoBLL.insertContactInfo(objContactInfo);
        LoadPhoneNumber();
    }
    protected void GridViewPhone_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string Code = ((DropDownList)((GridView)sender).Rows[e.RowIndex].FindControl("lstCountryCode")).SelectedValue;
        string PhoneNumber = ((TextBox)((GridView)sender).Rows[e.RowIndex].FindControl("txtPhoneNumber")).Text;
        if(PhoneNumber.IndexOf('-')>0)
        PhoneNumber = PhoneNumber.Substring(PhoneNumber.IndexOf('-') + 1);

        e.NewValues["ContactInfoData"] = Code + "-" + PhoneNumber;

    }
    protected string EvalPhoneNumber(string fieldName)
    {
        object value = this.Eval(fieldName);
        if (value == null)
            return null;
        string str = value.ToString();
         str =str.Substring(str.IndexOf('-')+1);
            return str;
       
    }

    protected void lbtnAddWebsites_Click(object sender, EventArgs e)
    {
        ContactInfoBO objContactInfo = new ContactInfoBO();
        objContactInfo.Name= txtWebsites.Text;
        objContactInfo.UserId = userid;
        objContactInfo.Type = "Website";
        ContactInfoBLL.insertContactInfo(objContactInfo);
        LoadWebsites();
    }

    protected void LoadOthersEmail()
    {

        GridViewEmail.DataSource = ContactInfoBLL.getContactInfo("Email", userid);
       GridViewEmail.DataBind();

    }

    protected void LoadWebsites()
    {

        GridViewWebsites.DataSource = ContactInfoBLL.getContactInfo("Website", userid);
        GridViewWebsites.DataBind();

    }

    protected void LoadPhoneNumber()
    {

        GridViewPhone.DataSource = ContactInfoBLL.getContactInfo("PhoneNumber", userid);
        GridViewPhone.DataBind();

    }
}