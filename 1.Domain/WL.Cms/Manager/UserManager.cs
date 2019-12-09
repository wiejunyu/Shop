using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Cms.Models;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class UserManager
    {
        #region 用户
        /// <summary>
        /// 查询用户;
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string login)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", login);
            string sql = "Select * from CMS_UserInfo where UserName=@UserName";
            return new BaseDAL().Single<UserInfo>(sql, param);
        }
        /// <summary>
        /// 更新账号信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateUser(UserInfo user)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Update CMS_UserInfo set UserName=@UserName,");
            if (!string.IsNullOrEmpty(user.PassWord))
                sb.Append("PassWord=@PassWord,");
            sb.Append("Permission=@Permission,Leader=@Leader,Remark=@Remark,CreateTime=@CreateTime,LoginTime=@LoginTime,IP=@IP");
            sb.Append(" where UserName=@UserName");

            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", user.UserName);
            if (!string.IsNullOrEmpty(user.PassWord))
                param.Add("@PassWord", user.PassWord);
            param.Add("@Permission", user.Permission);
            param.Add("@Leader", user.Leader);
            param.Add("@Remark", user.Remark);

            param.Add("@CreateTime", Convert.ToDateTime(user.CreateTime));
            param.Add("@LoginTime", Convert.ToDateTime(user.LoginTime));
            param.Add("@IP", user.IP);
            #endregion

            return new BaseDAL().Update(sb.ToString(), param);
        }
        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddUser(UserInfo user)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Insert into CMS_UserInfo (");
            sb.Append("UserName,PassWord,Permission,Leader,Remark,CreateTime,LoginTime,IP)");
            sb.Append(" values (");
            sb.Append("@UserName,@PassWord,@Permission,@Leader,@Remark,@CreateTime,@LoginTime,@IP)");
            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", user.UserName);
            param.Add("@PassWord", user.PassWord);
            param.Add("@Permission", user.Permission);
            param.Add("@Leader", user.Leader);
            param.Add("@Remark", user.Remark);
            param.Add("@CreateTime", user.CreateTime);
            param.Add("@LoginTime", user.LoginTime);
            param.Add("@IP", user.IP);
            #endregion

            return new BaseDAL().Add(sb.ToString(), param);
        }
        /// <summary>
        /// 删除用户;
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static bool DeleteUser(string login)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserName", login);
            string sql = "Delete CMS_UserInfo where UserName=@UserName";

            return new BaseDAL().Delete(sql, param);
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetUserList()
        {
            string sql = "Select * from CMS_UserInfo";
            List<UserInfo> list = new BaseDAL().GetList<UserInfo>(sql, null);
            return list;
        }
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetUserListByPermission(string permission)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Permission", permission);
            string sql = "Select * from CMS_UserInfo where Permission=@Permission";
            List<UserInfo> list = new BaseDAL().GetList<UserInfo>(sql, param);
            return list;
        }

        #endregion
    }
}
