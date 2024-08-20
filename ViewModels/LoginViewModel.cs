using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using loginDb.Models;
using loginDb.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Windows.Controls;

namespace loginDb.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _sPassword;
        private string _password;
        private int _id;
        private string _email;
        private string _firstName;
        private string _lastName;
        private int _price;
        private string _errorMessage;
        private string _signErrorMessage;
        private bool _isViewVisible = true;
        private int _selectedTabIndex = 0;
        private IUserRepository _userRepository;

        //Properties
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString SPassword
        {
            get
            {
                return _sPassword;
            }

            set
            {
                _sPassword = value;
                OnPropertyChanged(nameof(SPassword));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public int Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string SignErrorMessage
        {
            get
            {
                return _signErrorMessage;
            }

            set
            {
                _signErrorMessage = value;
                OnPropertyChanged(nameof(SignErrorMessage));
            }
        }
        public int SelectedTabIndex
        {
            get
            {
                return _selectedTabIndex;
            }

            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
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

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }


        //Constructor
        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand);

            ErrorMessage = string.Empty;
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                SPassword == null || SPassword.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {

            var isValidUser = _userRepository.AuthenticateUser(new NetworkCredential(Username, SPassword));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);

                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }
        private bool CanSignUpCommand(object obj)
        {
            var ualst = _userRepository.GetWhere<UserAccount>(ua => ua.Username == ua.Username);
            if (ualst != null && ualst.Count() > 0)
            {
                SignErrorMessage = "* There is already a registered user, it is impossible to add.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
            {
                SignErrorMessage = "* Invalid username or password";
                return false;
            }
            var lst = _userRepository.GetWhere<UserAccount>(ua => ua.Username == Username);
            if (lst != null && lst.Count() > 0)
            {
                SignErrorMessage = "* This username is not available, try another one.";
                return false;
            }
            if (Id.ToString().Length < 8)
            {
                SignErrorMessage = $"Incorrect ID";
                return false;
            }
            else if (FirstName == null || !FirstName.Replace(" ", "").All(char.IsLetter) || FirstName.Length > 20 ||
                LastName == null || !LastName.Replace(" ", "").All(char.IsLetter) || LastName.Length > 20)
            {
                SignErrorMessage = $"Incorrect Name";
                return false;
            }
            else if (Email == null || Email.Length > 30 || !Regex.IsMatch(Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                SignErrorMessage = $"Incorrect Email";
                return false;
            }
            else
            {
                SignErrorMessage = "";
            }
            return true;
        }

        private void ExecuteSignUpCommand(object obj)
        {
            if (CanSignUpCommand(obj))
            {
                UserAccount userAccount = new UserAccount { Username = Username, DisplayName = FirstName + " " + LastName };
                _userRepository.Add(userAccount);
                User user = new User { Username = Username, Password = Password, Id = Id, Email = Email, Name = FirstName, LastName = LastName, Price = Price };
                _userRepository.Add(user);
                SignErrorMessage = "User Signed up successfully!";
                Task.Delay(2500).ContinueWith(_ => // Wait before closing
                {
                    SignErrorMessage = "";
                    SelectedTabIndex = 0;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

        }

    }
}
