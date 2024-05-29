using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models.DTO;

public class CalorieTrackerDto
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int? PersonCalories { get; set; }

}