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
    /// 用户表
    /// </summary>
        [Table("User")]
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int ID { get; set; }
		/// <summary>
        /// 用户名
        /// </summary>  
        [DisplayName("用户名")]
        [MaxLength(50,ErrorMessage="用户名最大长度为50")]
        		        public string UserName { get; set; }
		/// <summary>
        /// 密码
        /// </summary>  
        [DisplayName("密码")]
        [MaxLength(50,ErrorMessage="密码最大长度为50")]
        		        public string PassWord { get; set; }
		/// <summary>
        /// 创建时间
        /// </summary>  
        [DisplayName("创建时间")]
        		        public DateTime CreateTime { get; set; }
		/// <summary>
        /// 最后登陆时间
        /// </summary>  
        [DisplayName("最后登陆时间")]
        		        public DateTime LoginTime { get; set; }
		/// <summary>
        /// IP
        /// </summary>  
        [DisplayName("IP")]
        [MaxLength(50,ErrorMessage="IP最大长度为50")]
        		        public string IP { get; set; }
		/// <summary>
        /// 备注
        /// </summary>  
        [DisplayName("备注")]
        [MaxLength(50,ErrorMessage="备注最大长度为50")]
        		        public string Remark { get; set; }
		/// <summary>
        /// 权限
        /// </summary>  
        [DisplayName("权限")]
        		        public int? Permission { get; set; }
		/// <summary>
        /// 头像
        /// </summary>  
        [DisplayName("头像")]
        [MaxLength(100,ErrorMessage="头像最大长度为100")]
        		        public string Portrait { get; set; }
		/// <summary>
        /// 邮箱
        /// </summary>  
        [DisplayName("邮箱")]
        [MaxLength(50,ErrorMessage="邮箱最大长度为50")]
        		        public string Email { get; set; }
		/// <summary>
        /// 手机
        /// </summary>  
        [DisplayName("手机")]
        [MaxLength(20,ErrorMessage="手机最大长度为20")]
        		        public string Phone { get; set; }
		/// <summary>
        /// QQ
        /// </summary>  
        [DisplayName("QQ")]
        [MaxLength(15,ErrorMessage="QQ最大长度为15")]
        		        public string QQ { get; set; }
		/// <summary>
        /// 余额
        /// </summary>  
        [DisplayName("余额")]
        		        public decimal Money { get; set; }
		/// <summary>
        /// Token
        /// </summary>  
        [DisplayName("Token")]
        [MaxLength(50,ErrorMessage="Token最大长度为50")]
        		        public string Token { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public User()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<User> User { get; set; }
    }    
}
