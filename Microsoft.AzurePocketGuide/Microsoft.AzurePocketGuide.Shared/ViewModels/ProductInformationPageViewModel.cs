using System.Collections.Generic;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.AzurePocketGuide.Services;
using Microsoft.Practices.Prism.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Microsoft.AzurePocketGuide.ViewModels
{
	public class ProductInformationPageViewModel : ViewModel
	{
		private readonly IServicesRepository _repository;
		private IEnumerable<ProductInformation> _items;

		public ProductInformationPageViewModel() { }

		public ProductInformationPageViewModel(IServicesRepository repository)
		{
			_repository = repository;
		}

		public override  void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
		{
			if (navigationParameter != null)
			{
				Items = navigationParameter as IEnumerable<ProductInformation>;
			}
		}

		public IEnumerable<ProductInformation> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

	}
}
