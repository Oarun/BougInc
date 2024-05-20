using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CulturNary.DAL.Concrete{
    public class FriendRequestRepository : Repository<FriendRequest>, IFriendRequestRepository
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly IPersonRepository _personRepository;
        public FriendRequestRepository(CulturNaryDbContext context, IPersonRepository personRepository, UserManager<SiteUser> userManager) : base(context)
        {
            _userManager = userManager;
            _personRepository = personRepository;
        }
        public Task AcceptFriendRequest(string currentUserId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task CancelFriendRequest(string currentUserId, string userId)
        {
            throw new NotImplementedException();
        }

        public bool IsFriendRequestPending(string currentUserId, string userId)
        {
            int currentUserPersonId = _personRepository.GetPersonByIdentityId(currentUserId).Id;
            int userPersonId = _personRepository.GetPersonByIdentityId(userId).Id;
            return base.Where(x => x.RequesterId == currentUserPersonId && x.RecipientId == userPersonId).Any();
        }

        public Task RejectFriendRequest(string currentUserId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task SendFriendRequest(string currentUserId, string userId)
        {
            int currentUserPersonId = _personRepository.GetPersonByIdentityId(currentUserId).Id;
            int userPersonId = _personRepository.GetPersonByIdentityId(userId).Id;
            base.Add(new FriendRequest
            {
                RequesterId = currentUserPersonId,
                RecipientId = userPersonId,
                RequestDate = DateTime.Now, // Set the DateTime field to the current date and time
                Status = "Pending"
            });
            
        }
        public async Task<List<SiteUser>> GetFriendRequests(string Id)
        {
            int userPersonId = _personRepository.GetPersonByIdentityId(Id).Id;
            var requesterIds = base
                .Where(f => f.RecipientId == userPersonId)
                .Select(f => f.RequesterId)
                .ToList();

            var friendRequests = new List<SiteUser>();
            foreach (var requesterId in requesterIds)
            {
                if (requesterId.HasValue)
                {
                    var person = _personRepository.GetPersonByPersonId(requesterId.Value);
                    if (person != null)
                    {
                        var user = await _userManager.FindByIdAsync(person.IdentityId);
                        if (user != null)
                        {
                            friendRequests.Add(user);
                        }
                    }
                }
                else
                {
                    // Handle the case where requesterId is null if needed
                    // For example, log a warning or continue with the next requesterId
                }
            }

            return friendRequests;
        }
        public FriendRequest GetByRequestAndRecipientId(int requestId, int recipientId){
            return base.Where(x => x.RequesterId == requestId && x.RecipientId == recipientId).FirstOrDefault();
        }
    }
}