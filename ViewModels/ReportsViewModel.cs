using loginDb;
using loginDb.Models;
using loginDb.Repositories;
using System.Windows.Input;
using System.Collections.ObjectModel;
using loginDb.View;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace loginDb.ViewModels
{



    public class ReportsViewModel : ViewModelBase
    {
        public class Filtered : ViewModelBase
        {
            public string Name { get; set; }
            public int MeetingsAmount { get; set; }
            public int Debt { get; set; }


            public Filtered(string name, int meetingsAmount, int debt)
            {
                Name = name;
                MeetingsAmount = meetingsAmount;
                Debt = debt;
            }
        }
        //Fields
        private int _numOfClients;
        private int _numOfMeetings;
        private int _revenue;
        private int _receivable;
        
        private ObservableCollection<Filtered> _filteredClients;

        private ObservableCollection<Filtered> _filteredPayers;

        private string _errorMessage;
        private bool _isViewVisible = false;

        private IUserRepository userRepository;
  
        //Properties
        public int NumOfClients
        {
            get { return _numOfClients; }
            set
            {
                _numOfClients = value;
                OnPropertyChanged(nameof(NumOfClients));
            }
        }        
        public int NumOfMeetings
        {
            get { return _numOfMeetings; }
            set
            {
                _numOfMeetings = value;
                OnPropertyChanged(nameof(NumOfMeetings));
            }
        }        
        public int Revenue
        {
            get { return _revenue; }
            set
            {
                _revenue = value;
                OnPropertyChanged(nameof(Revenue));
            }
        }
        public int Receivable
        {
            get { return _receivable; }
            set
            {
                _receivable = value;
                OnPropertyChanged(nameof(Receivable));
            }
        }

        public ObservableCollection<Filtered> FilteredPayers
        {
            get
            {
                return _filteredPayers;
            }

            set
            {
                _filteredPayers = value;
                OnPropertyChanged(nameof(FilteredPayers));
            }
        }
        public ObservableCollection<Filtered> FilteredClients
        {
            get
            {
                return _filteredClients;
            }

            set
            {
                _filteredClients = value;
                OnPropertyChanged(nameof(FilteredClients));
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

        //Constructor
        public ReportsViewModel()
        {
            userRepository = new UserRepository();
            LoadAll();
        }

        private async void LoadAll()
        {
            ObservableCollection<Client> LstClients = new ObservableCollection<Client>(userRepository.GetWhere<Client>(c => c.Cname == c.Cname));
            int UserId = ReadUserIdFromFile();
            var results = await userRepository.LoadAllAsync(UserId);
            NumOfClients = results.NumOfClients;
            NumOfMeetings = results.NumOfMeetings;
            Revenue = results.Revenue;
            Receivable = results.Receivable;

            FilteredClients = new ObservableCollection<Filtered>();
            foreach (Client c in LstClients)
            {
                string name = c.Cname;
                int amoubt = userRepository.GetWhere<Meeting>(m => m.ClientId == c.Id).Count();
                int debt = userRepository.GetDebtById(c.Id,true, UserId);
                Filtered fc = new Filtered(name, amoubt, debt);
                FilteredClients.Add(fc);
            }

            ObservableCollection<Payer> LstPayers = new ObservableCollection<Payer>(userRepository.GetWhere<Payer>(p => p.Pname == p.Pname));
            FilteredPayers = new ObservableCollection<Filtered>();
            foreach (Payer p in LstPayers)
            {
                string name = p.Pname;
                if (name.Equals(" -")) //none payer
                    continue;

                int amoubt = userRepository.GetWhere<Client>(c => c.PayerId == p.Id).Count();
                int debt = userRepository.GetDebtById(p.Id, false, UserId);
                Filtered fc = new Filtered(name, amoubt, debt);
                FilteredPayers.Add(fc);
            }

        }

    }
}