using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Verser
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Poem> Poems { get; set; }

        public ApplicationContext() : base("DefaultConnection")
        {

        }
    }
}
