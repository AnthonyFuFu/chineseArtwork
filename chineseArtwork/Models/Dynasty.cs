using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Dynasty
{
    public int DynastyId { get; set; }

    public string DynastyName { get; set; } = null!;

    public string? DynastyDescription { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
