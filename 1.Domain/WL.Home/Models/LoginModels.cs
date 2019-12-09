using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Home.Models
{
    public class LoginModels
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Checked { get; set; }
        public string Vdcode { get; set; }
        public LoginModels()
        {
            UserName = "";
            PassWord = "";
            Checked = "";
            Vdcode = "";
        }
    }
}
