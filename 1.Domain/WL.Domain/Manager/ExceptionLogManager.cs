using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WL.Domain;
using WL.Domain.Enum;

namespace WL.Cms.Manager
{
    public class ExceptionLogManager
    {
        const long timeout = 3000;
        public static void TimeOutLog(string url, string request, string response, long time)
        {
            if (time > timeout)
            {
                try
                {
                    using (WLDbContext db = new WLDbContext())
                    {
                        ExceptionLog exceptionLog = new ExceptionLog();
                        exceptionLog.Exception_Type = (int)ExceptionTypeEnum.TimeOut;
                        exceptionLog.Url = url;
                        exceptionLog.Remark = time.ToString();
                        exceptionLog.Request = request;
                        exceptionLog.Response = response;
                        exceptionLog.CreateTime = DateTime.Now;
                        db.ExceptionLog.Add(exceptionLog);
                        db.SaveChanges();
                    }
                }
                catch (Exception xe)
                {
                    throw;
                }
            }
        }
    }
}
