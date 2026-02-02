using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string NewsName { get; set; } = null!;

    public string NewsContent { get; set; } = null!;

    public int NewsStatus { get; set; }

    public DateTime? NewsStart { get; set; }

    public DateTime? NewsEnd { get; set; }

    public string NewsCreateBy { get; set; } = null!;

    public DateTime? NewsCreateDate { get; set; }

    public string NewsUpdateBy { get; set; } = null!;

    public DateTime? NewsUpdateDate { get; set; }

    public virtual ICollection<Banner> Banners { get; set; } = new List<Banner>();
}
