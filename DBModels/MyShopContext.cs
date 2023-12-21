using DAFW_IS220.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.DBModels 
{
    // App.DBModels.MyShopContext
    public class MyShopContext : IdentityDbContext<AppUser>
    {
        public MyShopContext(DbContextOptions<MyShopContext> options) : base(options)
        {
          //..
          // this.Roles
          // IdentityRole<string>
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CHITIETGIOHANG>()
            .HasKey(c => new { c.MATK, c.MASP, c.MACTSP });

            modelBuilder.Entity<CHITIETGIOHANG>()
            .HasIndex(p => p.TONGGIA).HasDatabaseName("index-giohang-tonggia");

            modelBuilder.Entity<CTDH>()
            .HasKey(c => new { c.MADH, c.MACTSP });

            modelBuilder.Entity<VOUCHER_DONHANG>()
            .HasKey(c => new { c.MADH, c.MAVOUCHER });

            modelBuilder.Entity<CTDH>()
            .HasIndex(p => p.TONGGIA).HasDatabaseName("index-CTDH-tonggia");

            modelBuilder.Entity<DONHANG>()
            .HasIndex(p => p.TONGTIEN).HasDatabaseName("index-DONHANG-tongtien");

            modelBuilder.Entity<SANPHAM>()
            .HasIndex(p => p.GIAGOC).HasDatabaseName("index-SANPHAM-giagoc");

            modelBuilder.Entity<SANPHAM>()
            .HasIndex(p => p.GIABAN).HasDatabaseName("index-SANPHAM-giaban");

            modelBuilder.Entity<THANHTOAN>()
            .HasIndex(p => p.SOTIEN).HasDatabaseName("index-THANHTOAN-sotien");

            modelBuilder.Entity<THONGTINVANCHUYEN>()
            .HasKey(c => new { c.MADH, c.MATTGH });
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

        }

        public DbSet<CHITIETGIOHANG> CHITIETGIOHANGs {set;get;}

        public DbSet<CHITIETSANPHAM> CHITIETSANPHAMs {set;get;}

        public DbSet<CTDH> CTDHs {set;get;}

        public DbSet<DANHGIA> DANHGIAs {set;get;}

        public DbSet<DONHANG> DONHANGs {set;get;}

        public DbSet<HINHANH> HINHANHs {set;get;}

        public DbSet<MAUSAC> MAUSACs {set;get;}

        public DbSet<PL_SP> PL_SPs {set;get;}

        public DbSet<SANPHAM> SANPHAMs {set;get;}

        public DbSet<THANHTOAN> THANHTOANs {set;get;}

        public DbSet<THONGTINGIAOHANG> THONGTINGIAOHANGs {set;get;}

        public DbSet<VOUCHER> VOUCHERs {set;get;}

        public DbSet<VOUCHER_DONHANG> VOUCHER_DONHANGs {set;get;}

        public DbSet<SIZE> SIZEs {set;get;}

        public DbSet<THONGTINVANCHUYEN> THONGTINVANCHUYENs {set;get;}

    }
}