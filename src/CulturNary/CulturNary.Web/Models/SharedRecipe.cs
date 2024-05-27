using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class SharedRecipe
{
    public int Id { get; set; }

    public int SharerId { get; set; }

    public int SharedWithId { get; set; }

    public int FavoriteRecipeId { get; set; }

    public DateTime ShareDate { get; set; }

    public virtual FavoriteRecipe FavoriteRecipe { get; set; } = null!;

    public virtual Person SharedWith { get; set; } = null!;

    public virtual Person Sharer { get; set; } = null!;
}
