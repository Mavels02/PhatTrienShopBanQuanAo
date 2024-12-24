using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models;

public partial class AsmC4Context : DbContext
{
    public AsmC4Context()
    {
    }

    public AsmC4Context(DbContextOptions<AsmC4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-2HQQGK0E\\SQLEXPRESS;Initial Catalog=ASM_C4;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Categori__CBD74706075FB9D8");

            entity.Property(e => e.MoTaCategory).HasMaxLength(255);
            entity.Property(e => e.TenCategory).HasMaxLength(100);
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.IdChiTietDonHang).HasName("PK__ChiTietD__613E7D1C9755F43C");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.GiaBan).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.IdDonHang)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ChiTietDo__IdDon__4222D4EF");

            entity.HasOne(d => d.IdSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.IdSanPham)
                .HasConstraintName("FK__ChiTietDo__IdSan__4316F928");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.IdDonHang).HasName("PK__DonHang__C7C3C3C5DD4BC3DB");

            entity.ToTable("DonHang");

            entity.Property(e => e.TongTien).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DonHang__IdUser__3F466844");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.IdSanPham).HasName("PK__SanPham__5FFA2D42DB78CB1C");

            entity.ToTable("SanPham");

            entity.Property(e => e.Gia).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__SanPham__IdCateg__3C69FB99");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__B7C926380E1F2DAD");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053476EE4F35").IsUnique();

            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("_Password");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
