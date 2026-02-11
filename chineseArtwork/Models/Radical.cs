using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Radical
{
    public int RadicalId { get; set; }

    public string RadicalWord { get; set; } = null!;

    public int? RadicalStrokes { get; set; }

    public int? RadicalSort { get; set; }

    public virtual ICollection<Dictionary> Dictionaries { get; set; } = new List<Dictionary>();

    public virtual ICollection<RadicalPic> RadicalPics { get; set; } = new List<RadicalPic>();
}
