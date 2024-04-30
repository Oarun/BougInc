// using CulturNary.Web.Areas.Identity.Data;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using CulturNary.Web.Models;
// namespace CulturNary.DAL.Abstract
// {
//     public interface IFriendRequestRepository : IRepository<FriendRequest>
//     {
//         bool IsFriendRequestPending(string currentUserId, string userId);
//         Task<List<SiteUser>> GetFriendRequests(string Id);
//         Task SendFriendRequest(string currentUserId, string userId);
//         Task AcceptFriendRequest(string currentUserId, string userId);
//         Task RejectFriendRequest(string currentUserId, string userId);
//         Task CancelFriendRequest(string currentUserId, string userId);
//         FriendRequest GetByRequestAndRecipientId(int requestId, int recipientId);
//     }
// }