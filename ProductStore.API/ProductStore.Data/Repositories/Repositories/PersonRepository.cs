using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class PersonRepository : AbstractSingleKeyRepository<Person>, IPersonRepository
	{
		public PersonRepository(StoreDbContext context) : base(context) { }
	}
}
