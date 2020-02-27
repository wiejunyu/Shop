﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Web.Routing;
using WL.Cms.Manager;
using WL.Infrastructure.Common;
using WL.Web.Cms.Controllers;
using WL.Domain;
using WL.Infrastructure.Commom;

namespace WL.Web.Cms.Filters
{
    public class LoginCheckerAttribute : ActionFilterAttribute
    {
        

        //当action跳转的时候验证
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (WLDbContext db = new WLDbContext())
            {
                ReturnResult result = new ReturnResult();
                try
                {
                    CookiesManager.AddCookies();
                    List<Cms_Menu> list = new List<Cms_Menu>();
                    List<Cms_Menu> Toplist = new List<Cms_Menu>();
                    List<Cms_Menu> Menulist = new List<Cms_Menu>();
                    List<Cms_Menu> Btnlist = new List<Cms_Menu>();
                    filterContext.Controller.ViewData["Toplist"] = Toplist;
                    filterContext.Controller.ViewData["Menulist"] = Menulist;
                    filterContext.Controller.ViewData["Btnlist"] = Btnlist;
                    string controller = filterContext.RouteData.Values["Controller"].ToString().ToLower();
                    string action = filterContext.RouteData.Values["Action"].ToString().ToLower();
                    ContentResult cr = new ContentResult();
                    Cms_UserInfo user = HttpContext.Current.Session["user"] as Cms_UserInfo;
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["usercookie_gg_cms"];
                    if (user == null && cookie == null) throw new Exception("用户未登录");
                    if (user == null) {
                        string login = cookie.Values["userName"].ToString();
                        string pwd = cookie.Values["password"].ToString();
                        user = db.Cms_UserInfo.Where(x => x.UserName == login && x.PassWord == pwd).FirstOrDefault();
                    }
                    if (user != null) throw new Exception("用户未登录");
                    filterContext.Controller.ViewData["username"] = user.UserName;
                    filterContext.Controller.ViewBag.Permission = user.Permission;
                    filterContext.Controller.ViewBag.Leader = user.Leader;
                    string ip = Common.GetUserIp();
                    user.IP = ip;
                    user.LoginTime = DateTime.Now;
                    db.SaveChanges();
                    HttpContext.Current.Session["user"] = user;

                    //根据该权限查询数据
                    if (BaseController.dictionary.ContainsKey(user.Permission ?? 0))
                    {
                        list = BaseController.dictionary[user.Permission ?? 0];
                    }
                    else
                    {
                        //查询菜单
                        list = MenuManager.GetMenuListByPermission(user.Permission.ToString());
                        //添加进数组
                        BaseController.dictionary.Add(user.Permission ?? 0, list);
                    }
                    Toplist = list.Where(u => u.Lv == 0).OrderBy(u => u.Sort).ToList();
                    Menulist = list.Where(u => u.Lv == 1).OrderBy(u => u.Sort).ToList();
                    //按钮菜单
                    Btnlist = list.Where(u => u.Lv == 2).OrderBy(u => u.Sort).ToList();
                    filterContext.Controller.ViewData["Toplist"] = Toplist;
                    filterContext.Controller.ViewData["Menulist"] = Menulist;
                    filterContext.Controller.ViewData["Btnlist"] = Btnlist;
                    string url = "";
                    if (controller != "home") HttpContext.Current.Response.Redirect(url);
                    foreach (Cms_Menu t in Toplist)
                    {
                        foreach (Cms_Menu m in Menulist.Where(u => u.Pid == t.ID).OrderBy(u => u.Sort).ToList())
                        {
                            Cms_Menu bt = Btnlist.SingleOrDefault(u => u.Pid == m.ID && u.Action == "Look");
                            if (bt == null)
                            {
                                continue;
                            }
                            url = m.Url;
                            break;
                        }
                        if (url == "")
                        {
                            continue;
                        }
                        break;
                    }
                    if (string.IsNullOrWhiteSpace(url)) throw new Exception("该账号没有任何查看的权限");
                    HttpContext.Current.Response.Redirect(url);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                HttpContext.Current.Response.Redirect("/Login/Login");
            }
        }  
    }
}