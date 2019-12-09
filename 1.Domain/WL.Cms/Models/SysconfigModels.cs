using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Cms.Models
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
        public SysconfigModels()
        {
            Id = 0;
            Address = "";
            Tel = "";
            Record = "";
            Title = "";
            Description = "";
            Keywords = "";
        }
    }
}
