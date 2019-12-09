using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DepartmentOA.Filters
{
    /// <summary>
    /// downLoad 的摘要说明
    /// </summary>
    public class downLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath("~/App_Data/data.csv"));
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileInfo.Name);
            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream;charset=gb2312";
            HttpContext.Current.Response.Filter.Close();
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
            HttpContext.Current.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}