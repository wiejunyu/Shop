using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WL.API.Models;
using WL.Cms.Manager;
using WL.Domain;
using WL.Domain.Enum;
using WL.Infrastructure.Common;

namespace WL.API.Controllers
{

    public class ApiBaseController : ApiController
    {
        /// <summary>
        /// HttpResponse直接返回Json结果
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Json(string json)
        {
            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        private string requestBody = "";
        [ApiExplorerSettings(IgnoreApi = true)]
        public ResponseData<T> InvokeFunc<T>(Func<T> func)
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            var response = new ResponseData<T>();
            try
            {
                //使用期限
                // UsingTimeOut();
                requestBody = GetPostData();
                T result = func();
                response.Status = (int)ReturnResultStatus.Succeed;
                response.Data = result;

            }
            catch (AppException exception)
            {
                response.HandleException(exception);
                response.Status = (int)ReturnResultStatus.BLLError;
            }
            catch (Exception exception)
            {
                //记录数据库日志
                #region 日志记录
                string errorCode = DateTime.Now.ToString("yyMMddHHmmss");
                ExceptionLog exceptionLog = new ExceptionLog();
                exceptionLog.Response = JsonConvert.SerializeObject(exception);
                exceptionLog.Error_code = errorCode;
                exceptionLog.Exception_Type = (int)ExceptionTypeEnum.System;
                exceptionLog.Url = Url;
                exceptionLog.CreateTime = DateTime.Now;
                exceptionLog.Request = requestBody;
                response.Message = "网络异常或超时，请稍后再试！";
                response.Status = (int)ReturnResultStatus.BLLError;
                using (WLDbContext db = new WLDbContext()) 
                {
                    db.ExceptionLog.Add(exceptionLog);
                    db.SaveChanges();
                }
                #endregion
            }

            stop.Stop();
            long runtime = stop.ElapsedMilliseconds;
            ExceptionLogManager.TimeOutLog(Url, requestBody, JsonConvert.SerializeObject(response), runtime);
            return response;
        }

        /// <summary>
        /// 获取POST传过来的数据
        /// </summary>
        /// <returns></returns>
        protected string GetPostData()
        {
            try
            {
                HttpRequest rq = HttpContext.Current.Request;
                return getResultData(rq.InputStream);
            }
            catch (Exception xe)
            {
                return "";
            }

        }
        private string getResultData(Stream task)
        {
            string taskData = string.Empty;
            using (System.IO.Stream sm = task)
            {
                if (sm != null)
                {
                    sm.Seek(0, SeekOrigin.Begin);
                    int len = (int)sm.Length;
                    byte[] inputByts = new byte[len];
                    sm.Read(inputByts, 0, len);
                    sm.Close();
                    taskData = Encoding.UTF8.GetString(inputByts);
                }
            }
            return taskData;
        }
        /// <summary>
        /// 获取请求url
        /// </summary>
        protected string Url
        {
            get
            {
                if (Request == null || Request.RequestUri == null)
                {
                    return string.Empty;
                }
                return Request.RequestUri.ToString();
            }
        }
    }
}