using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service.Tables;

namespace Microsoft.AzurePocketGuide.MobileServices.Models
{
	public class AzureProductInformation
	{
		[Key]
		public int AzureProductInformationId { get; set; }
		public int AzureProductId { get; set; }
		public int AzureCategoryId { get; set; }
		public string AzureProductInformationDescription { get; set; }
		public DateTime InformationDate { get; set; }
		public bool IsNew { get; set; }
		public bool IsPreview { get; set; }
		public bool AzureProductInformationStatus { get; set; }

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
