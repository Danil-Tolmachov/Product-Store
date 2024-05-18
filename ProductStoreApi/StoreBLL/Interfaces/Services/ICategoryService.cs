using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface ICategoryService : IAdminPanelItem<CategoryModel>
    {
        Task<IEnumerable<ProductModel>> GetProducts(long id);
		Task<IEnumerable<ProductModel>> GetProducts(long id, int pageNumber, int rowCount);
	}
}
