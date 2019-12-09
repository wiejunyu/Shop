using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WL.Cms.Models;
using WL.Cms.Manager;
using Newtonsoft.Json;
using WL.Infrastructure.Common;

namespace WL.Web.Cms.Controllers
{
    public class ColumnController : BaseController
    {
        int lang = Convert.ToInt32(CookiesManager.GetCookies());
        // GET: Column
        public ActionResult ColumuAdd()
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["Categorylist"] = list;
            ViewData["lang"] = lang;
            return View();
        }

        //添加栏目语言切换
        public string ColumuAddLang(int langInt)
        {
            CookiesManager.SetCookies(langInt);
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetFatherColumu(lang);
            return JsonConvert.SerializeObject(list);
        }

        //栏目管理语言切换
        public JsonResult ColumuManagementLangFather(int langInt)
        {
            CookiesManager.SetCookies(langInt);
            return Json("1");
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

            //获取缩略图
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }

            ColumuModels cl = new ColumuModels();
            cl.catname = Request.Form["catname"];
            cl.parentid = Convert.ToInt32(Request.Form["parentid"]);
            cl.moduleid = Convert.ToInt32(Request.Form["moduleid"]);
            cl.title = Request.Form["title"];
            cl.keywords = Request.Form["keywords"];
            cl.description = Request.Form["description"];

            //添加排序
            List<ColumuModels> list = new List<ColumuModels>();
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

            //获取url，如果没有"/",自动添加
            ColumuManager.CreateFolder(Request.Form["url"], cl, ColumuManager.GetColumu(lang));
            //获取url，如果没有"/",自动添加
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

        //栏目管理
        public ActionResult ColumuManagement()
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["listFather"] = list;
            list = new List<ColumuModels>();
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
            List<ColumuModels> list = new List<ColumuModels>();
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
                List<ColumuModels> img = new List<ColumuModels>();
                img = ColumuManager.GetColumuImage(Convert.ToInt32(Request.Form["id"]));
                id_positive_url = img[0].image;
            }
            ColumuModels cl = new ColumuModels();
            cl.ID = Convert.ToInt32(Request.Form["id"]);
            cl.catname = Request.Form["catname"];
            cl.parentid = Convert.ToInt32(Request.Form["parentid"]);
            cl.moduleid = Convert.ToInt32(Request.Form["moduleid"]);
            cl.title = Request.Form["title"];
            cl.keywords = Request.Form["keywords"];
            cl.description = Request.Form["description"];

            List<ColumuModels> list = new List<ColumuModels>();
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

            //获取url，如果没有"/",自动添加
            ColumuManager.CreateFolder(Request.Form["url"], cl, ColumuManager.GetColumu(lang));
            //获取url，如果没有"/",自动添加
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
            List<ColumuModels> f = ColumuManager.IdGetColumu(id);
            if (f[0].parentid == 0)
            {
                List<ColumuModels> s = ColumuManager.GetSonColumu(lang, id);
                if (s.Count != 0)
                {
                    foreach (ColumuModels temp in s)
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

        //外部链接添加
        public ActionResult LinkAdd()
        {
            List<ColumuModels> list = new List<ColumuModels>();
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