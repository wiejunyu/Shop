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
    /// Cms_UserInfo
    /// </summary>
    public class Cms_UserInfo
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		        public int ID { get; set; }
		/// <summary>
        /// UserName
        /// </summary>  
        [DisplayName("UserName")]
        [MaxLength(50,ErrorMessage="UserName最大长度为50")]
        		        public string UserName { get; set; }
		/// <summary>
        /// PassWord
        /// </summary>  
        [DisplayName("PassWord")]
        [MaxLength(50,ErrorMessage="PassWord最大长度为50")]
        		        public string PassWord { get; set; }
		/// <summary>
        /// Permission
        /// </summary>  
        [DisplayName("Permission")]
        		        public int? Permission { get; set; }
		/// <summary>
        /// Leader
        /// </summary>  
        [DisplayName("Leader")]
        		        public int? Leader { get; set; }
		/// <summary>
        /// Remark
        /// </summary>  
        [DisplayName("Remark")]
        [MaxLength(50,ErrorMessage="Remark最大长度为50")]
        		        public string Remark { get; set; }
		/// <summary>
        /// CreateTime
        /// </summary>  
        [DisplayName("CreateTime")]
        		        public DateTime? CreateTime { get; set; }
		/// <summary>
        /// LoginTime
        /// </summary>  
        [DisplayName("LoginTime")]
        		        public DateTime? LoginTime { get; set; }
		/// <summary>
        /// IP
        /// </summary>  
        [DisplayName("IP")]
        [MaxLength(50,ErrorMessage="IP最大长度为50")]
        		        public string IP { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_UserInfo()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_UserInfo> Cms_UserInfo { get; set; }
    }    
}
