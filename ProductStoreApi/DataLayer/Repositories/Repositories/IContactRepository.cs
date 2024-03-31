using StoreDAL.Entities;
using StoreDAL.Infrastructure;
using StoreDAL.Repositories;

namespace StoreDAL.Interfaces.Repositories
{
	public class ContactRepository : AbstractRepository<Contact>, IContactRepository
	{
		public ContactRepository(StoreDbContext context) : base(context) { }
	}
}
