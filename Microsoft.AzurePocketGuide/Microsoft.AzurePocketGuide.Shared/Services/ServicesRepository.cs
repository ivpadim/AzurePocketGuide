using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Microsoft.AzurePocketGuide.Services
{
	public class ServicesRepository : IServicesRepository
	{
		private readonly MobileServiceClient _mobileService;
		private readonly IMobileServiceSyncTable<ServiceItem> _table;

		public ServicesRepository()
		{
			_mobileService = new MobileServiceClient("YOUR URL HERE", "YOUR APP SECRET HERE");
			_table = _mobileService.GetSyncTable<ServiceItem>();

			InitLocalStore();
		}


		private async void InitLocalStore()
		{
			if (!_mobileService.SyncContext.IsInitialized)
			{
				var store = new MobileServiceSQLiteStore("pocketguide.db");
				store.DefineTable<ServiceItem>();
				await _mobileService.SyncContext.InitializeAsync(store, null);
			}

			await SyncData();
		}

		private async Task SyncData()
		{
			await _table.PullAsync("ServiceItems", _table.CreateQuery());
		}

		public async Task<List<ServiceItem>> GetAllServiceItems()
		{
			try
			{
				var items = await _table
					.OrderBy(service => service.Name)
					.ToCollectionAsync();

				return items.ToList();
			}
			catch (MobileServiceInvalidOperationException e)
			{
				throw e;
			}
		}
	}
}
