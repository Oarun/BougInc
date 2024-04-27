

namespace CulturNary.DAL.Abstract
{
    public interface IFriendRequestRepository
    {
        bool IsFriendRequestPending(string currentUserId, string userId);
        Task SendFriendRequest(string currentUserId, string userId);
        Task AcceptFriendRequest(string currentUserId, string userId);
        Task RejectFriendRequest(string currentUserId, string userId);
        Task CancelFriendRequest(string currentUserId, string userId);
    }
}