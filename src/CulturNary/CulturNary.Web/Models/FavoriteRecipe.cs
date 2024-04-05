using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class FavoriteRecipe
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int RecipeId { get; set; }

    public DateTime FavoriteDate { get; set; }

    public string? Tags { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
