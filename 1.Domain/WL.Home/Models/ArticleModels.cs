using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class ArticleModels
    {
        //id
        public int id { get; set; }
        //栏目ID
        public int catid { get; set; }
        //发布文章用户id
        public int userid { get; set; }
        //发布文章用户名
        public string username { get; set; }
        //文章标题
        public string title { get; set; }
        //文章关键字
        public string keywords { get; set; }
        //文章描述
        public string description { get; set; }
        //文章内容
        public string content { get; set; }
        //文章缩略图
        public string thumb { get; set; }
        //文章排序
        public int listorder { get; set; }
        //文章URL链接
        public string url { get; set; }
        //文章点击次数
        public int hits { get; set; }
        //文章发布时间
        public int createtime { get; set; }
        //文章更新时间
        public int updatetime { get; set; }
        //文章语言
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
}
