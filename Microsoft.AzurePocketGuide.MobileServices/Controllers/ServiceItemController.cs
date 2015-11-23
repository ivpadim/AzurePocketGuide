using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.AzurePocketGuide.MobileServices.DataObjects;
using Microsoft.AzurePocketGuide.MobileServices.Models;

namespace Microsoft.AzurePocketGuide.MobileServices.Controllers
{
	public class ServiceItemController : TableController<ServiceItem>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<ServiceItem>(context, Request, Services);
		}

		// GET tables/ServiceItem
		public IQueryable<ServiceItem> GetAllServiceItems()
		{
			return Query();
		}

		// GET tables/ServiceItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<ServiceItem> GetServiceItem(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/ServiceItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<ServiceItem> PatchServiceItem(string id, Delta<ServiceItem> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/ServiceItem
		public async Task<IHttpActionResult> PostServiceItem(ServiceItem item)
		{
			ServiceItem current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/ServiceItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteServiceItem(string id)
		{
			return DeleteAsync(id);
		}
	}
}