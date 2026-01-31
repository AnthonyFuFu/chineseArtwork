using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class FamousArtworkPic
{
    public int FmsAwPicId { get; set; }

    public int? FmsAwId { get; set; }

    public int? FmsAwPicSort { get; set; }

    public string? FmsAwPicture { get; set; }

    public string? FmsAwImage { get; set; }

    public virtual FamousArtwork? FmsAw { get; set; }
}
