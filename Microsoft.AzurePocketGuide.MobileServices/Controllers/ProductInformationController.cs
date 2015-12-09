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
	public class ProductInformationController : TableController<ProductInformation>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			var context = new MobileServiceContext();
			DomainManager = new ProductInformationDomainManager(context, Request, Services);
		}

		public IQueryable<ProductInformation> GetAllProductInformations()
		{
			return Query();
		}

		public SingleResult<ProductInformation> GetProductInformation(string id)
		{
			return Lookup(id);
		}

		public Task<ProductInformation> PatchProductInformation(string id, Delta<ProductInformation> patch)
		{
			return base.UpdateAsync(id, patch);
		}

		public async Task<IHttpActionResult> PostProductInformation(ProductInformation item)
		{
			var current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		public Task DeleteProductInformation(string id)
		{
			return base.DeleteAsync(id);
		}
	}
}