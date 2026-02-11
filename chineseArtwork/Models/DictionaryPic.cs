using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class DictionaryPic
{
    public int DictPicId { get; set; }

    public int DictId { get; set; }

    public int StyleId { get; set; }

    public int? DictPicSort { get; set; }

    public string? DictName { get; set; }

    public string? DictPicture { get; set; }

    public string? DictCachePicture { get; set; }

    public string? DictImage { get; set; }

    public string DictCreateBy { get; set; } = null!;

    public DateTime? DictCreateDate { get; set; }

    public string DictUpdateBy { get; set; } = null!;

    public DateTime? DictUpdateDate { get; set; }

    public virtual Dictionary Dict { get; set; } = null!;

    public virtual Style Style { get; set; } = null!;
}
