using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Web.Routing;
using WL.Home.Models;
using WL.Home.Manager;
using WL.Infrastructure.Common;

namespace WL.Web.Home.Filters
{
    public class PersonalCheckerAttribute : ActionFilterAttribute
    {
        //当action跳转的时候验证
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;
            filterContext.Controller.ViewBag.username = "";
            if (user == null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["usercookie_wl"];
                if (cookie != null)
                {
                    string login = cookie.Values["userName"].ToString();
                    string pwd = cookie.Values["password"].ToString();
                    user = UserManager.NameGetUser(login);
                    if (user != null)
                    {
                        if (user.PassWord == pwd)
                        {
                            string ip = Common.IPAddress;
                            user.IP = ip;
                            user.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            UserManager.UpdateUser(user);
                            HttpContext.Current.Session["user"] = user;
                            filterContext.Controller.ViewBag.user = user;
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect("/Login/Login");
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("/Login/Login");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/Login/Login");
                }
            }
            else
            {
                filterContext.Controller.ViewBag.user = user;
            }
        }  
    }
}