using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class FamousArtist
{
    public int FmsArtId { get; set; }

    public string FmsArtName { get; set; } = null!;

    public string? FmsArtGender { get; set; }

    public string? FmsArtGivenName { get; set; }

    public string? FmsArtCourtesyName { get; set; }

    public string? FmsArtPseudonymName { get; set; }

    public string? FmsArtDescription { get; set; }

    public string? FmsArtPicture { get; set; }

    public string? FmsArtImage { get; set; }

    public virtual ICollection<FamousArtwork> FamousArtworks { get; set; } = new List<FamousArtwork>();
}
