using System;
using System.Collections.Generic;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.AzurePocketGuide.Services;
using Microsoft.Practices.Prism.Mvvm;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Microsoft.AzurePocketGuide.ViewModels
{
	public class ServiceDetailPageViewModel : ViewModel
	{
		private readonly IServicesRepository _repository;
		private Product _selectedItem;

		public ServiceDetailPageViewModel() { }

		public ServiceDetailPageViewModel(IServicesRepository repository)
		{
			_repository = repository;
		}

		public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
		{
			if (navigationParameter != null)
			{
				var productId = int.Parse(navigationParameter.ToString());
				try
				{
					SelectedItem = await _repository.GetProductById(productId);
				}
				catch (Exception e)
				{
					await new MessageDialog(e.Message, "Error loading service").ShowAsync();
				}
			}
		}

		public Product SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value); }
		}

	}
}
