using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WL.Home.Models;
using WL.Infrastructure.Data;

namespace WL.Home.Manager
{
    public class HomeManager
    {
        //文章
        #region

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

        #endregion

        //商品
        #region

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
        /// 获取商品列表，参数为0获取推荐
        /// </summary>
        /// <returns></returns>
        public static List<ShopModels> GetShopList(int? id)
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

        /// <summary>
        /// 按ID获取商品
        /// </summary>
        /// <returns></returns>
        public static ShopModels GetShop(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            string sql = "select * from Shop where id = @id";
            ShopModels shop = new BaseDAL().Single<ShopModels>(sql, param);
            return shop;
        }

        /// <summary>
        /// 获取销量排行商品
        /// </summary>
        /// <returns></returns>
        public static List<ShopModels> GetListShopBuy()
        {
            string sql = "select * from Shop ORDER BY Buy";
            List<ShopModels> list = new BaseDAL().GetList<ShopModels>(sql, null);
            return list.Take(10).ToList();
        }

        /// <summary>
        /// 获取模糊查询商品
        /// </summary>
        /// <returns></returns>
        public static List<ShopModels> GetListShopSearch(string str)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@str","%" + str + "%");
            string sql = "Select * from Shop where name like @str";
            return new BaseDAL().GetList<ShopModels>(sql, param);
        }

        #endregion

        //购物车
        #region

        /// <summary>
        /// 写入购物车
        /// </summary>
        /// <returns></returns>
        public static bool SetShoppingCart(ShoppingCartModels sc)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ShopId", sc.ShopId);
            param.Add("@UserId", sc.UserId);
            param.Add("@Number", sc.Number);
            param.Add("@Date", sc.Date);
            string sql = "INSERT INTO ShoppingCart VALUES (@ShopId,@UserId,@Number,@Date)";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <returns></returns>
        public static List<ShoppingCartModels> GetShoppingCart()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", user.ID);
            string sql = "Select * from ShoppingCart where UserId=@UserId";
            List<ShoppingCartModels> list = new BaseDAL().GetList<ShoppingCartModels>(sql, param);
            list = list.Where(p => p.Date > DateTime.Now.AddDays(-1)).ToList();
            return list;
        }

        /// <summary>
        /// 获取用户购物车列表
        /// </summary>
        /// <returns></returns>
        public static List<ShopModels> GetUserShoppingCart()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", user.ID);
            param.Add("@Date", DateTime.Now.AddDays(-1));
            string sql = "SELECT [id],[Name],[Catid],[Uptime],[Units],[Brand],[Trueprice],[Price],[Body],[Model],[Vocation],[Click],[Recommend],[Image],[HighImage] ,[Remarks] ,[Parameter],[Certificate],(Select Sum(Number) from ShoppingCart where ShopId = a.id) as Buy FROM Shop as a where exists(Select * from ShoppingCart where ShopId = a.id and UserId = 1 and Date > @Date)";
            List<ShopModels> list = new BaseDAL().GetList<ShopModels>(sql, param);
            return list;
        }

        /// <summary>
        /// 删除过期购物车
        /// </summary>
        /// <returns></returns>
        public static bool DelShoppingCart()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Date",DateTime.Now.AddDays(-1));
            string sql = "DELETE FROM ShoppingCart WHERE Date < @Date";
            return new BaseDAL().Delete(sql, param);
        }

        #endregion

        //订单
        #region

        /// <summary>
        /// 写入订单
        /// </summary>
        /// <returns></returns>
        public static int SetOrder(OrderModels order)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Order", order.Order);
            param.Add("@UserId", order.UserId);
            param.Add("@Sid", order.Sid);
            param.Add("@Number", order.Number);
            param.Add("@Freight", order.Freight);
            param.Add("@Money", order.Money);
            param.Add("@Address", order.Address);
            param.Add("@Invoice", order.Invoice);
            param.Add("@Date", order.Date);
            param.Add("@PayState", order.PayState);
            param.Add("@PayMethod", order.PayMethod);
            param.Add("@PayMethod", order.PayMethod);
            param.Add("@LogisticsState", order.LogisticsState);
            param.Add("@Remarks", order.Remarks);
            param.Add("@MultipleSid", order.MultipleSid);
            param.Add("@ShoppingCart", order.ShoppingCart);
            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            string sql = "INSERT INTO [Order](UserId,[Order],[Sid],Number,Freight,[Money],[Address],Invoice,Remarks,[Date],PayState,PayMethod,LogisticsState,Logistics,MultipleSid,ShoppingCart) VALUES (@UserId,@Order,@Sid,@Number,@Freight,@Money,@Address,@Invoice,@Remarks,@Date,@PayState,@PayMethod,@LogisticsState,null,@MultipleSid,@ShoppingCart);SELECT @ID=SCOPE_IDENTITY()";
            return new BaseDAL().AddGetID(sql, param);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public static List<OrderModels> GetOrder()
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", user.ID);
            string sql = "SELECT * from [Order] where userid = @UserId";
            return new BaseDAL().GetList<OrderModels>(sql, param);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public static List<OrderModels> GetSelectOrder(string state)
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", user.ID);
            param.Add("@state", state);
            string sql = "SELECT * from [Order] where userid = @UserId and PayState = @state";
            return new BaseDAL().GetList<OrderModels>(sql, param);
        }

        /// <summary>
        /// 按订单ID获取订单
        /// </summary>
        /// <returns></returns>
        public static OrderModels GetOrder(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", id);
            string sql = "SELECT * from [Order] where id = @id";
            return new BaseDAL().Single<OrderModels>(sql, param);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <returns></returns>
        public static bool DelOrder(string Order)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Order", Order);
            string sql = "Delete [Order] where [Order] = @Order";
            return new BaseDAL().Delete(sql, param);
        }

        #endregion










    }
}
