using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Dictionary
{
    public int DictId { get; set; }

    public int RadicalId { get; set; }

    public string DictWord { get; set; } = null!;

    public string? DictDescription { get; set; }

    public int DictStrokes { get; set; }

    public int DictStatus { get; set; }

    public int DictIsDel { get; set; }

    public virtual ICollection<DictionaryPic> DictionaryPics { get; set; } = new List<DictionaryPic>();

    public virtual Radical Radical { get; set; } = null!;
}
