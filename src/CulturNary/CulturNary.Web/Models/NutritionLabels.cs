namespace CulturNary.Web.Models
{
    public static class NutritionLabels
    {
        public static readonly Dictionary<string, string> Allergies = new Dictionary<string, string>
        {
            { "CeleryFree", "Celery-free" },
            { "CrustaceanFree", "Crustacean-free" },
            { "DairyFree", "Dairy-free" },
            { "EggFree", "Egg-free" },
            { "FishFree", "Fish-free" },
            { "GlutenFree", "Gluten-free" },
            { "LupineFree", "Lupine-free" },
            { "MustardFree", "Mustard-free" },
            { "PeanutFree", "Peanut-free" },
            { "SesameFree", "Sesame-free" },
            { "ShellfishFree", "Shellfish-free" },
            { "SoyFree", "Soy-free" },
            { "TreeNutFree", "Tree-Nut-free" },
            { "WheatFree", "Wheat-free" },
            { "FODMAPFree", "FODMAP-free" },
            { "PeanutSupportive", "Peanut-Supportive" }
        };

        public static readonly Dictionary<string, string> Diets = new Dictionary<string, string>
        {
            { "AlcoholFree", "Alcohol-free" },
            { "Balanced", "Balanced" },
            { "DASH", "DASH" },
            { "HighFiber", "High-Fiber" },
            { "HighProtein", "High-Protein" },
            { "Keto", "Keto" },
            { "KidneyFriendly", "Kidney friendly" },
            { "Kosher", "Kosher" },
            { "LowCarb", "Low-Carb" },
            { "LowFat", "Low-Fat" },
            { "LowPotassium", "Low potassium" },
            { "LowSodium", "Low-Sodium" },
            { "Mediterranean", "Mediterranean" },
            { "NoOilAdded", "No oil added" },
            { "NoSugar", "No-sugar" },
            { "Paleo", "Paleo" },
            { "Pescatarian", "Pescatarian" },
            { "PorkFree", "Pork-free" },
            { "RedMeatFree", "Red meat-free" },
            { "SugarConscious", "Sugar-conscious" },
            { "Vegan", "Vegan" },
            { "Vegetarian", "Vegetarian" },
            { "MolluskFree", "Mollusk-Free" },
            { "SulfiteFree", "Sulfite-Free" }
        };

        public static readonly Dictionary<string, string> Macronutrients = new Dictionary<string, string>
        {
            { "Fat", "Fat" },
            { "Saturated", "Saturated" },
            { "Trans", "Trans" },
            { "Monounsaturated", "Monounsaturated" },
            { "Polyunsaturated", "Polyunsaturated" },
            { "Carbs", "Carbs" },
            { "Fiber", "Fiber" },
            { "Sugars", "Sugars" },
            { "Protein", "Protein" },
            { "AddedSugar", "Added sugar" },
            { "Carbohydrate", "Carbohydrate" },
            { "Water", "Water" }
        };

        public static readonly Dictionary<string, string> Micronutrients = new Dictionary<string, string>
        {
            { "Cholesterol", "Cholesterol" },
            { "Sodium", "Sodium" },
            { "Calcium", "Calcium" },
            { "Magnesium", "Magnesium" },
            { "Potassium", "Potassium" },
            { "Iron", "Iron" },
            { "Phosphorus", "Phosphorus" },
            { "VitaminA", "Vitamin A" },
            { "VitaminC", "Vitamin C" },
            { "ThiaminB1", "Thiamin (B1)" },
            { "RiboflavinB2", "Riboflavin (B2)" },
            { "NiacinB3", "Niacin (B3)" },
            { "VitaminB6", "Vitamin B6" },
            { "FolateEquivalent", "Folate (Equivalent)" },
            { "VitaminB12", "Vitamin B12" },
            { "VitaminD", "Vitamin D" },
            { "VitaminE", "Vitamin E" },
            { "VitaminK", "Vitamin K" },
            { "FolateFood", "Folate, food" },
            { "FolicAcid", "Folic acid" },
            { "SugarAlcohols", "Sugar alcohols" },
            { "ZincZn", "Zinc, Zn" }
        };
    }
}
