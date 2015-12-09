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
		#region vars

		private readonly MobileServiceClient _mobileService;
		private readonly IMobileServiceSyncTable<Category> _categoryTable;
		private readonly IMobileServiceSyncTable<ServiceType> _serviceTypeTable;
		private readonly IMobileServiceSyncTable<Product> _productTable;
		private readonly IMobileServiceSyncTable<ProductInformation> _productInfoTable;

		#endregion

		#region ctor

		public ServicesRepository()
		{
			_mobileService = new MobileServiceClient("YOUR URL HERE", "YOUR APP SECRET HERE");
			_categoryTable = _mobileService.GetSyncTable<Category>();
			_serviceTypeTable = _mobileService.GetSyncTable<ServiceType>();
			_productTable = _mobileService.GetSyncTable<Product>();
			_productInfoTable = _mobileService.GetSyncTable<ProductInformation>();
			InitLocalStore();
		}

		#endregion

		#region private methods

		/// <summary>
		/// Initialize the local database and sync the data
		/// </summary>
		private async void InitLocalStore()
		{
			if (!_mobileService.SyncContext.IsInitialized)
			{
				var store = new MobileServiceSQLiteStore("pocketguide.db");
				store.DefineTable<Category>();
				store.DefineTable<ServiceType>();
				store.DefineTable<Product>();
				store.DefineTable<ProductInformation>();
				await _mobileService.SyncContext.InitializeAsync(store, null);
			}
			await SyncData();
		}

		/// <summary>
		/// Checks if there are any records on the local database
		/// </summary>
		/// <returns></returns>
		private async Task<bool> IsSynced()
		{
			return (await _serviceTypeTable.ToListAsync()).Count > 0;
		}

		/// <summary>
		/// Pulls the data from the mobile service and fills all the tables
		/// </summary>
		/// <returns></returns>
		private async Task SyncData()
		{
			await _categoryTable.PullAsync("Category", _categoryTable.CreateQuery());
			await _serviceTypeTable.PullAsync("ServiceType", _serviceTypeTable.CreateQuery());
			await _productTable.PullAsync("Product", _productTable.CreateQuery());
			await _productInfoTable.PullAsync("ProductInformation", _productInfoTable.CreateQuery());
		}

		#endregion

		#region IServicesRepository

		public async Task<List<ServiceType>> GetAllServices(bool forceSync = false)
		{
			var synced = await IsSynced();
			if (forceSync || !synced)
				await SyncData();

			try
			{
				var categories = (await _categoryTable
						.OrderBy(c => c.CategoryId)
						.ToCollectionAsync())
						.ToList();

				var serviceTypes = (await _serviceTypeTable
						.OrderBy(s => s.ServiceTypeId)
						.ToCollectionAsync())
						.ToList();

				var products = (await _productTable
						.OrderBy(p => p.ProductId)
						.ToCollectionAsync())
						.ToList();

				var productInformations = (await _productInfoTable
						.OrderBy(p => p.ProductInformationId)
						.ToCollectionAsync())
						.ToList();


				foreach (var productInfo in productInformations)
					productInfo.Category = categories.FirstOrDefault(c => c.CategoryId == productInfo.CategoryId);

				foreach (var product in products)
					product.Information = productInformations.Where(p => p.ProductId == product.ProductId).ToList();

				foreach (var service in serviceTypes)
					service.Products = products.Where(p => p.ServiceTypeId == service.ServiceTypeId).ToList();

				return serviceTypes;

			}
			catch (MobileServiceInvalidOperationException e)
			{
				throw e;
			}
		}

		public async Task<Product> GetProductById(int productId)
		{
			try
			{
				var categories = (await _categoryTable
						.OrderBy(c => c.CategoryId)
						.ToCollectionAsync())
						.ToList();

				var products = (await _productTable
						.OrderBy(p => p.ProductId)
						.ToCollectionAsync())
						.ToList();

				var productInformations = (await _productInfoTable
						.OrderBy(p => p.ProductInformationId)
						.Where(p => p.ProductId == productId)
						.ToCollectionAsync())
						.ToList();

				var product = (await _productTable
						.Where(p => p.ProductId == productId)
						.ToCollectionAsync())
						.FirstOrDefault();

				foreach (var productInfo in productInformations)
					productInfo.Category = categories.FirstOrDefault(c => c.CategoryId == productInfo.CategoryId);

				product.Information = productInformations;

				return product;

			}
			catch (MobileServiceInvalidOperationException e)
			{
				throw e;
			}
		}

		#endregion
	}
}
