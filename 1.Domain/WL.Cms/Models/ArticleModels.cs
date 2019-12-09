using System;
using System.Collections.Generic;
using System.Text;

namespace WL.Cms.Models
{
    public class ArticleModels
    {
        //id
        public int id { get; set; }
        //栏目id
        public int catid { get; set; }
        //发布用户id
        public int userid { get; set; }
        //发布用户名
        public string username { get; set; }
        //标题
        public string title { get; set; }
        //关键字
        public string keywords { get; set; }
        //描述
        public string description { get; set; }
        //内容
        public string content { get; set; }
        //略缩图
        public string thumb { get; set; }
        //排序
        public int listorder { get; set; }
        //URL
        public string url { get; set; }
        //点击数
        public int hits { get; set; }
        //添加时间
        public int createtime { get; set; }
        //更新时间
        public int updatetime { get; set; }
        //语言
        public int lang { get; set; }

        public ArticleModels()
        {
            id = 0;
            catid = 0;
            userid = 0;
            username = "";
            title = "";
            keywords = "";
            description = "";
            content = "";
            thumb = "";
            listorder = 0;
            url = "";
            hits = 0;
            createtime = 0;
            updatetime = 0;
            lang = 0;
        }
    }

    public class ArticleCuttingModels
    {
        //id
        public int id { get; set; }
        //栏目ID
        public int catid { get; set; }
        //发布用户名
        public string username { get; set; }
        //标题
        public string title { get; set; }
        //点击次数
        public int hits { get; set; }
        //发布时间
        public int createtime { get; set; }
        //URL
        public string url { get; set; }
        //栏目名称
        public string catname { get; set; }

        public ArticleCuttingModels()
        {
            id = 0;
            catid = 0;
            username = "";
            title = "";
            hits = 0;
            createtime = 0;
            url = "";
            catname = "";
        }
    }
}