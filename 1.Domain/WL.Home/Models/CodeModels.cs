using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class CodeModels
    {
        public int id { get; set; }
        public int type { get; set; }
        public string number { get; set; }
        public string code { get; set; }
        public DateTime time { get; set; }
        public int frequency { get; set; }
        public CodeModels()
        {
            id = 0;
            type = -1;
            number = "";
            code = "";
            time = DateTime.Now;
            frequency = 0;
        }
    }
}
