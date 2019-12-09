using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Cms.Manager;
using WL.Cms.Models;
using WL.Infrastructure.Common;
using WL.Web.Cms.Filters;

namespace WL.Web.Cms.Controllers
{
    public class UserController : BaseController
    {
        #region 用户管理
        public ActionResult UserList()
        {
            return View();
        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <returns></returns>
        [Logger(Top = "UserList", Key = "Look", Description = "查看")]
        public JsonResult GetUserList()
        {
            List<UserInfo> NewList = new List<UserInfo>();

            List<UserInfo> list  = UserManager.GetUserList();
            if (list.Count > 0)
            {
                foreach (UserInfo UserInfo in list)
                {
                    UserInfo.Permission= MenuManager.GetRoleByID(UserInfo.Permission).Name;
                    NewList.Add(UserInfo);
                }
            }

            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = NewList.Count(), iTotalDisplayRecords = NewList.Count(), aaData = NewList, };
            return json;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UserEdit(string UserName)
        {
            UserInfo user = UserManager.GetUserInfo(UserName);
            ViewData["permission"] = GetPermissionSelectList(user.Permission.ToString());
            return View(user);
        }
        /// <summary>
        /// 修改账号信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="remark"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        [Logger(Top = "UserList", Key = "Edit", Description = "编辑")]
        public JsonResult UpdateUser(string json)
        {
            int rel = -1;
            try
            {
                JObject jo = JObject.Parse(json);
                UserInfo temp = jo.ToObject<UserInfo>();
                //获取原本的信息
                UserInfo poco = UserManager.GetUserInfo(temp.UserName.ToString());
                if (temp.PassWord != "" && temp.PassWord != null)
                {
                    string password = MD5.Md5(temp.PassWord);
                    poco.PassWord = password;
                }
                poco.Remark = temp.Remark;

                poco.Permission = temp.Permission;
                poco.Leader = 1;

                bool flag = UserManager.UpdateUser(poco);
                if (flag)
                {
                    rel = 1;
                }
            }
            catch (System.Exception ex)
            {
                rel = -1;
            }
            return Json(rel);
        }
        /// <summary>
        /// 跳转到添加账号页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd()
        {
            ViewData["permission"] =GetPermissionSelectList("");
            return View();
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetPermissionSelectList(string ID)
        {
            List<Role> role = MenuManager.GetRoleList();
            string str = "";
            //添加或者修改
            foreach (Role Permission in role)
            {
                //修改
                if (ID != null && ID != "")
                {
                    if (ID == Permission.ID.ToString())
                    {
                        str += "<option value='" + Permission.ID + "' selected=\"selected\">" + Permission.Name + "</option>";
                    }
                    else
                    {
                        str += "<option value='" + Permission.ID + "'>" + Permission.Name + "</option>";
                    }
                }
                //添加
                else
                {
                    str += "<option value='" + Permission.ID + "'>" + Permission.Name + "</option>";
                }
            }

            return str;
        }
        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="remark"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        [Logger(Top = "UserList", Key = "Add", Description = "添加")]
        public JsonResult AddUser(string json)
        {
            int rel = -1;
            try
            {
                JObject jo = JObject.Parse(json);
                UserInfo u = new UserInfo();
                UserInfo temp = jo.ToObject<UserInfo>();
                //获取原本的信息
                UserInfo poco = UserManager.GetUserInfo(temp.UserName.ToString());
                //判断该账号是否被创建过
                if (poco != null)
                {
                    rel = 0;
                    return Json(rel);
                }
                u.UserName = temp.UserName;
                u.Remark = temp.Remark;


                string pwd = MD5.Md5(temp.PassWord);
                u.PassWord = pwd;
                u.CreateTime = DateTime.Now.ToString();
                u.Permission = temp.Permission;
                u.Leader = 1;

                bool flag = UserManager.AddUser(u);
                if (flag)
                {
                    rel = 1;
                }
            }
            catch (System.Exception ex)
            {
                rel = -1;
            }
            return Json(rel);
        }
        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Logger(Top = "UserList", Key = "Delete", Description = "删除")]
        public JsonResult DeleteUser(string key)
        {
            int rel = -1;
            bool flag = UserManager.DeleteUser(key);
            if (flag)
            {
                rel = 1;
            }
            return Json(rel);
        }
        #endregion
    }
}