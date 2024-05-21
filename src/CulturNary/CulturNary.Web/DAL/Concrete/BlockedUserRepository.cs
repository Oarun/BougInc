using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CulturNary.DAL.Concrete{
    public class BlockedUserRepository : Repository<BlockedUser>, IBlockedUserRepository
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly IPersonRepository _personRepository;
        public BlockedUserRepository(CulturNaryDbContext context, IPersonRepository personRepository, UserManager<SiteUser> userManager) : base(context)
        {
            _userManager = userManager;
            _personRepository = personRepository;
        }
        public bool BlockUser(string currentUserId, string userId){
            base.Add(new BlockedUser
            {
                BlockerIdentityId = currentUserId,
                BlockedIdentityId = userId,
                BlockDate = DateTime.Now
            });
            return true; // You need to return a value since the method signature expects a bool
        }
        public bool UnblockUser(string currentUserId, string userId){
            var blockedUser = base.Where(x => x.BlockerIdentityId == currentUserId && x.BlockedIdentityId == userId).FirstOrDefault();
            if(blockedUser != null){
                base.Delete(blockedUser);
                return true; // User was found and unblocked
            }
            return false; // User was not found
        }
        public bool IsUserBlocked(string currentUserId, string userId){
            return base.Where(x => x.BlockerIdentityId == currentUserId && x.BlockedIdentityId == userId).Any();
        }
        public List<SiteUser> GetBlockedUsers(string id){
            var blockedIds = base
                .Where(f => f.BlockerIdentityId == id)
                .Select(f => f.BlockedIdentityId)
                .ToList();
            var blockedUsers = _userManager.Users
                .Where(u => blockedIds.Contains(u.Id))
                .ToList();
            return blockedUsers;
        }
    }
}