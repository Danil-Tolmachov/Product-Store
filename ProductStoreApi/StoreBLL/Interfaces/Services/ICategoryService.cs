using StoreBLL.Models;

namespace StoreBLL.Interfaces.Services
{
    public interface ICategoryService : IAdminPanelItem<CategoryModel>
    {
        IEnumerable<ProductModel> GetProducts(long id);
    }
}
