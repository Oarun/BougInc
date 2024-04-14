using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public int? CollectionId { get; set; }

    public int? PersonId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Img { get; set; }

    public string? Uri { get; set; }

    public virtual Collection? Collection { get; set; }

    public virtual Person? Person { get; set; }
}
