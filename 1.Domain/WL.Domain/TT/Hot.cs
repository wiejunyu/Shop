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
    /// 商品热门表
    /// </summary>
    public class Hot
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// 热门图片
        /// </summary>  
        [DisplayName("热门图片")]
        [MaxLength(200,ErrorMessage="热门图片最大长度为200")]
        		        public string Image { get; set; }
		/// <summary>
        /// 商品ID
        /// </summary>  
        [DisplayName("商品ID")]
        		        public int Sid { get; set; }
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
