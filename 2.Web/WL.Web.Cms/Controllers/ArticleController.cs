
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
        static public int lang = 1;
        // GET: Article
        public ActionResult ArticleAdd()
        {
            List<Columu> Flist = new List<Columu>();
            List<Columu> FlistAll = new List<Columu>();
            List<Columu> Slist = new List<Columu>();
            List<Columu> list = new List<Columu>();
            Slist = ColumuManager.GetMidColumu(lang, 2);
            FlistAll = ColumuManager.GetFatherColumu(lang);
            foreach (Columu temp in Slist)
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
            return View();
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
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }
            Article al = new Article();
            al.title = Request.Form["title"];
            al.catid = Convert.ToInt32(Request.Form["catid"]);
            
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

        public ActionResult ArticleView()
        {
            List<Columu> list = new List<Columu>();
            list = ColumuManager.GetFatherColumu(lang);
            ViewData["listFather"] = list;
            list = new List<Columu>();
            list = ColumuManager.GetSonColumu(lang);
            ViewData["listSon"] = list;
            List<Article> list1 = new List<Article>();
            list1 = ArticleManager.GetArticleList(lang, 0);
            ViewData["listArticle"] = list1;
            ViewData["lang"] = lang;
            return View();
        }

        //语言切换
        public JsonResult ArticleManagementLangFather(int langInt)
        {
            lang = langInt;
            return Json("1");
        }

        public ActionResult ArticleIndex()
        {
            List<Columu> Flist = new List<Columu>();
            List<Columu> FlistAll = new List<Columu>();
            List<Columu> Slist = new List<Columu>();
            List<Columu> list = new List<Columu>();
            Slist = ColumuManager.GetMidColumu(lang, 2);
            FlistAll = ColumuManager.GetFatherColumu(lang);
            foreach (Columu temp in Slist)
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
            return View();
        }

        //获取文章列表
        public JsonResult GetArticleList(int cid = 0)
        {
            if (cid != 0)
            {
                if (ColumuManager.JudgeFatherColumu(cid))
                {
                    List<ArticleCutting> listArticle = new List<ArticleCutting>();
                    List<Columu> listColumu = ColumuManager.GetSonColumu(lang, cid);

                    foreach (Columu temp in listColumu)
                    {
                        listArticle.AddRange(ArticleManager.GetArticleCuttingList(lang, temp.ID));
                    }
                    JsonResult json = new JsonResult();
                    json.Data = new { sEcho = 0, iTotalRecords = listArticle.Count(), iTotalDisplayRecords = listArticle.Count(), aaData = listArticle, };
                    return json;
                }
                else
                {
                    List<ArticleCutting> list = new List<ArticleCutting>();
                    list = ArticleManager.GetArticleCuttingList(lang, cid);
                    JsonResult json = new JsonResult();
                    json.Data = new { sEcho = 0, iTotalRecords = list.Count(), iTotalDisplayRecords = list.Count(), aaData = list, };
                    return json;
                }
            }
            else
            {
                List<Article> list = new List<Article>();
                list = ArticleManager.GetArticleList(lang, 0);
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

        public ActionResult ArticleEdit(int ID)
        {

            List<Columu> Flist = new List<Columu>();
            List<Columu> FlistAll = new List<Columu>();
            List<Columu> Slist = new List<Columu>();
            List<Columu> list = new List<Columu>();
            Slist = ColumuManager.GetMidColumu(lang, 2);
            FlistAll = ColumuManager.GetFatherColumu(lang);
            foreach (Columu temp in Slist)
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
            List<Article> listArticle = ArticleManager.GetArticle(ID);
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
            if (id_positive != null && id_positive.ContentLength > 0)
            {
                id_positive_url = Upload(id_positive);
            }
            else
            {
                List<Article> img = new List<Article>();
                img = ArticleManager.GetArticleImage(Convert.ToInt32(Request.Form["id"]));
                id_positive_url = img[0].thumb;
            }
            Article al = new Article();
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