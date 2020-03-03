using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WL.Api.Enum;
using WL.Api.Manager;
using WL.API.Controllers;
using WL.API.Manager;
using WL.API.Models;
using WL.Domain;
using WL.Domain.Api;
using WL.Infrastructure.Caching;
using WL.Infrastructure.Common;
using WL.Infrastructure.Email;

namespace WL.Api.Home.Controllers
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    [RoutePrefix("v1/data")]
    public class DataController : ApiBaseController
    {
        #region 验证码
        /// <summary>
        /// 生成验证码图片并返回图片
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("ShowValidateCode"), HttpPost]
        public ResponseData<string> ShowValidateCode(string CodeKey)
        {
            return InvokeFunc(() =>
            {
                ValidateCode ValidateCode = new ValidateCode();
                //生成验证码，传几就是几位验证码
                string code = ValidateCode.CreateValidateCode(4);
                //保存验证码
                CacheManager.Current.Set<string>(CommentConfig.ImageCacheCode + CodeKey,code);
                //把验证码转成字节
                byte[] buffer = ValidateCode.CreateValidateGraphic(code);
                //把验证码转成Base64
                return $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
            });
        }

        /// <summary>
        /// 生成邮箱验证码并返回
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("EmaliGetCode"), HttpPost]
        public ResponseData<string> EmaliGetCode(string CodeKey, string Emali)
        {
            return InvokeFunc(() =>
            {
                using (WLDbContext db = new WLDbContext())
                {
                    ValidateCode ValidateCode = new ValidateCode();
                    Code code = db.Code.FirstOrDefault(x => x.number == Emali);
                    if (code == null) {
                        code = new Code();
                        code.number = Emali;
                    }
                    if (LoginManager.CodeNumber(code)) throw new AppException("发送超出次数");
                    code.code = ValidateCode.CreateValidateCode(6);//生成验证码，传几就是几位验证码
                    //删除冗余验证码
                    DateTime dt = DateTime.Now.Date;
                    if (db.Code.Where(x => x.time < dt && x.type == (int)CodeType.Emali).Any())
                    {
                        List<Code> list = db.Code.Where(x => x.time < dt && x.type == (int)CodeType.Emali).ToList();
                        list.ForEach(x => db.Code.Remove(x));
                    }
                    if (!Mail.MailSending(Emali, "宇宙物流验证码", $"您在宇宙物流的验证码是:{code.code},10分钟内有效")) throw new AppException("发送超出次数");
                    //保存验证码
                    CacheManager.Current.Set<string>(CommentConfig.MailCacheCode + CodeKey, code.code,new TimeSpan(600000000));
                    db.SaveChanges();
                    return "验证码发送成功";
                }
            });
        }
        #endregion

        #region 注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("UserRegister"), HttpPost]
        public ResponseData<bool> UserRegister([FromBody]RegisterRequest request)
        {
            return InvokeFunc(() =>
            {
                using (WLDbContext db = new WLDbContext())
                {
#if DEBUG
#else         
                    string code = CacheManager.Current.Get<string>(CommentConfig.MailCacheCode + request.CodeKey);
                    if(string.IsNullOrWhiteSpace(code)) throw new AppException("验证码错误或已过期");
                    if (request.Code != code) throw new AppException("验证码错误或已过期");
#endif
                    if (db.User.Where(x => x.Email == request.Email).Any()) throw new AppException("该邮箱已经注册过");
                    if (db.User.Where(x => x.UserName == request.UserName).Any()) throw new AppException("该用户名已经注册过");
                    if (string.IsNullOrWhiteSpace(request.PassWord)) throw new AppException("请输入密码");
                    if (string.IsNullOrWhiteSpace(request.ConfirmPassWord)) throw new AppException("请输入确认密码");
                    if (string.IsNullOrWhiteSpace(request.ConfirmPassWord)) throw new AppException("请输入确认密码");
                    if (request.PassWord != request.ConfirmPassWord) throw new AppException("密码和确认密码不一致");

                    User user = new User();
                    user.UserName = request.UserName;
                    user.PassWord = request.PassWord;
                    user.CreateTime = DateTime.Now;
                    user.LoginTime = DateTime.Now;
                    user.IP = Common.IPAddress;
                    user.Portrait = "/Image/user.png";
                    user.Email = request.Email;
                    user.Money = 0;
                    db.User.Add(user);
                    db.SaveChanges();
                    return true;
                }
            });
        }
#endregion

#region 登陆
        /// <summary>
        /// 用户登陆获取Token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetLogin"), HttpPost]
        public ResponseData<string> UserLogin([FromBody]LoginRequest request)
        {
            return InvokeFunc(() =>
            {
                //读取验证码
#if DEBUG

#else
                var code = CacheManager.Current.Get<string>(CommentConfig.ImageCacheCode + request.CodeKey);
                if (string.IsNullOrWhiteSpace(code)) throw new AppException("图片验证码不正确");
                if (string.IsNullOrWhiteSpace(request.Code)) throw new AppException("图片验证码不能为空");
                if (request.Code != code) throw new AppException("图片验证码不正确");
#endif
                User user = UserLoginHelper.GetUserLoginBy(request.UserName, request.PassWord);
                if(user == null) throw new AppException("账户密码错误");
                var result = user.Token;
                return result;
            });
        }

        /// <summary>
        /// 判断是否登陆
        /// </summary>
        /// <returns></returns>
        [Route("CheckLogin"), HttpGet]
        public ResponseData<bool> CheckLogin()
        {
            return InvokeFunc(() =>
            {
                return UserLoginHelper.CheckLogin();
            });
        }
#endregion
    }
}
