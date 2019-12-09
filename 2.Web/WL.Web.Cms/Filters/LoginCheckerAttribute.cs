using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Web.Routing;
using WL.Cms.Models;
using WL.Cms.Manager;
using WL.Web.Cms.Controllers;
using WL.Infrastructure.Common;

namespace WL.Web.Cms.Filters
{
    public class LoginCheckerAttribute : ActionFilterAttribute
    {
        

        //当action跳转的时候验证
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<Menu> list = new List<Menu>();
            List<Menu> Toplist = new List<Menu>();
            List<Menu> Menulist = new List<Menu>();
            List<Menu> Btnlist = new List<Menu>();
            filterContext.Controller.ViewData["Toplist"] = Toplist;
            filterContext.Controller.ViewData["Menulist"] = Menulist;
            filterContext.Controller.ViewData["Btnlist"] = Btnlist;
            string controller = filterContext.RouteData.Values["Controller"].ToString().ToLower();
            string action = filterContext.RouteData.Values["Action"].ToString().ToLower();
            ContentResult cr = new ContentResult();
            UserInfo user = HttpContext.Current.Session["user"] as UserInfo;
            if (user == null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["usercookie_gg_cms"];
                if (cookie != null)
                {
                    string login = cookie.Values["userName"].ToString();
                    string pwd = cookie.Values["password"].ToString();
                    user = UserManager.GetUserInfo(login);
                    if (user != null)
                    {
                        if (user.PassWord == pwd)
                        {
                            filterContext.Controller.ViewData["username"] = user.UserName;
                            filterContext.Controller.ViewBag.Permission = user.Permission;
                            filterContext.Controller.ViewBag.Leader = user.Leader;
                            string ip = Common.IPAddress;
                            user.IP = ip;
                            user.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            UserManager.UpdateUser(user);
                            HttpContext.Current.Session["user"] = user;
                            
                            //根据该权限查询数据
                            if (BaseController.dictionary.ContainsKey(user.Permission))
                            {
                                list = BaseController.dictionary[user.Permission];
                            }
                            else
                            {
                                //查询菜单
                                list = MenuManager.GetMenuListByPermission(user.Permission.ToString());
                                //添加进数组
                                BaseController.dictionary.Add(user.Permission, list);
                            }
                            Toplist = list.Where(u => u.Lv == 0).OrderBy(u => u.Sort).ToList();
                            Menulist = list.Where(u => u.Lv == 1).OrderBy(u => u.Sort).ToList();
                            //按钮菜单
                            Btnlist = list.Where(u => u.Lv == 2).OrderBy(u => u.Sort).ToList();
                            filterContext.Controller.ViewData["Toplist"] = Toplist;
                            filterContext.Controller.ViewData["Menulist"] = Menulist;
                            filterContext.Controller.ViewData["Btnlist"] = Btnlist;
                            if (controller == "home")
                            {
                                string url = "";
                                foreach (Menu t in Toplist)
                                {
                                    foreach (Menu m in Menulist.Where(u => u.Pid == t.ID).OrderBy(u => u.Sort).ToList())
                                    {
                                        Menu bt = Btnlist.SingleOrDefault(u => u.Pid == m.ID && u.Action == "Look");
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
                                if (url == "")
                                {
                                    cr.Content = "<script>alert('该账号没有任何查看的权限！');window.location.href='/Login/Login';</script>";
                                    filterContext.Result = cr;
                                }
                                else
                                {
                                    HttpContext.Current.Response.Redirect(url);
                                }
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
                    HttpContext.Current.Response.Redirect("/Login/Login");
                }
            }
            else
            {
                filterContext.Controller.ViewData["username"] = user.UserName;
                filterContext.Controller.ViewBag.Permission = user.Permission;
                filterContext.Controller.ViewBag.Leader = user.Leader;

                //根据该权限查询数据
                if (BaseController.dictionary.ContainsKey(user.Permission))
                {
                    list = BaseController.dictionary[user.Permission];
                }
                else
                {
                    //查询菜单
                    list = MenuManager.GetMenuListByPermission(user.Permission.ToString());
                    //添加进数组
                    BaseController.dictionary.Add(user.Permission, list);
                }
                Toplist = list.Where(u => u.Lv == 0).OrderBy(u => u.Sort).ToList();
                Menulist = list.Where(u => u.Lv == 1).OrderBy(u => u.Sort).ToList();
                //按钮菜单
                Btnlist = list.Where(u => u.Lv == 2).OrderBy(u => u.Sort).ToList();
                filterContext.Controller.ViewData["Toplist"] = Toplist;
                filterContext.Controller.ViewData["Menulist"] = Menulist;
                filterContext.Controller.ViewData["Btnlist"] = Btnlist;
                if (controller == "home")
                {
                    string url = "";
                    foreach(Menu t in Toplist)
                    {
                        foreach (Menu m in Menulist.Where(u=>u.Pid == t.ID).OrderBy(u=>u.Sort).ToList())
                        {
                            
                            Menu bt = Btnlist.SingleOrDefault(u => u.Pid == m.ID && u.Action == "Look");
                            if(bt == null)
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
                    if (url == "")
                    {
                        cr.Content = "<script>alert('该账号没有任何查看的权限！');window.location.href='/Login/Login';</script>";
                        filterContext.Result = cr;
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(url);
                    }
                }
            }
        }  
    }
}