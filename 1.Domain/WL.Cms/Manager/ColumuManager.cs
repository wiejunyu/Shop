using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WL.Cms.Models;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class ColumuManager
    {

        /// <summary>
        /// 获取所有栏目名称
        /// </summary>
        /// <param name="lang">语言类型1、中文 2、英文 3、粤语 6、越南语</param>
        /// <returns></returns>
        public static List<ColumuModels> GetColumu(int lang)
        {
            string sql = "select * from Cms_Category where lang = " + lang.ToString() + " ORDER BY listorder";
            return new BaseDAL().GetList<ColumuModels>(sql, null);
        }

        /// <summary>
        /// 获取所有父栏目
        /// </summary>
        /// <param name="lang">语言类型1、中文 2、英文 3、粤语 6、越南语</param>
        /// <returns></returns>
        public static List<ColumuModels> GetFatherColumu(int lang)
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetColumu(lang);

            return list.Where(u => u.parentid == 0).ToList();
        }

        /// <summary>
        /// 获取所有子栏目
        /// </summary>
        /// <param name="lang">语言类型1、中文 2、英文 3、粤语 6、越南语</param>
        /// <returns></returns>
        public static List<ColumuModels> GetSonColumu(int lang)
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetColumu(lang);
            return list.Where(u => u.parentid != 0).ToList();
        }

        /// <summary>
        /// 按文章模型获取栏目
        /// </summary>
        /// <param name="lang">语言类型1、中文 2、英文 3、粤语 6、越南语</param>
        /// <param name="mid">模型id</param>
        /// <returns></returns>
        public static List<ColumuModels> GetMidColumu(int lang,int mid)
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetColumu(lang);
            return list.Where(u => u.moduleid == mid).ToList();
        }

        /// <summary>
        /// 父栏目id获取所有子栏目
        /// </summary>
        /// <param name="lang">语言类型1、中文 2、英文 3、粤语 6、越南语</param>
        /// <param name="id">父栏目id</param>
        /// <returns></returns>
        public static List<ColumuModels> GetSonColumu(int lang,int id)
        {
            List<ColumuModels> list = new List<ColumuModels>();
            list = ColumuManager.GetColumu(lang);
            return list.Where(u => u.parentid == id).ToList();
        }

        /// <summary>
        /// 添加新栏目
        /// </summary>
        /// <param name="Columu"></param>
        /// <returns></returns>
        public static bool AddColumu(ColumuModels cl)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Catname", cl.catname);
            param.Add("@Parentid", cl.parentid);
            param.Add("@Moduleid", cl.moduleid);
            param.Add("@Title", cl.title);
            param.Add("@Keywords", cl.keywords);
            param.Add("@Description", cl.description);
            param.Add("@Listorder", cl.listorder);
            param.Add("@Hits", cl.hits);
            param.Add("@Image", cl.image);
            param.Add("@Url", cl.url);
            param.Add("@Lang", cl.lang);
            param.Add("@Catdir", cl.catdir);

            string sql = "INSERT INTO Cms_Category VALUES (@Catname,@Parentid,@Moduleid,@Title,@Keywords,@Description,@Listorder,@Hits,@Image,@Url,@Lang,@Catdir)";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// ID获取栏目
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<ColumuModels> IdGetColumu(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            string sql = "select * from Cms_Category where id=@id";
            return new BaseDAL().GetList<ColumuModels>(sql, param);
        }

        /// <summary>
        /// 修改栏目
        /// </summary>
        /// <param name="Columu"></param>
        /// <returns></returns>
        public static bool EditColumu(ColumuModels cl)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", cl.ID);
            param.Add("@Catname", cl.catname);
            param.Add("@Parentid", cl.parentid);
            param.Add("@Moduleid", cl.moduleid);
            param.Add("@Title", cl.title);
            param.Add("@Keywords", cl.keywords);
            param.Add("@Description", cl.description);
            param.Add("@Listorder", cl.listorder);
            param.Add("@Hits", cl.hits);
            param.Add("@Image", cl.image);
            param.Add("@Url", cl.url);
            param.Add("@Lang", cl.lang.ToString());
            param.Add("@Catdir", cl.catdir);

            string sql = "UPDATE Cms_Category SET [catname]=@Catname,[parentid]=@Parentid,[moduleid]=@Moduleid,[title]=@Title,[keywords]=@Keywords,[description]=@Description,[listorder]=@Listorder,[hits]=@Hits,[image]=@Image,[url]=@Url,[lang]=@Lang,[catdir]=@Catdir WHERE [id] = @ID";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelColumu(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            string sql = "DELETE FROM Cms_Category WHERE id = @ID";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 按栏目删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelColumuArticle(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            string sql = "DELETE FROM Cms_Article WHERE catid = @ID";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 获取栏目图像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ColumuModels> GetColumuImage(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            string sql = "select [image] from Cms_Category where id=@id";
            return new BaseDAL().GetList<ColumuModels>(sql, param);
        }

        /// <summary>
        /// 获取栏目URL
        /// </summary>
        /// <param name="strUrl">栏目名称</param>
        /// <param name="parentid">父栏目路径</param>
        /// <returns></returns>
        public static string CreateFolder(string strUrl, ColumuModels cl, List<ColumuModels> list)
        {
            string strNow = HttpRuntime.AppDomainAppPath.ToString();
            if (cl.moduleid != 0)
            {
                if (cl.parentid == 0)
                {
                    cl.url = "/" + strUrl + "/";
                    strUrl = strNow + strUrl;
                    return cl.url;
                }
                else
                {
                    string strFatherUrl = (list.Where(u => u.ID == cl.parentid).ToList())[0].url;
                    if (strFatherUrl.Trim() == "/" || strFatherUrl.Trim() == "")
                    {
                        cl.url = "/" + strUrl + "/";
                        strUrl = strNow + strUrl;
                    }
                    else
                    {
                        cl.url = strFatherUrl + strUrl + "/";
                        strUrl = (strFatherUrl.Substring(1, strFatherUrl.Length - 1)).Replace("/", "\\") + strUrl;
                        strUrl = strNow + strUrl;
                    }
                    return cl.url;
                }
            }
            else
            {
                return strUrl;
            }
        }

        /// <summary>
        /// 判断是否是父栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool JudgeFatherColumu(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);

            string sql = "select [parentid] from Cms_Category where id=@id";
            List<int> list = new List<int>();
            list = new BaseDAL().GetList<int>(sql, param);
            if (list[0] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 按栏目catdir获取栏目
        /// </summary>
        /// <param name="Catdir"></param>
        /// <returns></returns>
        public static ColumuModels CatdirGetColumu(string catdir)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@catdir", catdir);
            string sql = "select * from Cms_Category where catdir=@catdir";
            return new BaseDAL().Single<ColumuModels>(sql, param);
        }

    }
}
