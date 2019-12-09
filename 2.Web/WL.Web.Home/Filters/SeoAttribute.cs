using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Cms.Manager;
using WL.Cms.Models;

namespace WL.Web.Home.Filters
{
    public class SeoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string action = filterContext.RouteData.Values["Action"].ToString().ToLower();
            ColumuModels Columu = ColumuManager.CatdirGetColumu(action);
            if (Columu == null)
            {
                Columu = SysconfigManager.GetSeo();
            }
            else
            {
                ColumuModels temp = SysconfigManager.GetSeo();
                if (Columu.title == "")
                    Columu.title = temp.title;
                if (Columu.description == "")
                    Columu.description = temp.description;
                if (Columu.keywords == "")
                    Columu.keywords = temp.keywords;
            }
            filterContext.Controller.ViewBag.title = Columu.title;
            filterContext.Controller.ViewBag.keywords = Columu.keywords;
            filterContext.Controller.ViewBag.description = Columu.description;
        }
    }
}