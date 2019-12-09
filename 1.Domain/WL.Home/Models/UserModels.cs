using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class UserModels
    {
        //id
        public int ID { get; set; }
        //用户名
        public string UserName { get; set; }
        //用户密码
        public string PassWord { get; set; }
        //添加时间
        public string CreateTime { get; set; }
        //最后登陆时间
        public string LoginTime { get; set; }
        //ip
        public string IP { get; set; }
        //备注
        public string Remark { get; set; }
        //权限
        public int Permission { get; set; }
        //头像
        public string Portrait { get; set; }
        //电子邮箱
        public string Email { get; set; }
        //手机
        public string Phone { get; set; }
        //QQ
        public string QQ { get; set; }
        //余额
        public decimal Money { get; set; }

        public UserModels()
        {
            ID = 0;
            UserName = "";
            PassWord = "";
            CreateTime = "";
            LoginTime = "";
            IP = "";
            Remark = "";
            Permission = 0;
            Portrait = "";
            Email = "";
            Phone = "";
            QQ = "";
            Money = 0;
        }
    }

    public class UserDetailsModels
    {
        //Msn
        public string Msn { get; set; }
        //电话
        public string Tel { get; set; }
        //生日
        public string Birthday { get; set; }
        //情感状况
        public int Emotional { get; set; }
        //兴趣
        public string Interest { get; set; }
        //个人简介
        public string Describe { get; set; }
        //个人网站
        public string Website { get; set; }
        //省、自治区
        public string province { get; set; }
        //市
        public string city { get; set; }
        //县区
        public string district { get; set; }

        public UserDetailsModels()
        {
            Msn = "";
            Tel = "";
            Birthday = "";
            Emotional = 0;
            Interest = "";
            Describe = "";
            Website = "";
            province = "";
            city = "";
            district = "";
        }
    }
}
