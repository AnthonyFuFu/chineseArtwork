using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Function
{
    public int FuncId { get; set; }

    public string FuncName { get; set; } = null!;

    public string FuncLayer { get; set; } = null!;

    public string FuncParentId { get; set; } = null!;

    public string FuncLink { get; set; } = null!;

    public int? FuncStatus { get; set; }

    public string? FuncIcon { get; set; }

    public virtual ICollection<Authority> Authorities { get; set; } = new List<Authority>();
}
