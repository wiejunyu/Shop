using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WL.Infrastructure.Commom;

namespace WL.API.Filters
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected string GetUserId()
        {
            var pricipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var userIdclaim = pricipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdclaim == null)
            {
                return string.Empty;
            }
            return userIdclaim.Value;
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            //actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>() 允许匿名访问

            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (!SkipAuthorization(actionContext) && Manager.UserLoginHelper.CheckLogin())
            {
                // 非法请求，统一返回400错误代码。
                throw new Exception(JsonConvert.SerializeObject(new { Status = (int)ReturnResultStatus.UnLogin, Message = "请求被拒绝！" }));
            }
        }
    }
}