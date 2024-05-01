//using System;
//using System.Collections.Generic;

//namespace CulturNary.Web.Models.DTO
//{

//    public class MealPlannerDto
//    {
//        public int Size { get; set; }
//        public PlanDto Plan { get; set; }
//    }

//    public class PlanDto
//    {
//        public AcceptDto Accept { get; set; }
//        public FitDto Fit { get; set; }
//        public SectionsDto Sections { get; set; }
//    }

//    public class AcceptDto
//    {
//        public List<AllDto> All { get; set; }
//    }

//    public class FitDto
//    {
//        public EnercKcalDto Enerc_KCAL { get; set; }
//        public SugarAddedDto Sugar_ADDED { get; set; }
//    }

//    public class SectionsDto
//    {
//        public MealDto Breakfast { get; set; }
//        public MealDto Lunch { get; set; }
//        public MealDto Dinner { get; set; }
//    }

//    public class MealDto
//    {
//        public AcceptMealsDto Accept { get; set; }
//    }

//    public class AllDto
//    {
//        public List<HealthDto> Health { get; set; }
//    }

//    public class HealthDto
//    {
//        public int Id { get; set; }
//        public int AllId { get; set; }
//        public string HealthName { get; set; }
//    }

//    public class AcceptMealsDto
//    {
//        public List<AllMealsDto> All { get; set; }
//        public FitDto Fit { get; set; }
//    }

//    public class AllMealsDto
//    {
//        public List<AllMealsDto> Dish { get; set; }
//        public List<AllMealsDto> Meal { get; set; }
//    }

//    public class EnercKcalDto
//    {
//        public int Min { get; set; }
//        public int Max { get; set; }
//    }

//    public class SugarAddedDto
//    {
//        public int Min { get; set; }
//        public int Max { get; set; }
//    }
//}