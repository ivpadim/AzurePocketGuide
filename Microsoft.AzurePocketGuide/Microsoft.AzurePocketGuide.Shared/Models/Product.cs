using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Microsoft.AzurePocketGuide.Models
{
	/// <summary>
	/// Service offered by azure
	/// </summary>
	public class Product
    {
		/// <summary>
		/// Mobile Service Id Field
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Product Identifier
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Type of Service Identifier
		/// </summary>
		public int ServiceTypeId { get; set; }

		/// <summary>
		/// Name of the service
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Icon of the service
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		public bool Status { get; set; }

		/// <summary>
		/// Information about the product
		/// </summary>
		[JsonIgnore]
		public IEnumerable<ProductInformation> Information { get; set; }

		[Version]
		public string Version { get; set; }
	}
}
