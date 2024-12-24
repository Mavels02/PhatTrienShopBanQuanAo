using System;
using System.Collections.Generic;

namespace Assignment.Models;

public partial class DonHang
{
    public int IdDonHang { get; set; }

    public int? IdUser { get; set; }

    public DateOnly NgayDat { get; set; }

    public string TrangThai { get; set; } = null!;

    public decimal TongTien { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual User? IdUserNavigation { get; set; }
}
