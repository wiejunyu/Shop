using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Infrastructure.Data;

namespace WL.Web.Home.Filters
{
    public class DbWorkImplAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// 上级节点
        /// </summary>
        public string dbWorkImpl { get; set; }
        /// <summary>
        /// aciton执行前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BaseDAL bdal = new BaseDAL();
            bdal.GetConnectionString(dbWorkImpl);
        }
    }
}