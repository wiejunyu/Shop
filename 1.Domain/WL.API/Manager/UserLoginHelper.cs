using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using WL.Domain;
using WL.Domain.Api;
using WL.Infrastructure.Caching;
using WL.Infrastructure.Common;

namespace WL.API.Manager
{
    public class UserLoginHelper
    {
        /// <summary>
        /// 获取登陆用户
        /// </summary>
        /// <param name="keyword">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static User GetUserLoginBy(string keyword, string password)
        {
            using (WLDbContext db = new WLDbContext())
            {
                string pwd = MD5.Md5(password).ToLower();
                var account = db.User.Single(a => (a.UserName == keyword.Trim() || a.Phone == keyword.Trim() || a.Email == keyword.Trim()) && a.PassWord == pwd);
                if (account != null)
                {
                    if (string.IsNullOrWhiteSpace(account.Token))
                    {
                        account.Token = Guid.NewGuid().ToString("N");
                        db.SaveChanges();
                    }
                    else
                    {
                        ////如果想实现同一账号同一时间只能一处登录，就用开放以下这段代码
                        string key = WL.Domain.Api.CommentConfig.PrefixKey + account.Token;
                        //CacheManager.Current.Remove(key);

                        account.Token = Guid.NewGuid().ToString("N");
                        db.SaveChanges();
                    }
                    SetTicket(account.Token);
                    return account;
                }
                return null;
            }
        }

        /// <summary>
        /// 登录写入票据
        /// </summary>
        /// <param name="token">token</param>
        public static void SetTicket(string token)
        {
            #region 存入 Cookie 票据
            // 设置Ticket信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                 1,
                token,
                DateTime.Now,
                DateTime.Now.AddDays(30),//票据有效30天
                false,//票据是否持久性，若为false，设定时间到后票据过期，若为true,票据持久有效，设定时间无效。当设定为true时只是票据持久，但cookie并不是持久，还有根据需要设定cookie的Expires
                Common.IPAddress
            );

            // 加密验证票据
            string strTicket = FormsAuthentication.Encrypt(ticket);

            // 使用新userdata保存cookie
            HttpCookie cookie = new HttpCookie(CommentConfig.FormsCookieName, strTicket);
            cookie.Expires = DateTime.Now.AddDays(15);//cookie保存15天 注释永久
            cookie.Path = "/";

            HttpContext.Current.Response.Cookies.Add(cookie);

            #endregion

            #region 写入当前 HttpContext.Current.User
            IPrincipal pl = new GenericPrincipal(new FormsIdentity(ticket), new string[] { ticket.UserData });
            HttpContext.Current.User = pl;
            #endregion
        }

        /// <summary>
        /// 获取当前会员登录对象
        /// <para>当没登陆或者登录信息不符时，这里返回 null </para>
        /// </summary>
        /// <returns></returns>
        public static User CurrentUser()
        {
            //线程获取失败
            if (HttpContext.Current == null)
                return null;

            //客户端凭证验证不通过，要求重新登录
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            //客户端IP不一样，要求重新登录
            var ticket = ((System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity).Ticket;
            if (ticket.UserData != Common.IPAddress)
                return null;
            //票据过期，要求重新登录
            if (ticket.Expired)
                return null;
            //延长过期时间(票据)
            if ((ticket.Expiration - DateTime.Now).Days <= 14)
            {
                SetTicket(ticket.Name);
            }

            //从Redis获取用户
            string key = WL.Domain.Api.CommentConfig.PrefixKey + ticket.Name;
            User user = null;
            user = CacheManager.Current.Get<User>(key);
            //获取不到存入一个有效时长30分钟的Redis
            if (user == null)
            {
                using (WLDbContext db = new WLDbContext())
                {
                    user = db.User.Single(d => d.Token == ticket.Name);
                    CacheManager.Current.Set<User>(key, user, new TimeSpan((long)600000000 * 30));
                }
            }
            return user;
        }

        /// <summary>
        /// 从Token获取登陆账户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static User GetUserLoginByToken(string token)
        {
            using (WLDbContext db = new WLDbContext())
            {
                var account = db.User.Single(a => a.Token == token.Trim());
                if (account != null)
                {
                    SetTicket(account.Token);
                    return account;
                }
                return null;
            }
        }

        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        public static bool CheckLogin()
        {
            return CurrentUser() != null;
        }
    }
}
