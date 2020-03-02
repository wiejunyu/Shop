using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WL.Domain
{
	/// <summary>
    /// Shop
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// id
        /// </summary>  
        public int id { get; set; }
		/// <summary>
        /// 商品名称
        /// </summary>  
        public string Name { get; set; }
		/// <summary>
        /// 商品栏目ID
        /// </summary>  
        public int? Catid { get; set; }
		/// <summary>
        /// 上架时间
        /// </summary>  
        public DateTime? Uptime { get; set; }
		/// <summary>
        /// 计量单位
        /// </summary>  
        public string Units { get; set; }
		/// <summary>
        /// 品牌
        /// </summary>  
        public string Brand { get; set; }
		/// <summary>
        /// 优惠价
        /// </summary>  
        public decimal? Trueprice { get; set; }
		/// <summary>
        /// 市场价
        /// </summary>  
        public decimal? Price { get; set; }
		/// <summary>
        /// 详细介绍
        /// </summary>  
        public string Body { get; set; }
		/// <summary>
        /// 型号
        /// </summary>  
        public string Model { get; set; }
		/// <summary>
        /// 行业
        /// </summary>  
        public string Vocation { get; set; }
		/// <summary>
        /// 点击次数
        /// </summary>  
        public int? Click { get; set; }
		/// <summary>
        /// 购买次数
        /// </summary>  
        public int? Buy { get; set; }
		/// <summary>
        /// 推荐
        /// </summary>  
        public bool? Recommend { get; set; }
		/// <summary>
        /// 缩略图
        /// </summary>  
        public string Image { get; set; }
		/// <summary>
        /// 高清图
        /// </summary>  
        public string HighImage { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        public string Remarks { get; set; }
		/// <summary>
        /// 参数
        /// </summary>  
        public string Parameter { get; set; }
		/// <summary>
        /// 品牌证书
        /// </summary>  
        public string Certificate { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Shop()
        {
        }

    }    
}
