using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class CategoryModels
    {
        //id
        public int id { get; set; }
        //栏目名称
        public string Name { get; set; }
        //栏目等级
        public int Rank { get; set; }
        //栏目图
        public string Picture { get; set; }
        //Url
        public string Url { get; set; }
        //父栏目ID
        public int Parentid { get; set; }

        public CategoryModels()
        {
            id = 0;
            Name = "";
            Rank = 0;
            Picture = "";
            Url = "";
            Parentid = 0;
        }
    }
}
