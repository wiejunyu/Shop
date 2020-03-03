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
    /// Cms_Sysconfig
    /// </summary>
        [Table("Cms_Sysconfig")]
    public class Cms_Sysconfig
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int Id { get; set; }
		/// <summary>
        /// 地址
        /// </summary>  
        [DisplayName("地址")]
        [MaxLength(200,ErrorMessage="地址最大长度为200")]
        		        public string Address { get; set; }
		/// <summary>
        /// 电话
        /// </summary>  
        [DisplayName("电话")]
        [MaxLength(50,ErrorMessage="电话最大长度为50")]
        		        public string Tel { get; set; }
		/// <summary>
        /// 备案号
        /// </summary>  
        [DisplayName("备案号")]
        [MaxLength(50,ErrorMessage="备案号最大长度为50")]
        		        public string Record { get; set; }
		/// <summary>
        /// 网站标题
        /// </summary>  
        [DisplayName("网站标题")]
        [MaxLength(50,ErrorMessage="网站标题最大长度为50")]
        		        public string Title { get; set; }
		/// <summary>
        /// 网站描述
        /// </summary>  
        [DisplayName("网站描述")]
        [MaxLength(200,ErrorMessage="网站描述最大长度为200")]
        		        public string Description { get; set; }
		/// <summary>
        /// 网站关键字
        /// </summary>  
        [DisplayName("网站关键字")]
        [MaxLength(50,ErrorMessage="网站关键字最大长度为50")]
        		        public string Keywords { get; set; }
		/// <summary>
        /// Facebook
        /// </summary>  
        [DisplayName("Facebook")]
        [MaxLength(50,ErrorMessage="Facebook最大长度为50")]
        		        public string Facebook { get; set; }
		/// <summary>
        /// 网站图标
        /// </summary>  
        [DisplayName("网站图标")]
        [MaxLength(50,ErrorMessage="网站图标最大长度为50")]
        		        public string Icon { get; set; }
		/// <summary>
        /// 网站二维码
        /// </summary>  
        [DisplayName("网站二维码")]
        [MaxLength(50,ErrorMessage="网站二维码最大长度为50")]
        		        public string QR_code { get; set; }
		/// <summary>
        /// 邮箱配置发件人
        /// </summary>  
        [DisplayName("邮箱配置发件人")]
        [MaxLength(100,ErrorMessage="邮箱配置发件人最大长度为100")]
        		        public string Mail_From { get; set; }
		/// <summary>
        /// 邮箱配置地址
        /// </summary>  
        [DisplayName("邮箱配置地址")]
        [MaxLength(100,ErrorMessage="邮箱配置地址最大长度为100")]
        		        public string Mail_Host { get; set; }
		/// <summary>
        /// 授权码
        /// </summary>  
        [DisplayName("授权码")]
        [MaxLength(50,ErrorMessage="授权码最大长度为50")]
        		        public string Mail_Code { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Sysconfig()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_Sysconfig> Cms_Sysconfig { get; set; }
    }    
}
