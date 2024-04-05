
namespace CulturNary.DAL.Concrete{
    public class FavoriteRecipeRepository : Repository<FavoriteRecipe>, IFavoriteRecipeRepository{
        public FavoriteRecipeRepository(DbContext ctx) : base(ctx){
            
        }
    }
}