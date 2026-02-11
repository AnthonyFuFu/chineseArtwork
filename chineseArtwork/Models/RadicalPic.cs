using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class RadicalPic
{
    public int RadicalPicId { get; set; }

    public int RadicalId { get; set; }

    public int StyleId { get; set; }

    public int? RadicalPicSort { get; set; }

    public string? RadicalName { get; set; }

    public string? RadicalPicture { get; set; }

    public string? RadicalCachePicture { get; set; }

    public string? RadicalImage { get; set; }

    public string RadicalCreateBy { get; set; } = null!;

    public DateTime? RadicalCreateDate { get; set; }

    public string RadicalUpdateBy { get; set; } = null!;

    public DateTime? RadicalUpdateDate { get; set; }

    public virtual Radical Radical { get; set; } = null!;

    public virtual Style Style { get; set; } = null!;
}
