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
    /// 购物车
    /// </summary>
        [Table("ShoppingCart")]
    public class ShoppingCart
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int Id { get; set; }
		/// <summary>
        /// 用户ID
        /// </summary>  
        [DisplayName("用户ID")]
        		        public int UserId { get; set; }
		/// <summary>
        /// 商品ID
        /// </summary>  
        [DisplayName("商品ID")]
        		        public int ShopId { get; set; }
		/// <summary>
        /// 数量
        /// </summary>  
        [DisplayName("数量")]
        		        public int Number { get; set; }
		/// <summary>
        /// 加入购物车时间
        /// </summary>  
        [DisplayName("加入购物车时间")]
        		        public DateTime Date { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public ShoppingCart()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
    }    
}
