using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class SysconfigModels
    {
        //id
        public int Id { get; set; }
        //地址
        public string Address { get; set; }
        //电话
        public string Tel { get; set; }
        //备案号
        public string Record { get; set; }
        //标题
        public string Title { get; set; }
        //描述
        public string Description { get; set; }
        //关键字
        public string Keywords { get; set; }
        //Facebook
        public string Facebook { get; set; }
        //网站图标
        public string Icon { get; set; }
        //二维码
        public string QR_code { get; set; }

        public SysconfigModels()
        {
            Id = 0;
            Address = "";
            Tel = "";
            Record = "";
            Title = "";
            Description = "";
            Keywords = "";
            Facebook = "";
            Icon = "";
            QR_code = "";
        }
    }
}
