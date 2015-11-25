using System;
using System.Collections.Generic;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.AzurePocketGuide.Services;
using Microsoft.Practices.Prism.Mvvm;
using Windows.UI.Popups;

namespace Microsoft.AzurePocketGuide.ViewModels
{
	public class ServicesPageViewModel : ViewModel
	{
		private readonly IServicesRepository _repository;
		private List<ServiceItem> _items;
		private ServiceItem _selectedItem;

		public ServicesPageViewModel() { }

		public ServicesPageViewModel(IServicesRepository repository)
		{
			_repository = repository;
			LoadData();
		}

		public async void LoadData()
		{
			try
			{
				Items = await _repository.GetAllServiceItems();
			}
			catch (Exception e)
			{
				await new MessageDialog(e.Message, "Error loading items").ShowAsync();
			}
		}


		public List<ServiceItem> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }

		}

		public ServiceItem SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value); }
		}

	}
}
