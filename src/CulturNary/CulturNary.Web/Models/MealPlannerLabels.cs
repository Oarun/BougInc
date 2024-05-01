namespace CulturNary.Web.Models
{
    public static class MealPlannerLabels
    {
        public static readonly Dictionary<string, List<string>> MealOptions = new Dictionary<string, List<string>>
        {
            {
                "Breakfast", new List<string>
                {
                    "Alcohol cocktail", "Biscuits and cookies", "Bread", "Cereals",
                    "Condiments and sauces", "Desserts", "Drinks", "Egg",
                    "Ice cream and custard", "Main course", "Pancake", "Pasta",
                    "Pastry", "Pies and tarts", "Preps", "Preserve",
                    "Salad", "Sandwiches", "Seafood", "Side dish",
                    "Soup", "Special occasions", "Starter", "Sweets"
                }
            },
            {
                "Lunch", new List<string>
                {
                    "Alcohol cocktail", "Biscuits and cookies", "Bread", "Cereals",
                    "Condiments and sauces", "Desserts", "Drinks", "Egg",
                    "Ice cream and custard", "Main course", "Pancake", "Pasta",
                    "Pastry", "Pies and tarts", "Preps", "Preserve",
                    "Salad", "Sandwiches", "Seafood", "Side dish",
                    "Soup", "Special occasions", "Starter", "Sweets"
                }
            },
            {
                "Dinner", new List<string>
                {
                    "Alcohol cocktail", "Biscuits and cookies", "Bread", "Cereals",
                    "Condiments and sauces", "Desserts", "Drinks", "Egg",
                    "Ice cream and custard", "Main course", "Pancake", "Pasta",
                    "Pastry", "Pies and tarts", "Preps", "Preserve",
                    "Salad", "Sandwiches", "Seafood", "Side dish",
                    "Soup", "Special occasions", "Starter", "Sweets"
                }
            }
        };
        public static readonly List<string> MealSelections = new List<string>
        {
            "Breakfast",
            "Lunch",
            "Dinner"
        };
        public static readonly List<(int Number, string Description)> MealPlanSteps = new List<(int, string)>
        {
            (1, "Choose meals per day"),
            (2, "Set filters for all meals"),
            (3, "Set your meals")
        };
    }
}
