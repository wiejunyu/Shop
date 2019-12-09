using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class MenuController : BaseController
    {
        #region 权限管理
        public ActionResult RoleList()
        {
            return View();
        }
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        [Logger(Top = "RoleList", Key = "Look", Description = "查看")]
        public JsonResult GetRoleList()
        {
            List<Role> list = new List<Role>();
            list = MenuManager.GetRoleList();
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleEdit(string ID)
        {
            Role Role = MenuManager.GetRoleByID(ID);
            return View(Role);
        }
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="pwd"></param>
        /// <param name="remark"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        [Logger(Top = "RoleList", Key = "Edit", Description = "编辑")]
        public JsonResult UpdateRole(string json)
        {
            int rel = -1;
            try
            {
                JObject jo = JObject.Parse(json);
                Role temp = jo.ToObject<Role>();
                //获取原本的信息
                Role poco = MenuManager.GetRoleByID(temp.ID.ToString());
                if (poco != null)
                {
                    poco.Name = temp.Name;
                    poco.Remark = temp.Remark;

                    bool flag = MenuManager.UpdateRole(poco);
                    if (flag)
                    {
                        rel = 1;
                    }
                }
                else
                {
                    rel = -1;
                    return Json(rel);
                }
            }
            catch (System.Exception ex)
            {
                rel = -1;
            }
            return Json(rel);
        }
        /// <summary>
        /// 跳转到添加角色页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleAdd()
        {
            return View();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="RoleName"></param>
        /// <param name="pwd"></param>
        /// <param name="remark"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        [Logger(Top = "RoleList", Key = "Add", Description = "添加")]
        public JsonResult AddRole(string json)
        {
            int rel = -1;
            try
            {
                JObject jo = JObject.Parse(json);
                Role u = new Role();
                Role temp = jo.ToObject<Role>();
                //获取原本的信息
                Role poco = MenuManager.GetRoleByName(temp.Name.ToString());
                //判断该账号是否被创建过
                if (poco != null)
                {
                    rel = 0;
                    return Json(rel);
                }
                u.Name = temp.Name;
                u.Remark = temp.Remark;

                bool flag = MenuManager.AddRole(u);
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
        /// 删除角色
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Logger(Top = "RoleList", Key = "Delete", Description = "删除")]
        public JsonResult DeleteRole(string key)
        {
            int rel = -1;
            bool flag = MenuManager.DeleteRole(key);
            if (flag)
            {
                rel = 1;
            }
            return Json(rel);
        }
        /// <summary>
        /// 设置用户的权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRole(string id)
        {
            ViewBag.pid = id;
            List<Menu> list = new List<Menu>();
            list = MenuManager.GetMenuList();
            //查询跟菜单
            List<Menu> Toplist = list.Where(u => u.Lv == 0).OrderBy(u => u.Sort).ToList();
            //查询页面菜单
            List<Menu> Menulist = list.Where(u => u.Lv == 1).OrderBy(u => u.Sort).ToList();
            //查询功能菜单
            List<Menu> Btnlist = list.Where(u => u.Lv == 2).OrderBy(u => u.Sort).ToList();
            List<MenuTreeTop> mtop = new List<MenuTreeTop>();
            List<RoleMenu> pmlist = MenuManager.GetRoleMenuList(id);
            foreach (Menu top in Toplist)
            {
                MenuTreeTop mt = new MenuTreeTop();
                mt.id = top.ID;
                mt.text = top.Name;
                foreach (Menu menu in Menulist.Where(u => u.Pid == top.ID).ToList())
                {
                    MenuTree m = new MenuTree();
                    m.id = menu.ID;
                    m.text = menu.Name;
                    
                    List<Menu> blist = Btnlist.Where(u => u.Pid == menu.ID).ToList();
                    //当有下级功能按键的时候
                    MenuState ms = new MenuState();
                    ms.opened = true;
                    m.state = ms;
                    foreach (Menu btn in blist)
                    {
                        MenuBtnTree b = new MenuBtnTree();
                        b.id = btn.ID;
                        b.text = btn.Name;
                        RoleMenu p = pmlist.SingleOrDefault(u => u.MenuID == btn.ID);
                        if (p != null)
                        {
                            MenuBtnState bs = new MenuBtnState();
                            bs.selected = true;
                            b.state = bs;
                        }
                        m.children.Add(b);
                    }
                    mt.children.Add(m);
                }
                mtop.Add(mt);
            }
            string json = JsonConvert.SerializeObject(mtop);
            ViewData["menu"] = json;
            return View();
        }
        /// <summary>
        /// 设置权限菜单
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Logger(Top = "RoleList", Key = "SetRole", Description = "设置权限")]
        public JsonResult SetRoleMenu(string id, string pid)
        {
            int rel = -1;
            string[] ids = id.Split(',');
            //把该权限的所有关联删除
            try
            {
                MenuManager.DeleteRoleMenuByRoleID(pid);
                //从新添加该权限菜单
                foreach (string temp in ids)
                {
                    RoleMenu m = new RoleMenu();
                    m.RoleID = Convert.ToInt32(pid);
                    m.MenuID = Convert.ToInt32(temp);
                    MenuManager.AddRoleMenu(m);
                }
                rel = 1;
                //清楚所有菜单关联
                dictionary.Clear();
            }
            catch
            {
                rel = -1;
            }
            return Json(rel);
        }
        #endregion
        #region 菜单管理
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuList()
        {
            List<Menu> last = new List<Menu>();
            List<Menu> list = new List<Menu>();
            List<Menu> top = new List<Menu>();
            list = MenuManager.GetMenuList();
            top = list.Where(u => u.Lv == 0).OrderBy(u=>u.Sort).ToList();
            foreach (Menu temp in top)
            {
                last.Add(temp);
                List<Menu> viewlist = list.Where(u => u.Pid == temp.ID && u.Lv == 1).OrderBy(u => u.Sort).ToList();
                int i = 1;
                foreach (Menu view in viewlist)
                {
                    string s1 = "";
                    string s2 = "";
                    if (i == viewlist.Count)
                    {
                        s1 = "&nbsp;&nbsp;&nbsp;&nbsp;└─ ";
                        s2 = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    else
                    {
                        s1 = " &nbsp;&nbsp;&nbsp;&nbsp;├─ ";
                        s2 = " &nbsp;&nbsp;&nbsp;&nbsp;│&nbsp;";
                    }
                    view.Name = s1 + view.Name;
                    last.Add(view);
                    List<Menu> btnlist = list.Where(u => u.Pid == view.ID && u.Lv == 2).OrderBy(u => u.Sort).ToList();
                    i++;
                    int j = 1;
                    foreach (Menu btn in btnlist)
                    {
                        if (j == btnlist.Count)
                        {
                            btn.Name = s2 + " └─ " + btn.Name;
                        }
                        else
                        {
                            btn.Name = s2 +  " ├─ " + btn.Name;
                        } 
                        last.Add(btn);
                        j++;
                    }
                }
            }
            ViewData["list"] = last;
            return View();
        }
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        [Logger(Top = "MenuList", Key = "Look", Description = "查看")]
        public JsonResult GetMenuList()
        {
            List<Menu> list = new List<Menu>();
            list = MenuManager.GetMenuList();
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuEdit(string id)
        {
            Menu menu = MenuManager.GetMenu(id);
            return View(menu);
        }
        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="ServerLogin"></param>
        /// <param name="pwd"></param>
        /// <param name="remark"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        [Logger(Top = "MenuList", Key = "Edit", Description = "编辑")]
        public JsonResult EditMenu(string json)
        {
            int rel = -1;
            try
            {
                JObject jo = JObject.Parse(json);
                Menu temp = jo.ToObject<Menu>();
                bool flag = MenuManager.UpdateMenu(temp);
                if (flag)
                {
                    //清楚所有菜单关联
                    dictionary.Clear();
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
        /// 跳转到添加菜单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuAdd()
        {
            return View();
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="ServerLogin"></param>
        /// <param name="pwd"></param>
        /// <param name="remark"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        [Logger(Top = "MenuList", Key = "Add", Description = "添加")]
        public JsonResult AddMenu(string json)
        {
            int rel = -1;
            try
            {
                JObject jo = JObject.Parse(json);
                Menu temp = jo.ToObject<Menu>();


                int id = MenuManager.AddMenu(temp);
                if (id != -1)
                {
                    //获取超级管理员admin账号
                    UserInfo user = UserManager.GetUserInfo("admin");
                    RoleMenu p = new RoleMenu();
                    p.RoleID = Convert.ToInt32(user.Permission);
                    p.MenuID = id;
                    bool flag = MenuManager.AddRoleMenu(p);
                    if (flag)
                    {
                        rel = 1;
                    }
                    //清楚所有菜单关联
                    dictionary.Clear();
                }

            }
            catch (System.Exception ex)
            {
                rel = -1;
            }
            return Json(rel);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Logger(Top = "MenuList", Key = "Delete", Description = "删除")]
        public JsonResult DeleteMenu(string key)
        {
            int rel = -1;
            bool flag = MenuManager.DeleteMenu(key);
            if (flag)
            {
                MenuManager.DeleteRoleMenu(key);
                //清楚所有菜单关联
                dictionary.Clear();
                rel = 1;
            }
            return Json(rel);
        }
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenu(string lv)
        {
            List<Menu> list = new List<Menu>();
            list = MenuManager.GetMenuList();
            list = list.Where(u => u.Lv == (Convert.ToInt32(lv) - 1)).ToList();
            string str = "";
            foreach(Menu temp in list)
            {
                str += "<option value='" + temp.ID + "'>" + temp.Name + "</option>";
            }
            return Json(str);
        }
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenuEdit(string lv,string hpid)
        {
            List<Menu> list = new List<Menu>();
            list = MenuManager.GetMenuList();
            list = list.Where(u => u.Lv == (Convert.ToInt32(lv) - 1)).ToList();
            string str = "";
            foreach (Menu temp in list)
            {
                if(temp.ID == Convert.ToInt32(hpid))
                {
                    str += "<option value='" + temp.ID + "' selected>" + temp.Name + "</option>";
                }
                else
                {
                    str += "<option value='" + temp.ID + "'>" + temp.Name + "</option>";
                }
            }
            return Json(str);
        }
        #endregion
    }
}