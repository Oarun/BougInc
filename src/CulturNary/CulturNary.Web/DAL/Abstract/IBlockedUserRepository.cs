using CulturNary.Web.Areas.Identity.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using CulturNary.Web.Models;

namespace CulturNary.DAL.Abstract
{
    public interface IBlockedUserRepository: IRepository<BlockedUser>
    {
        public bool BlockUser(string currentUserId, string userId);
        public bool UnblockUser(string currentUserId, string userId);
        public bool IsUserBlocked(string currentUserId, string userId);
        public List<SiteUser> GetBlockedUsers(string id);
    }
}