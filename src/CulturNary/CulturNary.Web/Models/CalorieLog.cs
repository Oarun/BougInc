using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models;

public partial class CalorieLog
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int? CaloriesLogged { get; set; }

    public DateOnly? LogDate { get; set; }

    public virtual Person Person { get; set; } = null!;
}
