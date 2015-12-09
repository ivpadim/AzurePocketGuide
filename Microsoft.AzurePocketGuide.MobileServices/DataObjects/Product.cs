using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.DataObjects
{
	public class Product : EntityData
	{
		public int ProductId { get; set; }
		public int ServiceTypeId { get; set; }
		public string Description { get; set; }
		public string Icon { get; set; }
		public bool Status { get; set; }
	}
}