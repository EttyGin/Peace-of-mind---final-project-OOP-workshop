using loginDb.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            HomeViewModel hvm = new HomeViewModel();
            DataContext = hvm;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth != 548 || ActualHeight != 333)
            {
                Set(60, 120);                
            }
            else
            {
                // The UserControl is not full screen
                Set(30, 45);
            }
        }

        private void Set(int fsize, int hsize)
        {
            foreach (var item in exStackPanel.Children)
            {
                if (item is Label l)
                {
                    l.FontSize = fsize;
                    l.Height = hsize;

                }
            }
            foreach (var item in inStackPanel.Children)
            {
                if (item is Label l)
                {
                    l.FontSize = fsize;
                    l.Height = hsize;
                }
            }
        }
    }
}
