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
	public class ProductController : TableController<Product>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			var context = new MobileServiceContext();
			DomainManager = new ProductDomainManager(context, Request, Services);
		}

		public IQueryable<Product> GetAllProducts()
		{
			return Query();
		}

		public SingleResult<Product> GetProduct(string id)
		{
			return Lookup(id);
		}

		public Task<Product> PatchProduct(string id, Delta<Product> patch)
		{
			return base.UpdateAsync(id, patch);
		}

		public async Task<IHttpActionResult> PostProduct(Product item)
		{
			var current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		public Task DeleteProduct(string id)
		{
			return base.DeleteAsync(id);
		}
	}
}