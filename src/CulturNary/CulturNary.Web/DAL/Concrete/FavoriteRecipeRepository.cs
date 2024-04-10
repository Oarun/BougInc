using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CulturNary.DAL.Concrete
{
    public class FavoriteRecipeRepository : Repository<FavoriteRecipe>, IFavoriteRecipeRepository
    {
        private readonly CulturNaryDbContext _context;

        public FavoriteRecipeRepository(CulturNaryDbContext context) : base(context)
        {
            _context = context;
        }

        // Implement the missing methods from IRepository<FavoriteRecipes> here. For example:
        public override FavoriteRecipe FindById(int id)
        {
            return _context.FavoriteRecipes.Find(id);
        }

        public override IQueryable<FavoriteRecipe> GetAll()
        {
            return _context.FavoriteRecipes;
        }
        public async Task AddFavoriteRecipe(FavoriteRecipe favoriteRecipes)
        {
            _context.FavoriteRecipes.Add(favoriteRecipes);
            await _context.SaveChangesAsync();
        }
        // Implement the rest of the missing methods...
    }
}