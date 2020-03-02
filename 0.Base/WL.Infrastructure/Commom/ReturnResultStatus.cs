using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Commom
{
    public enum ReturnResultStatus
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Succeed = 200,
        /// <summary>
        /// 数据未通过验证
        /// </summary>
        UnValidate = 401,
        /// <summary>
        /// 非法操作
        /// </summary>
        Illegal = 402,
        /// <summary>
        /// 未登录
        /// </summary>
        UnLogin = 403,
        /// <summary>
        /// 没有操作权限
        /// </summary>
        NoPermission = 404,
        /// <summary>
        /// 业务出错
        /// </summary>
        BLLError = 405,
        /// <summary>
        /// 系统找不到数据
        /// </summary>
        NoData = 406,
        /// <summary>
        /// 服务器出错
        /// </summary>
        ServerError = 500
    }
}
