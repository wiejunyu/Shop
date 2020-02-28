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
    public class LoginCheckerAttribute : ActionFilterAttribute
    {
        //当action跳转的时候验证
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var url = request.Url.AbsolutePath.ToString();

            #region 熔断
            //var key = request.UserHostAddress + url + GapTimeType.Min;
            ////RedisHelper.StringIncrement($"Count:{GapTimeType.Day}{url}",1);
            //if (CheckCache(key))
            //{
            //    int times = GetCache<int>(key);
            //    if (times > 20)
            //    {
            //        filterContext.Result = new EmptyResult();
            //        filterContext.HttpContext.Response.Write("操作过于频繁 触发系统熔断策略");
            //        return;
            //    }
            //}
            #endregion
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
                            filterContext.Controller.ViewBag.username = user.UserName;
                            filterContext.Controller.ViewBag.userportrait = user.Portrait;
                            filterContext.Controller.ViewBag.Sysconfig = SysconfigManager.GetSysconfig();
                        }
                    }
                }
                else
                {
                    filterContext.Controller.ViewBag.Sysconfig = SysconfigManager.GetSysconfig();
                }
            }
            else
            {
                filterContext.Controller.ViewBag.username = user.UserName;
                filterContext.Controller.ViewBag.userportrait = user.Portrait;
                filterContext.Controller.ViewBag.Sysconfig = SysconfigManager.GetSysconfig();
            }
        }  
    }
}