using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain.Enum
{
    public enum ExceptionTypeEnum
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        Business = 1,
        /// <summary>
        ///系统异常
        /// </summary>
        System = 2,
        /// <summary>
        /// 订单
        /// </summary>
        Order = 3,
        /// <summary>
        /// 逻辑过程
        /// </summary>
        Logical = 4,
        /// <summary>
        /// 请求时间过长
        /// </summary>
        TimeOut = 5,
        /// <summary>
        /// 前端APP日志
        /// </summary>
        APP = 6
    }
}
