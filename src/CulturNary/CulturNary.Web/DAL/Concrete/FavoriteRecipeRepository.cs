using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CulturNary.DAL.Concrete
{
    public class FavoriteRecipeRepository : Repository<FavoriteRecipe>, IFavoriteRecipeRepository
    {
        private readonly IPersonRepository _personRepository;

        public FavoriteRecipeRepository(CulturNaryDbContext context, IPersonRepository personRepository) : base(context)
        {
            _personRepository = personRepository;
        }
        public List<FavoriteRecipe> GetFavoriteRecipeForPersonID(int personId){
            return base.Where(x => x.PersonId == personId).ToList();
        }
        public void DeleteAllRecipeForPersonID(string identityId)
        {
            var person = _personRepository.GetPersonByIdentityId(identityId);
            if (person != null)
            {
                var favoriteRecipes = GetFavoriteRecipeForPersonID(person.Id);
                foreach (var recipe in favoriteRecipes)
                {
                    base.Delete(recipe);
                }   
            }
        }
        public FavoriteRecipe GetFavoriteRecipeForPersonIDAndRecipeID(int personId, int Id){
            return base.Where(x => x.PersonId == personId && x.Id == Id).FirstOrDefault();
        }
        public FavoriteRecipe GetFavoriteRecipeByRecipeId(string recipeId){
            return base.Where(x => x.RecipeId == recipeId).FirstOrDefault();
        }
        public List<FavoriteRecipe> SearchFavoriteRecipesForPersonID(int personId, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return base.Where(x => x.PersonId == personId).ToList();
            }
            else
            {
                return base.Where(x => x.PersonId == personId && x.Label.Contains(search)).ToList();
            }
        }
    }
}