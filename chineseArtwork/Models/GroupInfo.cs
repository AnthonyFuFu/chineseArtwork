using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class GroupInfo
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public string? GroupDescription { get; set; }

    public string? GroupPhone { get; set; }

    public string? GroupEmail { get; set; }

    public string? GroupAddress { get; set; }

    public DateOnly? GroupEstablishedDate { get; set; }

    public string? GroupOwner { get; set; }

    public int GroupStatus { get; set; }

    public virtual ICollection<Association> Associations { get; set; } = new List<Association>();
}
