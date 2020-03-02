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
using WL.Infrastructure.File;
using WL.Web.Home.Filters;

namespace WL.Web.Home.Controllers
{
    [RoutePrefix("Data")]
    [Route("{action}")]
    public class DataController : BaseController
    {
        //验证
        #region
        /// <summary>
        /// 生成验证码图片并返回图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowValidateCode()
        {
            ValidateCode ValidateCode = new ValidateCode();
            string code = ValidateCode.CreateValidateCode(4);//生成验证码，传几就是几位验证码
            System.Web.HttpContext.Current.Session["code"] = code;
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);//把验证码画到画布
            return File(buffer, "image/jpeg");
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
        #endregion

        //登陆注册
        #region
        /// <summary>
        /// 判断是否登陆
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLogin()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user == null)
            {
                return Json("false");
            }
            else
            {
                return Json("true");
            }
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
                            HttpCookie cookie = new HttpCookie("usercookie_wl");
                            cookie.Values.Add("userName", user.UserName);
                            cookie.Values.Add("password", user.PassWord);
                            cookie.Expires = DateTime.Now.AddDays(7);
                            Response.AppendCookie(cookie);
                        }
                        string url = "/index.html";
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
                            string url = "/index.html";
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
            jsond.Data = new { error = false, message = "用户注册成功", link = "/index.html", };
            return jsond;
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        public JsonResult NameUserExistence(string username)
        {
            return Json(UserManager.NameUserExistence(username));
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

        #endregion

        //个人中心
        #region
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Edit", Description = "头像编辑")]
        public JsonResult SetPortrait(string image)
        {
            try
            {
                UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
                string portrait = user.Portrait;
                if (UserManager.SetPortrait(ImageFile.SetImage(image)))
                {
                    if(portrait.ToLower() != "/Image/user.png".ToLower())
                        ImageFile.DeleteImage(portrait);
                    UserManager.SetSessionUser();
                    return Json("1");
                }
                else
                {
                    return Json("-1");
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        [Logger(Top = "Personal", Key = "Edit", Description = "修改密码")]
        public JsonResult SetPass(string pass)
        {
            if (UserManager.SetUserPass(pass))
            {
                HttpCookie cookie = Request.Cookies["usercookie_wl"];
                if (cookie != null)
                {
                    cookie["password"] = pass;
                }
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Edit", Description = "修改资料")]
        public JsonResult SetData(string json)
        {
            UserDetailsModels userdetails = JsonConvert.DeserializeObject<UserDetailsModels>(json);
            if (userdetails.Birthday == "请选择生日")
            {
                userdetails.Birthday = null;
            }
            if (UserManager.SetUserDetails(userdetails))
            {
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 写入收货地址
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Add", Description = "添加收货地址")]
        public JsonResult SetAddress(string json)
        {
            AddressModels Address = JsonConvert.DeserializeObject<AddressModels>(json);
            if (AddressManager.SetAddress(Address))
            {
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Add", Description = "删除收货地址")]
        public JsonResult DelAddress(int date)
        {
            if (AddressManager.DelAddress(date))
            {
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 更新收货地址
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Add", Description = "更新收货地址")]
        public JsonResult EditAddress(string json)
        {
            AddressModels Address = JsonConvert.DeserializeObject<AddressModels>(json);
            if (AddressManager.EditAddress(Address))
            {
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 更新收货地址
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Add", Description = "更新收货地址")]
        public JsonResult Choice(int date)
        {
            if (AddressManager.Choice(date))
            {
                return Json("1");
            }
            return Json("-1");
        }

        /// <summary>
        /// 批量删除订单
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [Logger(Top = "Portrait", Key = "Del", Description = "批量删除订单")]
        public JsonResult DelOrderList(string json)
        {
            if (json != "" && json != null)
            {
                List<string> Order = JsonConvert.DeserializeObject<List<string>>(json);
                foreach (string temp in Order)
                {
                    HomeManager.DelOrder(temp);
                }
            }
            else
            {
                return Json("0");
            }
            return Json("1");
        }
        #endregion

        //商城
        #region
        //写入单个订单
        public JsonResult SetOrder(int Sid, int Number, decimal Money, string Address, int Invoice, string Remarks, int Pay)
        {
            ShopModels shop = HomeManager.GetShop(Sid);

            decimal ActualMoney = (System.Decimal.Round(shop.Trueprice, 2) * Convert.ToDecimal(Number)) + 0;
            JsonResult jsond = new JsonResult();
            if (ActualMoney != Money)
            {
                jsond.Data = new { error = true, message = "商品信息已经更新，请从新刷新页面", link = "", };
                return jsond;
            }
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user == null)
            {
                jsond.Data = new { error = true, message = "0", link = "", };
                return jsond;
            }
            OrderModels order = new OrderModels();
            order.Order = Common.GetOrderNo();
            order.UserId = user.ID;
            order.Sid = Sid;
            order.Number = Number;
            order.Freight = 80;
            order.Money = Money;
            order.Address = Address;
            order.Invoice = Invoice;
            order.Remarks = Remarks;
            order.PayMethod = Pay;
            order.ShoppingCart = false;
            order.MultipleSid = "";
            if (order.PayMethod == 0)
            {
                order.PayState = true;
            }
            jsond.Data = new { error = true, message = "添加订单成功", link = "/shop/pay.html?id=" + HomeManager.SetOrder(order), };
            return jsond;
        }

        //获取购物车数目
        public JsonResult GetShoppingCartCount()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user == null)
            {
                return Json("0");
            }
            return Json(HomeManager.GetShoppingCart().Sum(p => p.Number).ToString());
        }

        //加入购物车
        public JsonResult SetShoppingCart(int sid, int number)
        {
            JsonResult jsond = new JsonResult();
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            if (user == null)
            {
                jsond.Data = new { error = 2, ShoppingCart = "" };
                return jsond;
            }
            ShoppingCartModels sc = new ShoppingCartModels();
            sc.ShopId = sid;
            sc.UserId = user.ID;
            sc.Number = number;
            sc.Date = DateTime.Now;
            if (HomeManager.SetShoppingCart(sc))
            {
                HomeManager.DelShoppingCart();
                jsond.Data = new { error = 1, ShoppingCart = HomeManager.GetShoppingCart().Count };
                return jsond;
            }
            jsond.Data = new { error = 0, ShoppingCart = HomeManager.GetShoppingCart().Count };
            return jsond;
        }
        #endregion
    }
}