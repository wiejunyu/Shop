using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_RoleMenu
    /// </summary>
    public class Cms_RoleMenu
    {
        /// <summary>
        /// ID
        /// </summary>  
        public int ID { get; set; }
		/// <summary>
        /// MenuID
        /// </summary>  
        public int? MenuID { get; set; }
		/// <summary>
        /// RoleID
        /// </summary>  
        public int? RoleID { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_RoleMenu()
        {
        }

    }    
}
