using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WL.API.Controllers;
using WL.API.Manager;
using WL.API.Models;
using WL.Domain;
using WL.Domain.Api;
using WL.Home.Models;
using WL.Infrastructure.Commom;

namespace WL.Api.Home.Controllers
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    [RoutePrefix("v1/data")]
    public class DataController : ApiBaseController
    {
        /// <summary>
        /// 用户登陆获取Token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetLogin"), HttpPost]
        public ResponseData<string> UserLogin([FromBody]LoginRequest request)
        {
            return InvokeFunc(() =>
            {
                //var sessionCode = HttpContext.Current.Session[CommentConfig.ImageSessionCode];
                //if (sessionCode == null || request.Code != Convert.ToString(sessionCode))
                //{
                //    throw new AppException("图片验证码不正确");
                //}
                throw new AppException("图片验证码不正确");
                HttpContext.Current.Session[CommentConfig.ImageSessionCode] = string.Empty;
                var result = UserLoginHelper.GetUserLoginBy(request.UserName, request.PassWord).Token;
                return result;
            });
        }

        /// <summary>
        /// 判断是否登陆
        /// </summary>
        /// <returns></returns>
        [Route("CheckLogin"), HttpPost]
        public ResponseData<bool> CheckLogin()
        {
            return InvokeFunc(() =>
            {
                return UserLoginHelper.CheckLogin();
            });
        }
    }
}
