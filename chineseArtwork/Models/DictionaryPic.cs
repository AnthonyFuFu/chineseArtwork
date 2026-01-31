using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class DictionaryPic
{
    public int DictPicId { get; set; }

    public int DictId { get; set; }

    public int StyleId { get; set; }

    public int? DictPicSort { get; set; }

    public string? DictPicture { get; set; }

    public string? DictImage { get; set; }

    public virtual Dictionary Dict { get; set; } = null!;

    public virtual Style Style { get; set; } = null!;
}
