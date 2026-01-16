using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class RadicalPic
{
    public int RadicalPicId { get; set; }

    public int RadicalId { get; set; }

    public int? RadicalPicSort { get; set; }

    public string? RadicalPicture { get; set; }

    public string? RadicalImage { get; set; }

    public virtual Radical Radical { get; set; } = null!;
}
