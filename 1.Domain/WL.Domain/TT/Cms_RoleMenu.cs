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
    /// 角色按钮关联表
    /// </summary>
        [Table("Cms_RoleMenu")]
    public class Cms_RoleMenu
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int ID { get; set; }
		/// <summary>
        /// 按钮ID
        /// </summary>  
        [DisplayName("按钮ID")]
        		        public int MenuID { get; set; }
		/// <summary>
        /// 角色ID
        /// </summary>  
        [DisplayName("角色ID")]
        		        public int RoleID { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_RoleMenu()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_RoleMenu> Cms_RoleMenu { get; set; }
    }    
}
