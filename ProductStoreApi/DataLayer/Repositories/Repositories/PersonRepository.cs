using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class PersonRepository : AbstractRepository<Person>, IPersonRepository
	{
		public PersonRepository(StoreDbContext context) : base(context) { }
	}
}
