using WL.Web.Home.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;

using WL.Infrastructure.Email;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    public class LoginController : BaseController
    {
        //登陆
        #region
        //登陆页
        [Route("login/login.html")]
        [Route("{lang}/login/login.html")]
        public ActionResult Login()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        #endregion

        //注册
        #region

        //注册页
        [Route("login/register.html")]
        [Route("{lang}/login/register.html")]
        public ActionResult Register()
        {
            return View();
        }

        #endregion


    }
}