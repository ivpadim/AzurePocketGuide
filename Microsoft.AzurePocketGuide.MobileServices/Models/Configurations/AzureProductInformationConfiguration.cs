using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Microsoft.AzurePocketGuide.MobileServices.Models.Configurations
{
	public class AzureProductInformationConfiguration : EntityTypeConfiguration<AzureProductInformation>
	{
		public AzureProductInformationConfiguration()
			: this("dbo")
		{
		}

		public AzureProductInformationConfiguration(string schema)
		{
			ToTable(schema + ".AzureProduct_Information");
			HasKey(x => x.AzureProductInformationId);

			Property(x => x.AzureProductInformationId).HasColumnName("AzureProduct_Information_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(x => x.AzureProductId).HasColumnName("product_id").IsRequired();
			Property(x => x.AzureCategoryId).HasColumnName("azurecategory_id").IsRequired();
			Property(x => x.AzureProductInformationDescription).HasColumnName("product_information").IsRequired().HasMaxLength(1000);
			Property(x => x.InformationDate).HasColumnName("product_information_date").IsRequired();
			Property(x => x.IsNew).HasColumnName("isnew").IsRequired();
			Property(x => x.IsPreview).HasColumnName("ispreview").IsOptional();
			Property(x => x.AzureProductInformationStatus).HasColumnName("product_information_status").IsRequired();
		}
	}
}
