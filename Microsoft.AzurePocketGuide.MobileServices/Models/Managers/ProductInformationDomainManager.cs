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
	public class ProductInformationDomainManager : MappedEntityDomainManager<ProductInformation, AzureProductInformation>
	{
		private MobileServiceContext context;

		public ProductInformationDomainManager(MobileServiceContext context, HttpRequestMessage request, ApiServices services)
			: base(context, request, services)
		{
			Request = request;
			this.context = context;
		}

		public static int GetKey(string productInformationId, DbSet<AzureProductInformation> products, HttpRequestMessage request)
		{
			int customerId = products
			   .Where(c => c.Id == productInformationId)
			   .Select(c => c.AzureProductInformationId)
			   .FirstOrDefault();

			if (customerId == 0)
			{
				throw new HttpResponseException(request.CreateNotFoundResponse());
			}
			return customerId;
		}

		protected override T GetKey<T>(string productInformationId)
		{
			return (T)(object)GetKey(productInformationId, this.context.AzureProduct_Information, this.Request);
		}

		public override SingleResult<ProductInformation> Lookup(string productInformationId)
		{
			int id = GetKey<int>(productInformationId);
			return LookupEntity(c => c.AzureProductInformationId == id);
		}

		public override async Task<ProductInformation> InsertAsync(ProductInformation productInformation)
		{
			return await base.InsertAsync(productInformation);
		}

		public override async Task<ProductInformation> UpdateAsync(string productInformationId, Delta<ProductInformation> patch)
		{
			int id = GetKey<int>(productInformationId);

			var existingProductInformation = await this.Context.Set<AzureProductInformation>().FindAsync(id);
			if (existingProductInformation == null)
			{
				throw new HttpResponseException(this.Request.CreateNotFoundResponse());
			}

			var existingProductInformationDTO = Mapper.Map<AzureProductInformation, ProductInformation>(existingProductInformation);
			patch.Patch(existingProductInformationDTO);
			Mapper.Map<ProductInformation, AzureProductInformation>(existingProductInformationDTO, existingProductInformation);

			await this.SubmitChangesAsync();

			var updated = Mapper.Map<AzureProductInformation, ProductInformation>(existingProductInformation);

			return updated;
		}

		public override async Task<ProductInformation> ReplaceAsync(string productInformationId, ProductInformation productInformation)
		{
			return await base.ReplaceAsync(productInformationId, productInformation);
		}

		public override async Task<bool> DeleteAsync(string productInformationId)
		{
			int id = GetKey<int>(productInformationId);
			return await DeleteItemAsync(id);
		}
	}
}
