using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain.Api
{
    /// <summary>
    /// 配置字符串类
    /// </summary>
    public class CommentConfig
    {
        /// <summary>
        /// 图片验证码RedisKey
        /// </summary>
        public static string ImageCacheCode { get { return "ImagesCacheCode"; } }

        /// <summary>
        /// 邮箱验证码RedisKey
        /// </summary>
        public static string MailCacheCode { get { return "MailCacheCode"; } }

        /// <summary>
        /// Cookie票据名称
        /// </summary>
        public static string FormsCookieName { get { return "WL"; } }

        /// <summary>
        /// 用户RedisKey
        /// </summary>
        public static string PrefixKey { get { return "UserLogin_"; } }

        /// <summary>
        /// 验证身份头
        /// </summary>
        public static string Headers_Token { get { return "Token"; } }
    }
}
