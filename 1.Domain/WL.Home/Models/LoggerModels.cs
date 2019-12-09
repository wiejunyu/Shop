using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class LoggerModels
    {

        public int ID { get; set; }
        public string View { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string UserName { get; set; }
        public string Time { get; set; }
        public string IP { get; set; }

        public LoggerModels()
        {
            ID = 0;
            View = "";
            Action = "";
            Description = "";
            Remark = "";
            UserName = "";
            Time = "";
            IP = "";
        }
    }
}
