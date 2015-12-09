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
	public class CategoryDomainManager : MappedEntityDomainManager<Category, AzureCategory>
		{
			private MobileServiceContext context;

			public CategoryDomainManager(MobileServiceContext context, HttpRequestMessage request, ApiServices services)
				: base(context, request, services)
			{
				Request = request;
				this.context = context;
			}

			public static int GetKey(string categoryId, DbSet<AzureCategory> categories, HttpRequestMessage request)
			{
				int customerId = categories
				   .Where(c => c.Id == categoryId)
				   .Select(c => c.AzureCategoryId)
				   .FirstOrDefault();

				if (customerId == 0)
				{
					throw new HttpResponseException(request.CreateNotFoundResponse());
				}
				return customerId;
			}

			protected override T GetKey<T>(string categoryId)
			{
				return (T)(object)GetKey(categoryId, this.context.AzureCategory, this.Request);
			}

			public override SingleResult<Category> Lookup(string categoryId)
			{
				int id = GetKey<int>(categoryId);
				return LookupEntity(c => c.AzureCategoryId == id);
			}

			public override async Task<Category> InsertAsync(Category category)
			{
				return await base.InsertAsync(category);
			}

			public override async Task<Category> UpdateAsync(string categoryId, Delta<Category> patch)
			{
				int id = GetKey<int>(categoryId);

				var existingCategory = await this.Context.Set<AzureCategory>().FindAsync(id);
				if (existingCategory == null)
				{
					throw new HttpResponseException(this.Request.CreateNotFoundResponse());
				}

				var existingCategoryDTO = Mapper.Map<AzureCategory, Category>(existingCategory);
				patch.Patch(existingCategoryDTO);
				Mapper.Map<Category, AzureCategory>(existingCategoryDTO, existingCategory);

				await this.SubmitChangesAsync();

				var updatedCustomerDTO = Mapper.Map<AzureCategory, Category>(existingCategory);

				return updatedCustomerDTO;
			}

			public override async Task<Category> ReplaceAsync(string categoryId, Category category)
			{
				return await base.ReplaceAsync(categoryId, category);
			}

			public override async Task<bool> DeleteAsync(string categoryId)
			{
				int id = GetKey<int>(categoryId);
				return await DeleteItemAsync(id);
			}
		}
	}
