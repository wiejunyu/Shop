using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Infrastructure.Common;

namespace WL.Web.Home.Filters
{
    public class LoggerAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 上级节点
        /// </summary>
        public string Top { get; set; }
        /// <summary>
        /// 日志关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 日志描述
        /// </summary>
        public string Description { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //当没设置Logger标识的时候则不进入日志记录
            if (Top != null)
            {
                //获取用户的信息
                UserModels user = HttpContext.Current.Session["user"] as UserModels;
                string tkey = Key;
                string d = Description;
                try
                {
                    //请求类各个字段的值
                    Dictionary<string, string> parmsObj = new Dictionary<string, string>();

                    foreach (var item in filterContext.ActionDescriptor.GetParameters())
                    {
                        var itemType = item.ParameterType;
                        if (itemType.IsClass && itemType.Name != "String")
                        {
                            PropertyInfo[] infos = itemType.GetProperties();
                            foreach (PropertyInfo info in infos)
                            {
                                if (info.CanRead)
                                {
                                    var propertyValue = filterContext.Controller.ValueProvider.GetValue(info.Name);// 暂不支持多层嵌套 后期优化?
                                    if (!parmsObj.ContainsKey(info.Name))
                                    {
                                        parmsObj.Add(info.Name, null == propertyValue ? "" : propertyValue.AttemptedValue);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var parameterValue = filterContext.Controller.ValueProvider.GetValue(item.ParameterName);
                            if (!parmsObj.ContainsKey(item.ParameterName))
                            {
                                parmsObj.Add(item.ParameterName, null == parameterValue ? "" : parameterValue.AttemptedValue);
                            }
                        }
                    }
                    string top = Top;
                    string key = Key;
                    string des = Description;
                    string content = "";
                    foreach (KeyValuePair<string, string> temp in parmsObj)
                    {
                        content += temp.Key + "=" + temp.Value + ",";
                    }
                    if (content == "")
                    {
                        content = "无";
                    }
                    else
                    {
                        content = content.Remove(content.Length - 1, 1);
                    }
                    //记录日志
                    LoggerModels log = new LoggerModels();
                    log.View = top;
                    log.Action = key;
                    log.Description = des;
                    log.Remark = content;
                    log.UserName = user.UserName;
                    string ip = Common.IPAddress;
                    log.IP = ip;
                    LoggerManager.AddLoggerModels(log);
                }
                catch
                {

                }
            }

        }
    }
}