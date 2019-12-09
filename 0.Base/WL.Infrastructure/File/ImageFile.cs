using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WL.Infrastructure.File
{
    public class ImageFile
    {

        /// <summary>
        /// 图片base64写入文件夹，返回图片路径
        /// </summary>
        /// <param name="image">图片base64</param>
        /// <returns></returns>
        public static string SetImage(string image)
        {
            image = image.Remove(0, 23);
            byte[] bt = Convert.FromBase64String(image);//获取图片base64
            string fileName = DateTime.Now.ToString("yyyyMMdd");//年月日
            string ImageFilePath = "/Uploads" + "/" + fileName;
            if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ImageFilePath)) == false)//如果不存在就创建文件夹
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ImageFilePath));
            }
            DateTime dt = DateTime.Now;
            string result = ImageFilePath + "/" + dt.ToString("yyyyHHddHHmmss") + ".png";
            string ImagePath = System.Web.HttpContext.Current.Server.MapPath(ImageFilePath) + "/" + dt.ToString("yyyyHHddHHmmss");//定义图片名称
            System.IO.File.WriteAllBytes(ImagePath + ".png", bt); //保存图片到服务器，然后获取路径  
            return result;
        }

        /// <summary>
        /// 根据路径删除图片
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteImage(string path)
        {
            if (path != "/Image/man.jpg" && path != "/Image/woman.jpg")
            {
                string root = System.Web.HttpContext.Current.Server.MapPath(path);//获取承载在当前应用程序域中的应用程序的应用程序目录的物理驱动器路径。
                FileAttributes attr = System.IO.File.GetAttributes(root);
                if (attr == FileAttributes.Directory)
                {
                    Directory.Delete(root, true);
                }
                else
                {
                    System.IO.File.Delete(root);
                }
            }
        }
    }
}
