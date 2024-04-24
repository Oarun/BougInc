using CulturNary.Web.Models;
using System.Threading.Tasks;

namespace CulturNary.DAL.Abstract
{
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
        public FriendRequest SendFriendRequestToPerson(int requesterId, int recipientId);
        public FriendRequest AcceptFriendRequest(int requesterId, int recipientId);
        public FriendRequest RejectFriendRequest(int requesterId, int recipientId);
        public FriendRequest GetFriendRequestByRequesterIdAndRecipientId(int requesterId, int recipientId);
    }
}