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
    /// CMS文章表
    /// </summary>
        [Table("Cms_Article")]
    public class Cms_Article
    {
        /// <summary>
        /// ID
        /// </summary>  
        [DisplayName("ID")]
        		        public int id { get; set; }
		/// <summary>
        /// 栏目ID
        /// </summary>  
        [DisplayName("栏目ID")]
        		        public int catid { get; set; }
		/// <summary>
        /// 用户ID
        /// </summary>  
        [DisplayName("用户ID")]
        		        public int userid { get; set; }
		/// <summary>
        /// 用户名称
        /// </summary>  
        [DisplayName("用户名称")]
        [MaxLength(50,ErrorMessage="用户名称最大长度为50")]
        		        public string username { get; set; }
		/// <summary>
        /// 标题
        /// </summary>  
        [DisplayName("标题")]
        [MaxLength(120,ErrorMessage="标题最大长度为120")]
        		        public string title { get; set; }
		/// <summary>
        /// 关键字
        /// </summary>  
        [DisplayName("关键字")]
        [MaxLength(120,ErrorMessage="关键字最大长度为120")]
        		        public string keywords { get; set; }
		/// <summary>
        /// 描述
        /// </summary>  
        [DisplayName("描述")]
        [MaxLength(8,ErrorMessage="描述最大长度为8")]
        		        public string description { get; set; }
		/// <summary>
        /// 内容
        /// </summary>  
        [DisplayName("内容")]
        [MaxLength(8,ErrorMessage="内容最大长度为8")]
        		        public string content { get; set; }
		/// <summary>
        /// thumb
        /// </summary>  
        [DisplayName("thumb")]
        [MaxLength(100,ErrorMessage="thumb最大长度为100")]
        		        public string thumb { get; set; }
		/// <summary>
        /// 排序
        /// </summary>  
        [DisplayName("排序")]
        		        public int listorder { get; set; }
		/// <summary>
        /// URL地址
        /// </summary>  
        [DisplayName("URL地址")]
        [MaxLength(150,ErrorMessage="URL地址最大长度为150")]
        		        public string url { get; set; }
		/// <summary>
        /// 点击次数
        /// </summary>  
        [DisplayName("点击次数")]
        		        public int hits { get; set; }
		/// <summary>
        /// 创建时间
        /// </summary>  
        [DisplayName("创建时间")]
        		        public int createtime { get; set; }
		/// <summary>
        /// 更新时间
        /// </summary>  
        [DisplayName("更新时间")]
        		        public int updatetime { get; set; }
		/// <summary>
        /// 语言
        /// </summary>  
        [DisplayName("语言")]
        		        public int? lang { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Article()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_Article> Cms_Article { get; set; }
    }    
}
