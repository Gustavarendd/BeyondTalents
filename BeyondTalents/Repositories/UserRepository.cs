using BeyondTalents.Interfaces;
using BeyondTalents.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeyondTalents.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void AddUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser = false;

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("SELECT * FROM [User] WHERE Username = @username AND Password = @password", connection))

                {
                    connection.Open();

                    

                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = credential.UserName;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = credential.Password;

                    validUser = command.ExecuteScalar() == null ? false : true;
                }
            }
            catch (SqlException ex)
            {
                // Log exception (consider a logging framework)
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow or handle the exception
            }

            return validUser;
        }

        public void DeleteUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserById(string id)
        {
            UserModel user = null;

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("SELECT * FROM [User] WHERE Id = @id", connection))
                {
                    connection.Open();
                    command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                Id = reader["Id"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = String.Empty,
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()
                            };
                        }
                        else
                        {
                            user = null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log exception (consider a logging framework)
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow or handle the exception
            }

            return user;
        }

        public UserModel GetUserByUsername(string username)
        {
            UserModel user = null;

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("SELECT * FROM [User] WHERE Username = @username", connection))

                {
                    connection.Open();
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel
                            {
                                Id = reader["userID"].ToString(),
                                Username = reader["Username"].ToString(),
                                Password = String.Empty,
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()

                            };
                        }
                        else
                        {
                            user = null;
                        }
                    }



                }
            }
            catch (SqlException ex)
            {
                // Log exception (consider a logging framework)
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Optionally rethrow or handle the exception
            }

            return user;
        }

        public void UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

       
    }
}
