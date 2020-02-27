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
    /// Hot
    /// </summary>
    public class Hot
    {
        /// <summary>
        /// id
        /// </summary>  
        [DisplayName("id")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// Image
        /// </summary>  
        [DisplayName("Image")]
        [MaxLength(200,ErrorMessage="Image最大长度为200")]
        		        public string Image { get; set; }
		/// <summary>
        /// Sid
        /// </summary>  
        [DisplayName("Sid")]
        		        public int? Sid { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Hot()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Hot> Hot { get; set; }
    }    
}
