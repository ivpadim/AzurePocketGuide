using System;
using Microsoft.WindowsAzure.MobileServices;

namespace Microsoft.AzurePocketGuide.Models
{
	/// <summary>
	/// Category information of the product
	/// </summary>
	public class Category
	{
		/// <summary>
		/// Mobile Service Id Field
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Category Identifier
		/// </summary>
		public int CategoryId { get; set; }

		/// <summary>
		/// Description of the Category
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Icon of the Category
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		/// Status of the record
		/// </summary>
		public bool Status { get; set; }

		[Version]
		public string Version { get; set; }
	}
}
