
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using WL.Cms.Models;
using WL.Cms.Manager;
using WL.Infrastructure.Common;

namespace WL.Web.Cms.Controllers
{
    public class ColumnController : BaseController
    {
        static public int lang = 1;
        // GET: Column
        public ActionResult ColumuAdd()
        {
            List<Columu> list = new List<Columu>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["Categorylist"] = list;
            ViewData["lang"] = lang;
            return View();
        }

        //添加栏目语言切换
        public string ColumuAddLang(int langInt)
        {
            lang = langInt;
            List<Columu> list = new List<Columu>();
            list = ColumuManager.GetFatherColumu(lang);
            return JsonConvert.SerializeObject(list);
        }

        //栏目管理语言切换
        public JsonResult ColumuManagementLangFather(int langInt)
        {
            lang = langInt;
            return Json("1");
        }
        public string ColumuManagementLangSon(int langInt)
        {
            lang = langInt;
            List<Columu> list = new List<Columu>();
            list = ColumuManager.GetFatherColumu(lang);
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 写入栏目
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public JsonResult SetColumu(string json)
        {
            int rel = -1;
            HttpPostedFileBase id_positive = Request.Files["id_positive"];
            string id_positive_url = "";
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }
            Columu cl = new Columu();
            cl.catname = Request.Form["catname"];
            cl.parentid = Convert.ToInt32(Request.Form["parentid"]);
            cl.moduleid = Convert.ToInt32(Request.Form["moduleid"]);
            cl.title = Request.Form["title"];
            cl.keywords = Request.Form["keywords"];
            cl.description = Request.Form["description"];

            //添加排序
            List<Columu> list = new List<Columu>();
            if (cl.parentid == 0)
            {
                list = ColumuManager.GetFatherColumu(lang);
                if (list.Count != 0)
                {
                    var cou = list.Max(u => u.listorder);
                    cl.listorder = (Convert.ToInt32(cou) + 1);
                }
                else
                {
                    cl.listorder = 0;
                }
            }
            else
            {
                list = ColumuManager.GetColumu(lang);
                list = list.Where(u => u.parentid == cl.parentid).ToList();
                if (list.Count != 0)
                {
                    var cou = (list.Where(u => u.parentid == cl.parentid)).Max(u => u.listorder);
                    cl.listorder = (Convert.ToInt32(cou) + 1);
                }
                else
                {
                    cl.listorder = 0;
                }
            }

            cl.hits = Convert.ToInt32(Request.Form["hits"]);
            cl.image = id_positive_url;

            //获取url并创建该文件夹，如果没有"/",自动添加
            ColumuManager.CreateFolder(Request.Form["url"], cl, ColumuManager.GetColumu(lang));
            //获取url并创建该文件夹，如果没有"/",自动添加
            cl.lang = lang;
            cl.catdir = Request.Form["catdir"];
            cl.url = Request.Form["url"];
            if (ColumuManager.AddColumu(cl))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        public ActionResult ColumuManagement()
        {
            List<Columu> list = new List<Columu>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["listFather"] = list;
            list = new List<Columu>();
            list = ColumuManager.GetSonColumu(lang);
            ViewData["listSon"] = list;
            ViewData["lang"] = lang;
            return View();
        }

        /// <summary>
        /// 获取传入栏目所有信息
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string IdGetColumu(int id)
        {
            List<Columu> list = new List<Columu>();
            list = ColumuManager.IdGetColumu(id);
            return JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 修改栏目所有信息
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public JsonResult EditColumu()
        {
            int rel = -1;
            HttpPostedFileBase id_positive = Request.Files["id_positive"];
            string id_positive_url = "";
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }
            else
            {
                List<Columu> img = new List<Columu>();
                img = ColumuManager.GetColumuImage(Convert.ToInt32(Request.Form["id"]));
                id_positive_url = img[0].image;
            }
            Columu cl = new Columu();
            cl.ID = Convert.ToInt32(Request.Form["id"]);
            cl.catname = Request.Form["catname"];
            cl.parentid = Convert.ToInt32(Request.Form["parentid"]);
            cl.moduleid = Convert.ToInt32(Request.Form["moduleid"]);
            cl.title = Request.Form["title"];
            cl.keywords = Request.Form["keywords"];
            cl.description = Request.Form["description"];

            List<Columu> list = new List<Columu>();
            if (cl.parentid == 0)
            {
                list = ColumuManager.GetFatherColumu(lang);
                var cou = list.Max(u => u.listorder);
                cl.listorder = (Convert.ToInt32(cou) + 1);
            }
            else
            {
                list = ColumuManager.GetColumu(lang);
                var cou = (list.Where(u => u.parentid == cl.parentid)).Max(u => u.listorder);
                cl.listorder = (Convert.ToInt32(cou) + 1);
            }

            cl.hits = Convert.ToInt32(Request.Form["hits"]);
            cl.image = id_positive_url;

            //获取url并创建该文件夹，如果没有"/",自动添加
            ColumuManager.CreateFolder(Request.Form["url"], cl, ColumuManager.GetColumu(lang));
            //获取url并创建该文件夹，如果没有"/",自动添加
            cl.catdir = Request.Form["catdir"];
            cl.url = Request.Form["url"];
            cl.lang = lang;
            if (ColumuManager.EditColumu(cl))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public JsonResult DelColumu(int id)
        {
            List<Columu> f = ColumuManager.IdGetColumu(id);
            if (f[0].parentid == 0)
            {
                List<Columu> s = ColumuManager.GetSonColumu(lang, id);
                if (s.Count != 0)
                {
                    foreach (Columu temp in s)
                    {
                        ColumuManager.DelColumu(temp.ID);
                        ColumuManager.DelColumuArticle(temp.ID);
                    }
                }
            }
            ColumuManager.DelColumu(id);
            ColumuManager.DelColumuArticle(id);
            return Json("1");
        }

        public ActionResult LinkAdd()
        {
            List<Columu> list = new List<Columu>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["Categorylist"] = list;
            ViewData["lang"] = lang;
            return View();
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string Upload(HttpPostedFileBase file, string name = null)
        {
            UserInfo user = GetSessionUserInfo();
            string time = DateTime.Now.ToString("yyyyMMdd");
            string sFileRoot = Server.MapPath("~/Uploads/images/" + time);
            string sFileName = Common.FileUploader(sFileRoot, file, name);
            return "/Uploads/images/" + time + "/" + sFileName;
        }


    }
}