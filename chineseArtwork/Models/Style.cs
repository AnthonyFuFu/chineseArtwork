using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Style
{
    public int StyleId { get; set; }

    public string StyleName { get; set; } = null!;

    public string StyleKeyword { get; set; } = null!;

    public int StyleStatus { get; set; }

    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
}
