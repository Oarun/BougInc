using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? IdentityId { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
