using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Cms_Article
    /// </summary>
    public class Cms_Article
    {
        /// <summary>
        /// id
        /// </summary>  
        public int id { get; set; }
		/// <summary>
        /// catid
        /// </summary>  
        public int? catid { get; set; }
		/// <summary>
        /// userid
        /// </summary>  
        public int? userid { get; set; }
		/// <summary>
        /// username
        /// </summary>  
        public string username { get; set; }
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
        /// content
        /// </summary>  
        public string content { get; set; }
		/// <summary>
        /// thumb
        /// </summary>  
        public string thumb { get; set; }
		/// <summary>
        /// listorder
        /// </summary>  
        public int? listorder { get; set; }
		/// <summary>
        /// url
        /// </summary>  
        public string url { get; set; }
		/// <summary>
        /// hits
        /// </summary>  
        public int? hits { get; set; }
		/// <summary>
        /// createtime
        /// </summary>  
        public int? createtime { get; set; }
		/// <summary>
        /// updatetime
        /// </summary>  
        public int? updatetime { get; set; }
		/// <summary>
        /// lang
        /// </summary>  
        public int? lang { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Article()
        {
        }

    }    
}
