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
    /// Cms_Role
    /// </summary>
    public class Cms_Role
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		        public int ID { get; set; }
		/// <summary>
        /// Name
        /// </summary>  
        [DisplayName("Name")]
        [MaxLength(50,ErrorMessage="Name最大长度为50")]
        		        public string Name { get; set; }
		/// <summary>
        /// Remark
        /// </summary>  
        [DisplayName("Remark")]
        [MaxLength(50,ErrorMessage="Remark最大长度为50")]
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
