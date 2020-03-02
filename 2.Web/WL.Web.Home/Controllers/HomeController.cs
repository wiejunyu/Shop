using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Infrastructure.Common;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    public class HomeController : BaseController
    {

        //首页页
        [Route("")]
        [Route("index.html")]
        [Route("{lang}")]
        [Route("{lang}/index.html")]
        public ActionResult Index()
        {
            List<ArticleModels> al = HomeManager.GetArticleList();
            al = al.Take(8).ToList();
            ViewData["Article"] = al;
            List<ShopModels> list = HomeManager.GetShopList(0);
            ViewData["HotShop"] = list;
            return View();
        }

        //关于我们页
        [Route("about.html")]
        [Route("{lang}/about.html")]
        public ActionResult About()
        {
            return View();
        }

        //供应链管理
        #region

        //采购页
        [Route("purchase.html")]
        [Route("{lang}/purchase.html")]
        public ActionResult Purchase()
        {
            return View();
        }

        //仓储页
        [Route("storage.html")]
        [Route("{lang}/storage.html")]
        public ActionResult Storage()
        {
            return View();
        }

        //终端配送页
        [Route("distribution.html")]
        [Route("{lang}/distribution.html")]
        public ActionResult Distribution()
        {
            return View();
        }

        #endregion

        //物流项目
        #region

        //东南亚专线页
        [Route("route.html")]
        [Route("{lang}/route.html")]
        public ActionResult Route()
        {
            return View();
        }

        /// <summary>
        /// 东南亚专线
        /// </summary>
        /// <returns></returns>
        #region
        //老挝专线页
        [Route("laosroute.html")]
        [Route("{lang}/laosroute.html")]
        public ActionResult LaosRoute()
        {
            return View();
        }

        //缅甸专线页
        [Route("myanmarroute.html")]
        [Route("{lang}/myanmarroute.html")]
        public ActionResult MyanmarRoute()
        {
            return View();
        }

        //柬埔寨专线页
        [Route("cambodiaroute.html")]
        [Route("{lang}/cambodiaroute.html")]
        public ActionResult CambodiaRoute()
        {
            return View();
        }

        //泰国专线页
        [Route("thailandroute.html")]
        [Route("{lang}/thailandroute.html")]
        public ActionResult ThailandRoute()
        {
            return View();
        }

        //越南专线页
        [Route("vietnamroute.html")]
        [Route("{lang}/vietnamroute.html")]
        public ActionResult VietnamRoute()
        {
            return View();
        }
        #endregion

        //报关报检页
        [Route("customs.html")]
        [Route("{lang}/customs.html")]
        public ActionResult Customs()
        {
            return View();
        }

        //双清关页
        [Route("cfsdoor.html")]
        [Route("{lang}/cfsdoor.html")]
        public ActionResult CFSDOOR()
        {
            return View();
        }

        #endregion

        //行业资讯页
        [Route("news.html")]
        [Route("{lang}/news.html")]
        public ActionResult News()
        {
            List<ArticleModels> list = HomeManager.GetArticleList();
            ViewData["list"] = list;
            return View();
        }

        //文章页
        [Route("article/{aid}.html")]
        [Route("{lang}/article/{aid}.html")]
        public ActionResult Article(int aid)
        {
            List<ArticleModels> list = HomeManager.GetArticleList();
            ViewData["list"] = list;
            ArticleModels article = HomeManager.GetArticle(aid);
            ColumuModels temp = SysconfigManager.GetSeo();
            if (article.title == "")
                article.title = temp.title;
            if (article.description == "")
                article.description = temp.description;
            if (article.keywords == "")
                article.keywords = temp.keywords;
            ViewBag.title = article.title;
            ViewBag.keywords = article.keywords;
            ViewBag.description = article.description;
            return View(article);
        }

        //行业资讯页
        [Route("test.html")]
        [Route("{lang}/test.html")]
        public ActionResult Test()
        {
            List<ArticleModels> list = HomeManager.GetArticleList();
            ViewData["list"] = list;
            return View();
        }
    }
}