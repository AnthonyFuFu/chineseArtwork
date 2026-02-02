using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Association
{
    public int AssocId { get; set; }

    public int ArtId { get; set; }

    public int GroupId { get; set; }

    public DateOnly? AssocJoinDate { get; set; }

    public string? AssocRole { get; set; }

    public int AssocStatus { get; set; }

    public DateOnly? AssocPaymentDate { get; set; }

    public DateOnly? AssocValidUntil { get; set; }

    public string? AssocRemark { get; set; }

    public virtual Artist Art { get; set; } = null!;

    public virtual GroupInfo Group { get; set; } = null!;
}
