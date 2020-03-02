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
    /// CMS菜单表
    /// </summary>
        [Table("Cms_Menu")]
    public class Cms_Menu
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int ID { get; set; }
		/// <summary>
        /// 菜单名称
        /// </summary>  
        [DisplayName("菜单名称")]
        [MaxLength(50,ErrorMessage="菜单名称最大长度为50")]
        		        public string Name { get; set; }
		/// <summary>
        /// URL路径
        /// </summary>  
        [DisplayName("URL路径")]
        [MaxLength(50,ErrorMessage="URL路径最大长度为50")]
        		        public string Url { get; set; }
		/// <summary>
        /// 控制器
        /// </summary>  
        [DisplayName("控制器")]
        		        public string Action { get; set; }
		/// <summary>
        /// 排序
        /// </summary>  
        [DisplayName("排序")]
        		        public int Sort { get; set; }
		/// <summary>
        /// 菜单等级
        /// </summary>  
        [DisplayName("菜单等级")]
        		        public int Lv { get; set; }
		/// <summary>
        /// 图标
        /// </summary>  
        [DisplayName("图标")]
        [MaxLength(50,ErrorMessage="图标最大长度为50")]
        		        public string Icon { get; set; }
		/// <summary>
        /// 父ID
        /// </summary>  
        [DisplayName("父ID")]
        		        public int Pid { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Menu()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_Menu> Cms_Menu { get; set; }
    }    
}
