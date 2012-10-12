﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
/// <summary>
/// Summary description for Class1
/// </summary>
public static class Global 
{
    //FRIENDS STATUS
    public const string CONFIRMED = "C";
    public const string PENDING = "P";
    public const string NOTNOW = "N";
    public const string SUGGESTED = "S";


    //ACTIVITIES
    public const string ACTIVITIES = "A";
    public const string INTERESTS = "I";


    //ENTERTAINMENT & SPORTS
    public const string MUSIC = "C";
    public const string BOOKS = "I";
    public const string MOVIE = "M";
    public const string TELEVISION = "T";
    public const string GAME = "G";
    public const string SPORTS = "S";
    public const string ATHELETE = "H";
    public const string TEAM = "E";
    

    //TYPE FOR MEDIA COMMENTS LIKE TAG WALL SHARE
    public const int PHOTO = 1;
    public const int VIDEO = 2;
    public const int PHOTO_ALBUM = 3;
    public const int VIDEO_ALBUM = 4;
    public const int WALL = 5;
    public const int TEXT_POST = 6;
    public const int WALL_COMMENT = 7;
    public const int TAG_POST = 8;
    public const int POST_VIDEOLINK = 15;
    public const int PROFILE_CHANGE= 10;
    public const int TAG_VIDEO = 11;
    public const int TAG_VIDEOLINK = 12;
    public const int TAG_PHOTO = 13;
    public const int TAG_PHOTO_ALBUM = 14;
    public const int LINK= 16;
    public const int SHARE = 9;

    //weights for social features
    public const int WEIGHT_LIST = 6;
    public const int WEIGHT_INTERESTS = 5;
    public const int WEIGHT_WORKPLACE = 4;
    public const int WEIGHT_EDUCATION = 4;
    public const int WEIGHT_HOMETOWN = 2;
    public const int WEIGHT_GENDER = 2;
    public const int WEIGHT_SHAREDFRIENDS = 8;
    public const int WEIGHT_RELIGIONPOLITICS = 2;


    //weights for post types
    public const int WEIGHT_PHOTO = 5;
    public const int WEIGHT_VIDEO = 4;
    public const int WEIGHT_VIDEOLINK = 3;
    public const int WEIGHT_TEXT = 2;

    //Values for Post Status
    public const int POST_HIDDEN = 1;
    public const int POST_SPAM = 2;

    //Values for Post Status Updates Type (All, Only Imp, Most Updates )
    public const int POST_ALL = 1;
    public const int POST_MOST = 2;
    public const int POST_ONLYIMP = 3;

    //Profile Picture
   // public const string PROFILE_PICTURE= "~/UI/UserProfile/profileimages/";
    
    public static string PROFILE_PICTURE = ConfigurationSettings.AppSettings["PathProfilePicture"];

    
    //User Photos
    public static string USER_PHOTOS = ConfigurationSettings.AppSettings["PathUserPhotos"];
    public static string THUMBNAIL_PHOTOS = ConfigurationSettings.AppSettings["PathThumbnailPhotos"];
    //User Photos
    public static string USER_VIDEO = ConfigurationSettings.AppSettings["PathUserVideos"];
    public static string PATH_COMPRESSED_USER_VIDEO = ConfigurationSettings.AppSettings["PathCompressedVideos"];
    // 
    public const string Signup_Mail_Link = "http://203.135.63.151:8080/team5/UI/SignUp/";
    
}