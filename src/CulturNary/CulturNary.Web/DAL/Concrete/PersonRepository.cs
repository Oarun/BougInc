using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CulturNary.DAL.Concrete
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(CulturNaryDbContext context, UserManager<SiteUser> userManager, ILogger<PersonRepository> logger) : base(context) // Add this line
        {
            _userManager = userManager;
            _logger = logger;
        }
        public Person GetPersonByIdentityId(string identityId){
            return base.Where(x => x.IdentityId == identityId).FirstOrDefault();
        }
        public async Task<Dictionary<SiteUser, double>> GetUsersWithDietaryRestrictions(string currentUserId)
        {
            // Fetch the current user and their dietary restrictions
            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var currentUserDietaryRestrictions = currentUser.GetDietaryRestrictionsActiveArray();
            _logger.LogInformation($"Current user dietary restrictions: {string.Join(", ", currentUserDietaryRestrictions)}");

            var users = await _userManager.Users
                .Where(user => user.Id != currentUserId)
                .ToListAsync();
            

            var userScores = new Dictionary<SiteUser, double>();

            foreach (var user in users)
            {
                var userDietaryRestrictions = user.GetDietaryRestrictionsActiveArray();
                _logger.LogInformation($"User {user.Id} dietary restrictions: {string.Join(", ", userDietaryRestrictions)}");

                double score = 0;
                if (currentUserDietaryRestrictions.Any() && userDietaryRestrictions.Any())
                {
                    var activeCurrentUserRestrictions = currentUserDietaryRestrictions
                        .Select((restriction, index) => new { Restriction = restriction, Index = index })
                        .Where(x => x.Restriction)
                        .Select(x => x.Index);

                    score = activeCurrentUserRestrictions
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