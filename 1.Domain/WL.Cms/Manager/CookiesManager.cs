using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WL.Cms.Manager
{
    public class CookiesManager
    {
        ///<summary>
        /// 添加cookeis
        ///</summary>
        public static void AddCookies()
        {
            if (HttpContext.Current.Request.Cookies["lang"] == null)
            {
                System.Web.HttpCookie cookies = new HttpCookie("lang");
                cookies.Value = "1";
                cookies.Expires = DateTime.Now.AddHours(1);
                HttpContext.Current.Response.Cookies.Add(cookies);
            }
        }

        #region##写入cookeis
        ///<summary>
        /// 写入cookeis
        ///</summary>
        public static void SetCookies(int lang)
        {
            if (HttpContext.Current.Request.Cookies["lang"] == null)
            {
                HttpContext.Current.Response.Cookies["lang"].Value = "1";
                HttpContext.Current.Response.Cookies["lang"].Path = "/";
                HttpContext.Current.Response.Cookies["lang"].HttpOnly = true;
                HttpContext.Current.Response.Cookies["lang"].Expires = DateTime.Now.AddHours(1);
            }
            else
            {
                HttpContext.Current.Response.Cookies["lang"].Value = lang.ToString();
                HttpContext.Current.Response.Cookies["lang"].Path = "/";
                HttpContext.Current.Response.Cookies["lang"].HttpOnly = true;
                HttpContext.Current.Response.Cookies["lang"].Expires = DateTime.Now.AddHours(1);
            }
        }
        #endregion

        #region##读取cookeis
        ///<summary>
        /// 读取cookeis
        ///</summary>
        public static string GetCookies()
        {
            if (HttpContext.Current.Request.Cookies["lang"] != null)
            {
                return HttpContext.Current.Request.Cookies["lang"].Value;
            }
            return "1";
        }
        #endregion

    }
}
