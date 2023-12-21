﻿// <auto-generated />
using System;
using App.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAFW_IS220.Migrations
{
    [DbContext(typeof(MyShopContext))]
    [Migration("20231220090004_ShippingCost")]
    partial class ShippingCost
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("App.DBModels.CHITIETGIOHANG", b =>
                {
                    b.Property<string>("MATK")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("MASP")
                        .HasColumnType("int");

                    b.Property<int>("MACTSP")
                        .HasColumnType("int");

                    b.Property<int>("SOLUONGMUA")
                        .HasColumnType("int");

                    b.Property<decimal>("TONGGIA")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("MATK", "MASP", "MACTSP");

                    b.HasIndex("MACTSP");

                    b.HasIndex("MASP");

                    b.HasIndex("TONGGIA")
                        .HasDatabaseName("index-giohang-tonggia");

                    b.ToTable("CHITIETGIOHANGs");
                });

            modelBuilder.Entity("App.DBModels.CHITIETSANPHAM", b =>
                {
                    b.Property<int>("MACTSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MAMAU")
                        .HasColumnType("int");

                    b.Property<int>("MASIZE")
                        .HasColumnType("int");

                    b.Property<int>("MASP")
                        .HasColumnType("int");

                    b.Property<int>("SOLUONG")
                        .HasColumnType("int");

                    b.HasKey("MACTSP");

                    b.HasIndex("MAMAU");

                    b.HasIndex("MASIZE");

                    b.HasIndex("MASP");

                    b.ToTable("CHITIETSANPHAMs");
                });

            modelBuilder.Entity("App.DBModels.CTDH", b =>
                {
                    b.Property<int>("MADH")
                        .HasColumnType("int");

                    b.Property<int>("MACTSP")
                        .HasColumnType("int");

                    b.Property<int>("SOLUONG")
                        .HasColumnType("int");

                    b.Property<decimal>("TONGGIA")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("MADH", "MACTSP");

                    b.HasIndex("MACTSP");

                    b.HasIndex("TONGGIA")
                        .HasDatabaseName("index-CTDH-tonggia");

                    b.ToTable("CTDHs");
                });

            modelBuilder.Entity("App.DBModels.DANHGIA", b =>
                {
                    b.Property<int>("MADANHGIA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MADH")
                        .HasColumnType("int");

                    b.Property<int>("MASP")
                        .HasColumnType("int");

                    b.Property<string>("MATK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NOIDUNG")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SOSAO")
                        .HasColumnType("int");

                    b.HasKey("MADANHGIA");

                    b.HasIndex("MADH");

                    b.HasIndex("MASP");

                    b.HasIndex("MATK");

                    b.ToTable("DANHGIAs");
                });

            modelBuilder.Entity("App.DBModels.DONHANG", b =>
                {
                    b.Property<int>("MADH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GHICHU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HINHTHUCTHANHTOAN")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("MATK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("MATTGH")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NGAYGIAO")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NGAYMUA")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NGAYSUADOI")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("PHIVANCHUYEN")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("TONGTIEN")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("TRANGTHAIDONHANG")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("TRANGTHAITHANHTOAN")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("MADH");

                    b.HasIndex("MATK");

                    b.HasIndex("MATTGH");

                    b.HasIndex("TONGTIEN")
                        .HasDatabaseName("index-DONHANG-tongtien");

                    b.ToTable("DONHANGs");
                });

            modelBuilder.Entity("App.DBModels.HINHANH", b =>
                {
                    b.Property<int>("MAHINHANH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LINK")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<int>("MASP")
                        .HasColumnType("int");

                    b.Property<string>("TENHINHANH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.HasKey("MAHINHANH");

                    b.HasIndex("MASP");

                    b.ToTable("HINHANHs");
                });

            modelBuilder.Entity("App.DBModels.MAUSAC", b =>
                {
                    b.Property<int>("MAMAU")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("HEX")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("TENMAU")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("MAMAU");

                    b.ToTable("MAUSACs");
                });

            modelBuilder.Entity("App.DBModels.PL_SP", b =>
                {
                    b.Property<int>("MAPL")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TENPL")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("MAPL");

                    b.ToTable("PL_SPs");
                });

            modelBuilder.Entity("App.DBModels.SANPHAM", b =>
                {
                    b.Property<int>("MASP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("GIABAN")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("GIAGOC")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("MAPL")
                        .HasColumnType("int");

                    b.Property<string>("MOTA")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MainImg")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<string>("PLTHOITRANG")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<string>("TENSP")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("MASP");

                    b.HasIndex("GIABAN")
                        .HasDatabaseName("index-SANPHAM-giaban");

                    b.HasIndex("GIAGOC")
                        .HasDatabaseName("index-SANPHAM-giagoc");

                    b.HasIndex("MAPL");

                    b.ToTable("SANPHAMs");
                });

            modelBuilder.Entity("App.DBModels.SIZE", b =>
                {
                    b.Property<int>("MASIZE")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.HasKey("MASIZE");

                    b.ToTable("SIZEs");
                });

            modelBuilder.Entity("App.DBModels.THANHTOAN", b =>
                {
                    b.Property<int>("MATHANHTOAN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MADH")
                        .HasColumnType("int");

                    b.Property<DateTime>("NGAYTHANHTOAN")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("SOTIEN")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("MATHANHTOAN");

                    b.HasIndex("MADH");

                    b.HasIndex("SOTIEN")
                        .HasDatabaseName("index-THANHTOAN-sotien");

                    b.ToTable("THANHTOANs");
                });

            modelBuilder.Entity("App.DBModels.THONGTINGIAOHANG", b =>
                {
                    b.Property<int>("MATTGH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DIACHI")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<string>("MATK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("NGAYHETDUNG")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NGAYTAO")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar");

                    b.Property<string>("TENNGUOINHAN")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.HasKey("MATTGH");

                    b.HasIndex("MATK");

                    b.ToTable("THONGTINGIAOHANGs");
                });

            modelBuilder.Entity("App.DBModels.THONGTINVANCHUYEN", b =>
                {
                    b.Property<int>("MADH")
                        .HasColumnType("int");

                    b.Property<int>("MATTGH")
                        .HasColumnType("int");

                    b.Property<DateTime>("NGAYGIAO")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NGAYKETTHUC")
                        .HasColumnType("datetime(6)");

                    b.HasKey("MADH", "MATTGH");

                    b.HasIndex("MATTGH");

                    b.ToTable("THONGTINVANCHUYENs");
                });

            modelBuilder.Entity("App.DBModels.VOUCHER", b =>
                {
                    b.Property<int>("MAVOUCHER")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("GIATRIGIAM")
                        .HasColumnType("int");

                    b.Property<string>("MOTA")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SOLUONG")
                        .HasColumnType("int");

                    b.Property<string>("TENVOUCHER")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("THOIGIANBD")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("THOIGIANKT")
                        .HasColumnType("datetime(6)");

                    b.HasKey("MAVOUCHER");

                    b.ToTable("VOUCHERs");
                });

            modelBuilder.Entity("App.DBModels.VOUCHER_DONHANG", b =>
                {
                    b.Property<int>("MADH")
                        .HasColumnType("int");

                    b.Property<int>("MAVOUCHER")
                        .HasColumnType("int");

                    b.HasKey("MADH", "MAVOUCHER");

                    b.HasIndex("MAVOUCHER");

                    b.ToTable("VOUCHER_DONHANGs");
                });

            modelBuilder.Entity("DAFW_IS220.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("DIACHI")
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("GIOITINH")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("NGAYDK")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("TENKH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("App.DBModels.CHITIETGIOHANG", b =>
                {
                    b.HasOne("App.DBModels.CHITIETSANPHAM", "CHITIETSANPHAM")
                        .WithMany()
                        .HasForeignKey("MACTSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.SANPHAM", "SANPHAM")
                        .WithMany()
                        .HasForeignKey("MASP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAFW_IS220.Models.AppUser", "TAIKHOAN")
                        .WithMany()
                        .HasForeignKey("MATK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CHITIETSANPHAM");

                    b.Navigation("SANPHAM");

                    b.Navigation("TAIKHOAN");
                });

            modelBuilder.Entity("App.DBModels.CHITIETSANPHAM", b =>
                {
                    b.HasOne("App.DBModels.MAUSAC", "MAUSAC")
                        .WithMany()
                        .HasForeignKey("MAMAU")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.SIZE", "SIZE")
                        .WithMany()
                        .HasForeignKey("MASIZE")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.SANPHAM", "SANPHAM")
                        .WithMany()
                        .HasForeignKey("MASP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MAUSAC");

                    b.Navigation("SANPHAM");

                    b.Navigation("SIZE");
                });

            modelBuilder.Entity("App.DBModels.CTDH", b =>
                {
                    b.HasOne("App.DBModels.CHITIETSANPHAM", "CHITIETSANPHAM")
                        .WithMany()
                        .HasForeignKey("MACTSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.DONHANG", "DONHANG")
                        .WithMany()
                        .HasForeignKey("MADH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CHITIETSANPHAM");

                    b.Navigation("DONHANG");
                });

            modelBuilder.Entity("App.DBModels.DANHGIA", b =>
                {
                    b.HasOne("App.DBModels.DONHANG", "DONHANG")
                        .WithMany()
                        .HasForeignKey("MADH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.SANPHAM", "SANPHAM")
                        .WithMany()
                        .HasForeignKey("MASP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAFW_IS220.Models.AppUser", "TAIKHOAN")
                        .WithMany()
                        .HasForeignKey("MATK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DONHANG");

                    b.Navigation("SANPHAM");

                    b.Navigation("TAIKHOAN");
                });

            modelBuilder.Entity("App.DBModels.DONHANG", b =>
                {
                    b.HasOne("DAFW_IS220.Models.AppUser", "TAIKHOAN")
                        .WithMany()
                        .HasForeignKey("MATK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.THONGTINGIAOHANG", "THONGTINGIAOHANG")
                        .WithMany()
                        .HasForeignKey("MATTGH");

                    b.Navigation("TAIKHOAN");

                    b.Navigation("THONGTINGIAOHANG");
                });

            modelBuilder.Entity("App.DBModels.HINHANH", b =>
                {
                    b.HasOne("App.DBModels.SANPHAM", "SANPHAM")
                        .WithMany("Image")
                        .HasForeignKey("MASP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SANPHAM");
                });

            modelBuilder.Entity("App.DBModels.SANPHAM", b =>
                {
                    b.HasOne("App.DBModels.PL_SP", "PL_SP")
                        .WithMany()
                        .HasForeignKey("MAPL")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PL_SP");
                });

            modelBuilder.Entity("App.DBModels.THANHTOAN", b =>
                {
                    b.HasOne("App.DBModels.DONHANG", "DONHANG")
                        .WithMany()
                        .HasForeignKey("MADH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DONHANG");
                });

            modelBuilder.Entity("App.DBModels.THONGTINGIAOHANG", b =>
                {
                    b.HasOne("DAFW_IS220.Models.AppUser", "TAIKHOAN")
                        .WithMany()
                        .HasForeignKey("MATK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TAIKHOAN");
                });

            modelBuilder.Entity("App.DBModels.THONGTINVANCHUYEN", b =>
                {
                    b.HasOne("App.DBModels.DONHANG", "DONHANG")
                        .WithMany()
                        .HasForeignKey("MADH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.THONGTINGIAOHANG", "THONGTINGIAOHANG")
                        .WithMany()
                        .HasForeignKey("MATTGH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DONHANG");

                    b.Navigation("THONGTINGIAOHANG");
                });

            modelBuilder.Entity("App.DBModels.VOUCHER_DONHANG", b =>
                {
                    b.HasOne("App.DBModels.DONHANG", "DONHANG")
                        .WithMany()
                        .HasForeignKey("MADH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.DBModels.VOUCHER", "VOUCHER")
                        .WithMany()
                        .HasForeignKey("MAVOUCHER")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DONHANG");

                    b.Navigation("VOUCHER");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DAFW_IS220.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DAFW_IS220.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAFW_IS220.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DAFW_IS220.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App.DBModels.SANPHAM", b =>
                {
                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
