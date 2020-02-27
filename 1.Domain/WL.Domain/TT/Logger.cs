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
    /// Logger
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
        /// View
        /// </summary>  
        [DisplayName("View")]
        [MaxLength(50,ErrorMessage="View最大长度为50")]
        		        public string View { get; set; }
		/// <summary>
        /// Action
        /// </summary>  
        [DisplayName("Action")]
        [MaxLength(50,ErrorMessage="Action最大长度为50")]
        		        public string Action { get; set; }
		/// <summary>
        /// Description
        /// </summary>  
        [DisplayName("Description")]
        [MaxLength(50,ErrorMessage="Description最大长度为50")]
        		        public string Description { get; set; }
		/// <summary>
        /// Remark
        /// </summary>  
        [DisplayName("Remark")]
        		        public string Remark { get; set; }
		/// <summary>
        /// UserName
        /// </summary>  
        [DisplayName("UserName")]
        [MaxLength(50,ErrorMessage="UserName最大长度为50")]
        		        public string UserName { get; set; }
		/// <summary>
        /// Time
        /// </summary>  
        [DisplayName("Time")]
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
