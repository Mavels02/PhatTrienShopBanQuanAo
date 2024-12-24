using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public partial class User
{
    public int IdUser { get; set; }

    [Display(Name ="Họ và tên")]
    public string HoTen { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = null!;

    [Display(Name = "Số điện thoại")]
    public string? SoDienThoai { get; set; }

    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    
    private string _chucVu = "KH";
    [Display(Name = "Chức vụ")]
    public string ChucVu
    {
        get { return _chucVu; }
        set { _chucVu = value; }
    }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
