using loginDb;
using loginDb.Models;
using loginDb.Repositories;
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
using System.Collections.ObjectModel;
using loginDb.View;
using System.Data.Entity;
using System.Windows;
using System.Linq.Expressions;
using FontAwesome.Sharp;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace loginDb.ViewModels
{
    public class PaymentsViewModel : ViewModelBase
    {
        //Fields
        public ObservableCollection<Payment> _lstPayments;

        private ObservableCollection<Payment> _filteredPayments;

        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        private string _searchText;

        //Properties
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ObservableCollection<Payment> LstPayments
        {
            get
            {
                return _lstPayments;
            }

            set
            {
                _lstPayments = value;
                OnPropertyChanged(nameof(LstPayments));
            }
        }

        public ObservableCollection<Payment> FilteredPayments
        {
            get => _filteredPayments;
            set
            {
                if (_filteredPayments != value)
                {
                    _filteredPayments = value;
                    OnPropertyChanged(nameof(FilteredPayments));
                }
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

        //-> Commands
        public ICommand ShowAddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ShowEditCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ShowPaymentsCommand { get; }

        //Constructor
        public PaymentsViewModel()
        {
            userRepository = new UserRepository();
            ShowAddCommand = new ViewModelCommand(ExecuteShowAddCommand);
            ShowEditCommand = new ViewModelCommand(ExecuteShowEditCommand);
            SearchCommand = new ViewModelCommand(ExecuteSearchCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand);
            ShowPaymentsCommand = new ViewModelCommand(ExecuteShowPaymentsCommand);

            LoadPayments(null);
        }

        private void LoadPayments(Expression<Func<Payment, bool>> predicate)
        {
            if (!(predicate is null))
                LstPayments = new ObservableCollection<Payment>(userRepository.GetWhere(predicate));
            else
                LstPayments = new ObservableCollection<Payment>(userRepository.GetWhere<Payment>(p => p.Id == p.Id)); //To avoid showing the none Payment
            
            FilteredPayments = new ObservableCollection<Payment>();
            var sorted  = LstPayments.OrderByDescending(p => p.LastUpdate).ToList();
            FilteredPayments = new ObservableCollection<Payment>();
            foreach (var meeting in sorted)
            {
                FilteredPayments.Add(meeting);
            }
        }

        private void ExecuteShowAddCommand(object obj)
        {
            AddOrEditPaymentView addPaymentWin = new AddOrEditPaymentView(EditMode.Add ,obj as Payment);
            addPaymentWin.ShowDialog();
            LoadPayments(null);
        }

        private void ExecuteSearchCommand(object obj)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {

                LoadPayments(p => (p.PayerID != null) ? p.Payer.Pname.ToString().Contains(SearchText) : p.Client.Cname.ToString().Contains(SearchText));
            }
            else
            {
                LoadPayments(p => p.Id == p.Id);
            }
        }

        private void ExecuteShowEditCommand(object obj)
        {
            AddOrEditPaymentView addPaymentWin = new AddOrEditPaymentView(EditMode.Edit, obj as Payment);
            addPaymentWin.ShowDialog();
            LoadPayments(null);
         }

        private void ExecuteShowPaymentsCommand(object obj)
        {
            IsViewVisible = false;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            Payment toRemove = obj as Payment;
            int id = toRemove.Id;

            var result = MessageBox.Show("Are you sure you want to delete it?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    userRepository.Remove(toRemove, "Id");
                    LstPayments.Remove(toRemove);
                }
                catch {
                    ErrorMessage = "The Payment cannot be deleted because it is active";
                    MessageBox.Show(ErrorMessage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ErrorMessage = string.Empty;
                }
                

            }
            LoadPayments(null);
        }
    }
}