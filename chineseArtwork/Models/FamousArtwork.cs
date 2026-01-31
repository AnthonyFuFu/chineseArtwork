using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class FamousArtwork
{
    public int FmsAwId { get; set; }

    public int FmsArtId { get; set; }

    public int CatId { get; set; }

    public int StyleId { get; set; }

    public string FmsAwTitle { get; set; } = null!;

    public string? FmsAwDimension { get; set; }

    public int FmsAwStatus { get; set; }

    public DateTime? FmsAwCreateTime { get; set; }

    public DateTime? FmsAwUpdateTime { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<FamousArtworkPic> FamousArtworkPics { get; set; } = new List<FamousArtworkPic>();

    public virtual FamousArtist FmsArt { get; set; } = null!;

    public virtual Style Style { get; set; } = null!;
}
