using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Cms.Models
{
    public class Columu
    {
        public int ID { get; set; }
        public string catname { get; set; }
        public int parentid { get; set; }
        public int moduleid { get; set; }
        public string title { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public int listorder { get; set; }
        public int hits { get; set; }
        public string image { get; set; }
        public string url { get; set; }
        public int lang { get; set; }
        public string catdir { get; set; }

        public Columu()
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
