using WL.Cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Infrastructure.Data;

namespace WL.Cms.Manager
{
    public class LoggerManager
    {
        #region 日志管理
        /// <summary>
        /// 查询日志
        /// </summary>
        /// <returns></returns>
        public static List<LoggerModels> GetLoggerModelsList(string action,string userName,string st,string et)
        {
            string sql = "Select * from Cms_Logger where Time between @st and @et";
            if(action != "-1")
            {
                sql += " and Action=@Action";
            }
            if (userName != "-1")
            {
                sql += " and UserName=@UserName";
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@st", Convert.ToDateTime(st + " 00:00:00"));
            param.Add("@et", Convert.ToDateTime(et + " 23:59:59"));
            if (action != "-1")
            {
                param.Add("@Action", action);
            }
            if (userName != "-1")
            {
                param.Add("@UserName", userName);
            }
            return new BaseDAL().GetList<LoggerModels>(sql, param);
        }
        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="sqlpar"></param>
        /// <returns></returns>
        public static LoggerModels GetLoggerModels(string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ID", id);
            string sql = "Select * from Cms_Logger where ID=@ID";
            return new BaseDAL().Single<LoggerModels>(sql, param);
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddLoggerModels(LoggerModels temp)
        {
            #region sql
            StringBuilder sb = new StringBuilder("Insert into Cms_Logger (");
            sb.Append("[View],Action,Description,Remark,UserName,Time,IP)");
            sb.Append(" values (");
            sb.Append("@View,@Action,@Description,@Remark,@UserName,@Time,@IP)");
            #endregion

            #region param
            DynamicParameters param = new DynamicParameters();
            param.Add("@View", temp.View);
            param.Add("@Action", temp.Action);
            param.Add("@Description", temp.Description);
            param.Add("@Remark", temp.Remark);
            param.Add("@UserName", temp.UserName);
            param.Add("@Time", DateTime.Now);
            param.Add("@IP", temp.IP);
            #endregion

            return new BaseDAL().Add(sb.ToString(), param);
        }
        /// <summary>
        /// 查询日志中的所有动作
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLoggerAction()
        {
            string sql = "Select Action from Cms_Logger group by Action";
            return new BaseDAL().GetList<string>(sql, null);
        }
        #endregion
    }
}
