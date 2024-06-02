using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBLL.Interfaces
{
	public interface IAdminPanelItem<TModel> : ICrud<TModel>
	{
		Task<int> CountPages(int rowCount = 5);

		Type GetModelType();
	}
}
