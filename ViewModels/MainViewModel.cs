using loginDb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using FontAwesome.Sharp;
using loginDb.Models;
using loginDb.Repositories;
using loginDb.View;
using System.Windows.Media.Imaging;
using System.IO;

using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using System.Configuration;



namespace loginDb.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccount _currentUserAccount;
        private int _userId;
        private ViewModelBase _currntChildView;
        private string _caption;
        private IconChar _icon;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;
  //      private readonly INavigationService _navigationService;


        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }



        //Properties
        public UserAccount CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currntChildView;
            }
            set
            {
                _currntChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowClientsViewCommand { get; }
        public ICommand ShowMeetingsViewCommand { get; }
        public ICommand ShowPayersViewCommand { get; }
        public ICommand ShowReportsViewCommand { get; }
        public ICommand ShowPaymentsViewCommand { get; }

        public ICommand LogoutCommand { get; }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccount();
            
            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowClientsViewCommand = new ViewModelCommand(ExecuteShowClientViewCommand);
            ShowMeetingsViewCommand = new ViewModelCommand(ExecuteShowMeetingsViewCommand);
            ShowPayersViewCommand = new ViewModelCommand(ExecuteShowPayersViewCommand);
            ShowReportsViewCommand = new ViewModelCommand(ExecuteShowReportsViewCommand);
            ShowPaymentsViewCommand = new ViewModelCommand(ExecuteShowPaymentsViewCommand);
            LogoutCommand = new ViewModelCommand(ExecuteLogoutCommand);

            //Default view
            LoadCurrentUserData();
            ExecuteShowHomeViewCommand(null);
        }



        private void ExecuteLogoutCommand(object obj)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Thread.CurrentPrincipal = null;
             
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
                string filePath = "D:\\לימודים\\2024ב סדנה במונחה עצמים\\פרויקט גמר - Peace Of Mind\\V1\\loginDb\\CurrentUserId.txt";

                // Check if file exists, if so, delete it
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }


            }
        }


        private void ExecuteShowClientViewCommand(object obj)
        {
            CurrentChildView = new ClientsViewModel();
            Caption = "Clients";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteShowMeetingsViewCommand(object obj)
        {
            CurrentChildView = new MeetingsViewModel();
            Caption = "Meetings";
            Icon = IconChar.Couch;
        }
        private void ExecuteShowPayersViewCommand(object obj)
        {
            CurrentChildView = new PayersViewModel();
            Caption = "Payers";
            Icon = IconChar.HandHoldingHeart;
        }

        private void ExecuteShowPaymentsViewCommand(object obj)
        {
            CurrentChildView = new PaymentsViewModel();
            Caption = "Payments";
            Icon = IconChar.HandHoldingHeart;
        }
        private void ExecuteShowReportsViewCommand(object obj)
        {
            CurrentChildView = new ReportsViewModel();
            Caption = "Reports";
            Icon = IconChar.ChartPie;
        }

        
        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"{user.Name} {user.LastName}";

                CurrentUserAccount.ProfilePic = null; // "/Images/profileP.png";
                UserId = user.Id;

                try
                {
                    CreateUserIdFile(UserId);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error while login, try later.");
                }

            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            }
        }


        public void CreateUserIdFile(int id)
        {
            string filePath = "D:\\לימודים\\2024ב סדנה במונחה עצמים\\פרויקט גמר - Peace Of Mind\\V1\\loginDb\\CurrentUserId.txt";

            // Check if file exists, if so, delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                
            }

            // Create the file and write the ID
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.Write(id);
            }
        }
    }
}




