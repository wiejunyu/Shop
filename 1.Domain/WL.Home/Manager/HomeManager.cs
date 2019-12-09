using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Home.Models;
using WL.Infrastructure.Data;

namespace WL.Home.Manager
{
    public class HomeManager
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<ArticleModels> GetArticleList()
        {
            string sql = "select * from CMS_Article where lang = 1 ORDER BY createtime DESC";
            return new BaseDAL().GetList<ArticleModels>(sql, null);
        }

        /// <summary>
        /// ID获取文章
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static ArticleModels GetArticle(int aid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", aid);
            string sql = "select * from CMS_Article where id = @id ";
            return new BaseDAL().Single<ArticleModels>(sql, param);
        }

        /// <summary>
        /// 获取所有商品栏目
        /// </summary>
        /// <returns></returns>
        public static List<CategoryModels> GetShopCatList()
        {
            string sql = "select * from Category";
            return new BaseDAL().GetList<CategoryModels>(sql, null);
        }

        /// <summary>
        /// 获取商品，参数为0获取推荐
        /// </summary>
        /// <returns></returns>
        public static List<ShopModels> GetShopList(int id)
        {
            string sql = "select * from Shop";
            List<ShopModels> list = new BaseDAL().GetList<ShopModels>(sql, null);
            if (id == 0)
            {
                list = list.Where(p => p.Recommend == true).ToList();
            }
            else
            {
                list = list.Where(p => p.Catid == id).ToList();
            }
            return list;
        }
    }
}
