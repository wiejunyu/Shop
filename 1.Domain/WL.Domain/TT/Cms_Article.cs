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
    /// Cms_Article
    /// </summary>
    public class Cms_Article
    {
        /// <summary>
        /// id
        /// </summary>  
        [DisplayName("id")]
        		        public int id { get; set; }
		/// <summary>
        /// catid
        /// </summary>  
        [DisplayName("catid")]
        		        public int? catid { get; set; }
		/// <summary>
        /// userid
        /// </summary>  
        [DisplayName("userid")]
        		        public int? userid { get; set; }
		/// <summary>
        /// username
        /// </summary>  
        [DisplayName("username")]
        [MaxLength(50,ErrorMessage="username最大长度为50")]
        		        public string username { get; set; }
		/// <summary>
        /// title
        /// </summary>  
        [DisplayName("title")]
        [MaxLength(120,ErrorMessage="title最大长度为120")]
        		        public string title { get; set; }
		/// <summary>
        /// keywords
        /// </summary>  
        [DisplayName("keywords")]
        [MaxLength(120,ErrorMessage="keywords最大长度为120")]
        		        public string keywords { get; set; }
		/// <summary>
        /// description
        /// </summary>  
        [DisplayName("description")]
        [MaxLength(8,ErrorMessage="description最大长度为8")]
        		        public string description { get; set; }
		/// <summary>
        /// content
        /// </summary>  
        [DisplayName("content")]
        [MaxLength(8,ErrorMessage="content最大长度为8")]
        		        public string content { get; set; }
		/// <summary>
        /// thumb
        /// </summary>  
        [DisplayName("thumb")]
        [MaxLength(100,ErrorMessage="thumb最大长度为100")]
        		        public string thumb { get; set; }
		/// <summary>
        /// listorder
        /// </summary>  
        [DisplayName("listorder")]
        		        public int? listorder { get; set; }
		/// <summary>
        /// url
        /// </summary>  
        [DisplayName("url")]
        [MaxLength(150,ErrorMessage="url最大长度为150")]
        		        public string url { get; set; }
		/// <summary>
        /// hits
        /// </summary>  
        [DisplayName("hits")]
        		        public int? hits { get; set; }
		/// <summary>
        /// createtime
        /// </summary>  
        [DisplayName("createtime")]
        		        public int? createtime { get; set; }
		/// <summary>
        /// updatetime
        /// </summary>  
        [DisplayName("updatetime")]
        		        public int? updatetime { get; set; }
		/// <summary>
        /// lang
        /// </summary>  
        [DisplayName("lang")]
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
