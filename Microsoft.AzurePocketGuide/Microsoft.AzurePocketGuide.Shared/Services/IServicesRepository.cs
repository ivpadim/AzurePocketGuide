using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AzurePocketGuide.Models;

namespace Microsoft.AzurePocketGuide.Services
{
	/// <summary>
	/// Repository to retrieve the azure services information through the mobile service
	/// </summary>
	public interface IServicesRepository
	{
		/// <summary>
		/// Get all the azure services grouped by type: IaaS, PaaS, SaaS
		/// </summary>
		/// <param name="forceSync">If true pulls the data from the mobile service forcing the synchronization, if false reads the data from the local database</param>
		/// <returns>List of azure services by type</returns>
		Task<List<ServiceType>> GetAllServices(bool forceSync = false);

		/// <summary>
		/// Gets a specific product
		/// </summary>
		/// <param name="productId">Id of the product to retrieve</param>
		/// <returns>Product</returns>
		Task<Product> GetProductById(int productId);

		/// <summary>
		///  Gets the product information
		/// </summary>
		/// <param name="productInfoId">Id of the product info to retrieve</param>
		/// <returns>Product Information</returns>
		Task<ProductInformation> GetProductInformationById(int productInfoId);
    }
}
