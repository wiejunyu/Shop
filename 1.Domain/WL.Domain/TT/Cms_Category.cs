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
    /// CMS栏目表
    /// </summary>
    public class Cms_Category
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
        [MaxLength(50,ErrorMessage="栏目名称最大长度为50")]
        		        public string catname { get; set; }
		/// <summary>
        /// 父ID
        /// </summary>  
        [DisplayName("父ID")]
        		        public int parentid { get; set; }
		/// <summary>
        /// 文章模型
        /// </summary>  
        [DisplayName("文章模型")]
        		        public int moduleid { get; set; }
		/// <summary>
        /// 标题
        /// </summary>  
        [DisplayName("标题")]
        [MaxLength(150,ErrorMessage="标题最大长度为150")]
        		        public string title { get; set; }
		/// <summary>
        /// 关键字
        /// </summary>  
        [DisplayName("关键字")]
        [MaxLength(100,ErrorMessage="关键字最大长度为100")]
        		        public string keywords { get; set; }
		/// <summary>
        /// 描述
        /// </summary>  
        [DisplayName("描述")]
        [MaxLength(255,ErrorMessage="描述最大长度为255")]
        		        public string description { get; set; }
		/// <summary>
        /// 排序
        /// </summary>  
        [DisplayName("排序")]
        		        public int listorder { get; set; }
		/// <summary>
        /// 点击次数
        /// </summary>  
        [DisplayName("点击次数")]
        		        public int hits { get; set; }
		/// <summary>
        /// 栏目图片
        /// </summary>  
        [DisplayName("栏目图片")]
        [MaxLength(100,ErrorMessage="栏目图片最大长度为100")]
        		        public string image { get; set; }
		/// <summary>
        /// URL路径
        /// </summary>  
        [DisplayName("URL路径")]
        [MaxLength(150,ErrorMessage="URL路径最大长度为150")]
        		        public string url { get; set; }
		/// <summary>
        /// 语言
        /// </summary>  
        [DisplayName("语言")]
        		        public int? lang { get; set; }
		/// <summary>
        /// 栏目目录
        /// </summary>  
        [DisplayName("栏目目录")]
        [MaxLength(50,ErrorMessage="栏目目录最大长度为50")]
        		        public string catdir { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Category()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_Category> Cms_Category { get; set; }
    }    
}
