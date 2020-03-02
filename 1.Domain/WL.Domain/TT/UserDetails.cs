using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// UserDetails
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// id
        /// </summary>  
        public int id { get; set; }
		/// <summary>
        /// Msn
        /// </summary>  
        public string Msn { get; set; }
		/// <summary>
        /// 电话
        /// </summary>  
        public string Tel { get; set; }
		/// <summary>
        /// 生日
        /// </summary>  
        public DateTime? Birthday { get; set; }
		/// <summary>
        /// 情感状况
        /// </summary>  
        public int? Emotional { get; set; }
		/// <summary>
        /// 兴趣
        /// </summary>  
        public string Interest { get; set; }
		/// <summary>
        /// 个人简介
        /// </summary>  
        public string Describe { get; set; }
		/// <summary>
        /// 个人网站
        /// </summary>  
        public string Website { get; set; }
		/// <summary>
        /// 省、自治区
        /// </summary>  
        public string province { get; set; }
		/// <summary>
        /// 市
        /// </summary>  
        public string city { get; set; }
		/// <summary>
        /// 县区
        /// </summary>  
        public string district { get; set; }
		/// <summary>
        /// address
        /// </summary>  
        public string address { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public UserDetails()
        {
        }

    }    
}
