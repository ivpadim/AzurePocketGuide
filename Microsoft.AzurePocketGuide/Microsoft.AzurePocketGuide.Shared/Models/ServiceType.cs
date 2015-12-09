using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Microsoft.AzurePocketGuide.Models
{
	/// <summary>
	/// Type of service offered by azure
	/// </summary>
	public class ServiceType
    {
		/// <summary>
		/// Mobile Service Id Field
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Service Type Identifier
		/// </summary>
		public int ServiceTypeId { get; set; }

		/// <summary>
		/// Name of the type of service
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		public bool Status { get; set; }

		[JsonIgnore]
		public IEnumerable<Product> Products { get; set; }

		[Version]
		public string Version { get; set; }
	}
}
