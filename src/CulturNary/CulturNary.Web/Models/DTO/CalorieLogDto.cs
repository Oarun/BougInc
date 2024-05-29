using System;
using System.Collections.Generic;

namespace CulturNary.Web.Models.DTO;

public class CalorieLogDto
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int? CaloriesLogged { get; set; }

    public DateOnly? LogDate { get; set; }

}