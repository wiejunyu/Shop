using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_Role
    /// </summary>
    public class Cms_Role
    {
        /// <summary>
        /// ID
        /// </summary>  
        public int ID { get; set; }
		/// <summary>
        /// Name
        /// </summary>  
        public string Name { get; set; }
		/// <summary>
        /// Remark
        /// </summary>  
        public string Remark { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Role()
        {
        }

    }    
}
