using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Microsoft.AzurePocketGuide.Models
{
	public class ProductInformation
	{
		public string Id { get; set; }

		public int ProductInformationId { get; set; }

		public int ProductId { get; set; }

		public int CategoryId { get; set; }

		public string Description { get; set; }

		public DateTime ProductInformationDate { get; set; }

		public bool IsNew { get; set; }

		public bool IsPreview { get; set; }

		public bool Status { get; set; }

		[JsonIgnore]
		public Category Category { get; set; }

		[JsonIgnore]
		public Product Product { get; set; }

		[Version]
		public string Version { get; set; }
	}
}
