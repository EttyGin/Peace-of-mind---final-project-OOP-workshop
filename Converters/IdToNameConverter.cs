using loginDb.Models;
using loginDb.Repositories;
using loginDb.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace loginDb.Converters
{
    public class IdToNameConverter : IValueConverter
    {
        private UserRepository userRepository;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                if (value is int id)
                {

                    userRepository = new UserRepository();
                    switch (parameter.ToString())
                    {
                        case "Client":
                            {
                                Client clnt = (Client)userRepository.GetById(id, "Client");
                                return clnt?.Cname ?? " ";
                            }
                        case "Payer":
                            {
                                Payer pyr = (Payer)userRepository.GetById(id, "Payer");
                                return pyr?.Pname ?? " ";
                            }
                        
                    }
                   
                }
                return null;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
