using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// id
        /// </summary>  
        public int id { get; set; }
		/// <summary>
        /// 栏目名称
        /// </summary>  
        public string Name { get; set; }
		/// <summary>
        /// 栏目等级
        /// </summary>  
        public int? Rank { get; set; }
		/// <summary>
        /// 栏目图片，空为无图栏目
        /// </summary>  
        public string Picture { get; set; }
		/// <summary>
        /// Url
        /// </summary>  
        public string Url { get; set; }
		/// <summary>
        /// 父栏目ID
        /// </summary>  
        public int? Parentid { get; set; }
		/// <summary>
        /// 推荐
        /// </summary>  
        public bool? Recommend { get; set; }
		/// <summary>
        /// 点击次数
        /// </summary>  
        public int? Click { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Category()
        {
        }

    }    
}
