using CulturNary.Web.Models;
using System.Threading.Tasks;

namespace CulturNary.DAL.Abstract
{
    public interface IPersonRepository : IRepository<Person>
    {
        public Person GetPersonByIdentityId(string identityId);
    }
}