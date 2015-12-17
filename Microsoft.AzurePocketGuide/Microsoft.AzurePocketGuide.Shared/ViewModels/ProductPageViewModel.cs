using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.AzurePocketGuide.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Microsoft.AzurePocketGuide.ViewModels
{
	public class ProductPageViewModel : ViewModel
	{
		private readonly IServicesRepository _repository;
		private readonly INavigationService _navigationService;
		private Product _item;
		private Category _selectedItem;

		public ProductPageViewModel() { }

		public ProductPageViewModel(IServicesRepository repository, INavigationService navigationService)
		{
			_repository = repository;
			_navigationService = navigationService;
		}

		public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
		{
			if (navigationParameter != null)
			{
				var productId = int.Parse(navigationParameter.ToString());
				try
				{
					Item = await _repository.GetProductById(productId);
				}
				catch (Exception e)
				{
					await new MessageDialog(e.Message, "Error loading service").ShowAsync();
				}
			}
		}

		public Product Item
		{
			get { return _item; }
			set { SetProperty(ref _item, value); }
		}

		public Category SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				SetProperty(ref _selectedItem, value);
				if (_selectedItem != null)
					_navigationService.Navigate("ProductInformation", Item.Information.Where(i=>i.CategoryId == _selectedItem.CategoryId).ToList());
			}
		}

	}
}
