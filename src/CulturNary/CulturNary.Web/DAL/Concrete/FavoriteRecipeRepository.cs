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
        public FavoriteRecipe Add(FavoriteRecipe favoriteRecipes)
        {
            var entityEntry = _dbSet.Add(favoriteRecipes);
            return entityEntry.Entity;
        }
    }
}