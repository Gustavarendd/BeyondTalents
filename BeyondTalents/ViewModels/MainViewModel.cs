using BeyondTalents.Interfaces;
using BeyondTalents.Models;
using BeyondTalents.Repositories;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BeyondTalents.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // fields
        private UserAccountModel _userAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository _userRepository;


        // properties
        public UserAccountModel UserAccount
        {
            get => _userAccount;
            set
            {
                _userAccount = value;
                OnPropertyChanged(nameof(UserAccount));
            }
        }

        public ViewModelBase CurrentChildView 
        { 
            get => _currentChildView; 
            set 
            { 
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption 
        { 
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            } 
        }
        public IconChar Icon 
        { 
            get => _icon;
            set 
            { 
                _icon = value; 
                OnPropertyChanged(nameof(Icon));
            } 
        }

        // commands
        public ICommand ShowDashboardViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }
        public ICommand ShowAddCandidateViewCommand { get; }


        // constructor
        public MainViewModel()
        {
            _userRepository = new UserRepository();
            UserAccount = new UserAccountModel();

            // initialize commands
            ShowDashboardViewCommand = new RelayCommand(ExecuteShowDashboardViewCommand);
            ShowCustomerViewCommand = new RelayCommand(ExecuteShowCustomerViewCommand);
            ShowAddCandidateViewCommand = new RelayCommand(ExecuteShowAddCandidateViewCommand);

            // Defalut view
            ExecuteShowDashboardViewCommand(null);

            // load user data
            LoadUserData();
        }

        private void ExecuteShowAddCandidateViewCommand(object obj)
        {
            CurrentChildView = new AddCandidateViewModel();
            Caption = "Add Candidate";
            Icon = IconChar.UserPlus;
        }

        private void ExecuteShowDashboardViewCommand(object obj)
        {
            CurrentChildView = new DashboardViewModel();
            Caption = "Dashboard";
            Icon = IconChar.ChartBar;
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new SearchCandidateViewModel();
            Caption = "Candidates";
            Icon = IconChar.UserGroup;
        }

        private void LoadUserData()
        {
            var identity = Thread.CurrentPrincipal?.Identity;
            if (identity != null && identity.IsAuthenticated)
            {
                var user = _userRepository.GetUserByUsername(identity.Name);
                if (user != null)
                {
                    UserAccount.Username = user.Username;
                    UserAccount.DisplayName = $"{user.FirstName} {user.LastName}";
                    UserAccount.ProfilePicture = null;
                }
                else
                {
                    UserAccount.DisplayName = "User not found";
                    // hide child views
                }
            }
            else
            {
                UserAccount.DisplayName = "User not authenticated";
                // hide child views
            }
        }
    }
}
