using FontAwesome.Sharp;
using loginDb.Models;
using loginDb.Repositories;
using loginDb.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace loginDb.View
{
    /// <summary>
    /// Interaction logic for ClientsView.xaml
    /// </summary>
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
            ClientsViewModel cvm = new ClientsViewModel();
            DataContext = cvm;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth != 548 || ActualHeight != 333)
            {
                // The UserControl is effectively full screen
                SetColumnWidths(new DataGridLength(1, DataGridLengthUnitType.Star));
            }
            else
            {
                // The UserControl is not full screen
                SetColumnWidths(DataGridLength.Auto);
            }
        }

        private void SetColumnWidths(DataGridLength width)
        {
            foreach (var column in datagrid.Columns)
            {
                column.Width = width;
            }
        }
    }
}
