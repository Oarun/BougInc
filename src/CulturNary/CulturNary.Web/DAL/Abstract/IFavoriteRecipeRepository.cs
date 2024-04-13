using CulturNary.Web.Models;
using System.Threading.Tasks;

namespace CulturNary.DAL.Abstract
{
    public interface IFavoriteRecipeRepository : IRepository<FavoriteRecipe>
    {
        public FavoriteRecipe Add(FavoriteRecipe favoriteRecipes);
        public List<FavoriteRecipe> GetFavoriteRecipeForPersonID(int personId);
        public void DeleteAllRecipeForPersonID(int personId);
        public FavoriteRecipe GetFavoriteRecipeForPersonIDAndRecipeID(int personId, int Id);
        public FavoriteRecipe GetFavoriteRecipeByRecipeId(string recipeId);
    }
}