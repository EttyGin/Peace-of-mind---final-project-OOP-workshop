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
    public class PayersViewModel : ViewModelBase
    {
        //Fields
        public ObservableCollection<Payer> _lstPayers;

        private ObservableCollection<Payer> _filteredPayers;

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

        public ObservableCollection<Payer> LstPayers
        {
            get
            {
                return _lstPayers;
            }

            set
            {
                _lstPayers = value;
                OnPropertyChanged(nameof(LstPayers));
            }
        }

        public ObservableCollection<Payer> FilteredPayers
        {
            get => _filteredPayers;
            private set
            {
                if (_filteredPayers != value)
                {
                    _filteredPayers = value;
                    OnPropertyChanged(nameof(FilteredPayers));
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
        public ICommand ShowPayersCommand { get; }

        //Constructor
        public PayersViewModel()
        {
            userRepository = new UserRepository();

            ShowAddCommand = new ViewModelCommand(ExecuteShowAddCommand);
            ShowEditCommand = new ViewModelCommand(ExecuteShowEditCommand);
            SearchCommand = new ViewModelCommand(ExecuteSearchCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand);
            ShowPayersCommand = new ViewModelCommand(ExecuteShowPayersCommand);

            LoadPayers(null);
        }

        private void LoadPayers(Expression<Func<Payer, bool>> predicate)
        {
            string NoneName = " -";
            if (!(predicate is null))
                LstPayers = new ObservableCollection<Payer>(userRepository.GetWhere(predicate));
            else
                LstPayers = new ObservableCollection<Payer>(userRepository.GetWhere<Payer>(p => p.Pname != NoneName)); //To avoid showing the none payer
            
            FilteredPayers = new ObservableCollection<Payer>(LstPayers);
        }

        private void ExecuteShowAddCommand(object obj)
        {
            AddOrEditPayerView addPayerWin = new AddOrEditPayerView(EditMode.Add ,obj as Payer);
            addPayerWin.ShowDialog();
            LoadPayers(null);
        }

        private void ExecuteSearchCommand(object obj)
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                LoadPayers(p => p.Pname.Contains(SearchText));
            }
            else
            {
                LoadPayers(p => p.Pname == p.Pname);
            }
        }

        private void ExecuteShowEditCommand(object obj)
        {
            AddOrEditPayerView addPayerWin = new AddOrEditPayerView(EditMode.Edit, obj as Payer);
            addPayerWin.ShowDialog();
            LoadPayers(null);
         }

        private void ExecuteShowPayersCommand(object obj)
        {
            IsViewVisible = false;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            Payer toRemove = obj as Payer;
            string name = toRemove.Pname;

            var result = MessageBox.Show($"Are you sure you want to delete {name}?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    userRepository.Remove(toRemove, "Id");
                    LstPayers.Remove(toRemove);
                }
                catch {
                    ErrorMessage = $"The Payer {name} cannot be deleted because it is active";
                    MessageBox.Show(ErrorMessage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ErrorMessage = string.Empty;
                }
                

            }
            LoadPayers(null);
        }
    }
}