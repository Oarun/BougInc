using CulturNary.Web.Models;
using System.Threading.Tasks;

namespace CulturNary.DAL.Abstract
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        public List<Friendship> GetFriendshipByPersonId(int personId);
    }
}
