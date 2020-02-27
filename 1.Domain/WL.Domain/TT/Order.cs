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
    /// Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单ID
        /// </summary>  
        [DisplayName("订单ID")]
        		[Key]
                public int Id { get; set; }
		/// <summary>
        /// 用户ID
        /// </summary>  
        [DisplayName("用户ID")]
        		        public int? UserId { get; set; }
		/// <summary>
        /// 订单号
        /// </summary>  
        [DisplayName("订单号")]
        [MaxLength(50,ErrorMessage="订单号最大长度为50")]
        		        public string OrderNumber { get; set; }
		/// <summary>
        /// 商品ID
        /// </summary>  
        [DisplayName("商品ID")]
        		        public int? Sid { get; set; }
		/// <summary>
        /// 商品价格
        /// </summary>  
        [DisplayName("商品价格")]
        		        public decimal? Price { get; set; }
		/// <summary>
        /// 商品数量
        /// </summary>  
        [DisplayName("商品数量")]
        		        public int? Number { get; set; }
		/// <summary>
        /// 运费
        /// </summary>  
        [DisplayName("运费")]
        		        public decimal? Freight { get; set; }
		/// <summary>
        /// 应付金额
        /// </summary>  
        [DisplayName("应付金额")]
        		        public decimal? Money { get; set; }
		/// <summary>
        /// 收货地址
        /// </summary>  
        [DisplayName("收货地址")]
        [MaxLength(200,ErrorMessage="收货地址最大长度为200")]
        		        public string Address { get; set; }
		/// <summary>
        /// 发票信息
        /// </summary>  
        [DisplayName("发票信息")]
        		        public int? Invoice { get; set; }
		/// <summary>
        /// 订单生成日期
        /// </summary>  
        [DisplayName("订单生成日期")]
        		        public DateTime? Date { get; set; }
		/// <summary>
        /// 支付状态
        /// </summary>  
        [DisplayName("支付状态")]
        		        public bool? PayState { get; set; }
		/// <summary>
        /// 支付方式：0货到付款
        /// </summary>  
        [DisplayName("支付方式：0货到付款")]
        		        public int? PayMethod { get; set; }
		/// <summary>
        /// 物流状态，0未发货，1运输中，2已收货
        /// </summary>  
        [DisplayName("物流状态，0未发货，1运输中，2已收货")]
        		        public int? LogisticsState { get; set; }
		/// <summary>
        /// 物流信息
        /// </summary>  
        [DisplayName("物流信息")]
        		        public string Logistics { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        [DisplayName("备注")]
        		        public string Remarks { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Order()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Order> Order { get; set; }
    }    
}
