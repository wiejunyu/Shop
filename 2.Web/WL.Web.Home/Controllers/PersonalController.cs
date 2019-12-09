using WL.Web.Home.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Infrastructure.File;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    [PersonalChecker]
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index()
        {
            UserDetailsModels userdetails = UserManager.GetUserDetails();
            return View(userdetails);
        }
        
        public ActionResult MyOrder()
        {
            return View();
        }

        public ActionResult Portrait()
        {
            return View();
        }

        public ActionResult LogisticsQuery()
        {
            return View();
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Edit", Description = "头像编辑")]
        public ActionResult SetPortrait(string image)
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            string portrait = user.Portrait;
            if (UserManager.SetPortrait(ImageFile.SetImage(image)))
            {
                ImageFile.DeleteImage(portrait);
                UserManager.SetSessionUser();
                return Json("1");
            }
            else
            {
                return Json("-1");
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        [Logger(Top = "Personal", Key = "Edit", Description = "修改密码")]
        public ActionResult SetPass(string pass)
        {
            if(UserManager.SetUserPass(pass))
            {
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Edit", Description = "修改资料")]
        public ActionResult SetData(string json)
        {
            UserDetailsModels userdetails = JsonConvert.DeserializeObject<UserDetailsModels>(json);
            if(UserManager.SetUserDetails(userdetails))
            {
                return Json("1");
            }
            return Json("-1");
        }
    }
}