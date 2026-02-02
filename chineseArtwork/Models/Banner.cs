using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Banner
{
    public int BanId { get; set; }

    public int NewsId { get; set; }

    public string? BanPicture { get; set; }

    public string? BanImage { get; set; }

    public virtual News News { get; set; } = null!;
}
