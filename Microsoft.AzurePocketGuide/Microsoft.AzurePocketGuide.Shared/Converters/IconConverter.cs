using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Microsoft.AzurePocketGuide.Converters
{
	/// <summary>
	/// Converter for the icon resources
	/// </summary>
	public class IconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value == null)
				return App.Current.Resources["azure"];

			return App.Current.Resources[value.ToString()];
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
