using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public int DynastyId { get; set; }

    public string AuthorName { get; set; } = null!;

    public string? AuthorGivenName { get; set; }

    public string? AuthorCourtesyName { get; set; }

    public string? AuthorPseudonymName { get; set; }

    public string? AuthorDescription { get; set; }

    public virtual Dynasty Dynasty { get; set; } = null!;

    public virtual ICollection<Poetry> Poetries { get; set; } = new List<Poetry>();
}
