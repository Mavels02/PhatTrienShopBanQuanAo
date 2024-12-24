using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGiaBanColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCategory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTaCategory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__CBD74706075FB9D8", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    _Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B7C926380E1F2DAD", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    IdSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IdCategory = table.Column<int>(type: "int", nullable: true),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SanPham__5FFA2D42DB78CB1C", x => x.IdSanPham);
                    table.ForeignKey(
                        name: "FK__SanPham__IdCateg__3C69FB99",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    IdDonHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: true),
                    NgayDat = table.Column<DateOnly>(type: "date", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonHang__C7C3C3C5DD4BC3DB", x => x.IdDonHang);
                    table.ForeignKey(
                        name: "FK__DonHang__IdUser__3F466844",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    IdChiTietDonHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDonHang = table.Column<int>(type: "int", nullable: true),
                    IdSanPham = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__613E7D1C9755F43C", x => x.IdChiTietDonHang);
                    table.ForeignKey(
                        name: "FK__ChiTietDo__IdDon__4222D4EF",
                        column: x => x.IdDonHang,
                        principalTable: "DonHang",
                        principalColumn: "IdDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ChiTietDo__IdSan__4316F928",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "IdSanPham");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_IdDonHang",
                table: "ChiTietDonHang",
                column: "IdDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_IdSanPham",
                table: "ChiTietDonHang",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_IdUser",
                table: "DonHang",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdCategory",
                table: "SanPham",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053476EE4F35",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
