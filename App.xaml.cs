using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using loginDb.View;

namespace loginDb
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void Application_Startup(object sender, StartupEventArgs e)
        {
              var loginView = new LoginView();
              loginView.Show();
              loginView.IsVisibleChanged += (s, ev) =>
              {
                  if (loginView.IsVisible == false && loginView.IsLoaded)
                  {
                      var mainView = new MainView();
                      mainView.Show();
                      loginView.Close();
                  }
              };           
        }
    }
}