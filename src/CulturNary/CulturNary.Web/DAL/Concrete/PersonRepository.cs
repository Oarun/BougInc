using CulturNary.DAL.Abstract;
using CulturNary.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace CulturNary.DAL.Concrete
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(CulturNaryDbContext context) : base(context)
        {
            
        }
        public Person GetPersonByIdentityId(string identityId){
            return base.Where(x => x.IdentityId == identityId).FirstOrDefault();
        }
    }
}