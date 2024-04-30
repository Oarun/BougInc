using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Video
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string VideoName { get; set; } = null!;

    public string? VideoType { get; set; }

    public string? VideoLink { get; set; }

    public string? VideoNotes { get; set; }

    public virtual Person? Person { get; set; }
}
