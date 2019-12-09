using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Web.Cms.Filters;

namespace WL.Web.Cms.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [Logger(Key = "key", Description = "点击跳转到首页")]
        public ActionResult Index()
        {
            return View();
        }
    }
}