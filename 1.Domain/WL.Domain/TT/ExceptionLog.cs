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
    /// 系统异常记录
    /// </summary>
    public class ExceptionLog
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int ID { get; set; }
		/// <summary>
        /// Url
        /// </summary>  
        [DisplayName("Url")]
        [MaxLength(300,ErrorMessage="Url最大长度为300")]
        		        public string Url { get; set; }
		/// <summary>
        /// Request
        /// </summary>  
        [DisplayName("Request")]
        		        public string Request { get; set; }
		/// <summary>
        /// Response
        /// </summary>  
        [DisplayName("Response")]
        		        public string Response { get; set; }
		/// <summary>
        /// 1.系统异常  2.业务异常
        /// </summary>  
        [DisplayName("1.系统异常  2.业务异常")]
        		        public int? Exception_Type { get; set; }
		/// <summary>
        /// 错误代码
        /// </summary>  
        [DisplayName("错误代码")]
        [MaxLength(50,ErrorMessage="错误代码最大长度为50")]
        		        public string Error_code { get; set; }
		/// <summary>
        /// 状态
        /// </summary>  
        [DisplayName("状态")]
        		        public int? Status { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        [DisplayName("备注")]
        [MaxLength(200,ErrorMessage="备注最大长度为200")]
        		        public string Remark { get; set; }
		/// <summary>
        /// 创建时间
        /// </summary>  
        [DisplayName("创建时间")]
        		        public DateTime CreateTime { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public ExceptionLog()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<ExceptionLog> ExceptionLog { get; set; }
    }    
}
