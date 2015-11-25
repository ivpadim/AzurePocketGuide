using System.Threading.Tasks;
using Microsoft.AzurePocketGuide.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using Windows.ApplicationModel.Activation;
using Windows.UI;
using Windows.UI.Xaml;

namespace Microsoft.AzurePocketGuide
{
	sealed partial class App : MvvmAppBase
	{
		IUnityContainer _container = new UnityContainer();

		public App()
		{


		}

		/// <summary>
		/// Required override. Generally you do your initial navigation to launch page, or 
		/// to the page approriate based on a search, sharing, or secondary tile launch of the app
		/// </summary>
		/// <param name="args">The launch arguments passed to the application</param>
		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{

#if WINDOWS_PHONE_APP

			var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
			if (statusBar != null)
			{

				statusBar.BackgroundOpacity = 1;
				statusBar.ForegroundColor = Color.FromArgb(255,104,104,104);
			    statusBar.BackgroundColor = Colors.White;

			}
#endif



			// Use the logical name for the view to navigate to. The default convention
			// in the NavigationService will be to append "Page" to the name and look 
			// for that page in a .Views child namespace in the project. IF you want another convention
			// for mapping view names to view types, you can override 
			// the MvvmAppBase.GetPageNameToTypeResolver method
			NavigationService.Navigate("Services", null);
			return Task.FromResult<object>(null);
		}

		/// <summary>
		/// This is the place you initialize your services and set default factory or default resolver for the view model locator
		/// </summary>
		/// <param na
		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{
			// Register MvvmAppBase services with the container so that view models can take dependencies on them
			_container.RegisterInstance<ISessionStateService>(SessionStateService);
			_container.RegisterInstance<INavigationService>(NavigationService);
			_container.RegisterType<IServicesRepository, ServicesRepository>(new ContainerControlledLifetimeManager());

			// Register factory methods for the ViewModelLocator for each view model that takes dependencies so that you can pass in the
			// dependent services from the factory method here.
			ViewModelLocationProvider.SetDefaultViewModelFactory((viewModelType) => _container.Resolve(viewModelType));
			return Task.FromResult<object>(null);
		}

	}
}