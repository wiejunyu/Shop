using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        [DisplayName("id")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// type
        /// </summary>  
        [DisplayName("type")]
        		        public int? type { get; set; }
		/// <summary>
        /// number
        /// </summary>  
        [DisplayName("number")]
        [MaxLength(50,ErrorMessage="number最大长度为50")]
        		        public string number { get; set; }
		/// <summary>
        /// code
        /// </summary>  
        [DisplayName("code")]
        [MaxLength(20,ErrorMessage="code最大长度为20")]
        		        public string code { get; set; }
		/// <summary>
        /// time
        /// </summary>  
        [DisplayName("time")]
        		        public DateTime? time { get; set; }
		/// <summary>
        /// frequency
        /// </summary>  
        [DisplayName("frequency")]
        		        public int? frequency { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Code()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Code> Code { get; set; }
    }    
}
