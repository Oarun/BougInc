using CulturNary.Web.Models;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using System.Collections.Generic; // This is needed for List<>

namespace CulturNary.DAL.Abstract
{
    public interface ISharedRecipeRepository : IRepository<SharedRecipe>
    {
        public List<SharedRecipe> GetSharedRecipesBySharedWithId(int sharedWithId);
        public bool SendRecipeToFriend(int sharerId, int sharedWithId, int favoriteRecipeId);
        public bool DeleteSharedRecipe(int sharedRecipeId);
        public List<SharedRecipe> GetSharedRecipesByFavoriteRecipeId(int favoriteRecipeId);
    }
}