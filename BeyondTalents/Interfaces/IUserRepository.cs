using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BeyondTalents.Models;

namespace BeyondTalents.Interfaces
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void AddUser(UserModel user);
        void UpdateUser(UserModel user);
        void DeleteUser(UserModel user);
        UserModel GetUserById(string id);
        UserModel GetUserByUsername(string username);
        UserModel GetUserByEmail(string email);
        IEnumerable<UserModel> GetAllUsers();
    }
}
