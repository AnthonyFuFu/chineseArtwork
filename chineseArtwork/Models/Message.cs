using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Message
{
    public int MsgId { get; set; }

    public int ArtId { get; set; }

    public int MemId { get; set; }

    public int RoomId { get; set; }

    public string MsgContent { get; set; } = null!;

    public DateTime? MsgTime { get; set; }

    public int MsgDirection { get; set; }

    public string? MsgPicture { get; set; }

    public string? MsgImage { get; set; }

    public virtual Member Mem { get; set; } = null!;

    public virtual ChatRoom Room { get; set; } = null!;
}
