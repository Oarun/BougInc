using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CulturNary.DAL.Concrete
{
    public class FavoriteRecipeRepository : Repository<FavoriteRecipe>, IFavoriteRecipeRepository
    {

        public FavoriteRecipeRepository(CulturNaryDbContext context) : base(context)
        {
            
        }
        public List<FavoriteRecipe> GetFavoriteRecipeForPersonID(int personId){
            return base.Where(x => x.PersonId == personId).ToList();
        }
        public void DeleteAllRecipeForPersonID(int personId){
            var favoriteRecipes = GetFavoriteRecipeForPersonID(personId);
            foreach (var recipe in favoriteRecipes)
            {
                base.Delete(recipe);
            }
        }
        public FavoriteRecipe GetFavoriteRecipeForPersonIDAndRecipeID(int personId, int Id){
            return base.Where(x => x.PersonId == personId && x.Id == Id).FirstOrDefault();
        }
        public FavoriteRecipe GetFavoriteRecipeByRecipeId(string recipeId){
            return base.Where(x => x.RecipeId == recipeId).FirstOrDefault();
        }
        public List<FavoriteRecipe> SearchFavoriteRecipesForPersonID(int personId, string search){
            return base.Where(x => x.PersonId == personId && x.Label.Contains(search)).ToList();
        }
    }
}