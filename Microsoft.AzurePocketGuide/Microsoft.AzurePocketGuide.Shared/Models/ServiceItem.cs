using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Microsoft.AzurePocketGuide.Models
{
	public class ServiceItem
	{
		public string Id { get; set; }

		[JsonProperty(PropertyName="name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName ="description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName ="image")]
		public string Image { get; set; }

		[Version]
		public string Version { get; set; }
	}
}
