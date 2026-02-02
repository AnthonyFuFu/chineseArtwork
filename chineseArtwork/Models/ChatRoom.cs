using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class ChatRoom
{
    public int RoomId { get; set; }

    public string? RoomUrl { get; set; }

    public int RoomStatus { get; set; }

    public int RoomUpdateStatus { get; set; }

    public DateTime? RoomLastUpdate { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
