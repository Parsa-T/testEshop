using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEshop_Phone.Domain.Model;
using System.Data;

namespace MyEshop_Phone.Infra.Data.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<_Products> Products { get; set; }
        public DbSet<_Features> Features { get; set; }
        public DbSet<_OrderDetails> OrderDetails { get; set; }
        public DbSet<_Orders> Orders { get; set; }
        public DbSet<_Products_comment> Products_Comments { get; set; }
        public DbSet<_Products_Features> Products_Features { get; set; }
        public DbSet<_Products_Galleries> Products_Galleries { get; set; }
        public DbSet<_Products_Groups> Products_Groups { get; set; }
        public DbSet<_Products_Tags> Products_Tags { get; set; }
        public DbSet<_Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<_Users>()
                .HasOne(u=>u.products)
                .WithMany(p=>p.users)
                .HasForeignKey(u=>u.ProductsId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<_Users>()
                .Property(x => x.ProductsId)
                .IsRequired(false);
        }
    }
}
