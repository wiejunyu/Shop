using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    [Seo]
    [LoginChecker]
    public class BaseController : Controller
    {
        /// <summary>
        /// 构造
        /// </summary>
        public BaseController() : base()
        {
        }
        /// <summary>
        /// 控制器的统一异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        /// <summary>
        /// 移除客户端cookie
        /// </summary>
        public void RemoveClientCookie(string cookieName)
        {
            HttpContext.Response.Cookies[cookieName].Value = null;
            HttpContext.Response.Cookies[cookieName].HttpOnly = true;
            HttpContext.Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        /// 添加客户端cookie
        /// </summary>
        public void AddClientCookie(string cookieName, string cookieValue, int day)
        {
            HttpContext.Response.Cookies[cookieName].Value = cookieValue;
            HttpContext.Response.Cookies[cookieName].Path = "/";
            HttpContext.Response.Cookies[cookieName].HttpOnly = true;
            HttpContext.Response.Cookies[cookieName].Expires = DateTime.Now.AddDays(day);
        }

        /// <summary>
        /// 移除服务端缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveServerCookie(string key)
        {
            System.Web.HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// 刷新服务器缓存过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RefreshServerCookie(string key, object value)
        {
            System.Web.HttpContext.Current.Cache.Remove(key);
            System.Web.HttpContext.Current.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0));
        }

    }
}