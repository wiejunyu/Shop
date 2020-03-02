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
    /// Cms_Sysconfig
    /// </summary>
    public class Cms_Sysconfig
    {
        /// <summary>
        /// Id
        /// </summary>  
        [DisplayName("Id")]
        		[Key]
                public int Id { get; set; }
		/// <summary>
        /// Address
        /// </summary>  
        [DisplayName("Address")]
        [MaxLength(200,ErrorMessage="Address最大长度为200")]
        		        public string Address { get; set; }
		/// <summary>
        /// Tel
        /// </summary>  
        [DisplayName("Tel")]
        [MaxLength(50,ErrorMessage="Tel最大长度为50")]
        		        public string Tel { get; set; }
		/// <summary>
        /// Record
        /// </summary>  
        [DisplayName("Record")]
        [MaxLength(50,ErrorMessage="Record最大长度为50")]
        		        public string Record { get; set; }
		/// <summary>
        /// Title
        /// </summary>  
        [DisplayName("Title")]
        [MaxLength(50,ErrorMessage="Title最大长度为50")]
        		        public string Title { get; set; }
		/// <summary>
        /// Description
        /// </summary>  
        [DisplayName("Description")]
        [MaxLength(200,ErrorMessage="Description最大长度为200")]
        		        public string Description { get; set; }
		/// <summary>
        /// Keywords
        /// </summary>  
        [DisplayName("Keywords")]
        [MaxLength(50,ErrorMessage="Keywords最大长度为50")]
        		        public string Keywords { get; set; }
		/// <summary>
        /// Facebook
        /// </summary>  
        [DisplayName("Facebook")]
        [MaxLength(50,ErrorMessage="Facebook最大长度为50")]
        		        public string Facebook { get; set; }
		/// <summary>
        /// Icon
        /// </summary>  
        [DisplayName("Icon")]
        [MaxLength(50,ErrorMessage="Icon最大长度为50")]
        		        public string Icon { get; set; }
		/// <summary>
        /// QR_code
        /// </summary>  
        [DisplayName("QR_code")]
        [MaxLength(50,ErrorMessage="QR_code最大长度为50")]
        		        public string QR_code { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public Cms_Sysconfig()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<Cms_Sysconfig> Cms_Sysconfig { get; set; }
    }    
}
