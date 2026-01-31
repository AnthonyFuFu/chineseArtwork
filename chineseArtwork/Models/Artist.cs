using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Artist
{
    public int ArtId { get; set; }

    public string ArtName { get; set; } = null!;

    public string ArtPhone { get; set; } = null!;

    public string? ArtGender { get; set; }

    public string? ArtGivenName { get; set; }

    public string? ArtCourtesyName { get; set; }

    public string? ArtPseudonymName { get; set; }

    public DateOnly? ArtBirthday { get; set; }

    public string? ArtPicture { get; set; }

    public string? ArtImage { get; set; }

    public string ArtAccount { get; set; } = null!;

    public string ArtPassword { get; set; } = null!;

    public string ArtEmail { get; set; } = null!;

    public int ArtStatus { get; set; }

    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();

    public virtual ICollection<Authority> Authorities { get; set; } = new List<Authority>();
}
