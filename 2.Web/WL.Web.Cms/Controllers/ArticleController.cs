using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Cms.Manager;
using WL.Cms.Models;
using WL.Infrastructure.Common;

namespace WL.Web.Cms.Controllers
{
    public class ArticleController : BaseController
    {
        int lang = Convert.ToInt32(CookiesManager.GetCookies());
        // GET: Article
        public ActionResult ArticleAdd()
        {
            List<ColumuModels> Flist = new List<ColumuModels>();
            List<ColumuModels> FlistAll = new List<ColumuModels>();
            List<ColumuModels> Slist = new List<ColumuModels>();
            List<ColumuModels> list = new List<ColumuModels>();
            Slist = ColumuManager.GetMidColumu(lang, 2);
            FlistAll = ColumuManager.GetFatherColumu(lang);
            foreach (ColumuModels temp in Slist)
            {
                list = FlistAll.Where(p => p.ID == temp.parentid).ToList();
                if (list.Count != 0)
                {
                    if (Flist.Where(p => p.ID == list[0].ID).Count() < 1)
                    {
                        Flist.Add(list[0]);
                    }
                }
            }
            ViewData["listFather"] = Flist;
            ViewData["listSon"] = Slist;
            ViewData["lang"] = lang;
            var m = new WL.Cms.Models.ColumuModels();
            return View(m);
        }

        /// <summary>
        /// 写入文章
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SetArticle()
        {
            int rel = -1;
            HttpPostedFileBase id_positive = Request.Files["id_positive"];
            string id_positive_url = "";

            //保存略缩图
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }

            ArticleModels al = new ArticleModels();
            al.title = Request.Form["title"];
            al.catid = Convert.ToInt32(Request.Form["catid"]);

            //获取用户信息
            UserInfo user = new UserInfo();
            user = GetSessionUserInfo();
            al.userid = user.ID;
            al.username = user.UserName;
            al.title = Request.Form["title"];
            al.keywords = Request.Form["keywords"];
            al.description = Request.Form["description"];
            if (Request.Form["content"] != null)
            {
                al.content = Request.Form["content"].Replace("<p></p>", "");
            }
            al.thumb = id_positive_url;
            al.listorder = 0;

            al.hits = Convert.ToInt32(Request.Form["hits"]);
            string time = Request.Form["createtime"];
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            int t = (int)cha.TotalSeconds;
            al.createtime = t;
            al.updatetime = t;
            al.lang = lang;
            al.url = "";
            int aid = ArticleManager.AddArticle(al);

            if (ArticleManager.UrlArticle(al.catid, aid))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        //文章查看
        public ActionResult ArticleView()
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["listFather"] = list;
            list = new List<ColumuModels>();
            list = ColumuManager.GetSonColumu(lang);
            ViewData["listSon"] = list;
            List<ArticleModels> list1 = new List<ArticleModels>();
            list1 = ArticleManager.GetArticleList(lang, 0);
            ViewData["listArticle"] = list1;
            ViewData["lang"] = lang;
            return View();
        }

        //语言切换
        public JsonResult ArticleManagementLangFather(int langInt)
        {
            CookiesManager.SetCookies(langInt);
            return Json("1");
        }

        public ActionResult ArticleIndex(int id)
        {
            List<ColumuModels> Flist = new List<ColumuModels>();
            List<ColumuModels> FlistAll = new List<ColumuModels>();
            List<ColumuModels> Slist = new List<ColumuModels>();
            List<ColumuModels> list = new List<ColumuModels>();
            Slist = ColumuManager.GetMidColumu(lang, 2);
            FlistAll = ColumuManager.GetFatherColumu(lang);
            foreach (ColumuModels temp in Slist)
            {
                list = FlistAll.Where(p => p.ID == temp.parentid).ToList();
                if (list.Count != 0)
                {
                    if (Flist.Where(p => p.ID == list[0].ID).Count() < 1)
                    {
                        Flist.Add(list[0]);
                    }
                }
            }
            ViewData["listFather"] = Flist;
            ViewData["listSon"] = Slist;
            ColumuModels cl = new ColumuModels();
            foreach (ColumuModels temp in ColumuManager.GetColumu(lang))
            {
                if (temp.ID == id)
                {
                    cl = temp;
                }
            }
            return View(cl);
        }

        //获取文章列表
        public JsonResult GetArticleList(int cid = 0)
        {
            if (cid != 0)
            {

                if (ColumuManager.JudgeFatherColumu(cid))
                {
                    //获取当前栏目所有子栏目的文章
                    List<ArticleCuttingModels> listArticle = new List<ArticleCuttingModels>();
                    List<ColumuModels> listColumu = ColumuManager.GetSonColumu(lang, cid);

                    foreach (ColumuModels temp in listColumu)
                    {
                        listArticle.AddRange(ArticleManager.GetArticleCuttingList(lang, temp.ID));
                    }
                    listArticle.OrderBy(p => p.createtime).ThenBy(p => p.createtime).ToList();
                    List<ColumuModels> cat = ColumuManager.GetSonColumu(lang);
                    foreach (ArticleCuttingModels temp in listArticle)
                    {
                        temp.catname = cat.Where(p => p.ID == temp.catid).ToList()[0].catname;
                    }
                    JsonResult json = new JsonResult();
                    json.Data = new { sEcho = 0, iTotalRecords = listArticle.Count(), iTotalDisplayRecords = listArticle.Count(), aaData = listArticle, };
                    return json;
                }
                else
                {
                    List<ArticleCuttingModels> list = new List<ArticleCuttingModels>();
                    list = ArticleManager.GetArticleCuttingList(lang, cid);
                    list.OrderBy(p => p.createtime).ThenBy(p => p.createtime).ToList();
                    List<ColumuModels> cat = ColumuManager.GetSonColumu(lang);
                    foreach (ArticleCuttingModels temp in list)
                    {
                        temp.catname = cat.Where(p => p.ID == temp.catid).ToList()[0].catname;
                    }
                    JsonResult json = new JsonResult();
                    json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
                    return json;
                }
            }
            else
            {
                List<ArticleCuttingModels> list = new List<ArticleCuttingModels>();
                list = ArticleManager.GetArticleCuttingList(lang, 0);
                list.OrderBy(p => p.createtime).ThenBy(p => p.createtime).ToList();
                JsonResult json = new JsonResult();
                json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
                return json;
            }
        }

        //删除文章
        public JsonResult DelArticle(int aid)
        {
            int rel = -1;
            if (ArticleManager.DelArticle(aid))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        //批量删除文章
        public JsonResult BatchDelArticle(string aid)
        {
            List<string> list = JsonConvert.DeserializeObject<List<string>>(aid);
            foreach (string temp in list)
            {
                ArticleManager.DelArticle(Convert.ToInt32(temp));
            }
            return Json("1");
        }

        //批量移动文章
        public JsonResult BatchMove(string aid,int cid)
        {
            List<string> list = JsonConvert.DeserializeObject<List<string>>(aid);
            foreach (string temp in list)
            {
                ArticleManager.MoveArticle(Convert.ToInt32(temp), cid);
            }
            return Json("1");
        }

        /// <summary>
        /// 文章编辑
        /// </summary>
        /// <param name="ID">文章ID</param>
        /// <returns></returns>
        public ActionResult ArticleEdit(int ID)
        {

            List<ColumuModels> Flist = new List<ColumuModels>();
            List<ColumuModels> FlistAll = new List<ColumuModels>();
            List<ColumuModels> Slist = new List<ColumuModels>();
            List<ColumuModels> list = new List<ColumuModels>();
            Slist = ColumuManager.GetMidColumu(lang, 2);
            FlistAll = ColumuManager.GetFatherColumu(lang);
            foreach (ColumuModels temp in Slist)
            {
                list = FlistAll.Where(p => p.ID == temp.parentid).ToList();
                if (list.Count != 0)
                {
                    if (Flist.Where(p => p.ID == list[0].ID).Count() < 1)
                    {
                        Flist.Add(list[0]);
                    }
                }
            }
            ViewData["listFather"] = Flist;
            ViewData["listSon"] = Slist;
            List<ArticleModels> listArticle = ArticleManager.GetArticle(ID);
            return View(listArticle[0]);
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditArticle()
        {
            int rel = -1;
            HttpPostedFileBase id_positive = Request.Files["id_positive"];
            string id_positive_url = "";

            //保存略缩图
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }
            else
            {
                List<ArticleModels> img = new List<ArticleModels>();
                img = ArticleManager.GetArticleImage(Convert.ToInt32(Request.Form["id"]));
                id_positive_url = img[0].thumb;
            }

            ArticleModels al = new ArticleModels();
            al.id = Convert.ToInt32(Request.Form["id"]);
            al.title = Request.Form["title"];
            al.catid = Convert.ToInt32(Request.Form["catid"]);

            UserInfo user = new UserInfo();
            user = GetSessionUserInfo();
            al.userid = user.ID;
            al.username = user.UserName;
            al.title = Request.Form["title"];
            al.keywords = Request.Form["keywords"];
            al.description = Request.Form["description"];
            al.url = Request.Form["url"];

            //插件问题，需去掉p元素
            if (Request.Form["content"] != null)
            {
                al.content = Request.Form["content"].Replace("<p></p>", "");
            }

            al.thumb = id_positive_url;
            al.listorder = 0;

            al.hits = Convert.ToInt32(Request.Form["hits"]);
            string time = Request.Form["createtime"];
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            int t = (int)cha.TotalSeconds;
            al.updatetime = t;
            al.lang = lang;
            if (ArticleManager.EditArticle(al))
            {
                rel = 1;
            }
            else
            {
                rel = -1;
            }
            return Json(rel);
        }

        //最新文章
        public ActionResult ArticleNew()
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["listFather"] = list;
            list = new List<ColumuModels>();
            list = ColumuManager.GetSonColumu(lang);
            ViewData["listSon"] = list;
            List<ArticleModels> list1 = new List<ArticleModels>();
            list1 = ArticleManager.GetArticleList(lang, 0);
            ViewData["listArticle"] = list1;
            ViewData["lang"] = lang;
            return View();
        }

        //获取最新文章列表
        public JsonResult GetNewArticleList(string st = null, string et = null,int cid = 0)
        {
            List<ArticleCuttingModels> list = new List<ArticleCuttingModels>();
            list = ArticleManager.GetArticleCuttingList(lang, cid);
            if (st == null || et == null)
            {
                list = list.Where(p => p.createtime >= (p.createtime - (86400 * 3))).ToList();
            }
            else
            {
                list = list.Where(p => Common.ConvertDate(p.createtime.ToString()) >= Convert.ToDateTime(st)).Where(p => Common.ConvertDate(p.createtime.ToString()) <= Convert.ToDateTime(et).AddDays(1)).ToList();
            }
            List<ColumuModels> cat = ColumuManager.GetSonColumu(lang);
            foreach (ArticleCuttingModels temp in list)
            {
                temp.catname = cat.Where(p => p.ID == temp.catid).ToList()[0].catname;
            }
            JsonResult json = new JsonResult();
            json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
            return json;
        }

        //判断文章是否存在
        public JsonResult GetArticleExistence(string title)
        {
            if (ArticleManager.GetArticleExistence(title)) {
                return Json("0");
            }
            return Json("1");
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