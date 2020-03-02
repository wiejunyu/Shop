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
    /// 商品表
    /// </summary>
        [Table("Shop")]
    public class Shop
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// 商品名称
        /// </summary>  
        [DisplayName("商品名称")]
        [MaxLength(50,ErrorMessage="商品名称最大长度为50")]
        		        public string Name { get; set; }
		/// <summary>
        /// 商品栏目ID
        /// </summary>  
        [DisplayName("商品栏目ID")]
        		        public int Catid { get; set; }
		/// <summary>
        /// 上架时间
        /// </summary>  
        [DisplayName("上架时间")]
        		        public DateTime Uptime { get; set; }
		/// <summary>
        /// 计量单位
        /// </summary>  
        [DisplayName("计量单位")]
        [MaxLength(20,ErrorMessage="计量单位最大长度为20")]
        		        public string Units { get; set; }
		/// <summary>
        /// 品牌
        /// </summary>  
        [DisplayName("品牌")]
        [MaxLength(100,ErrorMessage="品牌最大长度为100")]
        		        public string Brand { get; set; }
		/// <summary>
        /// 优惠价
        /// </summary>  
        [DisplayName("优惠价")]
        		        public decimal Trueprice { get; set; }
		/// <summary>
        /// 市场价
        /// </summary>  
        [DisplayName("市场价")]
        		        public decimal Price { get; set; }
		/// <summary>
        /// 详细介绍
        /// </summary>  
        [DisplayName("详细介绍")]
        [MaxLength(8,ErrorMessage="详细介绍最大长度为8")]
        		        public string Body { get; set; }
		/// <summary>
        /// 型号
        /// </summary>  
        [DisplayName("型号")]
        [MaxLength(20,ErrorMessage="型号最大长度为20")]
        		        public string Model { get; set; }
		/// <summary>
        /// 行业
        /// </summary>  
        [DisplayName("行业")]
        [MaxLength(50,ErrorMessage="行业最大长度为50")]
        		        public string Vocation { get; set; }
		/// <summary>
        /// 点击次数
        /// </summary>  
        [DisplayName("点击次数")]
        		        public int Click { get; set; }
		/// <summary>
        /// 购买次数
        /// </summary>  
        [DisplayName("购买次数")]
        		        public int Buy { get; set; }
		/// <summary>
        /// 推荐
        /// </summary>  
        [DisplayName("推荐")]
        		        public bool Recommend { get; set; }
		/// <summary>
        /// 缩略图
        /// </summary>  
        [DisplayName("缩略图")]
        [MaxLength(200,ErrorMessage="缩略图最大长度为200")]
        		        public string Image { get; set; }
		/// <summary>
        /// 高清图
        /// </summary>  
        [DisplayName("高清图")]
        		        public string HighImage { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        [DisplayName("备注")]
        [MaxLength(200,ErrorMessage="备注最大长度为200")]
        		        public string Remarks { get; set; }
		/// <summary>
        /// 参数
        /// </summary>  
        [DisplayName("参数")]
        [MaxLength(200,ErrorMessage="参数最大长度为200")]
        		        public string Parameter { get; set; }
		/// <summary>
        /// 品牌证书
        /// </summary>  
        [DisplayName("品牌证书")]
        [MaxLength(200,ErrorMessage="品牌证书最大长度为200")]
        		        public string Certificate { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Shop()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Shop> Shop { get; set; }
    }    
}
