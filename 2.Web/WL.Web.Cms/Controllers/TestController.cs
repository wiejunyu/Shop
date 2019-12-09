using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WL.Web.Cms.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            string url = "http://zhidao.baidu.com/";
            if (url.Length >= 7 && url.Substring(0, 7) == "http://")
            {
                string str = "f";
            }
            else
            {
                string str = "t";
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpLoadProcess(HttpPostedFileBase file)
        {
            //保存到临时文件夹  
            string filePathName = string.Empty;
            string localPath = "Uploads\\images\\" + DateTime.Now.ToString("yyyyMMdd");
            string localPathAll = HttpRuntime.AppDomainAppPath + localPath;
            if (Request.Files.Count == 0)
            {
                return Json(new { status = 0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPathAll))
            {
                System.IO.Directory.CreateDirectory(localPathAll);
            }
            file.SaveAs(Path.Combine(localPathAll, filePathName));

            return Json(new
            {
                status = 1,
                filePath = "\\" + localPath + "\\" + filePathName
            });
        }

        /// <summary>  
        /// 删除文件  
        /// </summary>  
        /// <param name="photoName">文件名</param>  
        /// <returns></returns>  
        public ActionResult DeletePhoto(string photoName)
        {

            string urlPath = "/Resource/Uploads/" + photoName;
            string localPath = HttpRuntime.AppDomainAppPath + urlPath;

            if (System.IO.File.Exists(localPath))
            {
                FileInfo fi = new FileInfo(localPath);

                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;

                System.IO.File.Delete(localPath);

                //返回删除状态  
                return Json(true);
            }

            return Json(false);
        }
    }
}