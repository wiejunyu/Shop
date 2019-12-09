using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WL.Cms.Models;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class ArticleManager
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="cid">0为获取所有</param>
        /// <returns></returns>
        public static List<ArticleModels> GetArticleList(int lang, int cid)
        {
            List<ArticleModels> list = new List<ArticleModels>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Lang", lang);
            string sql = "select * from Cms_Article where lang=@Lang";
            list = new BaseDAL().GetList<ArticleModels>(sql, param);
            if (cid == 0)
            {
                return list;
            }
            else
            {
                return list.Where(p => p.catid == cid).ToList();
            }
        }

        /// <summary>
        /// 获取简单字段文章列表
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="cid">0为获取所有</param>
        /// <returns></returns>
        public static List<ArticleCuttingModels> GetArticleCuttingList(int lang, int cid)
        {
            List<ArticleCuttingModels> list = new List<ArticleCuttingModels>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Lang", lang);
            string sql = "select * from Cms_Article where lang=@Lang ORDER BY updatetime DESC";
            list = new BaseDAL().GetList<ArticleCuttingModels>(sql, param);
            if (cid == 0)
            {
                return list;
            }
            else
            {
                return list.Where(p => p.catid == cid).ToList();
            }
        }

        /// <summary>
        /// 文章url链接
        /// </summary>
        /// <param name = "Article" ></ param >
        /// < returns ></ returns >
        public static bool UrlArticle(int cid, int aid)
        {
            string str = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@cid", cid);
            string sql = "select * from Cms_Category where id = @cid";
            List<ColumuModels> list = new BaseDAL().GetList<ColumuModels>(sql, param);
            sql = "select * from Cms_Category where id = (select MAX(id) from Category)";
            List<ColumuModels> listMax = new BaseDAL().GetList<ColumuModels>(sql, param);
            str = "/" + list[0].catdir + "/show/" + aid + ".html";
            param = new DynamicParameters();
            param.Add("@aid", aid);
            param.Add("@url", str);
            sql = "UPDATE Cms_Article SET url = @url WHERE id = @aid";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <param name="Article"></param>
        /// <returns></returns>
        public static int AddArticle(ArticleModels al)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@catid", al.catid);
            param.Add("@userid", al.userid);
            param.Add("@username", al.username);
            param.Add("@title", al.title);
            param.Add("@keywords", al.keywords);
            param.Add("@description", al.description);
            param.Add("@content", al.content);
            param.Add("@thumb", al.thumb);
            param.Add("@listorder", al.listorder);
            param.Add("@url", al.url);
            param.Add("@hits", al.hits);
            param.Add("@createtime", al.createtime);
            param.Add("@updatetime", al.updatetime);
            param.Add("@lang", al.lang);
            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sql = "INSERT INTO Cms_Article VALUES (@catid,@userid,@username,@title,@keywords,@description,@content,@thumb,@listorder,@url,@hits,@createtime,@updatetime,@lang);SELECT @ID=SCOPE_IDENTITY()";
            return new BaseDAL().AddGetID(sql, param);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="Article"></param>
        /// <returns></returns>
        public static bool EditArticle(ArticleModels al)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", al.id);
            param.Add("@catid", al.catid);
            param.Add("@userid", al.userid);
            param.Add("@username", al.username);
            param.Add("@title", al.title);
            param.Add("@keywords", al.keywords);
            param.Add("@description", al.description);
            param.Add("@content", al.content);
            param.Add("@thumb", al.thumb);
            param.Add("@listorder", al.listorder);
            param.Add("@url", al.url);
            param.Add("@hits", al.hits);
            param.Add("@updatetime", al.updatetime);
            param.Add("@lang", al.lang);

            string sql = "UPDATE Cms_Article SET [catid]=@catid,[userid]=@userid,[username]=@username,[title]=@title,[keywords]=@keywords,[description]=@description,[content]=@content,[thumb]=@thumb,[listorder]=@listorder,[url]=@url,[hits]=@hits,[updatetime]=@updatetime,[lang]=@lang WHERE [id] = @id";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static bool DelArticle(int aid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", aid);

            string sql = "delete from Cms_Article where id = @id";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 按id获取文章
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static List<ArticleModels> GetArticle(int aid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", aid);
            string sql = "select * from Cms_Article where id=@id";
            return new BaseDAL().GetList<ArticleModels>(sql, param);
        }

        /// <summary>
        /// 获取文章图像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ArticleModels> GetArticleImage(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            string sql = "select [thumb] from Cms_Article where id=@id";
            return new BaseDAL().GetList<ArticleModels>(sql, param);
        }

        /// <summary>
        /// 修改文章栏目
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <param name="cid">栏目ID</param>
        /// <returns></returns>
        public static bool MoveArticle(int aid, int cid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@aid", aid);
            param.Add("@cid", cid);
            string sql = "UPDATE Cms_Article SET catid = @cid WHERE id = @aid";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 按标题判断文章是否存在,存在true，不存在false
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool GetArticleExistence(string title)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@title", title);
            string sql = "Select * from Cms_Article WHERE title = @title";
            if (new BaseDAL().Single<ArticleModels>(sql, param) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 栏目ID获取栏目名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string IdGetCatname(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            string sql = "Select catname from Cms_Category WHERE id = @id";
            return new BaseDAL().Single<string>(sql, param);
        }
    }
}
