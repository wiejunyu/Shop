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
    /// Cms_Menu
    /// </summary>
    public class Cms_Menu
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
        /// Url
        /// </summary>  
        [DisplayName("Url")]
        [MaxLength(50,ErrorMessage="Url最大长度为50")]
        		        public string Url { get; set; }
		/// <summary>
        /// Action
        /// </summary>  
        [DisplayName("Action")]
        		        public string Action { get; set; }
		/// <summary>
        /// Sort
        /// </summary>  
        [DisplayName("Sort")]
        		        public int? Sort { get; set; }
		/// <summary>
        /// Lv
        /// </summary>  
        [DisplayName("Lv")]
        		        public int? Lv { get; set; }
		/// <summary>
        /// Icon
        /// </summary>  
        [DisplayName("Icon")]
        [MaxLength(50,ErrorMessage="Icon最大长度为50")]
        		        public string Icon { get; set; }
		/// <summary>
        /// Pid
        /// </summary>  
        [DisplayName("Pid")]
        		        public int? Pid { get; set; }
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
