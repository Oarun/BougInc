using CulturNary.Web.Models;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;

namespace CulturNary.DAL.Abstract
{
    public interface IPersonRepository : IRepository<Person>
    {
        public Person GetPersonByIdentityId(string identityId);
        public Task<List<SiteUser>> GetUsersWithDietaryRestrictions(FriendSearchModel model, string currentUserId);
    }
}