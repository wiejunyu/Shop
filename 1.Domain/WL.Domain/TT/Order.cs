using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Id { get; set; }
		/// <summary>
        /// 用户ID
        /// </summary>  
        public int? UserId { get; set; }
		/// <summary>
        /// 订单号
        /// </summary>  
        public string Order { get; set; }
		/// <summary>
        /// 商品ID
        /// </summary>  
        public int? Sid { get; set; }
		/// <summary>
        /// 商品价格
        /// </summary>  
        public decimal? Price { get; set; }
		/// <summary>
        /// 商品数量
        /// </summary>  
        public int? Number { get; set; }
		/// <summary>
        /// 运费
        /// </summary>  
        public decimal? Freight { get; set; }
		/// <summary>
        /// 应付金额
        /// </summary>  
        public decimal? Money { get; set; }
		/// <summary>
        /// 收货地址
        /// </summary>  
        public string Address { get; set; }
		/// <summary>
        /// 发票信息
        /// </summary>  
        public int? Invoice { get; set; }
		/// <summary>
        /// 订单生成日期
        /// </summary>  
        public DateTime? Date { get; set; }
		/// <summary>
        /// 支付状态
        /// </summary>  
        public bool? PayState { get; set; }
		/// <summary>
        /// 支付方式：0货到付款
        /// </summary>  
        public int? PayMethod { get; set; }
		/// <summary>
        /// 物流状态，0未发货，1运输中，2已收货
        /// </summary>  
        public int? LogisticsState { get; set; }
		/// <summary>
        /// 物流信息
        /// </summary>  
        public string Logistics { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        public string Remarks { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Order()
        {
        }

    }    
}
