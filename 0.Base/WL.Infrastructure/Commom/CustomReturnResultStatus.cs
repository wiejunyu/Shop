using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Infrastructure.Common
{
    //
    // 摘要:
    //     全局action 返回数据时状态码枚举
    public enum ReturnResultStatus
    {
        //
        // 摘要:
        //     操作成功
        Succeed = 200,
        //
        // 摘要:
        //     数据未通过验证
        UnValidate = 401,
        //
        // 摘要:
        //     非法操作
        Illegal = 402,
        //
        // 摘要:
        //     未登录
        UnLogin = 403,
        //
        // 摘要:
        //     没有操作权限
        NoPermission = 404,
        //
        // 摘要:
        //     业务出错
        BLLError = 405,
        //
        // 摘要:
        //     系统找不到数据
        NoData = 406,
        //
        // 摘要:
        //     服务器出错
        ServerError = 500
    }
}
