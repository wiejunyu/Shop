using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_Logger
    /// </summary>
    public class Cms_Logger
    {
        /// <summary>
        /// ID
        /// </summary>  
        public int ID { get; set; }
		/// <summary>
        /// View
        /// </summary>  
        public string View { get; set; }
		/// <summary>
        /// Action
        /// </summary>  
        public string Action { get; set; }
		/// <summary>
        /// Description
        /// </summary>  
        public string Description { get; set; }
		/// <summary>
        /// Remark
        /// </summary>  
        public string Remark { get; set; }
		/// <summary>
        /// UserName
        /// </summary>  
        public string UserName { get; set; }
		/// <summary>
        /// Time
        /// </summary>  
        public DateTime? Time { get; set; }
		/// <summary>
        /// IP
        /// </summary>  
        public string IP { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Logger()
        {
        }

    }    
}
