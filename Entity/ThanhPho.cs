﻿using System;
using System.Collections.Generic;

namespace MidTerm.Entity;

public partial class ThanhPho
{
    public int MaThanhPho { get; set; }

    public string? TenThanhPho { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
}
