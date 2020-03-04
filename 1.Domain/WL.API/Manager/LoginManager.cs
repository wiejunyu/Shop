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
    /// <summary>
    /// 用户登陆管理
    /// </summary>
    public class LoginManager
    {
        /// <summary>
        /// 删除冗余验证码
        /// </summary>
        public static void DelCode()
        {
            using (WLDbContext db = new WLDbContext())
            {
                DateTime dt = DateTime.Now.Date;
                if (db.Code.Where(x => x.time < dt).Any())
                {
                    List<Code> list = db.Code.Where(x => x.time < dt).ToList();
                    list.ForEach(x => db.Code.Remove(x));
                    db.SaveChanges();
                }
            }
        }
    }
}
