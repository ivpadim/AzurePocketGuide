using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Microsoft.AzurePocketGuide.MobileServices.Models.Configurations
{
	public class AzureServiceTypeConfiguration : EntityTypeConfiguration<AzureServiceType>
	{
		public AzureServiceTypeConfiguration()
			: this("dbo")
		{
		}

		public AzureServiceTypeConfiguration(string schema)
		{
			ToTable(schema + ".AzureService_Type");
			HasKey(x => x.AzureServiceTypeId);

			Property(x => x.AzureServiceTypeId).HasColumnName("azureservice_type_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(x => x.AzureServiceTypeDescription).HasColumnName("azureservice_type_description").IsRequired().HasMaxLength(50);
			Property(x => x.AzureServiceTypeStatus).HasColumnName("azureservice_type_status").IsRequired();
		}
	}

}
