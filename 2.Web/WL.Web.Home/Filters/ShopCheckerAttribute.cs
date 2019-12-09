using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;

namespace WL.Web.Home.Filters
{
    public class ShopCheckerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            List<CategoryModels> category = HomeManager.GetShopCatList();
            filterContext.Controller.ViewBag.category = category;
        }
    }
}