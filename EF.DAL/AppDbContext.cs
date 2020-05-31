using EF.DAL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL
{
    public class AppDbContext : DbContext
    {
      
        public AppDbContext():base(ConfigurationManager.ConnectionStrings["Mysql"].ToString())
        {
            
        }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
       

    }
}
