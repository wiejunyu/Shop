using WL.Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Infrastructure.Data;
using System.Data;

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
        public static List<Article> GetArticleList(int lang, int cid)
        {
            List<Article> list = new List<Article>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Lang", lang);
            string sql = "select * from Cms_Article where lang=@Lang";
            list = new BaseDAL().GetList<Article>(sql, param);
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
        public static List<ArticleCutting> GetArticleCuttingList(int lang, int cid)
        {
            List<ArticleCutting> list = new List<ArticleCutting>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Lang", lang);
            string sql = "select * from Cms_Article where lang=@Lang";
            list = new BaseDAL().GetList<ArticleCutting>(sql, param);
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
            string sql = "select * from Category where id = @cid";
            List<Columu> list = new BaseDAL().GetList<Columu>(sql, param);
            if (list[0].parentid == 0)
            {
                str = "/" + list[0].catdir + "/" + aid + ".html";
                param = new DynamicParameters();
                param.Add("@aid", aid);
                param.Add("@url", str);
                sql = "UPDATE Cms_Article SET url = @url WHERE id = @aid";
                return new BaseDAL().Update(sql, param);
            }
            else
            {
                param = new DynamicParameters();
                param.Add("@cid", list[0].parentid);
                sql = "select * from Cms_Category where id = @cid";
                List<Columu> list1 = new BaseDAL().GetList<Columu>(sql, param);
                str = "/" + list1[0].catdir + "/" + list[0].catdir + "/" + aid + ".html";

                param = new DynamicParameters();
                param.Add("@aid", aid);
                param.Add("@url", str);
                sql = "UPDATE Article SET url = @url WHERE id = @aid";
                return new BaseDAL().Update(sql, param);
            }
        }

        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <param name="Article"></param>
        /// <returns></returns>
        public static int AddArticle(Article al)
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

            string sql = "INSERT INTO Article VALUES (@catid,@userid,@username,@title,@keywords,@description,@content,@thumb,@listorder,@url,@hits,@createtime,@updatetime,@lang);SELECT @ID=SCOPE_IDENTITY()";
            return new BaseDAL().AddGetID(sql, param);
        }

        /// <summary>
        /// 添加新文章
        /// </summary>
        /// <param name="Article"></param>
        /// <returns></returns>
        public static bool EditArticle(Article al)
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

            string sql = "UPDATE Article SET [catid]=@catid,[userid]=@userid,[username]=@username,[title]=@title,[keywords]=@keywords,[description]=@description,[content]=@content,[thumb]=@thumb,[listorder]=@listorder,[url]=@url,[hits]=@hits,[updatetime]=@updatetime,[lang]=@lang WHERE [id] = @id";
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

            string sql = "delete from Article where id = @id";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 按id获取文章
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static List<Article> GetArticle(int aid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", aid);
            string sql = "select * from Article where id=@id";
            return new BaseDAL().GetList<Article>(sql, param);
        }

        /// <summary>
        /// 获取文章图像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Article> GetArticleImage(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            string sql = "select [thumb] from Article where id=@id";
            return new BaseDAL().GetList<Article>(sql, param);
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
            string sql = "UPDATE Article SET catid = @cid WHERE id = @aid";
            return new BaseDAL().Update(sql, param);
        }
    }
}
