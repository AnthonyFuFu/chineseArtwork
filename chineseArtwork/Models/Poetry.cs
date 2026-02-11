using System;
using System.Collections.Generic;

namespace chineseArtwork.Models;

public partial class Poetry
{
    public int PoetryId { get; set; }

    public int AuthorId { get; set; }

    public string PoetryTitle { get; set; } = null!;

    public string? PoetryContent { get; set; }

    public int? PoetryWordCount { get; set; }

    public string? PoetryAnalysis { get; set; }

    public string? PoetryCategory { get; set; }

    public string? PoetryKeywords { get; set; }

    public string PoetryCreateBy { get; set; } = null!;

    public DateTime? PoetryCreateDate { get; set; }

    public string PoetryUpdateBy { get; set; } = null!;

    public DateTime? PoetryUpdateDate { get; set; }

    public string? PoetryTranslation { get; set; }

    public virtual Author Author { get; set; } = null!;
}
