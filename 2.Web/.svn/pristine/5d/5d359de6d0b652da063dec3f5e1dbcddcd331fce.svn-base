
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Cms.Manager;
using WL.Cms.Models;
using WL.Web.Cms.Filters;

namespace WL.Web.Cms.Controllers
{
    public class LoggerController : BaseController
    {
        /// <summary>
        /// 交易记录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoggerList()
        {
            List<UserInfo> list = new List<UserInfo>();
            list = UserManager.GetUserList();
            ViewData["userlist"] = list;

            List<string> listu = new List<string>();
            listu = LoggerManager.GetLoggerAction();
            ViewData["actionlist"] = listu;
            return View();
        }
        /// <summary>
        /// 交易记录
        /// </summary>
        /// <returns></returns>
        [Logger(Top = "LoggerList", Key = "Look", Description = "查看")]
        public JsonResult GetLoggerList(string action_c, string user, string st, string et)
        {
            UserInfo us = Session["user"] as UserInfo;
            List<Menu> alllist = new List<Menu>();
            List<Menu> Toplist = new List<Menu>();
            List<Menu> Menulist = new List<Menu>();
            List<Menu> Btnlist = new List<Menu>();
            //根据超级管理员的权限来获取所有的菜单
            if (BaseController.dictionary.ContainsKey("1"))
            {
                alllist = BaseController.dictionary["1"];
            }
            else
            {
                //查询菜单
                alllist = MenuManager.GetMenuListByPermission("1");
                //添加进数组
                BaseController.dictionary.Add("1", alllist);
            }
            Menulist = alllist.Where(u => u.Lv == 1).OrderBy(u => u.Sort).ToList();
            //按钮菜单
            Btnlist = alllist.Where(u => u.Lv == 2).OrderBy(u => u.Sort).ToList();

            List<LoggerModels> list = new List<LoggerModels>();
            list = LoggerManager.GetLoggerModelsList(action_c, user, st, et);
            foreach(LoggerModels temp in list)
            {
                if(temp.View == "Login")
                {
                    temp.View = "登陆";
                    temp.Description = "登陆系统";
                }
                else
                {
                    try
                    {
                        Menu d = Menulist.SingleOrDefault(u => u.Action == temp.View);
                        temp.View = d.Name;
                        temp.Description = Btnlist.SingleOrDefault(u => u.Action == temp.Action && u.Pid == d.ID).Name;
                    }
                    catch
                    { }
                }
            }
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }
    }
}