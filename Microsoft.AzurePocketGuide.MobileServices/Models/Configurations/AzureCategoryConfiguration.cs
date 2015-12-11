using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Microsoft.AzurePocketGuide.MobileServices.Models.Configurations
{
	public class AzureCategoryConfiguration : EntityTypeConfiguration<AzureCategory>
	{
		public AzureCategoryConfiguration()
			: this("dbo")
		{
		}

		public AzureCategoryConfiguration(string schema)
		{
			ToTable(schema + ".AzureCategory");
			HasKey(x => x.AzureCategoryId);

			Property(x => x.AzureCategoryId).HasColumnName("azurecategory_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			Property(x => x.AzureCategoryDescription).HasColumnName("azurecategory_description").IsRequired().HasMaxLength(100);
			Property(x => x.AzureCategoryIcon).HasColumnName("azurecategory_icon").HasMaxLength(400);
			Property(x => x.AzureCategoryStatus).HasColumnName("azurecategory_status").IsRequired();
		}
	}
}
