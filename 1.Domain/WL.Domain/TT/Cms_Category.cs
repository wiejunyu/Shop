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
    /// Cms_Category
    /// </summary>
    public class Cms_Category
    {
        /// <summary>
        /// id
        /// </summary>  
        [DisplayName("id")]
        		        public int id { get; set; }
		/// <summary>
        /// catname
        /// </summary>  
        [DisplayName("catname")]
        [MaxLength(50,ErrorMessage="catname最大长度为50")]
        		        public string catname { get; set; }
		/// <summary>
        /// parentid
        /// </summary>  
        [DisplayName("parentid")]
        		        public int? parentid { get; set; }
		/// <summary>
        /// moduleid
        /// </summary>  
        [DisplayName("moduleid")]
        		        public int? moduleid { get; set; }
		/// <summary>
        /// title
        /// </summary>  
        [DisplayName("title")]
        [MaxLength(150,ErrorMessage="title最大长度为150")]
        		        public string title { get; set; }
		/// <summary>
        /// keywords
        /// </summary>  
        [DisplayName("keywords")]
        [MaxLength(100,ErrorMessage="keywords最大长度为100")]
        		        public string keywords { get; set; }
		/// <summary>
        /// description
        /// </summary>  
        [DisplayName("description")]
        [MaxLength(255,ErrorMessage="description最大长度为255")]
        		        public string description { get; set; }
		/// <summary>
        /// listorder
        /// </summary>  
        [DisplayName("listorder")]
        		        public int? listorder { get; set; }
		/// <summary>
        /// hits
        /// </summary>  
        [DisplayName("hits")]
        		        public int? hits { get; set; }
		/// <summary>
        /// image
        /// </summary>  
        [DisplayName("image")]
        [MaxLength(100,ErrorMessage="image最大长度为100")]
        		        public string image { get; set; }
		/// <summary>
        /// url
        /// </summary>  
        [DisplayName("url")]
        [MaxLength(150,ErrorMessage="url最大长度为150")]
        		        public string url { get; set; }
		/// <summary>
        /// lang
        /// </summary>  
        [DisplayName("lang")]
        		        public int? lang { get; set; }
		/// <summary>
        /// catdir
        /// </summary>  
        [DisplayName("catdir")]
        [MaxLength(50,ErrorMessage="catdir最大长度为50")]
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
