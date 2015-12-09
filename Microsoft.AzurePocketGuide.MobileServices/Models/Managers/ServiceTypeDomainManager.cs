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
	public class ServiceTypeDomainManager : MappedEntityDomainManager<ServiceType, AzureServiceType>
	{
		private MobileServiceContext context;

		public ServiceTypeDomainManager(MobileServiceContext context, HttpRequestMessage request, ApiServices services)
			: base(context, request, services)
		{
			Request = request;
			this.context = context;
		}

		public static int GetKey(string serviceTypeId, DbSet<AzureServiceType> serviceTypes, HttpRequestMessage request)
		{
			int customerId = serviceTypes
			   .Where(c => c.Id == serviceTypeId)
			   .Select(c => c.AzureServiceTypeId)
			   .FirstOrDefault();

			if (customerId == 0)
			{
				throw new HttpResponseException(request.CreateNotFoundResponse());
			}
			return customerId;
		}

		protected override T GetKey<T>(string serviceTypeId)
		{
			return (T)(object)GetKey(serviceTypeId, this.context.AzureService_Type, this.Request);
		}

		public override SingleResult<ServiceType> Lookup(string serviceTypeId)
		{
			int id = GetKey<int>(serviceTypeId);
			return LookupEntity(c => c.AzureServiceTypeId == id);
		}

		public override async Task<ServiceType> InsertAsync(ServiceType serviceType)
		{
			return await base.InsertAsync(serviceType);
		}

		public override async Task<ServiceType> UpdateAsync(string serviceTypeId, Delta<ServiceType> patch)
		{
			int id = GetKey<int>(serviceTypeId);

			var existingServiceType = await this.Context.Set<AzureServiceType>().FindAsync(id);
			if (existingServiceType == null)
			{
				throw new HttpResponseException(this.Request.CreateNotFoundResponse());
			}

			var existingServiceTypeDTO = Mapper.Map<AzureServiceType, ServiceType>(existingServiceType);
			patch.Patch(existingServiceTypeDTO);
			Mapper.Map<ServiceType, AzureServiceType>(existingServiceTypeDTO, existingServiceType);

			await this.SubmitChangesAsync();

			var updated = Mapper.Map<AzureServiceType, ServiceType>(existingServiceType);

			return updated;
		}

		public override async Task<ServiceType> ReplaceAsync(string serviceTypeId, ServiceType serviceType)
		{
			return await base.ReplaceAsync(serviceTypeId, serviceType);
		}

		public override async Task<bool> DeleteAsync(string serviceTypeId)
		{
			int id = GetKey<int>(serviceTypeId);
			return await DeleteItemAsync(id);
		}
	}
}
