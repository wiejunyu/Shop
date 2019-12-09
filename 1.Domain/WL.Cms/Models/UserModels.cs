using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Cms.Models
{
    public class UserInfo
    {
        //ID
        public int ID { get; set; }
        //用户名
        public string UserName { get; set; }
        //密码
        public string PassWord { get; set; }
        //权限
        public string Permission { get; set; }
        //组长
        public int Leader { get; set; }
        //备注
        public string Remark { get; set; }
        //创建时间
        public string CreateTime { get; set; }
        //登陆时间
        public string LoginTime { get; set; }
        //登陆IP
        public string IP { get; set; }

        public UserInfo()
        {
            ID = 0;
            UserName = "";
            PassWord = "";
            Permission = "-1";
            Leader = -1;
            Remark = "";
            CreateTime = "";
            LoginTime = "";
            IP = "";
        }
    }


}
