using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class ArtworkPic
{
    public int AwPicId { get; set; }

    public int? AwId { get; set; }

    public int? AwPicSort { get; set; }

    public string? AwPicture { get; set; }

    public string? AwCachePicture { get; set; }

    public string? AwImage { get; set; }

    public virtual Artwork? Aw { get; set; }
}
