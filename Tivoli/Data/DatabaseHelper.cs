using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Data.Entity;
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;
using System.Drawing;

namespace Tivoli.Data
{
    public class DatabaseHelper
    {
        private string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }



        // Add a new user to the database
        public void AddUser(User user)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO Users (Username, FullName, Email, Password, Role) VALUES (@Username, @FullName, @Email, @Password, @Role)",
                    connection))
                {
                    command.Parameters.AddWithValue("@Username", user.username);
                    command.Parameters.AddWithValue("@FullName", user.fullname);
                    command.Parameters.AddWithValue("@Email", user.email);
                    command.Parameters.AddWithValue("@Password", user.passwordHash);
                    command.Parameters.AddWithValue("@Role", user.role);

                    command.ExecuteNonQuery();
                }
            }
        }




        // Update an existing user in the database
        /* public void UpdateUser(User user)
         {
             using (SqlConnection connection = GetConnection())
             {
                 connection.Open();
                 using (SqlCommand command = new SqlCommand(
                     "UPDATE Users SET Username = @Username, FullName = @FullName, Email = @Email, Password = @Password, Role = @Role WHERE Id = @Id",
                     connection))
                 {
                     // command.Parameters.AddWithValue("@Id", user.id);
                     command.Parameters.AddWithValue("@Username", user.username);
                     command.Parameters.AddWithValue("@FullName", user.fullname);
                     command.Parameters.AddWithValue("@Email", user.email);
                     command.Parameters.AddWithValue("@Password", user.passwordHash);
                     command.Parameters.AddWithValue("@Role", user.role);
                     command.ExecuteNonQuery();
                 }
             }
         }*/

        // Delete a user from the database
        public void DeleteUser(User user)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all users from the database
        public List<User> GetAllUsers()
        {
            int type = 5;
            List<User> users = new List<User>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            if (reader["WorkgroupId"] is DBNull)
                            {
                                User user = new User
                            (
                                 (int)reader["Id"],
                                 (string)reader["Username"],
                                 (string)reader["PasswordHash"],
                                 (string)reader["Role"],
                                 (string)reader["FullName"],
                                 (string)reader["Email"],
                                 (bool)reader["isActive"]
                            );
                                users.Add(user);
                            }
                            else
                            {
                                User user = new User
                            (
                                 (int)reader["Id"],
                                 (string)reader["Username"],
                                 (string)reader["PasswordHash"],
                                 (string)reader["Role"],
                                 (string)reader["FullName"],
                                 (string)reader["Email"],
                                 (bool)reader["isActive"],
                                 (int)reader["WorkgroupId"]
                            );
                                users.Add(user);
                            }



                        }
                    }
                }
            }

            return users;
        }


        public void ArchiveUserInDatabase(User user)
        {
            user.IsActive = false;
        }


        public void AddWorkgroup(Workgroup workgroup)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO Workgroups (Name, Description) VALUES (@Name, @Description)",
                    connection))
                {
                    command.Parameters.AddWithValue("@Name", workgroup.Name);
                    command.Parameters.AddWithValue("@Description", workgroup.Description);


                    command.ExecuteNonQuery();
                }
            }
        }



        public List<Workgroup> GetAllWorkgroups()
        {
            // Retrieve workgroups from the database.
            List<Workgroup> workergroups = new List<Workgroup>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Workgroups", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Workgroup wg = new Workgroup
                            (
                                 (int)reader["Id"],
                                 (string)reader["Name"],
                                 (string)reader["Description"]

                            );
                            workergroups.Add(wg);
                        }
                    }
                }
            }

            return workergroups;
        }

        public void AssignResponsibility(int userId, int workgroupId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Users SET WorkgroupId = @WorkgroupId WHERE Id = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@WorkgroupId", workgroupId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddUserRequest(Request userRequest)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                context.Requests.Add(userRequest);
                context.SaveChanges();
            }
        }
        /*
        public List<Request> GetUserRequests()
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                return context.UserRequests.Include(ur => ur.User).Include(ur => ur.Workgroup).ToList();
            }
        }

        public void UpdateUserRequest(Request userRequest)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                context.Entry(userRequest).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteUserRequest(Request userRequest)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                context.Entry(userRequest).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }*/
        /*
        public List<Request> GetUserRequests()
        {
            using (var context = new MyDatabaseContext())
            {
                return context.Requests
                    .Include(r => r.User)
                    .Include(r => r.Workgroup)
                    .Where(r => r.Status == "Pending")
                    .ToList();
            }
        }*/

        public void UpdateUserRequest(Request request)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Requests.Attach(request);
                context.Entry(request).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public bool MakeUserLeader(User user, Workgroup workgroup)
        {
            using (var context = new MyDatabaseContext())
            {
                // Check if the current user is admin
                if (!context.Users.Any(u => u.username == user.username && u.IsAdmin))
                    return false;

                // Set the user as the leader of the workgroup
                workgroup.Leader = user;
                context.SaveChanges();
                return true;
            }
        }

        public List<Request> GetUserRequests(User user)
        {
            using (var context = new MyDatabaseContext())
            {
                if (user.IsAdmin)
                {
                    // If the user is an admin, return all pending requests
                    return context.Requests
                        .Include(r => r.User)
                        .Include(r => r.Workgroup)
                        .Where(r => r.Status == "Pending")
                        .ToList();
                }
                else
                {
                    // If the user is a group leader, return only the requests related to their workgroup
                    return context.Requests
                        .Include(r => r.User)
                        .Include(r => r.Workgroup)
                        .Where(r => r.Status == "Pending" && r.Workgroup.LeaderId == user.id)
                        .ToList();
                }
            }
        }

        public bool AddUserToWorkgroup(int userID, int workgroupID)
        {
            using (var context = new MyDatabaseContext())
            {
                User user = context.Users.First(u => u.id == userID);
                Workgroup workgroup = context.Workgroups.First(u => u.Id == workgroupID);
                // Check if the user is already in another workgroup
                if (context.Users.Any(u => u.id == user.id && u.Workgroup != null))
                    return false;

                // Add the user to the workgroup
                workgroup.Users.Add(user);
                context.SaveChanges();
                return true;
            }
        }

        public void removeUserFromWorkgroup(int userID,  int workgroupID)
        {
            using (var context = new MyDatabaseContext())
            {
                // Check if the user is already in another workgroup
                User user = context.Users.First(u => u.id == userID);
                Workgroup workgroup = context.Workgroups.First(u => u.Id == workgroupID);
                user.workgroupId = null;
                // Add the user to the workgroup
                workgroup.Users.Remove(user);
                UpdateUser(user);
                context.SaveChanges();
            }

        }

        public User getUserById(int id)
        {
            using (var context = new MyDatabaseContext())
            {


                return context.Users.FirstOrDefault(u => u.id == id);
            }


        }


        public Workgroup getWorkgroupById(int id)
        {
            using (var context = new MyDatabaseContext())
            {


                return context.Workgroups.FirstOrDefault(u => u.Id == id);
            }


        }

        public Request getRequestById(int id)
        {
            using (var context = new MyDatabaseContext())
            {


                return context.Requests.FirstOrDefault(u => u.Id == id);
            }


        }
    }
}
