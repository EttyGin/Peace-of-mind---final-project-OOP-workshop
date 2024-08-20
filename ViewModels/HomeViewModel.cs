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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data.Entity.Infrastructure;

namespace loginDb.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        //Fields
        private ObservableCollection<Meeting> _lstmeetings;
        private bool _isViewVisible = true;
        private IUserRepository userRepository;
        private int _num;
        private string _displayName;
        public int UserId { get; set; }

        //Properties
        public ObservableCollection<Meeting> LstMeetings
        {
            get
            {
                return _lstmeetings;
            }

            set
            {
                _lstmeetings = value;
                OnPropertyChanged(nameof(LstMeetings));
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
        public string DisplayName
        {
            get
            {
                return _displayName;
            }

            set
            {
                _displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public int Num
        {
            get
            {
                return _num;
            }

            set
            {
                _num = value;
                OnPropertyChanged(nameof(Num));
            }
        }


        //Constructor
        public HomeViewModel()
        {
            userRepository = new UserRepository();
            UserId = ReadUserIdFromFile();
            var user = (User)userRepository.GetById(UserId, "User");
            DisplayName = "Hello " + user.Name + " " + user.LastName;
            LoadMeetings(null);
        }

        private void LoadMeetings(Expression<Func<Meeting, bool>> predicate)
        {
            ObservableCollection<Meeting> Meetings = new ObservableCollection<Meeting>(userRepository.GetWhere<Meeting>(m => m.UserId == UserId));

            var sortedMeetings = Meetings.OrderByDescending(meeting => meeting.Date).ToList();
            DateTime today = DateTime.Now;

            LstMeetings = new ObservableCollection<Meeting>();
            foreach (var meeting in sortedMeetings)
            {
                DateTime mdate = meeting.Date;
                if (today.Day == mdate.Day && today.Month == mdate.Month && today.Year == mdate.Year)
                    LstMeetings.Add(meeting);
            }
            Num = LstMeetings.Count();
        }

    }
}
        