using WL.Web.Home.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Infrastructure.File;
using System.Globalization;
using PagedList;

namespace WL.Web.Home.Controllers
{
    [PersonalChecker]
    public class PersonalController : BaseController
    {
        //个人管理
        #region
        //用户基本资料页
        [Route("personal/personal.html")]
        [Route("{lang}/personal/personal.html")]
        public ActionResult Personal()
        {
            UserDetailsModels userdetails = UserManager.GetUserDetails();
            if (userdetails.Birthday != "") {
                userdetails.Birthday = DateTime.Parse(userdetails.Birthday).ToString("yyyy-MM-dd");
            }
            ViewData["Order"] = HomeManager.GetOrder();
            return View(userdetails);
        }
        //头像设置页
        [Route("personal/portrait.html")]
        [Route("{lang}/portrait/portrait.html")]
        public ActionResult Portrait()
        {
            return View();
        }
        //收货地址页
        [Route("personal/address.html")]
        [Route("{lang}/portrait/address.html")]
        public ActionResult Address()
        {
            List<AddressModels> list = AddressManager.GetAddress();
            ViewData["list"] = list;
            return View();
        }
        #endregion

        //订单管理
        #region
        [Route("personal/myorder.html")]
        [Route("{lang}/portrait/myorder.html")]
        //[Route("personal/myorder/{page}.html")]
        //[Route("personal/myorder/{state}.html")]
        //[Route("personal/myorder/{page}/{state}.html")]
        //[Route("{lang}/portrait/myorder/{page}/{state}.html")]
        public ActionResult MyOrder(int? page,string state = "0")
        {
            ViewData["state"] = state;
            #region
            //列表  
            List<OrderModels> Order = new List<OrderModels>();
            if (state != "0" && state != "All")
            {
                Order = HomeManager.GetSelectOrder(state);
            }
            else
            {
                Order = HomeManager.GetOrder();
            }

            //第几页  
            int pageNumber = page ?? 1;

            //每页显示多少条  
            int pageSize = 8;

            //根据ID排序  
            Order = Order.OrderBy(x => x.Date).ToList();

            //通过ToPagedList扩展方法进行分页  
            IPagedList<OrderModels> OrderList = Order.ToPagedList(pageNumber, pageSize);
            #endregion
            //将分页处理后的列表传给View
            return View(OrderList);
        }
        #endregion

        //物流查询
        #region
        //物流查询页
        [Route("personal/logisticsquery.html")]
        [Route("{lang}/portrait/logisticsquery.html")]
        public ActionResult LogisticsQuery()
        {
            return View();
        }
        #endregion
    }
}