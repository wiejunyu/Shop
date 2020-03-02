using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain.Api
{
    public class CommentConfig
    {
        /// <summary>
        /// 图片验证码SessionCode
        /// </summary>
        public static string ImageSessionCode { get { return "ImagesCheckCode"; } }

        public static string FormsCookieName { get { return "WL"; } }

        public static string PrefixKey { get { return "UserLogin_"; } }

        public static string Headers_Token { get { return "Token"; } }
    }
}
