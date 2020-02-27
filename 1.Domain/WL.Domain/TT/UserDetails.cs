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
    /// UserDetails
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// id
        /// </summary>  
        [DisplayName("id")]
        		        public int id { get; set; }
		/// <summary>
        /// Msn
        /// </summary>  
        [DisplayName("Msn")]
        [MaxLength(50,ErrorMessage="Msn最大长度为50")]
        		        public string Msn { get; set; }
		/// <summary>
        /// 电话
        /// </summary>  
        [DisplayName("电话")]
        [MaxLength(15,ErrorMessage="电话最大长度为15")]
        		        public string Tel { get; set; }
		/// <summary>
        /// 生日
        /// </summary>  
        [DisplayName("生日")]
        		        public DateTime? Birthday { get; set; }
		/// <summary>
        /// 情感状况
        /// </summary>  
        [DisplayName("情感状况")]
        		        public int? Emotional { get; set; }
		/// <summary>
        /// 兴趣
        /// </summary>  
        [DisplayName("兴趣")]
        [MaxLength(20,ErrorMessage="兴趣最大长度为20")]
        		        public string Interest { get; set; }
		/// <summary>
        /// 个人简介
        /// </summary>  
        [DisplayName("个人简介")]
        		        public string Describe { get; set; }
		/// <summary>
        /// 个人网站
        /// </summary>  
        [DisplayName("个人网站")]
        [MaxLength(20,ErrorMessage="个人网站最大长度为20")]
        		        public string Website { get; set; }
		/// <summary>
        /// 省、自治区
        /// </summary>  
        [DisplayName("省、自治区")]
        [MaxLength(20,ErrorMessage="省、自治区最大长度为20")]
        		        public string province { get; set; }
		/// <summary>
        /// 市
        /// </summary>  
        [DisplayName("市")]
        [MaxLength(20,ErrorMessage="市最大长度为20")]
        		        public string city { get; set; }
		/// <summary>
        /// 县区
        /// </summary>  
        [DisplayName("县区")]
        [MaxLength(20,ErrorMessage="县区最大长度为20")]
        		        public string district { get; set; }
		/// <summary>
        /// address
        /// </summary>  
        [DisplayName("address")]
        [MaxLength(1000,ErrorMessage="address最大长度为1000")]
        		        public string address { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public UserDetails()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<UserDetails> UserDetails { get; set; }
    }    
}
