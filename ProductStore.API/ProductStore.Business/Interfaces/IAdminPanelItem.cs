
namespace ProductStore.Business.Interfaces
{
	public interface IAdminPanelItem<TModel> : ICrud<TModel>
	{
		Task<int> CountPages(int rowCount = 5);

		Type GetModelType();
	}
}
