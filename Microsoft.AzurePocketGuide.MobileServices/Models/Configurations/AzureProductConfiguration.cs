using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Microsoft.AzurePocketGuide.MobileServices.Models.Configurations
{
	public class AzureProductConfiguration : EntityTypeConfiguration<AzureProduct>
	{
		public AzureProductConfiguration()
			: this("dbo")
		{
		}

		public AzureProductConfiguration(string schema)
		{
			ToTable(schema + ".AzureProduct");
			HasKey(x => x.AzureProductId);

			Property(x => x.AzureProductId).HasColumnName("product_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(x => x.AzureServiceTypeId).HasColumnName("azureservice_type_id").IsOptional();
			Property(x => x.AzureProductDescription).HasColumnName("product_description").IsRequired().HasMaxLength(50);
			Property(x => x.AzureProductIcon).HasColumnName("product_icon").IsRequired().HasMaxLength(400);
			Property(x => x.AzureProductStatus).HasColumnName("product_status").IsRequired();

			// Foreign keys
			HasOptional(a => a.AzureServiceType).WithMany(b => b.AzureProducts).HasForeignKey(c => c.AzureServiceTypeId); // FK_AzureProduct_AzureService_Type
		}
	}
}
