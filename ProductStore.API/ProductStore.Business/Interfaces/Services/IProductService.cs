using ProductStore.Business.Models;

namespace ProductStore.Business.Interfaces.Services
{
    public interface IProductService : IAdminPanelItem<ProductModel>
    {
		Task<int> CountPagesByCategory(long id, int rowCount = 5);
		Task<int> CountByCategory(long id);
	}
}
