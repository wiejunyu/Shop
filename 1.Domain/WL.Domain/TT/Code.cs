using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Code
    /// </summary>
    public class Code
    {
        /// <summary>
        /// id
        /// </summary>  
        public int id { get; set; }
		/// <summary>
        /// type
        /// </summary>  
        public int? type { get; set; }
		/// <summary>
        /// number
        /// </summary>  
        public string number { get; set; }
		/// <summary>
        /// code
        /// </summary>  
        public string code { get; set; }
		/// <summary>
        /// time
        /// </summary>  
        public DateTime? time { get; set; }
		/// <summary>
        /// frequency
        /// </summary>  
        public int? frequency { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Code()
        {
        }

    }    
}
