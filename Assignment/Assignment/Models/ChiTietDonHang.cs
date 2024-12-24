using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class ChiTietDonHang
{
    public int IdChiTietDonHang { get; set; }

    public int? IdDonHang { get; set; }

    public int? IdSanPham { get; set; }

    public int SoLuong { get; set; }

    public decimal GiaBan { get; set; }

    public virtual DonHang? IdDonHangNavigation { get; set; }

    public virtual SanPham? IdSanPhamNavigation { get; set; }
}
