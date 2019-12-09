using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WL.Infrastructure.Common;

namespace WL.Home.Models
{
    public class AddressModels
    {
        //省、自治区
        public string province { get; set; }
        //市
        public string city { get; set; }
        //县区
        public string district { get; set; }
        //详细地址
        public string address { get; set; }
        //姓名
        public string name { get; set; }
        //tel
        public string tel { get; set; }
        //添加时间
        public int date { get; set; }
        //是否默认
        public bool Choice { get; set; }

        public AddressModels() {
            province = "";
            city = "";
            district = "";
            address = "";
            name = "";
            tel = "";
            date = Convert.ToInt32(Common.ConvertDateTimeInt(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            Choice = false;
        }
    }
}
