using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Home.Models;
using WL.Infrastructure.Data;

namespace WL.Home.Manager
{
    public class SysconfigManager
    {
        /// <summary>
        /// 获取网站配置
        /// </summary>
        /// <returns></returns>
        public static SysconfigModels GetSysconfig()
        {
            string sql = "Select * from Cms_Sysconfig where id = 1";
            return new BaseDAL().Single<SysconfigModels>(sql, null);
        }

        /// <summary>
        /// 获取网站SEO
        /// </summary>
        /// <returns></returns>
        public static ColumuModels GetSeo()
        {
            string sql = "Select * from Cms_Sysconfig where id = 1";
            return new BaseDAL().Single<ColumuModels>(sql, null);
        }
    }
}
