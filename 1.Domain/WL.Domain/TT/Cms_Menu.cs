using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_Menu
    /// </summary>
    public class Cms_Menu
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
        /// Url
        /// </summary>  
        public string Url { get; set; }
		/// <summary>
        /// Action
        /// </summary>  
        public string Action { get; set; }
		/// <summary>
        /// Sort
        /// </summary>  
        public int? Sort { get; set; }
		/// <summary>
        /// Lv
        /// </summary>  
        public int? Lv { get; set; }
		/// <summary>
        /// Icon
        /// </summary>  
        public string Icon { get; set; }
		/// <summary>
        /// Pid
        /// </summary>  
        public int? Pid { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Menu()
        {
        }

    }    
}
