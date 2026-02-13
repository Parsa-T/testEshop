using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEshop_Phone.Domain.Model;
using System.Data;
using MyEshop_Phone.Application.Common.Interfaces;

namespace MyEshop_Phone.Infra.Data.Context
{
    public class MyDbContext : DbContext, IApplicationDbContext
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
        public DbSet<_Color> colors { get; set; }
        public DbSet<_ProductsColor> productsColors { get; set; }
        public DbSet<_SubmenuGroups> submenuGroups { get; set; }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<_Users>()
                .HasOne(u => u.products)
                .WithMany(p => p.users)
                .HasForeignKey(u => u.ProductsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<_Users>()
                .Property(x => x.ProductsId)
                .IsRequired(false);
            modelBuilder.Entity<_Users>()
                .Property(x => x.Number)
                .IsRequired(false);
            modelBuilder.Entity<_Users>()
                .Property(x => x.StateName)
                .IsRequired(false);
            modelBuilder.Entity<_Users>()
                .Property(x => x.UrlPhoto)
                .IsRequired(false);

            modelBuilder.Entity<_ProductsColor>()
                .HasOne(c => c.products)
                .WithMany(p => p.productsColors)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<_ProductsColor>()
                .HasOne(pc => pc.color)
                .WithMany(c => c.productsColors)
                .HasForeignKey(x => x.ColorId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<_ProductsColor>()
            //    .Property(x=>x.ColorId)
            //    .IsRequired(false);

            modelBuilder.Entity<_Products>()
                .HasOne(p => p.submenuGroups)
                .WithMany(sg => sg.products)
                .HasForeignKey(p => p.SubmenuGroupsId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<_Products>()
                .Property(x => x.SubmenuGroupsId)
                .IsRequired(false);
        }
    }
}
