using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    [LoginChecker]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<ArticleModels> al = HomeManager.GetArticleList();
            al = al.Take(8).ToList();
            ViewData["Article"] = al;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Purchase()
        {
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }

        public ActionResult Distribution()
        {
            return View();
        }

        public ActionResult Customs()
        {
            return View();
        }

        public ActionResult CFSDOOR()
        {
            return View();
        }
        
        public ActionResult Route()
        {
            return View();
        }
        
        public ActionResult LaosRoute()
        {
            return View();
        }

        public ActionResult MyanmarRoute()
        {
            return View();
        }
        
        public ActionResult CambodiaRoute()
        {
            return View();
        }
        
        public ActionResult ThailandRoute()
        {
            return View();
        }

        public ActionResult VietnamRoute()
        {
            return View();
        }

        public ActionResult News()
        {
            List<ArticleModels> list = HomeManager.GetArticleList();
            ViewData["list"] = list;
            return View();
        }

        public ActionResult Article(int aid)
        {
            List<ArticleModels> list = HomeManager.GetArticleList();
            ViewData["list"] = list;
            ArticleModels article = HomeManager.GetArticle(aid);
            return View(article);
        }

        [ShopChecker]
        public ActionResult Product(int id = 0)
        {
            List<ShopModels> shop = HomeManager.GetShopList(id);
            ViewData["shop"] = shop;
            return View(id);
        }
    }
}