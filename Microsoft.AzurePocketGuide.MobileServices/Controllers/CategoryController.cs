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
	public class CategoryController : TableController<Category>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			var context = new MobileServiceContext();
			DomainManager = new CategoryDomainManager(context, Request, Services);
		}

		public IQueryable<Category> GetAllCategories()
		{
			return Query();
		}

		public SingleResult<Category> GetCategory(string id)
		{
			return Lookup(id);
		}

		public Task<Category> PatchCategory(string id, Delta<Category> patch)
		{
			return base.UpdateAsync(id, patch);
		}

		public async Task<IHttpActionResult> PostCategory(Category item)
		{
			var current = await InsertAsync(item);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		public Task DeleteCategory(string id)
		{
			return base.DeleteAsync(id);
		}
	}
}