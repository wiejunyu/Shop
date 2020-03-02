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
    /// 栏目表
    /// </summary>
        [Table("Category")]
    public class Category
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// 栏目名称
        /// </summary>  
        [DisplayName("栏目名称")]
        [MaxLength(30,ErrorMessage="栏目名称最大长度为30")]
        		        public string Name { get; set; }
		/// <summary>
        /// 栏目等级
        /// </summary>  
        [DisplayName("栏目等级")]
        		        public int Rank { get; set; }
		/// <summary>
        /// 栏目图片，空为无图栏目
        /// </summary>  
        [DisplayName("栏目图片，空为无图栏目")]
        [MaxLength(200,ErrorMessage="栏目图片，空为无图栏目最大长度为200")]
        		        public string Picture { get; set; }
		/// <summary>
        /// URL路径
        /// </summary>  
        [DisplayName("URL路径")]
        [MaxLength(200,ErrorMessage="URL路径最大长度为200")]
        		        public string Url { get; set; }
		/// <summary>
        /// 父栏目ID
        /// </summary>  
        [DisplayName("父栏目ID")]
        		        public int Parentid { get; set; }
		/// <summary>
        /// 是否推荐
        /// </summary>  
        [DisplayName("是否推荐")]
        		        public bool Recommend { get; set; }
		/// <summary>
        /// 点击次数
        /// </summary>  
        [DisplayName("点击次数")]
        		        public int Click { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Category()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Category> Category { get; set; }
    }    
}
