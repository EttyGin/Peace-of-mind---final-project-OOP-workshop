using static loginDb.ViewModels.ClientsViewModel;
using System.Globalization;
using System.Windows.Data;
using System;
using System.Windows;
using static loginDb.ViewModels.ViewModelBase;

namespace loginDb.Converters
{
    public class ModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string target = parameter as string;
            if (value is EditMode mode)
            {
                switch (target)
                {
                    case "Add":
                        {
                            if (mode == EditMode.Add)
                                return Visibility.Visible;
                            else 
                                return Visibility.Collapsed;
                        }
                    case "Edit":
                        {
                            if (mode == EditMode.Edit)
                                return Visibility.Visible;
                            else
                                return Visibility.Collapsed;
                        }
                    default:
                        return null;

                }

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
