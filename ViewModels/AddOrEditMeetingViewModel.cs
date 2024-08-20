using loginDb.Models;
using loginDb.Repositories;
using loginDb.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace loginDb.ViewModels
{

    public class AddOrEditMeetingViewModel : ViewModelBase
    {
        public static ObservableCollection<Client> Clients { get; set; }

        private IUserRepository userRepository;
        public ICommand AorECommand { get; }

        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; } = "00:00";
        public string Summary { get; set; }
        public Status Status { get; set; }

        public int UserId { get; set; }
        public int ClientId { get; set; }

        private string _errorMessage;
        private bool _isViewVisible = true;

        public EditMode CurrentMode { get; set; }

        private Meeting _selectedMeeting;

        public int SpeClientId { get; set; }


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

        public Meeting SelectedMeeting
        {
            get { return _selectedMeeting; }
            set
            {
                _selectedMeeting = value;
                OnPropertyChanged(nameof(SelectedMeeting));
            }
        }


        public AddOrEditMeetingViewModel(EditMode mode, Meeting meeting, int userId)
        {
            CurrentMode = mode;
            UserId = ReadUserIdFromFile();
            if (meeting != null)
            {
                _selectedMeeting = meeting;                
                SpeClientId = SelectedMeeting.ClientId;
            }
            else
            {
                _selectedMeeting = new Meeting();
            }
            userRepository = new UserRepository();
            AorECommand = new ViewModelCommand(ExecuteAorECommand);

            LoadClients();
        }

        private void LoadClients()
        {
            Clients = new ObservableCollection<Client>(userRepository.GetAll<Client>());
            OnPropertyChanged(nameof(Clients));
        }

        private bool CanAorECommand()
        {
            if (CurrentMode == EditMode.Add)
            {
                if (Date != null)
                {
                    DateTime lastAppointmentDate = userRepository.GetMeetingDateForClient(ClientId, Number); // פונקציה להביא את התאריך האחרון
                    if (Date <= lastAppointmentDate)
                    {
                        ErrorMessage = $"Meeting date must be later than the previous meeting";
                        return false;
                    }
                 }
                else if (Hour != null)
                {
                    if (!DateTime.TryParseExact(Hour, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        ErrorMessage = $"Incorrect Hour";
                        return false;

                    }

                }

                else if (Summary == null || Summary.Length > 100)
                {
                    ErrorMessage = $"Incorrect Summary";
                    return false;
                }

                else
                {
                    ErrorMessage = "";
                }
                return true;
            }
            else // edit mode
            {
                if (SelectedMeeting.ClientId == 0)
                {
                    ErrorMessage = $"Incorrect Client";
                    return false;
                }
                else if (SelectedMeeting.Date != null && SelectedMeeting.Number !=1)
                {
                    DateTime lastAppointmentDate = userRepository.GetMeetingDateForClient(SelectedMeeting.ClientId, SelectedMeeting.Number - 1);
                    if (SelectedMeeting.Date <= lastAppointmentDate)
                    {
                        ErrorMessage = $"Meeting date must be later than the previous meeting";
                        return false;
                    }
             }
                else if (SelectedMeeting.Summary == null || SelectedMeeting.Summary.Length > 100)
                {
                    ErrorMessage = $"Incorrect Summary";
                    return false;
                }

                else
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
                        User user = userRepository.GetByUsername("admin");
                        Client client = (Client)userRepository.GetById(ClientId, "Client");

                        if (DateTime.TryParseExact(Hour, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTime))
                        {
                            Date = new DateTime(Date.Year, Date.Month, Date.Day, parsedTime.Hour, parsedTime.Minute,0);
                        }
                        Status Stat = Date > DateTime.Now ? Status.planned : Status.unpaid;
                        Meeting m = new Meeting { Number = Number, Date = Date, Summary = Summary, Status = Stat, UserId = UserId, ClientId = ClientId };
                        userRepository.Add(m);
                        
                        ErrorMessage = "Meeting added successfully!";

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
                        
                        SelectedMeeting.ClientId = SpeClientId;
                        if (SelectedMeeting.Status == Status.planned && SelectedMeeting.Date < DateTime.Now)
                            SelectedMeeting.Status = Status.unpaid;
                        userRepository.Edit(SelectedMeeting);

                        ErrorMessage = "Meeting was saved successfully!";

                        Task.Delay(800).ContinueWith(_ => // Wait before closing
                        {
                            IsViewVisible = false;

                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = $"Error updating Meeting: {ex.Message}";
                    }
                }
            }
        }
    }
}
