using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WL.Web.Cms.Filters
{
    /// <summary>
    /// uploads 的摘要说明
    /// </summary>
    public class uploads : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string filename = context.Request.QueryString["filename"].ToString();
            string time = context.Request.QueryString["time"].ToString();
            try
            {
                string sFileRoot = HttpContext.Current.Server.MapPath("~/uploads/file/images/" + time + "/");
                if (!System.IO.Directory.Exists(sFileRoot))
                    System.IO.Directory.CreateDirectory(sFileRoot);
                string filePath = HttpContext.Current.Server.MapPath("~/uploads/file/images/" + time + "/" + filename);
                Bitmap img = new Bitmap(HttpContext.Current.Request.InputStream);
                img.Save(filePath);
                context.Response.Write("ok");
            }
            catch(Exception ex)
            {
                context.Response.Write(ex);
            }
            
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