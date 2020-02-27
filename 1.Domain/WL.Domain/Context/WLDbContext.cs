using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WL.Domain
{
    public partial class WLDbContext : DbContext
    {
        public WLDbContext() : base("name=connstr")
        {
        }
    }
}
