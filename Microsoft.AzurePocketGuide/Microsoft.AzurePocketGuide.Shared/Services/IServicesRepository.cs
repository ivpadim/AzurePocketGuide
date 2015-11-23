using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AzurePocketGuide.Models;

namespace Microsoft.AzurePocketGuide.Services
{
	public interface IServicesRepository
	{
		Task<List<ServiceItem>> GetAllServiceItems();
    }
}
