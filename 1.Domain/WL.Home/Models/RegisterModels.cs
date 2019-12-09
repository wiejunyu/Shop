using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Infrastructure.Common;

namespace WL.Home.Models
{
    public class RegisterModels
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LoginTime { get; set; }
        public string IP { get; set; }
        public string Remark { get; set; }
        public int Permission { get; set; }
        public string Portrait { get; set; }
        public string Email { get; set; }
        public string Vdcode { get; set; }
        public string Phone { get; set; }
        public RegisterModels()
        {
            UserName = "";
            PassWord = "";
            CreateTime = DateTime.Now;
            LoginTime = DateTime.Now;
            IP = Common.IPAddress; ;
            Remark = "";
            Permission = 1;
            Portrait = "/Image/user.png";
            Email = "";
            Vdcode = "";
            Phone = "";
        }
    }
}
