using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;

namespace WL.Web.Home.Controllers
{
    [RoutePrefix("Test")]
    [Route("{action}")]
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(int? page)
        {
            //列表  
            List<OrderModels> Order = HomeManager.GetOrder();

            //第几页  
            int pageNumber = page ?? 1;

            //每页显示多少条  
            int pageSize = 8;

            //根据ID排序  
            Order = Order.OrderBy(x => x.Date).ToList();

            //通过ToPagedList扩展方法进行分页  
            IPagedList<OrderModels> OrderList = Order.ToPagedList(pageNumber, pageSize);

            //将分页处理后的列表传给View  
            return View(OrderList);
        }

        public ActionResult Page(int? page)
        {
            //列表  
            List<OrderModels> Order = HomeManager.GetOrder();

            //第几页  
            int pageNumber = page ?? 1;

            //每页显示多少条  
            int pageSize = 8;

            //根据ID排序  
            Order = Order.OrderBy(x => x.Date).ToList();

            //通过ToPagedList扩展方法进行分页  
            IPagedList<OrderModels> OrderList = Order.ToPagedList(pageNumber, pageSize);

            //将分页处理后的列表传给View  
            return View(OrderList);
        }
    }
}