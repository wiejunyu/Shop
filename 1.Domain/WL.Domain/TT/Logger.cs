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
    /// 日志表
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int ID { get; set; }
		/// <summary>
        /// 视图
        /// </summary>  
        [DisplayName("视图")]
        [MaxLength(50,ErrorMessage="视图最大长度为50")]
        		        public string View { get; set; }
		/// <summary>
        /// 控制器
        /// </summary>  
        [DisplayName("控制器")]
        [MaxLength(50,ErrorMessage="控制器最大长度为50")]
        		        public string Action { get; set; }
		/// <summary>
        /// 描述
        /// </summary>  
        [DisplayName("描述")]
        [MaxLength(50,ErrorMessage="描述最大长度为50")]
        		        public string Description { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        [DisplayName("备注")]
        		        public string Remark { get; set; }
		/// <summary>
        /// 用户名
        /// </summary>  
        [DisplayName("用户名")]
        [MaxLength(50,ErrorMessage="用户名最大长度为50")]
        		        public string UserName { get; set; }
		/// <summary>
        /// 时间
        /// </summary>  
        [DisplayName("时间")]
        		        public DateTime? Time { get; set; }
		/// <summary>
        /// IP
        /// </summary>  
        [DisplayName("IP")]
        [MaxLength(50,ErrorMessage="IP最大长度为50")]
        		        public string IP { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Logger()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Logger> Logger { get; set; }
    }    
}
