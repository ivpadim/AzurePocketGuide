using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices.DataObjects
{
	public class ServiceItem : EntityData
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
	}
}