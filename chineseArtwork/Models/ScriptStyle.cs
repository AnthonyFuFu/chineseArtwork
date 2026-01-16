using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class ScriptStyle
{
    public int ScriptId { get; set; }

    public string ScriptWord { get; set; } = null!;

    public string ScriptDescription { get; set; } = null!;

    public virtual ICollection<Dictionary> Dictionaries { get; set; } = new List<Dictionary>();
}
