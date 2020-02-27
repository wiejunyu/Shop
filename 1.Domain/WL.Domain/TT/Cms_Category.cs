using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_Category
    /// </summary>
    public class Cms_Category
    {
        /// <summary>
        /// id
        /// </summary>  
        public int id { get; set; }
		/// <summary>
        /// catname
        /// </summary>  
        public string catname { get; set; }
		/// <summary>
        /// parentid
        /// </summary>  
        public int? parentid { get; set; }
		/// <summary>
        /// moduleid
        /// </summary>  
        public int? moduleid { get; set; }
		/// <summary>
        /// title
        /// </summary>  
        public string title { get; set; }
		/// <summary>
        /// keywords
        /// </summary>  
        public string keywords { get; set; }
		/// <summary>
        /// description
        /// </summary>  
        public string description { get; set; }
		/// <summary>
        /// listorder
        /// </summary>  
        public int? listorder { get; set; }
		/// <summary>
        /// hits
        /// </summary>  
        public int? hits { get; set; }
		/// <summary>
        /// image
        /// </summary>  
        public string image { get; set; }
		/// <summary>
        /// url
        /// </summary>  
        public string url { get; set; }
		/// <summary>
        /// lang
        /// </summary>  
        public int? lang { get; set; }
		/// <summary>
        /// catdir
        /// </summary>  
        public string catdir { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Category()
        {
        }

    }    
}
