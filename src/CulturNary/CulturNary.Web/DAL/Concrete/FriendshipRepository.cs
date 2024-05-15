using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace CulturNary.DAL.Concrete
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly UserManager<SiteUser> _userManager;
        public FriendshipRepository(CulturNaryDbContext context, IPersonRepository personRepository,UserManager<SiteUser> userManager,
        IFriendRequestRepository friendRequestRepository) : base(context)
        {
            _userManager = userManager;
            _personRepository = personRepository;
            _friendRequestRepository = friendRequestRepository;
        }

        public List<Friendship> GetFriendshipByPersonId(int personId)
        {
            return _dbSet
                .Where(f => f.Person1Id == personId || f.Person2Id == personId)
                .ToList();
        }
        public bool AreFriends(string currentUserId, string friendId){
            return _dbSet
                .Where(f => (f.Person1.IdentityId == currentUserId && f.Person2.IdentityId == friendId) || 
                            (f.Person1.IdentityId == friendId && f.Person2.IdentityId == currentUserId))
                .Any();
        }
        public async Task<List<SiteUser>> GetFriends(string id)
        {
            int userPersonId = _personRepository.GetPersonByIdentityId(id).Id;
            var friends = _dbSet
                .Where(f => f.Person1Id == userPersonId || f.Person2Id == userPersonId)
                .Select(f => f.Person1Id == userPersonId ? f.Person2 : f.Person1)
                .ToList();

            var friendAsUser = new List<SiteUser>();
            foreach (var person in friends)
            {
                var user = await _userManager.FindByIdAsync(person.IdentityId.ToString());
                if (user != null)
                {
                    friendAsUser.Add(user);
                }
            }

            return friendAsUser;
        }
        public bool AcceptFriendRequest(string currentUserId, string requestId)
        {
            int currentUserPersonId = _personRepository.GetPersonByIdentityId(currentUserId).Id;
            int requesterUserPersonId = _personRepository.GetPersonByIdentityId(requestId).Id;
            var thisFriendRequest = _friendRequestRepository.GetByRequestAndRecipientId(requesterUserPersonId, currentUserPersonId);
        
            if (thisFriendRequest == null)
            {
                return false;
            }
        
            // Ensure Person1Id is less than Person2Id
            int person1Id, person2Id;
            if (thisFriendRequest.RequesterId < thisFriendRequest.RecipientId)
            {
                person1Id = thisFriendRequest.RequesterId;
                person2Id = thisFriendRequest.RecipientId;
            }
            else
            {
                person1Id = thisFriendRequest.RecipientId;
                person2Id = thisFriendRequest.RequesterId;
            }
        
            _dbSet.Add(new Friendship
            {
                Person1Id = person1Id,
                Person2Id = person2Id,
                FriendshipDate = DateTime.Now
            });
            _friendRequestRepository.Delete(thisFriendRequest);
        
            return true;
        }
        
        public bool RejectFriendRequest(string currentUserId, string requestId)
        {
            int currentUserPersonId = _personRepository.GetPersonByIdentityId(currentUserId).Id;
            int requesterUserPersonId = _personRepository.GetPersonByIdentityId(requestId).Id;
            var thisFriendRequest = _friendRequestRepository.GetByRequestAndRecipientId(requesterUserPersonId, currentUserPersonId);
        
            if (thisFriendRequest == null)
            {
                return false;
            }
        
            _friendRequestRepository.Delete(thisFriendRequest);
        
            return true;
        }
        
        public bool RemoveFriend(string currentUserId, string friendId)
        {
            int currentUserPersonId = _personRepository.GetPersonByIdentityId(currentUserId).Id;
            int friendPersonId = _personRepository.GetPersonByIdentityId(friendId).Id;
            var friendship = _dbSet
                .Where(f => (f.Person1Id == currentUserPersonId && f.Person2Id == friendPersonId) || 
                            (f.Person1Id == friendPersonId && f.Person2Id == currentUserPersonId))
                .FirstOrDefault();
        
            if (friendship == null)
            {
                return false;
            }
        
            _dbSet.Remove(friendship);
            _context.SaveChanges();
        
            return true;
        }
    }
}