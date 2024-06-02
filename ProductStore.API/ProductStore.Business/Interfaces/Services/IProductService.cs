using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface IProductService : IAdminPanelItem<ProductModel>
    {
		Task<int> CountPagesByCategory(long id, int rowCount = 5);
		Task<int> CountByCategory(long id);
	}
}
