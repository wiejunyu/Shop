using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WL.API.Manager;
using WL.Domain.Api;
using WL.Infrastructure.Common;

namespace WL.API.Filters
{
    public class LoginApiAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 鉴权
        /// </summary>
        /// <param name="actionContext">上下文</param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(CommentConfig.Headers_Token))
            {
                string headerToken = actionContext.Request.Headers.GetValues(CommentConfig.Headers_Token).ToList()[0];

                var loginUser = UserLoginHelper.CurrentUser();
                if (loginUser == null)
                {
                    //未登录
                    if (string.IsNullOrWhiteSpace(headerToken)) return false;//鉴权失败

                    var onUser = UserLoginHelper.GetUserLoginByToken(headerToken);

                    if (onUser == null) return false;//鉴权失败
                }
                else
                {
                    if (loginUser.Token != headerToken)
                    {
                        //票据不一致,重新登录
                        var onUser = UserLoginHelper.GetUserLoginByToken(headerToken);

                        if (onUser == null) return false;//鉴权失败
                    }
                }

                return true;
            }

            return UserLoginHelper.CheckLogin();
        }

        /// <summary>
        /// 鉴权失败
        /// </summary>
        /// <param name="actionContext">请求上下文</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            ReturnResult result = new ReturnResult();
            result.Status = (int)ReturnResultStatus.UnLogin;
            result.Message = "你还没有登录或登录超时,请重新登录！";

            var response = actionContext.Request.CreateResponse();
            response.Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json");
            response.StatusCode = HttpStatusCode.OK;
            actionContext.Response = response;
        }
    }
}