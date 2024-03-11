using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Collection
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Tags { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
