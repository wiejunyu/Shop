using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_UserInfo
    /// </summary>
    public class Cms_UserInfo
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
        /// Permission
        /// </summary>  
        public int? Permission { get; set; }
		/// <summary>
        /// Leader
        /// </summary>  
        public int? Leader { get; set; }
		/// <summary>
        /// Remark
        /// </summary>  
        public string Remark { get; set; }
		/// <summary>
        /// CreateTime
        /// </summary>  
        public DateTime? CreateTime { get; set; }
		/// <summary>
        /// LoginTime
        /// </summary>  
        public DateTime? LoginTime { get; set; }
		/// <summary>
        /// IP
        /// </summary>  
        public string IP { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_UserInfo()
        {
        }

    }    
}
