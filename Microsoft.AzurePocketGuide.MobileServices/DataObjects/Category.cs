using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.DataObjects
{
	public class Category : EntityData
	{
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public bool Status { get; set; }
	}
}