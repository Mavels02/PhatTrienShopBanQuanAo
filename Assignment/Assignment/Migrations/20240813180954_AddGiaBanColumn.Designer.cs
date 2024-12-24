﻿// <auto-generated />
using System;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment.Migrations
{
    [DbContext(typeof(AsmC4Context))]
    [Migration("20240813180954_AddGiaBanColumn")]
    partial class AddGiaBanColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assignment.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategory"));

                    b.Property<string>("MoTaCategory")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TenCategory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdCategory")
                        .HasName("PK__Categori__CBD74706075FB9D8");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Assignment.Models.ChiTietDonHang", b =>
                {
                    b.Property<int>("IdChiTietDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdChiTietDonHang"));

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("IdDonHang")
                        .HasColumnType("int");

                    b.Property<int?>("IdSanPham")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("IdChiTietDonHang")
                        .HasName("PK__ChiTietD__613E7D1C9755F43C");

                    b.HasIndex("IdDonHang");

                    b.HasIndex("IdSanPham");

                    b.ToTable("ChiTietDonHang", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.DonHang", b =>
                {
                    b.Property<int>("IdDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDonHang"));

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateOnly>("NgayDat")
                        .HasColumnType("date");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdDonHang")
                        .HasName("PK__DonHang__C7C3C3C5DD4BC3DB");

                    b.HasIndex("IdUser");

                    b.ToTable("DonHang", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.SanPham", b =>
                {
                    b.Property<int>("IdSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSanPham"));

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("HinhAnh")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("IdCategory")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdSanPham")
                        .HasName("PK__SanPham__5FFA2D42DB78CB1C");

                    b.HasIndex("IdCategory");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("_Password");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("IdUser")
                        .HasName("PK__Users__B7C926380E1F2DAD");

                    b.HasIndex(new[] { "Email" }, "UQ__Users__A9D1053476EE4F35")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Assignment.Models.ChiTietDonHang", b =>
                {
                    b.HasOne("Assignment.Models.DonHang", "IdDonHangNavigation")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("IdDonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ChiTietDo__IdDon__4222D4EF");

                    b.HasOne("Assignment.Models.SanPham", "IdSanPhamNavigation")
                        .WithMany("ChiTietDonHangs")
                        .HasForeignKey("IdSanPham")
                        .HasConstraintName("FK__ChiTietDo__IdSan__4316F928");

                    b.Navigation("IdDonHangNavigation");

                    b.Navigation("IdSanPhamNavigation");
                });

            modelBuilder.Entity("Assignment.Models.DonHang", b =>
                {
                    b.HasOne("Assignment.Models.User", "IdUserNavigation")
                        .WithMany("DonHangs")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__DonHang__IdUser__3F466844");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("Assignment.Models.SanPham", b =>
                {
                    b.HasOne("Assignment.Models.Category", "IdCategoryNavigation")
                        .WithMany("SanPhams")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK__SanPham__IdCateg__3C69FB99");

                    b.Navigation("IdCategoryNavigation");
                });

            modelBuilder.Entity("Assignment.Models.Category", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("Assignment.Models.DonHang", b =>
                {
                    b.Navigation("ChiTietDonHangs");
                });

            modelBuilder.Entity("Assignment.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietDonHangs");
                });

            modelBuilder.Entity("Assignment.Models.User", b =>
                {
                    b.Navigation("DonHangs");
                });
#pragma warning restore 612, 618
        }
    }
}
