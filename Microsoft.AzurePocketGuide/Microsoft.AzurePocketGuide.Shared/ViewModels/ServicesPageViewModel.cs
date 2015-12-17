using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.AzurePocketGuide.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Popups;

namespace Microsoft.AzurePocketGuide.ViewModels
{
	/// <summary>
	/// ServicePageViewModel
	/// </summary>
	public class ServicesPageViewModel : ViewModel
	{
		#region vars

		private readonly IServicesRepository _repository;
		private readonly INavigationService _navigationService;
		private List<ServiceType> _items;
		private Product _selectedItem;
		private bool _isWorking = false;

		#endregion

		#region ctors

		public ServicesPageViewModel() { }

		public ServicesPageViewModel(IServicesRepository repository, INavigationService navigationService)
		{
			_repository = repository;
			_navigationService = navigationService;

			RefreshCommand = new DelegateCommand(RefreshExecute);

			LoadData();
		}

		#endregion

		#region commands

		/// <summary>
		/// Command to manually refresh the data
		/// </summary>
		public ICommand RefreshCommand { get; private set; }

		private void RefreshExecute()
		{
			LoadData(true); //force sync
		}

		#endregion

		#region methods

		/// <summary>
		/// Fetch the data from the repository
		/// </summary>
		/// <param name="forceSync">If true forces the sync to pull the data from the mobile services, if don't pulls the data from the local database</param>
		public async void LoadData(bool forceSync = false)
		{
			this.IsWorking = true;
			try
			{
				Items = await _repository.GetAllServices(forceSync);
			}
			catch (Exception e)
			{
				await new MessageDialog(e.Message, "Error loading data").ShowAsync();
			}
			finally
			{
				this.IsWorking = false;
			}
		}

		#endregion

		#region bindings

		/// <summary>
		/// Pivot list with all the azure services grouped by type
		/// </summary>
		public List<ServiceType> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }

		}

		/// <summary>
		/// Selected service/product
		/// </summary>
		public Product SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				SetProperty(ref _selectedItem, value);
				if (_selectedItem != null)
					_navigationService.Navigate("Product", _selectedItem.ProductId);
			}
		}

		/// <summary>
		/// Indicates if the viewModel is awaiting to complete some operation
		/// </summary>
		public bool IsWorking
		{
			get { return _isWorking; }
			set { SetProperty(ref _isWorking, value); }
		}

		#endregion
	}
}
