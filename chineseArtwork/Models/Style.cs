using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Style
{
    public int StyleId { get; set; }

    public int CatId { get; set; }

    public string StyleName { get; set; } = null!;

    public string StyleKeyword { get; set; } = null!;

    public string? StyleDescription { get; set; }

    public int StyleStatus { get; set; }

    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<DictionaryPic> DictionaryPics { get; set; } = new List<DictionaryPic>();

    public virtual ICollection<FamousArtwork> FamousArtworks { get; set; } = new List<FamousArtwork>();
}
