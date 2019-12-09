using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Cms.Models
{
    public class ColumuModels
    {
        //id
        public int ID { get; set; }
        //栏目ID
        public string catname { get; set; }
        //父栏目id
        public int parentid { get; set; }
        //模型id
        public int moduleid { get; set; }
        //标题
        public string title { get; set; }
        //关键字
        public string keywords { get; set; }
        //描述
        public string description { get; set; }
        //排序
        public int listorder { get; set; }
        //点击次数
        public int hits { get; set; }
        //缩略图
        public string image { get; set; }
        //Url
        public string url { get; set; }
        //语言
        public int lang { get; set; }
        //栏目目录
        public string catdir { get; set; }

        public ColumuModels()
        {
            ID = 0;
            catname = "";
            parentid = 0;
            moduleid = 0;
            title = "";
            keywords = "";
            description = "";
            listorder = 0;
            hits = 0;
            image = "";
            url = "";
            catdir = "";
        }
    }
}
