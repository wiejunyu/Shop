using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WL.Home.Models;
using WL.Infrastructure.Common;
using WL.Infrastructure.Data;

namespace WL.Home.Manager
{
    public class UserManager
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static UserModels GetUser(string username, string password)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@name", username);
            param.Add("@password", password);
            string sql = "select * from [User] where (Email = @name or UserName = @name) and PassWord = @password";
            return new BaseDAL().Single<UserModels>(sql, param);
        }

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
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", re.UserName);
            param.Add("@PassWord", (MD5.Md5(re.PassWord)).ToLower());
            param.Add("@CreateTime", re.CreateTime.ToString());
            param.Add("@LoginTime", re.LoginTime.ToString());
            param.Add("@IP", re.IP);
            param.Add("@Remark", re.Remark);
            param.Add("@Permission", re.Permission);
            param.Add("@Portrait", re.Portrait);
            param.Add("@Email", re.Email);
            param.Add("@Phone", re.Phone);
            param.Add("@QQ", "");
            param.Add("@Money", 0);
            param.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            string sql = "INSERT INTO [User] VALUES (@UserName,@PassWord,@CreateTime,@LoginTime,@IP,@Remark,@Permission,@Portrait,@Email,@Phone,@QQ,@Money);SELECT @ID=SCOPE_IDENTITY()";
            return new BaseDAL().AddGetID(sql, param); 
        }

        /// <summary>
        /// 插入基础信息
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        public static bool SetDetails(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@id",id);
            string sql = "INSERT INTO [UserDetails] VALUES (@id,null,null,null,null,null,null,null,null,null,null)";
            return new BaseDAL().Add(sql, param);
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
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", user.ID);
            param.Add("@Msn", userdetails.Msn);
            param.Add("@Tel", userdetails.Tel);
            param.Add("@Birthday", userdetails.Birthday);
            param.Add("@Emotional", userdetails.Emotional);
            param.Add("@Interest", userdetails.Interest);
            param.Add("@Describe", userdetails.Describe);
            param.Add("@Website", userdetails.Website);
            param.Add("@province", userdetails.province);
            param.Add("@city", userdetails.city);
            param.Add("@district", userdetails.district);
            string sql = "UPDATE [UserDetails] SET Msn = @Msn,Tel = @Tel,Birthday = @Birthday,Emotional = @Emotional,Interest = @Interest,Describe = @Describe,Website = @Website,province = @province,city = @city,district = @district WHERE ID = @Id";
            return new BaseDAL().Update(sql, param);
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <returns></returns>
        public static UserDetailsModels GetUserDetails()
        {
            UserModels user = System.Web.HttpContext.Current.Session["user"] as UserModels;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", user.ID);
            string sql = "Select * from [UserDetails] WHERE ID = @Id";
            return new BaseDAL().Single<UserDetailsModels>(sql, param);
        }
    }
}
