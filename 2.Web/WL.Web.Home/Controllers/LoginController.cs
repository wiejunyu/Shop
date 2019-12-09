using WL.Web.Home.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WL.Home.Manager;
using WL.Home.Models;
using WL.Infrastructure.Common;
using WL.Infrastructure.Email;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    [LoginChecker]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ShowValidateCode()
        {
            ValidateCode ValidateCode = new ValidateCode();
            string code = ValidateCode.CreateValidateCode(4);//生成验证码，传几就是几位验证码
            System.Web.HttpContext.Current.Session["code"] = code;
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);//把验证码画到画布
            return File(buffer, "image/jpeg");
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [Logger(Top = "Login", Key = "Login", Description = "用户登录")]
        public ActionResult UserLogin(string json)
        {
            LoginModels login = JsonConvert.DeserializeObject<LoginModels>(json);
            string code = System.Web.HttpContext.Current.Session["code"].ToString();
            JsonResult jsond = new JsonResult();
            if (login.Vdcode == code)
            {
                UserModels user = new UserModels();
                string pwd = MD5.Md5(login.PassWord).ToLower();
                user = UserManager.NameGetUser(login.UserName);
                if (user != null)
                {
                    if (user.PassWord == pwd)
                    {
                        string ip = Common.IPAddress;
                        user.IP = ip;
                        user.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        UserManager.UpdateUser(user);
                        Session["user"] = user;
                        if (login.Checked == "on")
                        {
                            HttpCookie cookie = new HttpCookie("usercookie_wl"); ;
                            cookie.Values.Add("userName", user.UserName);
                            cookie.Values.Add("password", user.PassWord);
                            cookie.Expires = DateTime.Now.AddDays(7);
                            Response.AppendCookie(cookie);
                        }
                        string url = "/Home/Index";
                        jsond.Data = new { error = false, message = "登陆成功", link = url, };
                        return jsond;
                    }
                    else
                    {
                        jsond.Data = new { error = true, message = "密码错误", link = "", };
                        return jsond;
                    }
                }
                else
                {
                    user = UserManager.EmailGetUser(login.UserName);
                    if (user != null)
                    {
                        if (user.PassWord == pwd)
                        {
                            string ip = Common.IPAddress;
                            user.IP = ip;
                            user.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            UserManager.UpdateUser(user);
                            Session["user"] = user;
                            if (login.Checked == "on")
                            {
                                HttpCookie cookie = new HttpCookie("usercookie_wl"); ;
                                cookie.Values.Add("userName", user.UserName);
                                cookie.Values.Add("password", user.PassWord);
                                cookie.Expires = DateTime.Now.AddDays(7);
                                Response.AppendCookie(cookie);
                            }
                            string url = "/Home/Index";
                            jsond.Data = new { error = false, message = "登陆成功", link = url, };
                            return jsond;
                        }
                        else
                        {
                            jsond.Data = new { error = true, message = "密码错误", link = "", };
                            return jsond;
                        }
                    }
                    else
                    {
                        jsond.Data = new { error = true, message = "邮箱或用户名不存在", link = "", };
                        return jsond;
                    }
                }
            }
            else
            {
                jsond.Data = new { error = true, message = "0", link = "", };
                return jsond;
            }
        }

        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        public JsonResult LogOut()
        {

            UserModels user = Session["user"] as UserModels;
            HttpCookie cookie = Request.Cookies["usercookie_wl"];
            if (cookie != null)
            {
                cookie.Expires = System.DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Session.Contents.Remove("user");
            return Json("1");
        }

        /// <summary>
        /// 发送邮箱验证码并保存
        /// </summary>
        /// <returns></returns>
        public JsonResult EmaliGetCode(string emali)
        {
            JsonResult jsond = new JsonResult();

            ValidateCode ValidateCode = new ValidateCode();
            CodeModels code = new CodeModels();
            code.code = ValidateCode.CreateValidateCode(6);//生成验证码，传几就是几位验证码
            code.type = 0;
            code.number = emali;
            if (LoginManager.CodeDay(code))
            {
                if (LoginManager.SetCode(code))
                {
                    string str = Mail.MailSending(emali, "宇宙物流验证码", "您在宇宙物流的验证码是:" + code.code);
                    if (str == "1")
                    {
                        jsond.Data = new { error = false, message = "验证码发送成功", link = "", };
                        return jsond;
                    }
                    else
                    {
                        jsond.Data = new { error = true, message = str, link = "", };
                        return jsond;
                    }
                }
                else
                {
                    jsond.Data = new { error = true, message = "写入验证码失败", link = "", };
                    return jsond;
                }
            }
            else
            {
                if (LoginManager.CodeNumber(code))
                {
                    if (LoginManager.UpdateCode(code))
                    {
                        string str = Mail.MailSending(emali, "宇宙物流验证码", "您在宇宙物流的验证码是:" + code.code);
                        if (str == "1")
                        {
                            jsond.Data = new { error = false, message = "验证码发送成功", link = "", };
                            return jsond;
                        }
                        else
                        {
                            jsond.Data = new { error = true, message = str, link = "", };
                            return jsond;
                        }
                    }
                    else
                    {
                        jsond.Data = new { error = true, message = "验证码更新失败", link = "", };
                        return jsond;
                    }
                }
                else
                {
                    jsond.Data = new { error = true, message = "验证码达最大发送次数", link = "", };
                    return jsond;
                }
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public JsonResult UserRegister(string json)
        {
            JsonResult jsond = new JsonResult();
            RegisterModels Re = JsonConvert.DeserializeObject<RegisterModels>(json);
            if (UserManager.EmailUserExistence(Re) == false)
            {
                jsond.Data = new { error = true, message = "该邮箱已经注册过", link = "", };
                return jsond;
            }
            if (UserManager.NameUserExistence(Re.UserName) == false)
            {
                jsond.Data = new { error = true, message = "该用户名已经注册过", link = "", };
                return jsond;
            }
            if (UserManager.CompareCode(Re) == false)
            {
                jsond.Data = new { error = true, message = "验证码验证失败", link = "", };
                return jsond;
            }
            int userid = 0;
            userid = UserManager.SetUser(Re);
            if (userid != 0)
            {
                UserManager.SetDetails(userid);
            }
            else
            {
                jsond.Data = new { error = true, message = "用户注册失败", link = "", };
                return jsond;
            }
            jsond.Data = new { error = false, message = "用户注册成功", link = "/Home/Index", };
            return jsond;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public JsonResult NameUserExistence(string username)
        {
            return Json(UserManager.NameUserExistence(username));
        }
    }
}