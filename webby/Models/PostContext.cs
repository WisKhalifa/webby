using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
namespace webby.Models
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {

        }

        public PostContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb; Database = aspnet-webby-20181110020856; Trusted_Connection = True; MultipleActiveResultSets = true");
            }
        }
        

        public DbSet<PostModels> PostsModels { get; set; }
    }
}