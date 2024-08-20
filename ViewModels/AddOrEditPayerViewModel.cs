using loginDb.Models;
using loginDb.Repositories;
using loginDb.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using static loginDb.ViewModels.PayersViewModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace loginDb.ViewModels
{

    public class AddOrEditPayerViewModel : ViewModelBase
    {
        public static ObservableCollection<Payer> Payers { get; set; }

        private IUserRepository userRepository;
        public ICommand AorECommand { get; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CName { get; set; }
        public string CEmail { get; set; }
        public short Payment { get; set; }

        private string _errorMessage;
        private bool _isViewVisible = true;

        public EditMode CurrentMode { get; set; }

        private Payer _selectedPayer;


        //Properties
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

        public Payer SelectedPayer
        {
            get { return _selectedPayer; }
            set
            {
                _selectedPayer = value;
                OnPropertyChanged(nameof(SelectedPayer));
            }
        }


        public AddOrEditPayerViewModel(EditMode mode, Payer payer)
        {
            CurrentMode = mode;
            if (payer != null)
            {
                SelectedPayer = payer;
            }
            else
            {
                SelectedPayer = new Payer();
            }
            userRepository = new UserRepository();
            AorECommand = new ViewModelCommand(ExecuteAorECommand);

            LoadPayers();
        }

        private void LoadPayers()
        {
            userRepository.InitNonePayer();
            Payers = new ObservableCollection<Payer>(userRepository.GetAll<Payer>());
            OnPropertyChanged(nameof(Payers));
        }
        private bool CanAorECommand()
        {
            if (CurrentMode == EditMode.Add)
            {
                if (Name == null || !Name.Replace(" ", "").All(char.IsLetter) || Name.Length > 20)
                {
                    ErrorMessage = "Incorrect Payer Name";
                    return false;
                }
                if (CName == null || !CName.Replace(" ", "").All(char.IsLetter) || CName.Length > 20)
                {
                    ErrorMessage = "Incorrect Contact Name";
                    return false;
                }
                else if (CEmail == null || CEmail.Length > 30 || !Regex.IsMatch(CEmail, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                {
                    ErrorMessage = $"Incorrect Email";
                    return false;
                }
                else if (Payment == 0 )
                {
                    ErrorMessage = $"Incorrect Payment";
                    return false;
                }
                {
                    ErrorMessage = "";
                }
                return true;
            }
            else
            {
                if (SelectedPayer.Pname == null || !SelectedPayer.Pname.Replace(" ", "").All(char.IsLetter) || SelectedPayer.Pname.Length > 20)
                {
                    ErrorMessage = "Incorrect Payer Name";
                    return false;
                }
                if (SelectedPayer.ContactName == null || !SelectedPayer.ContactName.Replace(" ", "").All(char.IsLetter) || SelectedPayer.ContactName.Length > 20)
                {
                    ErrorMessage = "Incorrect Contact Name";
                    return false;
                }
                else if (SelectedPayer.ContactEmail == null || SelectedPayer.ContactEmail.Length > 30 || !Regex.IsMatch(SelectedPayer.ContactEmail, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                {
                    ErrorMessage = $"Incorrect Email";
                    return false;
                }
                else if (SelectedPayer.TotalPayment == 0)
                {
                    ErrorMessage = $"Incorrect Payment";
                    return false;
                }
                {
                    ErrorMessage = "";
                }
                return true;
            }
        }

        private void ExecuteAorECommand(object obj)
        {
            if (CanAorECommand())
            {
                if (CurrentMode == EditMode.Add)
                {
                    try
                    {
                        Payer p = new Payer {Pname = Name, ContactName = CName, ContactEmail = CEmail, TotalPayment = Payment };
                        userRepository.Add(p);
                        

                        ErrorMessage = "Payer added successfully!";

                        Task.Delay(1200).ContinueWith(_ => // Wait before closing
                        {
                            IsViewVisible = false;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    catch (Exception)
                    {
                        ErrorMessage = $"Please fill in all required fields correctly";
                    }
                }
                else // CurrentMode == EditMode.Edit
                {
                    try
                    {
                        userRepository.Edit(SelectedPayer);

                        ErrorMessage = "Payer was saved successfully!";

                        Task.Delay(800).ContinueWith(_ => // Wait before closing
                        {
                            IsViewVisible = false;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = $"Error updating Payer: {ex.Message}";

                    }
                }
            }
        }
    }
}
