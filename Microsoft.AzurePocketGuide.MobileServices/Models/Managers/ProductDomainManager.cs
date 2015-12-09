using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using AutoMapper;
using Microsoft.AzurePocketGuide.MobileServices.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.Models.Managers
{
	public class ProductDomainManager : MappedEntityDomainManager<Product, AzureProduct>
	{
		private MobileServiceContext context;

		public ProductDomainManager(MobileServiceContext context, HttpRequestMessage request, ApiServices services)
			: base(context, request, services)
		{
			Request = request;
			this.context = context;
		}

		public static int GetKey(string productId, DbSet<AzureProduct> products, HttpRequestMessage request)
		{
			int customerId = products
			   .Where(c => c.Id == productId)
			   .Select(c => c.AzureProductId)
			   .FirstOrDefault();

			if (customerId == 0)
			{
				throw new HttpResponseException(request.CreateNotFoundResponse());
			}
			return customerId;
		}

		protected override T GetKey<T>(string productId)
		{
			return (T)(object)GetKey(productId, this.context.AzureProduct, this.Request);
		}

		public override SingleResult<Product> Lookup(string productId)
		{
			int id = GetKey<int>(productId);
			return LookupEntity(c => c.AzureProductId == id);
		}

		public override async Task<Product> InsertAsync(Product product)
		{
			return await base.InsertAsync(product);
		}

		public override async Task<Product> UpdateAsync(string productId, Delta<Product> patch)
		{
			int id = GetKey<int>(productId);

			var existingProduct = await this.Context.Set<AzureProduct>().FindAsync(id);
			if (existingProduct == null)
			{
				throw new HttpResponseException(this.Request.CreateNotFoundResponse());
			}

			var existingProductDTO = Mapper.Map<AzureProduct, Product>(existingProduct);
			patch.Patch(existingProductDTO);
			Mapper.Map<Product, AzureProduct>(existingProductDTO, existingProduct);

			await this.SubmitChangesAsync();

			var updated = Mapper.Map<AzureProduct, Product>(existingProduct);

			return updated;
		}

		public override async Task<Product> ReplaceAsync(string productId, Product product)
		{
			return await base.ReplaceAsync(productId, product);
		}

		public override async Task<bool> DeleteAsync(string productId)
		{
			int id = GetKey<int>(productId);
			return await DeleteItemAsync(id);
		}
	}
}
