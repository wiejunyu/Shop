using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Api.Enum;
using WL.Domain;
using WL.Infrastructure.Data;

namespace WL.Api.Manager
{
    public class LoginManager
    {
        /// <summary>
        /// 判断验证码是否达到6次，没到false，达到true
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CodeNumber(Code code)
        {
            using (WLDbContext db = new WLDbContext())
            {
                if (CodeDay(code))
                {
                    int number = db.Code.FirstOrDefault(x => x.number == code.number && x.type == (int)CodeType.Emali).frequency;
                    if (number > 6)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// 判断当天是否有验证码，有true，无false
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool CodeDay(Code code)
        {
            using (WLDbContext db = new WLDbContext())
            {
                DateTime dt = DateTime.Now.Date;
                if (db.Code.Where(x => x.number == code.number && x.time > dt && x.type == (int)CodeType.Emali).Any())
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 写入验证码
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool SetCode(Code code)
        {
            DateTime dt = Convert.ToDateTime(code.time);
            dt = dt.AddDays(-1);
            DynamicParameters param = new DynamicParameters();
            param.Add("@number", code.number);
            param.Add("@time", dt.ToString());
            string sql = "select * from Code where time < @time and type = 0";
            Code cd = new BaseDAL().Single<Code>(sql, param);
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
        public static bool UpdateCode(Code code)
        {
            using (WLDbContext db = new WLDbContext())
            {
                DateTime dt = code.time.AddDays(-1);
                DynamicParameters param = new DynamicParameters();
                param.Add("@number", code.number);
                param.Add("@time", dt.ToString());
                string sql = "select * from Code where time < @time and type = 0";
                Code cd = new BaseDAL().Single<Code>(sql, param);
                if (db.Code.Where(x => x.time < dt && x.type == (int)CodeType.Emali).Any())
                {
                    Code temp = db.Code.FirstOrDefault(x => x.time < dt && x.type == (int)CodeType.Emali);
                    db.Code.Remove(temp);
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
}
