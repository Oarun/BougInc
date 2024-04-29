using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CulturNary.DAL.Concrete
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly ILogger<PersonRepository> _logger;
        public PersonRepository(CulturNaryDbContext context, UserManager<SiteUser> userManager, ILogger<PersonRepository> logger) : base(context)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public Person GetPersonByIdentityId(string identityId){
            return base.Where(x => x.IdentityId == identityId).FirstOrDefault();
        }
        public async Task<Dictionary<SiteUser, double>> GetUsersWithDietaryRestrictions(FriendSearchModel model, string currentUserId)
        {
            var modelDietaryRestrictions = model.GetDietaryRestrictionsActiveArray();
            _logger.LogInformation($"Model dietary restrictions: {string.Join(", ", modelDietaryRestrictions)}");

            var users = await _userManager.Users
                .Where(user => user.Id != currentUserId)
                .ToListAsync();

            var userScores = new Dictionary<SiteUser, double>();

            foreach (var user in users)
            {
                var userDietaryRestrictions = user.GetDietaryRestrictionsActiveArray();
                _logger.LogInformation($"User {user.Id} dietary restrictions: {string.Join(", ", userDietaryRestrictions)}");

                double score = 0;
                if (modelDietaryRestrictions.Any() && userDietaryRestrictions.Any())
                {
                    var activeModelRestrictions = modelDietaryRestrictions
                        .Select((restriction, index) => new { Restriction = restriction, Index = index })
                        .Where(x => x.Restriction)
                        .Select(x => x.Index);

                    score = activeModelRestrictions
                        .Select(index => userDietaryRestrictions[index] ? 1 : 0)
                        .DefaultIfEmpty(0)
                        .Average();
                }
                userScores[user] = Math.Round(score * 100, 2);
            }

            var orderedUserScores = userScores
                .OrderByDescending(kv => kv.Value) // Use Value instead of Score
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return orderedUserScores;
        }
        public Person GetPersonByPersonId(int personId){
            return base.Where(x => x.Id == personId).FirstOrDefault();
        }
    }
}