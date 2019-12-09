using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Home.Models;
using WL.Infrastructure.Data;

namespace WL.Home.Manager
{
    public class LoginManager
    {
        /// <summary>
        /// 判断验证码是否达到6次，没到true，达到false
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CodeNumber(CodeModels code)
        {
            if (CodeDay(code))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@number", code.number);
                string sql = "select frequency from Code where number = @number and type = 0";
                int number = new BaseDAL().Single<int>(sql, param);
                if (number > 6)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// 判断当天是否有验证码，有false，无true
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CodeDay(CodeModels code)
        {
            DateTime dt = Convert.ToDateTime(code.time);
            dt = dt.AddDays(-1);
            DynamicParameters param = new DynamicParameters();
            param.Add("@number", code.number);
            param.Add("@time", dt.ToString());
            string sql = "select * from Code where number = @number and [time] > @time and type = 0";
            CodeModels cd = new BaseDAL().Single<CodeModels>(sql, param);
            if (cd != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 写入验证码
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool SetCode(CodeModels code)
        {
            DateTime dt = Convert.ToDateTime(code.time);
            dt = dt.AddDays(-1);
            DynamicParameters param = new DynamicParameters();
            param.Add("@number", code.number);
            param.Add("@time", dt.ToString());
            string sql = "select * from Code where time < @time and type = 0";
            CodeModels cd = new BaseDAL().Single<CodeModels>(sql, param);
            if (cd != null)
            {
                param = new DynamicParameters();
                param.Add("@number", code.number);
                param.Add("@time", dt.ToString());
                sql = "delete Code where time < @time and type = 0";
                new BaseDAL().Delete(sql, param);
            }
            param = new DynamicParameters();
            param.Add("@code", code.code);
            param.Add("@type", code.type);
            param.Add("@number", code.number);
            param.Add("@time", code.time.ToString(""));
            param.Add("@frequency", code.frequency);
            sql = "INSERT INTO Code VALUES (@type,@number,@code,@time,@frequency)";
            return new BaseDAL().Add(sql, param);
        }

        /// <summary>
        /// 更新验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool UpdateCode(CodeModels code)
        {
            DateTime dt = Convert.ToDateTime(code.time);
            dt = dt.AddDays(-1);
            DynamicParameters param = new DynamicParameters();
            param.Add("@number", code.number);
            param.Add("@time", dt.ToString());
            string sql = "select * from Code where time < @time and type = 0";
            CodeModels cd = new BaseDAL().Single<CodeModels>(sql, param);
            if (cd != null)
            {
                param = new DynamicParameters();
                param.Add("@number", code.number);
                param.Add("@time", dt.ToString());
                sql = "delete Code where time < @time and type = 0";
                new BaseDAL().Delete(sql, param);
            }
            param = new DynamicParameters();
            param.Add("@code", code.code);
            param.Add("@type", code.type);
            param.Add("@number", code.number);
            param.Add("@time", code.time.ToString(""));
            sql = "UPDATE Code SET code = @code,time = @time,frequency = frequency + 1 WHERE type = @type and number = @number";
            return new BaseDAL().Update(sql, param);
        }
    }
}
