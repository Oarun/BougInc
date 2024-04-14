using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class FavoriteRecipe
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string? RecipeId { get; set; }

    public DateTime FavoriteDate { get; set; }

    public string? ImageUrl { get; set; }

    public string? Label { get; set; }

    public string? Uri { get; set; }

    public string? Tags { get; set; }
}
