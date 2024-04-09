using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CulturNary.DAL.Concrete
{
    public class FavoriteRecipeRepository : Repository<FavoriteRecipe>, IFavoriteRecipeRepository
    {
        public FavoriteRecipeRepository(DbContext ctx) : base(ctx)
        {
        }

        // Implement the missing methods from IRepository<FavoriteRecipe> here. For example:
        public override FavoriteRecipe FindById(int id)
        {
            return _context.Set<FavoriteRecipe>().Find(id);
        }

        public override IQueryable<FavoriteRecipe> GetAll()
        {
            return _context.Set<FavoriteRecipe>();
        }

        // Implement the rest of the missing methods...
    }
}