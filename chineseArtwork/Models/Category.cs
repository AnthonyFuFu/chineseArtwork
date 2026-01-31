using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Category
{
    public int CatId { get; set; }

    public string CatName { get; set; } = null!;

    public string CatKeyword { get; set; } = null!;

    public int CatStatus { get; set; }

    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

    public virtual ICollection<FamousArtwork> FamousArtworks { get; set; } = new List<FamousArtwork>();

    public virtual ICollection<Style> Styles { get; set; } = new List<Style>();
}
