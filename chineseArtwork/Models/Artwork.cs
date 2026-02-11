using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Artwork
{
    public int AwId { get; set; }

    public int ArtId { get; set; }

    public int CatId { get; set; }

    public int StyleId { get; set; }

    public string AwTitle { get; set; } = null!;

    public string AwDescription { get; set; } = null!;

    public decimal AwPrice { get; set; }

    public DateTime? AwCreated { get; set; }

    public string? AwDimension { get; set; }

    public int AwIsForSale { get; set; }

    public int AwStatus { get; set; }

    public int AwIsDel { get; set; }

    public DateTime? AwCreateDate { get; set; }

    public DateTime? AwUpdateDate { get; set; }

    public DateTime? AwSoldDate { get; set; }

    public virtual Artist Art { get; set; } = null!;

    public virtual ICollection<ArtworkPic> ArtworkPics { get; set; } = new List<ArtworkPic>();

    public virtual Category Cat { get; set; } = null!;

    public virtual Style Style { get; set; } = null!;
}
