using CulturNary.Web.Models;
using System.Threading.Tasks;

namespace CulturNary.DAL.Abstract
{
    public interface IFavoriteRecipeRepository : IRepository<FavoriteRecipe>
    {
        public FavoriteRecipe Add(FavoriteRecipe favoriteRecipes);
        public List<FavoriteRecipe> GetFavoriteRecipeForPersonID(int personId);
        public void DeleteAllRecipeForPersonID(string identityId);
        public FavoriteRecipe GetFavoriteRecipeForPersonIDAndRecipeID(int personId, int Id);
        public FavoriteRecipe GetFavoriteRecipeByRecipeId(string recipeId);
        public List<FavoriteRecipe> SearchFavoriteRecipesForPersonID(int personId, string search);
    }
}