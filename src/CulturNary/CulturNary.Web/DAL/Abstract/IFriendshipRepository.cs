using CulturNary.Web.Models;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;

namespace CulturNary.DAL.Abstract
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        public List<Friendship> GetFriendshipByPersonId(int personId);
        public Task<List<SiteUser>> GetFriends(string id);
        public bool AreFriends(string currentUserId, string friendId);
        public bool AcceptFriendRequest(string currentUserId, string requestId);
        public bool RejectFriendRequest(string currentUserId, string requestId);
        public bool RemoveFriend(string currentUserId, string friendId);
    }
}
