using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Cms.Models
{
    public class Article
    {
        public int id { get; set; }
        public int catid { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public string title { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string thumb { get; set; }
        public int listorder { get; set; }
        public string url { get; set; }
        public int hits { get; set; }
        public int createtime { get; set; }
        public int updatetime { get; set; }
        public int lang { get; set; }

        public Article()
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

    public class ArticleCutting
    {
        public int id { get; set; }
        public int catid { get; set; }
        public string username { get; set; }
        public string title { get; set; }
        public int hits { get; set; }
        public int createtime { get; set; }

        public ArticleCutting()
        {
            id = 0;
            catid = 0;
            username = "";
            title = "";
            hits = 0;
            createtime = 0;
        }
    }
}
