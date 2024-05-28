using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CulturNary.DAL.Concrete{
    public class SharedRecipeRepository : Repository<SharedRecipe>, ISharedRecipeRepository
    {
        public SharedRecipeRepository(DbContext context) : base(context)
        {
        }
        public List<SharedRecipe> GetSharedRecipesBySharedWithId(int sharedWithId)
        {
            return base.GetAll()
                .Where(x => x.SharedWithId == sharedWithId)
                .GroupBy(x => x.FavoriteRecipeId)
                .Select(g => g.First())
                .ToList();
        }
        public bool SendRecipeToFriend(int sharerId, int sharedWithId, int favoriteRecipeId){
            var sharedRecipe = new SharedRecipe{
                SharerId = sharerId,
                SharedWithId = sharedWithId,
                FavoriteRecipeId = favoriteRecipeId,
                ShareDate = DateTime.Now
            };
            base.Add(sharedRecipe);
            return true;
        }
        
        public bool DeleteSharedRecipe(int sharedRecipeId){
            var sharedRecipe = base.FindById(sharedRecipeId);
            if(sharedRecipe == null){
                return false;
            }
            base.Delete(sharedRecipe);
            return true;
        }
        public List<SharedRecipe> GetSharedRecipesByFavoriteRecipeId(int favoriteRecipeId){
            return base.Where(x => x.FavoriteRecipeId == favoriteRecipeId).ToList();
        }
    }
}