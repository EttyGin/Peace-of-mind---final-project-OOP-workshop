using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using loginDb.Models;
using loginDb.Repositories;
using loginDb.View;
using loginDb.ViewModels;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace loginDb.ViewModels
{
    public class Installed
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        // You can add other properties if needed
    }

    public class ClientConfig
    {
        public Installed installed { get; set; }
    }


    public class AddOrEditPaymentViewModel : ViewModelBase
    {
        public static ObservableCollection<Payment> Payments { get; set; }

        private ObservableCollection<Payer> _lstPayers = null;
        private ObservableCollection<Client> _lstClients = null;

        private IUserRepository userRepository;
        public ICommand AorECommand { get; }
        public ICommand OpenCommand { get; }

        public int Id { get; set; }

        private int? _cid;
        private int? _pid;

        private bool _isClient;
        private bool _isPayer;
        public int Debt { get; set; }

        private string _errorMessage;
        private bool _isViewVisible = true;

        private bool _isOpen = true;

        public EditMode CurrentMode { get; set; }

        private Payment _selectedPayment;
        public int Uid { get; set; } 

        //Properties
        public ObservableCollection<Payer> LstPayers
        {
            get => _lstPayers; set
            {
                _lstPayers = value;
                OnPropertyChanged(nameof(LstPayers));
            }
        }
        public ObservableCollection<Client> LstClients
        {
            get => _lstClients; set
            {
                _lstClients = value;
                OnPropertyChanged(nameof(LstClients));
            }
        }
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }

            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }
        public bool IsClient
        {
            get
            {
                return _isClient;
            }

            set
            {
                _isClient = value;
                OnPropertyChanged(nameof(IsClient));
            }
        }
        public bool IsPayer
        {
            get
            {
                return _isPayer;
            }

            set
            {
                _isPayer = value;
                OnPropertyChanged(nameof(IsPayer));
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
        public Nullable<int> Cid
        {
            get
            {
                return _cid;
            }

            set
            {
                _cid = value;
                if (value != null)
                {
                    Debt = userRepository.GetDebtById(Cid.Value, true, Uid);
                    OnPropertyChanged(nameof(Debt));

                }
                OnPropertyChanged(nameof(Cid));
            }
        }
        public Nullable<int> Pid
        {
            get
            {
                return _pid;
            }

            set
            {
                _pid = value;
                if (value != null)
                {
                    Debt = userRepository.GetDebtById(Pid.Value, false, Uid);
                    OnPropertyChanged(nameof(Debt));

                }
                OnPropertyChanged(nameof(Pid));
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
        public Payment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }

        //constructor
        public AddOrEditPaymentViewModel(EditMode mode, Payment p)
        {
            userRepository = new UserRepository();
            CurrentMode = mode;
            Uid = ReadUserIdFromFile();
            if (p != null)
            {
                SelectedPayment = p;
                Pid = p.PayerID;
                Cid = p.ClientID;
                IsOpen = p.IsOpen;
                if (p.ClientID != null)
                    IsClient = true;
                else IsClient = false;
                if (p.PayerID != null)
                    IsPayer = true;
                else IsPayer = false;
            }
            else
            {
                SelectedPayment = new Payment();
            }
            AorECommand = new ViewModelCommand(ExecuteAorECommand);
            OpenCommand = new ViewModelCommand(ExecuteOpenCommand);

            LoadPayments();
            LoadAll();
        }
        private void LoadPayments()
        {
            Payments = new ObservableCollection<Payment>(userRepository.GetAll<Payment>());
            OnPropertyChanged(nameof(Payments));
        }
        private void LoadAll()
        {
            userRepository.InitNonePayer();
            LstPayers = new ObservableCollection<Payer>(userRepository.GetWhere<Payer>(p => p.Pname != " -"));
            LstClients = new ObservableCollection<Client>(userRepository.GetAll<Client>());
        }
        private bool CanAorECommand()
        {
            if (CurrentMode == EditMode.Add)
            {
                if (IsClient)
                {
                    var ps = userRepository.GetWhere<Payment>(p => p.ClientID == Cid.Value && p.IsOpen);
                    if (ps.Count() > 0)
                    {
                        ErrorMessage = "There is an open request for this Client";
                        return false;
                    }
                }
                else //Payer
                {
                    var ps = userRepository.GetWhere<Payment>(p => p.PayerID == Pid.Value && p.IsOpen);
                    if (ps.Count() > 0)
                    {
                        ErrorMessage = "There is an open request for this Payer";
                        return false;
                    }
                }
                if (Debt == 0)
                {
                    ErrorMessage = "Its impossible to send a request with an empty amount";
                    return false;
                }
                return true;
            }
            else
            {
                if (SelectedPayment.IsOpen == false)
                {
                    ErrorMessage = "Payment has already been received.\nRequest is closed";
                    return false;
                }
                return true;
            }
        }
        private void ExecuteOpenCommand(object obj)
        {
            string val = (string)obj;
            if (CurrentMode == EditMode.Add)
                IsOpen = val.Equals("true");
            else
                SelectedPayment.IsOpen = val.Equals("true");
        }

        private void ClosePayment()
        {
            DateTime lastUp = CurrentMode == EditMode.Add ? DateTime.Now : SelectedPayment.LastUpdate;
            Payer None = userRepository.GetWhere<Payer>(p => p.Pname.Equals(" -")).FirstOrDefault();
            int NonePayerId = None.Id;
            if (IsClient)
            {
                var lstMeetings = userRepository.GetWhere<Meeting>(m => m.ClientId == Cid && m.UserId == Uid && (m.Status == Status.unpaid || m.Status == Status.payerPaid) && m.Date <= lastUp);
                foreach (Meeting m in lstMeetings)
                {
                    var clnt = (Client)userRepository.GetById(m.ClientId, "Client");
                    if (clnt.PayerId == NonePayerId)
                        m.Status = Status.paid;
                    else
                        m.Status = m.Status == Status.payerPaid? Status.paid: Status.clientPaid; //If part paid than now full paid, else - part paid.
                    userRepository.Edit(m);
                }
            }
            else
            {
                var lstCLients = userRepository.GetWhere<Client>(c => c.PayerId == Pid);
                foreach (var c in lstCLients)
                {
                    var lstMeetings = userRepository.GetWhere<Meeting>(m => m.ClientId == c.Id && m.UserId == Uid && (m.Status == Status.unpaid || m.Status == Status.clientPaid) && m.Date <= lastUp);
                    foreach (Meeting m in lstMeetings)
                    {
                        m.Status = m.Status == Status.clientPaid ? Status.paid : Status.payerPaid; //If part paid than now full paid, else - part paid.
                        userRepository.Edit(m);
                    }
                }
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
                        Payment p = new Payment { Id = Id, ClientID = Cid, PayerID = Pid, IsOpen = IsOpen, Debt = Debt, LastUpdate = DateTime.Now };
                        int secs = 4000;
                        if (IsOpen == false)
                        {
                            userRepository.Add(p);
                            ClosePayment();
                            ErrorMessage = "Closed Payment added successfully!";
                            secs = 1200;

                        }
                        else if (HundleSending())
                        {
                            userRepository.Add(p);
                            ErrorMessage = "Email sent. Payment added successfully!";
                        }
                        Task.Delay(secs).ContinueWith(_ => // Wait before closing
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
                        SelectedPayment.LastUpdate = DateTime.Today; // .Now;
                        SelectedPayment.IsOpen = IsOpen;

                        if (SelectedPayment.IsOpen == false)
                            ClosePayment();

                        userRepository.Edit(SelectedPayment);

                        ErrorMessage = "Payment was saved successfully!";

                        Task.Delay(800).ContinueWith(_ => // Wait before closing
                        {
                            IsViewVisible = false;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = $"Error updating Payment: {ex.Message}";

                    }
                }
            }
        }

        private string GetDebtDetailsForPayer(Payer p)
        {
            DateTime d = DateTime.Now.Date;
            string open = $"Hello {p.ContactName}. \nPlease find attached a summary of the sessions held for your clients up until {d:dd-MM-yyyy}.\n";
            string body = "A detailed breakdown per client is included below:";
            string end = $"\n\nThe total amount due is {Debt} ILS. \nThank you.";
            int totalMeetings = 0;
            var lstCLients = userRepository.GetWhere<Client>(c => c.PayerId == p.Id);
            var lstMeetings = userRepository.GetWhere<Meeting>(m => m.Client.PayerId == p.Id);
            foreach (var c in lstCLients)
            {
                int count = lstMeetings.Where(m => (m.Status == Status.unpaid || m.Status == Status.clientPaid) && m.ClientId == c.Id && m.Date <= d).Count();
                body += $"\n\t * NAME :{c.Cname} - ID : {c.Id} \t {count} meetings.";
                totalMeetings += count;
            }
            string res = open + $"A total of {totalMeetings} meetings have been conducted.\n\n" + body + end;
            return res;
        }

        private string GetDebtDetailsForClient(Client c)
        {
            DateTime d = DateTime.Now.Date;
            string open = $"Hello {c.Cname}. \nPlease find attached a summary of the sessions held for you up until {d:dd-MM-yyyy}.\n";
            string end = $"The total amount due is {Debt} ILS. \nThank you.";
            var lstMeetings = userRepository.GetWhere<Meeting>(m => m.ClientId == c.Id && m.Status == Status.unpaid && m.Date <= d);
            int totalMeetings = lstMeetings.Count();
            string body = $"A total of {totalMeetings} meetings have been conducted.\n\n";
            string res = open + body + end;
            return res;
        }

        private bool HundleSending()
        {
            ErrorMessage = "Follow the instructions to send the email.";
            try
            {
                Send();
                return true;

            }
            catch {
                return false;
            }
        }
        private async void Send()
        {
            var credential = await GetUserCredential();
            await SendEmail(credential);            
        }
        private async Task<UserCredential> GetUserCredential()
        {
            string[] scopes = { "https://www.googleapis.com/auth/gmail.send" };

            //Gets the CLient Details to google API 
            string jsonFilePath = "ClientsDetails.json";
            string jsonString = File.ReadAllText(jsonFilePath);

            ClientConfig config = JsonConvert.DeserializeObject<ClientConfig>(jsonString);

            string clientId = config.installed.client_id;
            string clientSecret = config.installed.client_secret;

            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore("TokenStorage", true));

            return credential;
        }

        private async Task SendEmail(UserCredential credential)
        {
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Peace Of Mind"
            });

            string toAddress, body, toName;
            if (IsClient)
            {
                Client c = (Client)userRepository.GetById(Cid.Value, "Client");
                toAddress = c.Email;
                toName = c.Cname;
                body = GetDebtDetailsForClient(c);
            }
            else
            {
                Payer p = (Payer)userRepository.GetById(Pid.Value, "Payer");
                toAddress = p.ContactEmail;
                toName = p.ContactName;
                body = GetDebtDetailsForPayer(p);

            }

            User u = (User)userRepository.GetById(Uid, "User");

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(u.Name+ " " + u.LastName, u.Email));
            mimeMessage.To.Add(new MailboxAddress(toName, toAddress));
            mimeMessage.Subject = "Request to arrange payment for the last meetings";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = body,
            };
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            // sending with Gmail API
            using (var stream = new MemoryStream())
            {
                await mimeMessage.WriteToAsync(stream);
                var email = new Message
                {
                    Raw = Base64UrlEncode(stream.ToArray())
                };

                await service.Users.Messages.Send(email, "me").ExecuteAsync();
            }
        }

        private string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}
