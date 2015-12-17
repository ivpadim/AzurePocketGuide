using System.Linq;
using Microsoft.AzurePocketGuide.Models;
using Microsoft.AzurePocketGuide.ViewModels;
using Microsoft.Practices.Prism.StoreApps;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Microsoft.AzurePocketGuide.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public partial class ServicesPage : VisualStateAwarePage
	{
		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var viewModel = this.DataContext as ServicesPageViewModel;
			var selectedItem = e.AddedItems.FirstOrDefault() as Product;

			viewModel.SelectedItem = selectedItem;
		}

	}
}
