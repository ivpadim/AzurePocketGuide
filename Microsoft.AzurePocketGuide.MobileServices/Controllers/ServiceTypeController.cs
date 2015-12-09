using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.AzurePocketGuide.MobileServices.DataObjects;
using Microsoft.AzurePocketGuide.MobileServices.Models;
using Microsoft.AzurePocketGuide.MobileServices.Models.Managers;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.Controllers
{
	public class ServiceTypeController : TableController<ServiceType>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			var context = new MobileServiceContext();
			DomainManager = new ServiceTypeDomainManager(context, Request, Services);
		}

		public IQueryable<ServiceType> GetAllServiceTypes()
		{
			return Query();
		}

		public SingleResult<ServiceType> GetServiceType(string id)
		{
			return Lookup(id);
		}

		public Task<ServiceType> PatchServiceType(string id, Delta<ServiceType> patch)
		{
			return base.UpdateAsync(id, patch);
		}

		public async Task<IHttpActionResult> PostServiceType(ServiceType item)
		{
			var current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		public Task DeleteServiceType(string id)
		{
			return base.DeleteAsync(id);
		}
	}
}