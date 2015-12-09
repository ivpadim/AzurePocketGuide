using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service.Tables;

namespace Microsoft.AzurePocketGuide.MobileServices.Models
{
	public class AzureServiceType
	{
		[Key]
		public int AzureServiceTypeId { get; set; }
		public string AzureServiceTypeDescription { get; set; }
		public bool AzureServiceTypeStatus { get; set; }
		public virtual ICollection<AzureProduct> AzureProducts { get; set; }


		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Index]
		[TableColumn(TableColumnType.CreatedAt)]
		public DateTimeOffset? CreatedAt { get; set; }

		[TableColumn(TableColumnType.Deleted)]
		public bool Deleted { get; set; }

		[Index]
		[TableColumn(TableColumnType.Id)]
		[MaxLength(36)]
		public string Id { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		[TableColumn(TableColumnType.UpdatedAt)]
		public DateTimeOffset? UpdatedAt { get; set; }

		[TableColumn(TableColumnType.Version)]
		[Timestamp]
		public byte[] Version { get; set; }
	}
}
