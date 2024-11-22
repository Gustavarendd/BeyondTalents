using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeyondTalents.Interfaces;
using BeyondTalents.Models;
using BeyondTalents.Repositories;

namespace BeyondTalents.ViewModels
{
    public class DashboardViewModel: ViewModelBase
    {

        private UserModel _userAccount;
        private IUserRepository _userRepository;
        private string _welcomeText;

        public string WelcomeText
        {
            get => _welcomeText;
            set
            {
                _welcomeText = value;
                OnPropertyChanged(nameof(WelcomeText));
            }
        }

        public UserModel UserAccount
        {
            get => _userAccount;
            set
            {
                _userAccount = value;
                OnPropertyChanged(nameof(UserAccount));
            }
        }

        public DashboardViewModel()
        {
            _userRepository = new UserRepository();
            UserAccount = new UserModel();
            LoadUserData();
        }


        private void LoadUserData()
        {
            var identity = Thread.CurrentPrincipal?.Identity;
            if (identity != null && identity.IsAuthenticated)
            {
                UserModel user = _userRepository.GetUserByUsername(identity.Name);
                if (user != null)
                {
                    UserAccount.Username = user.Username;
                    UserAccount.DisplayName = $"{user.FirstName} {user.LastName}";
                    UserAccount.Email = user.Email;
                    UserAccount.FirstName = user.FirstName;
                    UserAccount.LastName = user.LastName;
                    UserAccount.ProfilePicture = null;


                    WelcomeText = $"Welcome, {UserAccount.DisplayName}!";

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
