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
    /// CMS角色表
    /// </summary>
        [Table("Cms_Role")]
    public class Cms_Role
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int ID { get; set; }
		/// <summary>
        /// 权限名称
        /// </summary>  
        [DisplayName("权限名称")]
        [MaxLength(50,ErrorMessage="权限名称最大长度为50")]
        		        public string Name { get; set; }
		/// <summary>
        /// 权限备注
        /// </summary>  
        [DisplayName("权限备注")]
        [MaxLength(50,ErrorMessage="权限备注最大长度为50")]
        		        public string Remark { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Role()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_Role> Cms_Role { get; set; }
    }    
}
