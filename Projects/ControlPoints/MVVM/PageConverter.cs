using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlPoints
{
    class PageConverter : BaseValueConverter<PageConverter>
    {
        public static MainPage main = null;
        public static ParametersPage parametersPage = null;
        public static ViewModel model = null;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Main:
                    return main = new MainPage(model);
                case ApplicationPage.Parameters:
                    return parametersPage = new ParametersPage(model);
                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
