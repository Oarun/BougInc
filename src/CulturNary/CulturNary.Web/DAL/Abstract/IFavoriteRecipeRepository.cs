using CulturNary.Web.Models;
using System.Threading.Tasks;

namespace CulturNary.DAL.Abstract
{
    public interface IFavoriteRecipeRepository : IRepository<FavoriteRecipe>
    {
        public FavoriteRecipe Add(FavoriteRecipe favoriteRecipes);
    }
}