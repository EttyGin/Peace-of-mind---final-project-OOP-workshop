using loginDb.Models;
using loginDb.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;


namespace loginDb.Converters
{
    public class ToTargetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IUserRepository userRepository = new UserRepository();

            var myObject = value as Payment;
            if (myObject == null)
                return null;

            if (myObject.ClientID != null) {
                Client clnt = (Client)userRepository.GetById(myObject.ClientID.Value, "Client");
                return clnt?.Cname ?? " ";
            }
            else
            {
                Payer pyr = (Payer)userRepository.GetById(myObject.PayerID.Value, "Payer");
                return pyr?.Pname ?? " ";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
