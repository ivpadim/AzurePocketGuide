using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service.Tables;

namespace Microsoft.AzurePocketGuide.MobileServices.Models
{
	public class AzureCategory
    {
		[Key]
		public int AzureCategoryId { get; set; }
		public string AzureCategoryDescription { get; set; }
		public string AzureCategoryIcon { get; set; }
		public bool AzureCategoryStatus { get; set; }

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
