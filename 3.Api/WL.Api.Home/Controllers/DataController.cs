using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WL.API.Controllers;
using WL.API.Models;
using WL.Home.Models;

namespace WL.Api.Home.Controllers
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    [RoutePrefix("v1/data")]
    public class DataController : ApiBaseController
    {
        /// <summary>
        /// 判断是否登陆
        /// </summary>
        /// <returns></returns>
        [Route("GetLogin"), HttpGet]
        public ResponseData<bool> GetLogin()
        {
            return InvokeFunc(() =>
            {
                UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
                return user == null ? false : true;
            });
            
        }
    }
}
