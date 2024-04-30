using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class Restaurant
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string RestaurantsName { get; set; } = null!;

    public string? RestaurantsAddress { get; set; }

    public string? RestaurantsWebsite { get; set; }

    public string? RestaurantsMenu { get; set; }

    public string? RestaurantsPhoneNumber { get; set; }

    public string? RestaurantsNotes { get; set; }

    public string? RestaurantType { get; set; }

    public virtual Person? Person { get; set; }
}
