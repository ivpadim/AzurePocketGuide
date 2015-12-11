using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using Microsoft.AzurePocketGuide.MobileServices.DataObjects;
using Microsoft.AzurePocketGuide.MobileServices.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Microsoft.AzurePocketGuide.MobileServices
{
	public static class WebApiConfig
	{
		public static void Register()
		{
			// Use this class to set configuration options for your mobile service
			ConfigOptions options = new ConfigOptions();

			// Use this class to set WebAPI configuration options
			HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<AzureCategory, Category>()
						.ForMember(dest => dest.CategoryId, map => map.MapFrom(x => x.AzureCategoryId))
						.ForMember(dest => dest.Description, map => map.MapFrom(x => x.AzureCategoryDescription))
						.ForMember(dest => dest.Icon, map => map.MapFrom(x => x.AzureCategoryIcon))
						.ForMember(dest => dest.Status, map => map.MapFrom(x => x.AzureCategoryStatus));

				cfg.CreateMap<AzureServiceType, ServiceType>()
						.ForMember(dest => dest.ServiceTypeId, map => map.MapFrom(x => x.AzureServiceTypeId))
						.ForMember(dest => dest.Description, map => map.MapFrom(x => x.AzureServiceTypeDescription))
						.ForMember(dest => dest.Status, map => map.MapFrom(x => x.AzureServiceTypeStatus));

				cfg.CreateMap<AzureProduct, Product>()
						.ForMember(dest => dest.ProductId, map => map.MapFrom(x => x.AzureProductId))
						.ForMember(dest => dest.ServiceTypeId, map => map.MapFrom(x => x.AzureServiceTypeId))
						.ForMember(dest => dest.Description, map => map.MapFrom(x => x.AzureProductDescription))
						.ForMember(dest => dest.Icon, map => map.MapFrom(x => x.AzureProductIcon))
						.ForMember(dest => dest.Status, map => map.MapFrom(x => x.AzureProductStatus));

				cfg.CreateMap<AzureProductInformation, ProductInformation>()
						.ForMember(dest => dest.ProductInformationId, map => map.MapFrom(x => x.AzureProductInformationId))
						.ForMember(dest => dest.ProductId, map => map.MapFrom(x => x.AzureProductId))
						.ForMember(dest => dest.CategoryId, map => map.MapFrom(x => x.AzureCategoryId))
						.ForMember(dest => dest.Description, map => map.MapFrom(x => x.AzureProductInformationDescription))
						.ForMember(dest => dest.ProductInformationDate, map => map.MapFrom(x => x.InformationDate))
						.ForMember(dest => dest.Status, map => map.MapFrom(x => x.AzureProductInformationStatus));
			});



			// To display errors in the browser during development, uncomment the following
			// line. Comment it out again when you deploy your service for production use.
			// config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

			Database.SetInitializer(new MobileServiceInitializer());

		}
	}

	public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
	{

		protected override void Seed(MobileServiceContext context)
		{


			var categories = new List<AzureCategory>
			{
				new AzureCategory {Id = Guid.NewGuid().ToString(), AzureCategoryId =1, AzureCategoryDescription ="Licensing", AzureCategoryIcon="icon-licensing", AzureCategoryStatus=true },
				new AzureCategory {Id = Guid.NewGuid().ToString(), AzureCategoryId =2, AzureCategoryDescription ="Administration", AzureCategoryIcon="icon-administration", AzureCategoryStatus=true },
				new AzureCategory {Id = Guid.NewGuid().ToString(), AzureCategoryId =3, AzureCategoryDescription ="High Availability", AzureCategoryIcon="icon-high-availability", AzureCategoryStatus=true },
				new AzureCategory {Id = Guid.NewGuid().ToString(), AzureCategoryId =4, AzureCategoryDescription ="Performance", AzureCategoryIcon="icon-performance",  AzureCategoryStatus=true },
				new AzureCategory {Id = Guid.NewGuid().ToString(), AzureCategoryId =5, AzureCategoryDescription ="Security", AzureCategoryIcon="icon-security", AzureCategoryStatus=true }
			};

			foreach (var category in categories)
				context.Set<AzureCategory>().Add(category);

			var serviceTypes = new List<AzureServiceType>
			{
				new AzureServiceType{Id=Guid.NewGuid().ToString(), AzureServiceTypeId=1, AzureServiceTypeDescription="SaaS", AzureServiceTypeStatus=true },
				new AzureServiceType{Id=Guid.NewGuid().ToString(), AzureServiceTypeId=2, AzureServiceTypeDescription="PaaS", AzureServiceTypeStatus=true},
				new AzureServiceType{Id=Guid.NewGuid().ToString(), AzureServiceTypeId=3, AzureServiceTypeDescription="IaaS", AzureServiceTypeStatus=true,
					AzureProducts = new List<AzureProduct>
					{
						new AzureProduct {Id = Guid.NewGuid().ToString(), AzureProductId = 1, AzureProductDescription = "SQL",  AzureProductIcon ="icon-sql-database", AzureProductStatus = true },
						new AzureProduct {Id = Guid.NewGuid().ToString(), AzureProductId = 2, AzureProductDescription = "Active Directory", AzureProductIcon="icon-active-directory", AzureProductStatus = true },
					}
				}
			};

			foreach (var serviceType in serviceTypes)
				context.Set<AzureServiceType>().Add(serviceType);

			var productInformation = new AzureProductInformation
			{
				Id = Guid.NewGuid().ToString(),
				AzureProductInformationId = 1,
				AzureProductId = 1,
				AzureCategoryId = 4,
				AzureProductInformationDescription = "Create multiple files for tempdb on temporary drive (D) when using series D and G",
				InformationDate = DateTime.Now,
				AzureProductInformationStatus = true
			};

			context.Set<AzureProductInformation>().Add(productInformation);

			base.Seed(context);
		}
	}
}

