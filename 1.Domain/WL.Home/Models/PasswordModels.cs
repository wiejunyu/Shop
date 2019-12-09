using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class PasswordModels
    {
        public string PassWord { get; set; }
        public string NewPassWord { get; set; }
        public PasswordModels()
        {
            NewPassWord = "";
            PassWord = "";
        }
    }
}
