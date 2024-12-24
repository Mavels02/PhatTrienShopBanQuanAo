using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public partial class SanPham
{
    public int IdSanPham { get; set; }

    [Display(Name = "Tên Sản Phẩm")]
    public string TenSanPham { get; set; } = null!;

    [Display(Name = "Mô Tả")]
    public string? MoTa { get; set; }

    [Display(Name = "Giá")]
    public decimal Gia { get; set; }

    [Display(Name = "Số Lượng")]
    public int SoLuong { get; set; }

    [Display(Name = "Hình Ảnh")]
    public string? HinhAnh { get; set; }

    [Display(Name = "Danh Mục")]
    public int? IdCategory { get; set; }


    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual Category? IdCategoryNavigation { get; set; }
}
