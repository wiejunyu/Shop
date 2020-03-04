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
    /// 验证码表
    /// </summary>
        [Table("Code")]
    public class Code
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// 类型
        /// </summary>  
        [DisplayName("类型")]
        		        public int type { get; set; }
		/// <summary>
        /// 验证码邮箱
        /// </summary>  
        [DisplayName("验证码邮箱")]
        [MaxLength(50,ErrorMessage="验证码邮箱最大长度为50")]
        		        public string emali { get; set; }
		/// <summary>
        /// 验证码手机
        /// </summary>  
        [DisplayName("验证码手机")]
        [MaxLength(50,ErrorMessage="验证码手机最大长度为50")]
        		        public string phone { get; set; }
		/// <summary>
        /// 验证码
        /// </summary>  
        [DisplayName("验证码")]
        [MaxLength(50,ErrorMessage="验证码最大长度为50")]
        		        public string code { get; set; }
		/// <summary>
        /// 时间
        /// </summary>  
        [DisplayName("时间")]
        		        public DateTime time { get; set; }
		/// <summary>
        /// 次数
        /// </summary>  
        [DisplayName("次数")]
        		        public int count { get; set; }
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
