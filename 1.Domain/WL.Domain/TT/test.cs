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
    /// test
    /// </summary>
    public class test
    {
        /// <summary>
        /// id
        /// </summary>  
        [DisplayName("id")]
        		[Key]
                public int id { get; set; }
		/// <summary>
        /// name
        /// </summary>  
        [DisplayName("name")]
        [MaxLength(100,ErrorMessage="name最大长度为100")]
        		        public string name { get; set; }
		        /// <summary>
        /// 构造函数
        /// </summary>		
        public test()
        {
        }

    } 

    public partial class WLDbContext : DbContext
    {
        /// <summary>
        /// 把实体添加到EF上下文
        /// </summary>
        public DbSet<test> test { get; set; }
    }    
}
