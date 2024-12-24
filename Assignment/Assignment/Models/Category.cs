using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    [Display(Name = "Tên loại")]
    public string TenCategory { get; set; } = null!;

    [Display(Name = "Mô tả")]
    public string? MoTaCategory { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
