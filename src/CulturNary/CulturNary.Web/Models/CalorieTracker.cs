using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class CalorieTracker
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int? PersonCalories { get; set; }

    public virtual Person Person { get; set; } = null!;
}
