using ProductStore.Data.Entities;
using ProductStore.Data.Infrastructure;
using ProductStore.Data.Interfaces.Repositories;

namespace ProductStore.Data.Repositories.Repositories
{
	public class ContactRepository : AbstractSingleKeyRepository<Contact>, IContactRepository
	{
		public ContactRepository(StoreDbContext context) : base(context) { }
	}
}
