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
        private readonly IPersonRepository _personRepository;
        public FriendRequestRepository(CulturNaryDbContext context, IPersonRepository personRepository) : base(context)
        {
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
    }
}