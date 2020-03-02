using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Cms.Manager;
using WL.Cms.Models;
using WL.Infrastructure.Common;
using WL.Web.Cms.Filters;
using System.Configuration;

namespace WL.Web.Cms.Controllers
{
    public class LoginController : BaseNoLoginController
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="isChecked"></param>
        /// <returns></returns>
        [Logger(Top = "Login", Key = "Login", Description = "登陆")]
        public JsonResult LoginCheck(string userName, string passWord, string isChecked)
        {
            string pwd = MD5.Md5(passWord);
            UserInfo user = UserManager.GetUserInfo(userName);
            if (user != null)
            {
                if (user.PassWord == pwd.ToLower())
                {
                    string ip = Common.GetUserIp();
                    user.IP = ip;
                    user.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    UserManager.UpdateUser(user);
                    Session["user"] = user;
                    if (isChecked == "1")
                    {
                        HttpCookie cookie = new HttpCookie("usercookie_gg_cms");;
                        cookie.Values.Add("userName", user.UserName);
                        cookie.Values.Add("password", user.PassWord);
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.AppendCookie(cookie);
                    }

                    string url = "/Home/Index";

                    return Json(url);
                }
                else
                {
                    return Json("false");
                }
            }
            else
            {
                return Json("false");
            }
        }

        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        public JsonResult LogOut()
        {

            UserInfo user = Session["user"] as UserInfo;
            HttpCookie cookie = Request.Cookies["usercookie_gg_cms"];
            if (cookie != null)
            {
                cookie.Expires = System.DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Session.Contents.Remove("user");
            return Json("1");
        }

        // GET: Login
        public ActionResult Test()
        {
            return View();
        }
    }
}