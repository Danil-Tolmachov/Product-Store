using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class PersonRepository : AbstractRepository<Person>, IPersonRepository
	{
		public PersonRepository(StoreDbContext context) : base(context) { }
	}
}
