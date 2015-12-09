using System;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.DataObjects
{
	public class ProductInformation : EntityData
	{
		public int ProductInformationId { get; set; }
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public DateTime ProductInformationDate { get; set; }
		public bool IsNew { get; set; }
		public bool IsPreview { get; set; }
		public bool Status { get; set; }
	}
}