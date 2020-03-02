using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>  
        public int ID { get; set; }
		/// <summary>
        /// UserName
        /// </summary>  
        public string UserName { get; set; }
		/// <summary>
        /// PassWord
        /// </summary>  
        public string PassWord { get; set; }
		/// <summary>
        /// 创建时间
        /// </summary>  
        public DateTime? CreateTime { get; set; }
		/// <summary>
        /// 最后登陆时间
        /// </summary>  
        public DateTime? LoginTime { get; set; }
		/// <summary>
        /// IP
        /// </summary>  
        public string IP { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        public string Remark { get; set; }
		/// <summary>
        /// 权限
        /// </summary>  
        public int? Permission { get; set; }
		/// <summary>
        /// 头像
        /// </summary>  
        public string Portrait { get; set; }
		/// <summary>
        /// Email
        /// </summary>  
        public string Email { get; set; }
		/// <summary>
        /// Phone
        /// </summary>  
        public string Phone { get; set; }
		/// <summary>
        /// QQ
        /// </summary>  
        public string QQ { get; set; }
		/// <summary>
        /// Money
        /// </summary>  
        public decimal? Money { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public User()
        {
        }

    }    
}
