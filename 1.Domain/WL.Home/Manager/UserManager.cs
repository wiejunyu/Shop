using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WL.Domain;
using WL.Home.Models;
using WL.Infrastructure.Common;
using WL.Infrastructure.Data;

namespace WL.Home.Manager
{
    public class UserManager
    {
        /// <summary>
        /// 用户名获取用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static UserModels NameGetUser(string username)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@name", username);
            string sql = "select * from [User] where UserName = @name";
            return new BaseDAL().Single<UserModels>(sql, param);
        }

        /// <summary>
        /// 邮箱获取用户
        /// </summary>
        /// <param name="email">用户名</param>
        /// <returns></returns>
        public static UserModels EmailGetUser(string email)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@email", email);
            string sql = "select * from [User] where Email = @email";
            return new BaseDAL().Single<UserModels>(sql, param);
        }

        /// <summary>
        /// 获取已经登陆用户所有信息
        /// </summary>
        /// <returns></returns>
        public static UserModels GetUser()
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;
            if (user != null)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@id", user.ID);
                string sql = "select * from [User] where ID = @id";
                return new BaseDAL().Single<UserModels>(sql, param);
            }
            return new UserModels();
        }

        /// <summary>
        /// 更新账号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateUser(UserModels user)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Update [User] set UserName=@UserName,");
            if (!string.IsNullOrEmpty(user.PassWord))
                sb.Append("PassWord=@PassWord,");
            sb.Append("Permission=@Permission,Remark=@Remark,CreateTime=@CreateTime,LoginTime=@LoginTime,IP=@IP");
            sb.Append(" where UserName=@UserName");

            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", user.UserName);
            if (!string.IsNullOrEmpty(user.PassWord))
                param.Add("@PassWord", user.PassWord);
            param.Add("@Permission", user.Permission);
            param.Add("@Remark", user.Remark);

            param.Add("@CreateTime", Convert.ToDateTime(user.CreateTime));
            param.Add("@LoginTime", Convert.ToDateTime(user.LoginTime));
            param.Add("@IP", user.IP);
            #endregion

            return new BaseDAL().Update(sb.ToString(), param);
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="url">用户头像链接</param>
        /// <returns></returns>
        public static bool SetPortrait(string url)
        {
            UserModels user = HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@url", url);
            param.Add("@id", user.ID);
            string sql = "UPDATE [User] SET Portrait = @url WHERE ID = @id";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 更新账号Session信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static void SetSessionUser()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            user = UserManager.NameGetUser(user.UserName);
            System.Web.HttpContext.Current.Session["user"] = user;
        }

        /// <summary>
        /// 判断用户验证码是否正确
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static bool CompareCode(RegisterModels re)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Email", re.Email);
            string sql = "select * from Code where number = @Email";
            CodeModels cd = new BaseDAL().Single<CodeModels>(sql, param);
            if (cd == null)
            {
                return false;
            }
            if ((cd.time.AddMinutes(30)) < DateTime.Now)
            {
                return false;
            }
            if (cd.code != re.Vdcode)
            {
                return false;
            }
            param = new DynamicParameters();
            param.Add("@Email", re.Email);
            sql = "DELETE FROM Code WHERE number = @Email and [type] = 0";
            new BaseDAL().Delete(sql, param);
            return true;
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static int SetUser(RegisterModels re)
        {
            using (WLDbContext db = new WLDbContext()) {
                User user = new User();
                user.UserName = re.UserName;
                user.PassWord = (MD5.Md5(re.PassWord)).ToLower();
                user.CreateTime = re.CreateTime;
                user.LoginTime = re.LoginTime;
                user.IP = re.IP;
                user.Remark = re.Remark;
                user.Permission = re.Permission;
                user.Portrait = re.Portrait;
                user.Email = re.Email;
                user.Phone = re.Phone;
                user.Money = 0;
                db.User.Add(user);
                db.SaveChanges();
                return user.ID;
            }
        }

        /// <summary>
        /// 插入基础信息
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static bool SetDetails(int id)
        {
            using (WLDbContext db = new WLDbContext())
            {
                UserDetails user = new UserDetails();
                user.UID = id;
                db.UserDetails.Add(user);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 判断用户Email是否存在
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static bool EmailUserExistence(RegisterModels re)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Email", re.Email);
            string sql = "Select * from [User] where Email = @Email";
            if (new BaseDAL().Single<UserModels>(sql, param) == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static bool NameUserExistence(string username)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", username);
            string sql = "Select * from [User] where UserName = @UserName";
            UserModels user = new BaseDAL().Single<UserModels>(sql, param);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 写入用户密码
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static bool SetUserPass(string pass)
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@user", user.ID); 
            param.Add("@password", (MD5.Md5(pass)).ToLower());
            string sql = "UPDATE [User] SET PassWord = @password WHERE ID = @user";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        public static bool SetUserDetails(UserDetailsModels userdetails)
        {
            using (WLDbContext db = new WLDbContext())
            {
                UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
                UserDetails GetUserDetails = db.UserDetails.Single(x => x.UID == user.ID);
                GetUserDetails.Msn = userdetails.Msn;
                GetUserDetails.Tel = userdetails.Tel;
                GetUserDetails.Birthday = DateTime.Parse(userdetails.Birthday);
                GetUserDetails.Emotional = userdetails.Emotional;
                GetUserDetails.Interest = userdetails.Interest;
                GetUserDetails.Describe = userdetails.Describe;
                GetUserDetails.Website = userdetails.Website;
                GetUserDetails.province = userdetails.province;
                GetUserDetails.city = userdetails.city;
                GetUserDetails.district = userdetails.district;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <returns></returns>
        public static UserDetails GetUserDetails()
        {
            using (WLDbContext db = new WLDbContext())
            {

                UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
                return db.UserDetails.Single(x => x.UID == user.ID);
            }
        }
    }
}
