using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Infrastructure.Common;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    [ShopCheckerAttribute]
    public class ShopController : BaseController
    {
        //商城
        #region

        //商城首页页
        [Route("shop/shop.html")]
        [Route("{lang}/shop/shop.html")]
        public ActionResult Shop()
        {
            List<ShopModels> list = HomeManager.GetShopList(0);
            ViewData["HotShop"] = list;
            return View();
        }

        /// <summary>
        /// 商品列表页
        /// </summary>
        /// <param name="id">商品栏目ID</param>
        /// <param name="page">分页页数</param>
        /// <param name="sort">排序方式</param>
        /// <returns></returns>
        [Route("shop/list.html")]
        [Route("{lang}/shop/list.html")]
        public ActionResult List(int? id, int? page, int? sort)
        {
            ViewData["id"] = id;
            List<CategoryModels> listCat = HomeManager.GetShopCatList();
            List<CategoryModels> listCatTwo = listCat.Where(p => p.Rank == 2).ToList();
            List<CategoryModels> listCatThree = listCat.Where(p => p.Rank == 3).ToList();
            CategoryModels cat = new CategoryModels();
            CategoryModels catTwo = new CategoryModels();
            List<ShopModels> listShop = new List<ShopModels>();
            if (id != null)
            {
                HttpCookie usercookie_list_id = new HttpCookie("usercookie_list_id");
                usercookie_list_id.Value = id.ToString();
                Response.AppendCookie(usercookie_list_id);
                foreach (CategoryModels temp in listCat)
                {
                    if (temp.id == id)
                    {
                        cat = temp;
                    }
                }

                //如果当前栏目等于一级栏目
                if (cat.Rank == 1)
                {
                    //遍历当前栏目的二级栏目
                    foreach (CategoryModels tempTwo in listCatTwo)
                    {
                        if (tempTwo.Parentid == cat.id)
                        {
                            catTwo = tempTwo;

                            //遍历当前栏目的三级栏目并获取当前三级栏目的商品
                            foreach (CategoryModels tempThree in listCatThree)
                            {
                                if (catTwo.id == tempThree.Parentid)
                                {
                                    listShop.AddRange(HomeManager.GetShopList(tempThree.id));
                                }
                            }
                        }
                    }
                }

                //如果当前栏目等于二级栏目
                if (cat.Rank == 2)
                {
                    //遍历当前栏目的三级栏目并获取当前三级栏目的商品
                    foreach (CategoryModels tempThree in listCatThree)
                    {
                        if (cat.id == tempThree.Parentid)
                        {
                            listShop.AddRange(HomeManager.GetShopList(tempThree.id));
                        }
                    }
                }
                if (cat.Rank == 3)
                {
                    listShop = HomeManager.GetShopList(id);
                }
            }
            else
            {
                HttpCookie usercookie_list_id = System.Web.HttpContext.Current.Request.Cookies["usercookie_list_id"];
                int shopid = 0;
                if (usercookie_list_id != null)
                {
                    shopid = Convert.ToInt32(usercookie_list_id.Value);
                    foreach (CategoryModels temp in listCat)
                    {
                        if (temp.id == shopid)
                        {
                            cat = temp;
                        }
                    }

                    //如果当前栏目等于一级栏目
                    if (cat.Rank == 1)
                    {
                        //遍历当前栏目的二级栏目
                        foreach (CategoryModels tempTwo in listCatTwo)
                        {
                            if (tempTwo.Parentid == cat.id)
                            {
                                catTwo = tempTwo;

                                //遍历当前栏目的三级栏目并获取当前三级栏目的商品
                                foreach (CategoryModels tempThree in listCatThree)
                                {
                                    if (catTwo.id == tempThree.Parentid)
                                    {
                                        listShop.AddRange(HomeManager.GetShopList(tempThree.id));
                                    }
                                }
                            }
                        }
                    }

                    //如果当前栏目等于二级栏目
                    if (cat.Rank == 2)
                    {
                        //遍历当前栏目的三级栏目并获取当前三级栏目的商品
                        foreach (CategoryModels tempThree in listCatThree)
                        {
                            if (cat.id == tempThree.Parentid)
                            {
                                listShop.AddRange(HomeManager.GetShopList(tempThree.id));
                            }
                        }
                    }
                    if (cat.Rank == 3)
                    {
                        listShop = HomeManager.GetShopList(shopid);
                    }
                }
            }

            #region
            //列表

            //第几页  
            int pageNumber = page ?? 1;

            //每页显示多少条  
            int pageSize = 20;

            //排序  
            if (sort == null)
            {
                HttpCookie usercookie_list_sort = System.Web.HttpContext.Current.Request.Cookies["usercookie_list_sort"];
                if (usercookie_list_sort != null)
                {
                    int sortid = Convert.ToInt32(usercookie_list_sort.Value);
                    if (sortid == 1)
                        listShop = listShop.OrderBy(p => p.Buy).ToList();
                    if (sortid == 2)
                        listShop = listShop.OrderBy(p => p.Buy).ToList();
                }
                else
                {
                    listShop = listShop.OrderBy(p => p.Uptime).ToList();
                }
            }

            if (sort == 1)
            {
                listShop = listShop.OrderBy(p => p.Buy).ToList();
                HttpCookie usercookie_list_sort = new HttpCookie("usercookie_list_sort");
                usercookie_list_sort.Value = sort.ToString();
                Response.AppendCookie(usercookie_list_sort);
            }
            if (sort == 2)
            {
                listShop = listShop.OrderBy(p => p.Trueprice).ToList();
                HttpCookie usercookie_list_sort = new HttpCookie("usercookie_list_sort");
                usercookie_list_sort.Value = sort.ToString();
                Response.AppendCookie(usercookie_list_sort);
            }

            //通过ToPagedList扩展方法进行分页  
            IPagedList<ShopModels> Shoplist = listShop.ToPagedList(pageNumber, pageSize);
            #endregion
            //将分页处理后的列表传给View
            return View(Shoplist);
        }

        /// <summary>
        /// 商品列表页
        /// </summary>
        /// <param name="str">搜索词</param>
        /// <param name="page">分页页数</param>
        /// <param name="sort">排序方式</param>
        /// <returns></returns>
        [Route("shop/search.html")]
        [Route("{lang}/shop/search.html")]
        public ActionResult Search(string str, int? page, int? sort)
        {
            ViewData["str"] = str;
            List<ShopModels> list = HomeManager.GetListShopSearch(str);
            foreach (ShopModels temp in list)
            {
                temp.Name = temp.Name.Replace(str, "<font style='color: red;'>" + str + "</font>");
            }
            #region
            //列表

            //第几页  
            int pageNumber = page ?? 1;

            //每页显示多少条  
            int pageSize = 20;

            //排序  
            if (sort == null)
            {
                HttpCookie usercookie_list_sort = System.Web.HttpContext.Current.Request.Cookies["usercookie_search_sort"];
                if (usercookie_list_sort != null)
                {
                    int sortid = Convert.ToInt32(usercookie_list_sort.Value);
                    if (sortid == 1)
                        list = list.OrderBy(p => p.Buy).ToList();
                    if (sortid == 2)
                        list = list.OrderBy(p => p.Buy).ToList();
                }
                else
                {
                    list = list.OrderBy(p => p.Uptime).ToList();
                }
            }

            if (sort == 1)
            {
                list = list.OrderBy(p => p.Buy).ToList();
                HttpCookie usercookie_list_sort = new HttpCookie("usercookie_search_sort");
                usercookie_list_sort.Value = sort.ToString();
                Response.AppendCookie(usercookie_list_sort);
            }
            if (sort == 2)
            {
                list = list.OrderBy(p => p.Trueprice).ToList();
                HttpCookie usercookie_list_sort = new HttpCookie("usercookie_search_sort");
                usercookie_list_sort.Value = sort.ToString();
                Response.AppendCookie(usercookie_list_sort);
            }

            //通过ToPagedList扩展方法进行分页  
            IPagedList<ShopModels> Shoplist = list.ToPagedList(pageNumber, pageSize);
            #endregion
            //将分页处理后的列表传给View
            return View(Shoplist);
        }

        //商品显示页
        [Route("shop/product.html")]
        [Route("{lang}/shop/product.html")]
        public ActionResult Product(int id)
        {
            ViewData["list"] = HomeManager.GetListShopBuy();
            ShopModels shop = HomeManager.GetShop(id);
            return View(shop);
        }

        //立即购买页
        [Route("shop/shopok.html")]
        [Route("{lang}/shop/shopok.html")]
        public ActionResult ShopOk(int id, int number)
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ShopModels shop = HomeManager.GetShop(id);
            shop.Remarks = number.ToString();
            return View(shop);
        }

        //立即购买页
        [Route("shop/shoppingcart.html")]
        [Route("{lang}/shop/shoppingcart.html")]
        public ActionResult ShoppingCart()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user == null)
            {
                return RedirectToAction("Login", "Login");
            }
            List<ShopModels> List = HomeManager.GetUserShoppingCart();
            ViewData["list"] = List;
            decimal Total = 0;
            foreach (ShopModels temp in List)
            {
                Total += temp.Trueprice * temp.Buy;
            }
            ViewBag.Total = Total;
            return View();
        }
        #endregion

        //订单
        #region
        //订单支付页面
        [Route("shop/pay.html")]
        [Route("{lang}/shop/pay.html")]
        public ActionResult Pay(int id)
        {
            OrderModels Order = HomeManager.GetOrder(id);
            if (Order.PayMethod == 0)
            {
                return PartialView("OrderOk");
            }
            return View();
        }
        #endregion
    }
}