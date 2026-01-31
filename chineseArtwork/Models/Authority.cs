using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Authority
{
    public int ArtId { get; set; }

    public int FuncId { get; set; }

    public int AuthStatus { get; set; }

    public virtual Artist Art { get; set; } = null!;

    public virtual Function Func { get; set; } = null!;
}
