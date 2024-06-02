using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Interfaces.Repositories;

namespace StoreDAL.Repositories.Repositories
{
	public class ContactRepository : AbstractSingleKeyRepository<Contact>, IContactRepository
	{
		public ContactRepository(StoreDbContext context) : base(context) { }
	}
}
