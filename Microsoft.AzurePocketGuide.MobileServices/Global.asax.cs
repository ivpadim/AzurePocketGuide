using System.Web.Http;
using System.Web.Routing;

namespace Microsoft.AzurePocketGuide.MobileServices
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			WebApiConfig.Register();
		}
	}
}