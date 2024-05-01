using System.Collections.Generic;

namespace CulturNary.Web.Models
{
    public class ImageRecognitionResult
    {
        public OpenAIResponse response { get; set; }

        public string imageUrl { get; set; }
    }

    public class OpenAIResponse
    {
        public OpenAIData[] data { get; set; }
    }

    public class OpenAIData
    {
        public OpenAILabel[] labels { get; set; }
    }

    public class OpenAILabel
    {
        public string name { get; set; }
    }


    // public class ImageRecognitionResult
    // {
    //     public string analysis_Id { get; set; }
    //     public List<Scopes> scopes { get; set; }
    //     public List<AnalysisItem> items { get; set; }
    // }

    // public class Scopes
    // {
    //     public string multiple_Items { get; set; }
    //     public string nutritionMacro { get; set; }
    //     public string nutritionMicro { get; set; }
    //     public string nutritionNutriScore { get; set; }
    // }

    // public class AnalysisItem
    // {
    //     public AnalysisItemPosition? position { get; set; }
    //     public AnalysisFood food { get; set; }
    // }

    // public class AnalysisItemPosition
    // {
    //     public int x { get; set; }
    //     public int y { get; set; }
    //     public int width { get; set; }
    //     public int height { get; set; }
    // }

    // public class AnalysisFood
    // {
    //     public double confidence { get; set; }
    //     public double? quantity { get; set; }
    //     public FoodInfo foodInfo { get; set; }
    //     public List<AnalysisFood>? ingredients { get; set; }
    // }

    // public class FoodInfo
    // {
    //     public string food_Id { get; set; }
    //     public string display_Name { get; set; }
    //     public double g_Per_Serving { get; set; }
    //     public FVGrade? fv_grade { get; set; }
    //     public ImgNutrition? nutrition { get; set; }
    // }

    // public class FVGrade
    // {
    //     public string grade { get; set; }
    // }

    // public class ImgNutrition
    // {    
    //     // Required
    //     public double calories_100g { get; set; }

    //     // Optional
    //     public double? proteins_100g { get; set; }
    //     public double? fat_100g { get; set; }
    //     public double? carbs_100g { get; set; }
    //     public double? fibers_100g { get; set; }
    //     public double? alcohol_100g { get; set; }
    //     public double? calcium_100g { get; set; }
    //     public double? chloride_100g { get; set; }
    //     public double? cholesterol_100g { get; set; }
    //     public double? copper_100g { get; set; }
    //     public double? glycemic_index { get; set; }
    //     public double? insat_fat_100g { get; set; }
    //     public double? iodine_100g { get; set; }
    //     public double? iron_100g { get; set; }
    //     public double? magnesium_100g { get; set; }
    //     public double? manganese_100g { get; set; }
    //     public double? mono_fat_100g { get; set; }
    //     public double? omega_3_100g { get; set; }
    //     public double? omega_6_100g { get; set; }
    //     public double? phosphorus_100g { get; set; }
    //     public double? poly_fat_100g { get; set; }
    //     public double? polyols_100g { get; set; }
    //     public double? potassium_100g { get; set; }
    //     public double? salt_100g { get; set; }
    //     public double? sat_fat_100g { get; set; }
    //     public double? selenium_100g { get; set; }
    //     public double? sodium_100g { get; set; }
    //     public double? sugars_100g { get; set; }
    //     public double? veg_percent { get; set; }
    //     public double? vitamin_a_beta_k_100g { get; set; }
    //     public double? vitamin_a_retinol_100g { get; set; }
    //     public double? vitamin_b1_100g { get; set; }
    //     public double? vitamin_b12_100g { get; set; }
    //     public double? vitamin_b2_100g { get; set; }
    //     public double? vitamin_b3_100g { get; set; }
    //     public double? vitamin_b5_100g { get; set; }
    //     public double? vitamin_b6_100g { get; set; }
    //     public double? vitamin_b9_100g { get; set; }
    //     public double? vitamin_c_100g { get; set; }
    //     public double? vitamin_d_100g { get; set; }
    //     public double? vitamin_e_100g { get; set; }
    //     public double? vitamin_k1_100g { get; set; }
    //     public double? water_100g { get; set; }
    //     public double? zinc_100g { get; set; }
    // }

}