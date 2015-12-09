using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.DataObjects
{
	public class ServiceType : EntityData
	{
		public int ServiceTypeId { get; set; }
		public string Description { get; set; }
		public bool Status { get; set; }
	}
}