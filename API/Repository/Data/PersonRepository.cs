using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, string>
    {
        public PersonRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
