using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Home.Models;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class ShopManager
    {
        /// <summary>
        /// 获取所有商品栏目列表
        /// </summary>
        /// <returns></returns>
        public static List<CategoryModels> RankGetShopColumuAll()
        {
            string sql = "Select * from Category";
            return new BaseDAL().GetList<CategoryModels>(sql, null);
        }

        /// <summary>
        /// 按栏目级别获取商品栏目列表
        /// </summary>
        /// <param name="Rank"></param>
        /// <returns></returns>
        public static List<CategoryModels> RankGetShopColumu(int Rank)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Rank", Rank);
            string sql = "Select * from Category where [Rank] = @Rank";
            return new BaseDAL().GetList<CategoryModels>(sql, param);
        }

        /// <summary>
        /// 按栏目id获取商品列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static List<ShopModels> ColumuIdGetShopList(int cid)
        {
            string sql = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@cid", cid);
            if (cid == 0)
            {
                
                sql = "Select * from Shop";
            }
            else
            {
                sql = "Select * from Shop where Catid = @cid";
            }
            return new BaseDAL().GetList<ShopModels>(sql, param);
        }

        /// <summary>
        /// 按商品id获取商品
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static ShopModels AidGetShop(int aid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@aid", aid);
            string sql = "Select * from Shop where id = @aid";
            return new BaseDAL().Single<ShopModels>(sql, param);
        }

        /// <summary>
        /// 写入商品栏目
        /// </summary>
        /// <param name="Rank"></param>
        /// <returns></returns> 
        public static bool SetShopColumu(CategoryModels cat)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", cat.Name);
            param.Add("@Rank", cat.Rank);
            param.Add("@Picture", cat.Picture);
            param.Add("@Url", cat.Url);
            param.Add("@Parentid", cat.Parentid);
            param.Add("@Recommend", cat.Recommend);
            param.Add("@Click", cat.Click);
            string sql = "INSERT INTO Category VALUES (@Name,@Rank,@Picture,@Url,@Parentid,@Recommend,@Click)";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 更新商品栏目
        /// </summary>
        /// <param name="Rank"></param>
        /// <returns></returns> 
        public static bool UpdateShopColumu(CategoryModels cat)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", cat.id);
            param.Add("@Name", cat.Name);
            param.Add("@Rank", cat.Rank);
            param.Add("@Picture", cat.Picture);
            param.Add("@Url", cat.Url);
            param.Add("@Parentid", cat.Parentid);
            param.Add("@Recommend", cat.Recommend);
            param.Add("@Click", cat.Click);
            string sql = "UPDATE Category SET Name=@Name,Rank=@Rank,Picture=@Picture,Url=@Url,Parentid=@Parentid,Recommend=@Recommend,Click=@Click WHERE id = @id";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 删除商品栏目
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static bool DelShopColumu(int cid)
        {
            List<CategoryModels> list = RankGetShopColumuAll();

            CategoryModels cat = list.Where(p => p.id == cid).ToList()[0];
            if (cat.Rank == 1)
            {
                //遍历当前一级栏目的所有二级栏目
                List<CategoryModels> listTwo = list.Where(p => p.Parentid == cat.id).ToList();
                foreach (CategoryModels tempTwo in listTwo)
                {

                    //遍历当前二级栏目的所有三级栏目
                    List<CategoryModels> listThree = list.Where(p => p.Parentid == tempTwo.id).ToList();
                    foreach (CategoryModels tempThree in listThree)
                    {
                        //删除当前三级栏目所有商品
                        CidDelShop(tempThree.id);

                        //删除当前三级栏目
                        DynamicParameters ParamThree = new DynamicParameters();
                        ParamThree.Add("@ThreeCid", tempThree.id);
                        string SqlThree = "DELETE FROM Category WHERE id = @ThreeCid";
                        new BaseDAL().Delete(SqlThree, ParamThree);
                    }

                    //删除当前二级栏目
                    DynamicParameters ParamTwo = new DynamicParameters();
                    ParamTwo.Add("@TwoCid", tempTwo.id);
                    string SqlTwo = "DELETE FROM Category WHERE id = @TwoCid";
                    new BaseDAL().Delete(SqlTwo, ParamTwo);
                }

                //删除当前一级栏目
                DynamicParameters ParamOne = new DynamicParameters();
                ParamOne.Add("@OneCid", cat.id);
                string SqlOne = "DELETE FROM Category WHERE id = @OneCid";
                new BaseDAL().Delete(SqlOne, ParamOne);
            }
            if (cat.Rank == 2)
            {
                //遍历当前二级栏目的所有三级栏目
                List<CategoryModels> listThree = list.Where(p => p.Parentid == cat.id).ToList();
                foreach (CategoryModels tempThree in listThree)
                {
                    //删除当前三级栏目所有商品
                    CidDelShop(tempThree.id);

                    //删除当前三级栏目
                    DynamicParameters ParamThree = new DynamicParameters();
                    ParamThree.Add("@ThreeCid", tempThree.id);
                    string SqlThree = "DELETE FROM Category WHERE id = @ThreeCid";
                    new BaseDAL().Delete(SqlThree, ParamThree);
                }

                //删除当前二级栏目
                DynamicParameters ParamTwo = new DynamicParameters();
                ParamTwo.Add("@TwoCid", cat.id);
                string SqlTwo = "DELETE FROM Category WHERE id = @TwoCid";
                new BaseDAL().Delete(SqlTwo, ParamTwo);
            }
            if (cat.Rank == 3)
            {
                //删除当前三级栏目所有商品
                CidDelShop(cat.id);

                //删除当前三级栏目
                DynamicParameters ParamThree = new DynamicParameters();
                ParamThree.Add("@ThreeCid", cat.id);
                string SqlThree = "DELETE FROM Category WHERE id = @ThreeCid";
                new BaseDAL().Delete(SqlThree, ParamThree);
            }
            return true;
        }

        /// <summary>
        /// 写入商品
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static bool SetShop(ShopModels shop)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", shop.Name);
            param.Add("@Catid", shop.Catid);
            param.Add("@Uptime", shop.Uptime);
            param.Add("@Units", shop.Units);
            param.Add("@Brand", shop.Brand);
            param.Add("@Trueprice", shop.Trueprice);
            param.Add("@Price", shop.Price);
            param.Add("@Body", shop.Body);
            param.Add("@Model", shop.Model);
            param.Add("@Vocation", shop.Vocation);
            param.Add("@Click", shop.Click);
            param.Add("@Buy", shop.Buy);
            param.Add("@Recommend", shop.Recommend);
            param.Add("@Image", shop.Image);
            param.Add("@HighImage", shop.HighImage);
            param.Add("@Remarks", shop.Remarks);
            param.Add("@Parameter", shop.Parameter);
            param.Add("@Certificate", shop.Certificate);

            string sql = "INSERT INTO Shop VALUES (@Name,@Catid,@Uptime,@Units,@Brand,@Trueprice,@Price,@Body,@Model,@Vocation,@Click,@Buy,@Recommend,@Image,@HighImage,@Remarks,@Parameter,@Certificate)";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static bool DelShop(int sid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@sid", sid);

            string sql = "DELETE FROM Shop WHERE id = @sid";
            return new BaseDAL().Delete(sql, param);
        }

        /// <summary>
        /// 按栏目ID删除商品
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns> 
        public static bool CidDelShop(int cid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@cid", cid);

            string sql = "DELETE FROM Shop WHERE Catid = @cid";
            return new BaseDAL().Delete(sql, param);
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static bool UpdateShop(ShopModels shop)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id", shop.id);
            param.Add("@Name", shop.Name);
            param.Add("@Catid", shop.Catid);
            param.Add("@Uptime", shop.Uptime);
            param.Add("@Units", shop.Units);
            param.Add("@Brand", shop.Brand);
            param.Add("@Trueprice", shop.Trueprice);
            param.Add("@Price", shop.Price);
            param.Add("@Body", shop.Body);
            param.Add("@Model", shop.Model);
            param.Add("@Vocation", shop.Vocation);
            param.Add("@Click", shop.Click);
            param.Add("@Buy", shop.Buy);
            param.Add("@Recommend", shop.Recommend);
            param.Add("@Image", shop.Image);
            param.Add("@HighImage", shop.HighImage);
            param.Add("@Remarks", shop.Remarks);
            param.Add("@Parameter", shop.Parameter);
            param.Add("@Certificate", shop.Certificate);

            string sql = "UPDATE Shop SET Name=@Name,Catid=@Catid,Uptime=@Uptime,Units=@Units,Brand=@Brand,Trueprice=@Trueprice,Price=@Price,Body=@Body,Model=@Model,Vocation=@Vocation,Click=@Click,Buy=@Buy,Recommend=@Recommend,Image=@Image,HighImage=@HighImage,Remarks=@Remarks,Parameter=@Parameter,Certificate=@Certificate WHERE id = @id";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 获取商品缩略图
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static string GetShopImage(int sid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@sid", sid);

            string sql = "Select [Image] from Shop where id = @sid";
            return new BaseDAL().Single<string>(sql, param);
        }

        /// <summary>
        /// 获取商品栏目缩略图
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static string GetShopColumuImage(int cid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@cid", cid);

            string sql = "Select [Picture] from Category where id = @cid";
            return new BaseDAL().Single<string>(sql, param);
        }

        /// <summary>
        /// 更新商品的栏目
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns> 
        public static bool UpdateShopCatid(int sid,int cid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@sid", sid);
            param.Add("@Catid", cid);

            string sql = "UPDATE Shop SET Catid=@Catid WHERE id = @sid";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 按商品栏目id获取商品栏目
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static CategoryModels CidGetShopColumu(int cid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@cid", cid);
            string sql = "Select * from Category where id = @cid";
            return new BaseDAL().Single<CategoryModels>(sql, param);
        }

        /// <summary>
        /// 按父栏目获取商品栏目列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static List<CategoryModels> ParentidGetShopColumu(int cid)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@cid", cid);
            string sql = "Select * from Category where Parentid = @cid";
            return new BaseDAL().GetList<CategoryModels>(sql, param);
        }

    }
}
