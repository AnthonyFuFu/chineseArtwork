using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Member
{
    public int MemId { get; set; }

    public string MemName { get; set; } = null!;

    public string MemAccount { get; set; } = null!;

    public string MemPassword { get; set; } = null!;

    public string? MemGender { get; set; }

    public string? MemPhone { get; set; }

    public string MemEmail { get; set; } = null!;

    public string? MemAddress { get; set; }

    public DateOnly? MemBirthday { get; set; }

    public string? MemPicture { get; set; }

    public string? MemImage { get; set; }

    public int MemStatus { get; set; }

    public int MemVerificationStatus { get; set; }

    public string? MemVerificationCode { get; set; }

    public string? MemGoogleUid { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
