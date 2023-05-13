﻿using System;
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
using Azure.Core;

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
        public void AddUser(UserTivoli user)
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
        /* public void UpdateUser(UserTivoli user)
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
        public void DeleteUser(UserTivoli user)
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
        /*
        public List<UserTivoli> GetAllUsers()
        {
            int type = 5;
            List<UserTivoli> users = new List<UserTivoli>();

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
                                UserTivoli user = new UserTivoli
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
                                UserTivoli user = new UserTivoli
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
        }*/

        public List<UserTivoli> GetAllUsers()
        {
            using (var context = new MyDatabaseContext())
            {
                return context.Users.ToList();
            }
        }

        public List<WorkgroupTivoli> GetAllWorkgroups()
        {
            using (var context = new MyDatabaseContext())
            {
                return context.Workgroups.ToList();
            }
        }

        public void ArchiveUserInDatabase(UserTivoli user)
        {
            user.IsActive = false;
        }


        public void AddWorkgroup(WorkgroupTivoli workgroup)
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


        /*
        public List<WorkgroupTivoli> GetAllWorkgroups()
        {
            // Retrieve workgroups from the database.
            List<WorkgroupTivoli> workergroups = new List<WorkgroupTivoli>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Workgroups", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WorkgroupTivoli wg = new WorkgroupTivoli
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
        */
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

        public void AddUserRequest(RequestTivoli userRequest)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                context.Requests.Add(userRequest);
                context.SaveChanges();
            }
        }
        /*
        public List<RequestTivoli> GetUserRequests()
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                return context.UserRequests.Include(ur => ur.UserTivoli).Include(ur => ur.WorkgroupTivoli).ToList();
            }
        }

        public void UpdateUserRequest(RequestTivoli userRequest)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                context.Entry(userRequest).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteUserRequest(RequestTivoli userRequest)
        {
            using (MyDatabaseContext context = new MyDatabaseContext())
            {
                context.Entry(userRequest).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }*/
        /*
        public List<RequestTivoli> GetUserRequests()
        {
            using (var context = new MyDatabaseContext())
            {
                return context.Requests
                    .Include(r => r.UserTivoli)
                    .Include(r => r.WorkgroupTivoli)
                    .Where(r => r.Status == "Pending")
                    .ToList();
            }
        }*/

        public void UpdateUserRequest(RequestTivoli request)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Requests.Attach(request);
                context.Entry(request).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateUser(UserTivoli user)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public bool MakeUserLeader(UserTivoli user, WorkgroupTivoli workgroup)
        {
            using (var context = new MyDatabaseContext())
            {
                // Check if the current user is admin
                if (!context.Users.Any(u => u.username == user.username && u.IsAdmin))
                    return false;

                // Set the user as the leader of the workgroup
                workgroup.LeaderId = user.id;
                context.SaveChanges();
                return true;
            }
        }

        public List<RequestTivoli> GetUserRequests(int  userID)
        {
            using (var context = new MyDatabaseContext())
            {
                UserTivoli currentuser = context.Users.First(u => u.id == userID);
               



                if (currentuser.role == "Admin")
                {
                    // If the user is an admin, return all pending requests
                    
                    List<RequestTivoli> tvr= new List<RequestTivoli>();

                    tvr = context.Requests.ToList();

                    return context.Requests.Include(r => r.User).ToList();
                }
                else  
                {

                    if (currentuser.workgroupId != null)
                    {
                        WorkgroupTivoli workgroup = context.Workgroups.First(u => u.Id == currentuser.workgroupId);

                        return context.Requests
                      .Include(r => r.User)
                      .Include(r => r.Workgroup)
                      .Where(r => r.Status == "Pending" && r.Workgroup.LeaderId == currentuser.id)
                      .ToList();
                    }
                    // If the user is a group leader, return only the requests related to their workgroup
                    else
                    {
                        return new List<RequestTivoli>();
                    }

                }
            }
        }

        public bool AddUserToWorkgroup(int userID, int workgroupID, int currentuserID)
        {
            using (var context = new MyDatabaseContext())
            {
                UserTivoli currentuser = context.Users.First(u => u.id == currentuserID);

                UserTivoli user = context.Users.First(u => u.id == userID);
                WorkgroupTivoli workgroup = context.Workgroups.First(u => u.Id == workgroupID);
                // Check if the user is already in another workgroup
                if (context.Users.Any(u => u.id == user.id && u.Workgroup != null) || user.Workgroup != null)
                {
                    Logger.Log($"Failed to add user {user.username} to workgroup {workgroup.Name}: UserTivoli is already in another workgroup.");

                    return false;

                }



                if (currentuser.role == "Admin")
                {

                    workgroup.Users.Add(user);
                    Logger.Log($"Added user {user.username} to workgroup {workgroup.Name}.");

                }
                else if (currentuser.Workgroup.Name == "Hr")
                {
                    // If the user performing the action is HR, create a request
                    RequestTivoli request = new RequestTivoli
                    {
                        User = user,
                        Workgroup = workgroup,
                        RequestType = "AddUserToWorkgroup",
                    };
                    context.Requests.Add(request);
                    Logger.Log($"HR {currentuser.username} created a request to add user {user.username} to workgroup {workgroup.Name}.");
                }
                else
                {
                    Logger.Log($"Failed to add user {user.username} to workgroup {workgroup.Name}: UserTivoli is already in another workgroup.");

                    return false;
                }



                context.SaveChanges();


                return true;
            }
        }

        public bool removeUserFromWorkgroup(int userID, int workgroupID, int currentuserID)
        {
            using (var context = new MyDatabaseContext())
            {
                UserTivoli currentuser = context.Users.First(u => u.id == currentuserID);
                UserTivoli user = context.Users.First(u => u.id == userID);
                WorkgroupTivoli workgroup = context.Workgroups.First(u => u.Id == workgroupID);
                if (currentuser.role == "Admin") // Check if the user is an admin
                {
                    workgroup.Users.Remove(user);
                    context.SaveChanges();
                    Logger.Log($"User {currentuser.username} removed user {user.username} from workgroup {workgroup.Name}");
                    return true;
                }
                else if (currentuser.role == "Hr") // Check if the user is an HR
                {
                    RequestTivoli request = new RequestTivoli
                    {
                        User = user,
                        Workgroup = workgroup,
                        RequestType = "RemoveUserFromWorkgroup",
                        Status = "Hr"
                    };
                    context.Requests.Add(request);
                    context.SaveChanges();
                    Logger.Log($"User {currentuser.username} created a request to remove user {user.username} from workgroup {workgroup.Name}");
                    return true;
                }
                return false;
            }

        }

        public UserTivoli getUserById(int id)
        {
            using (var context = new MyDatabaseContext())
            {


                return context.Users.FirstOrDefault(u => u.id == id);
            }


        }


        public WorkgroupTivoli getWorkgroupById(int id)
        {
            using (var context = new MyDatabaseContext())
            {


                return context.Workgroups.FirstOrDefault(u => u.Id == id);
            }


        }

        public RequestTivoli getRequestById(int id)
        {
            using (var context = new MyDatabaseContext())
            {


                return context.Requests.FirstOrDefault(u => u.Id == id);
            }


        }

        public bool ApproveRequest(int requestID, int performedByUserID)
        {
            
            using (var context = new MyDatabaseContext())
            {
                UserTivoli performedByUser = context.Users.First(u => u.id == performedByUserID);
                RequestTivoli request = context.Requests.First(u => u.Id == requestID);
                context.Requests.Attach(request);
                if (performedByUser.role == "Admin") // Check if the user is an admin
                {
                    request.Status = "Approved";
                    if (request.RequestType == "RemoveUserFromWorkgroup")
                    {
                        request.Workgroup.Users.Remove(request.User);
                        request.User.workgroupId = null;

                        Logger.Log($"Admin {performedByUser.username} approved remove request {request.Id}");

                    }
                    else if (request.RequestType == "AddUserToWorkgroup")
                    {
                        request.Workgroup.Users.Add(request.User);
                        request.User.workgroupId = request.Workgroup.Id;
                        Logger.Log($"Admin {performedByUser.username} approved add request {request.Id}");


                    }
                    context.SaveChanges();
                    return true;
                    
                    



                }
                else if (performedByUserID == request.Workgroup.LeaderId)
                {
                    request.Status = "Approved";
                    if (request.RequestType == "RemoveUserFromWorkgroup")
                    {
                        request.Workgroup.Users.Remove(request.User);
                        request.User.workgroupId = null;

                        Logger.Log($"Leader {performedByUser.username} approved remove request {request.Id}");

                    }
                    else if (request.RequestType == "AddUserToWorkgroup")
                    {
                        request.Workgroup.Users.Add(request.User);
                        request.User.workgroupId = request.Workgroup.Id;

                        Logger.Log($"Leader {performedByUser.username} approved add request {request.Id}");



                    }
                    context.SaveChanges();
                    return true;




                }


                return false;
            }
        }

        internal bool RejectRequest(int requestID, int performedByUserID)
        {
            using (var context = new MyDatabaseContext())
            {
                UserTivoli performedByUser = context.Users.First(u => u.id == performedByUserID);
                RequestTivoli request = context.Requests.First(u => u.Id == requestID);
                if (performedByUser.role == "Admin") // Check if the user is an admin
                {
                    request.Status = "Approved";
                    if (request.RequestType == "RemoveUserFromWorkgroup")
                    {
                        request.Status = "Rejected";


                        Logger.Log($"Admin {performedByUser.username} Rejected remove request {request.Id}");

                    }
                    else if (request.RequestType == "AddUserToWorkgroup")
                    {
                        request.Status = "Rejected";
                        Logger.Log($"Admin {performedByUser.username} Rejected add request {request.Id}");


                    }
                    context.SaveChanges();
                    return true;





                }
                else if (performedByUserID == request.Workgroup.LeaderId)
                {
                    request.Status = "Rejected";
                    if (request.RequestType == "RemoveUserFromWorkgroup")
                    {
                        request.Status = "Rejected";


                        Logger.Log($"Leader {performedByUser.username} Rejected remove request {request.Id}");

                    }
                    else if (request.RequestType == "AddUserToWorkgroup")
                    {
                        request.Status = "Rejected";


                        Logger.Log($"Leader {performedByUser.username} Rejected add request {request.Id}");



                    }
                    context.SaveChanges();
                    return true;




                }
                return false;


            }
        }

        internal void AddLeaderUser(UserTivoli selectedUser, UserTivoli currentuser)
        {
            using (var context = new MyDatabaseContext())
            {

                WorkgroupTivoli workgroup = context.Workgroups.First(u => u.Id == selectedUser.workgroupId);

                workgroup.LeaderId = selectedUser.id;
                Logger.Log($"Admin {currentuser.username} made {selectedUser.id},{selectedUser.username} leader of workgroup {workgroup.Id}, {workgroup.Name} ");

            }
        }

        internal void SendEmailConfirmation(RequestTivoli selectedRequest)
        {
            EmailService emailService = new EmailService();

            using (var context = new MyDatabaseContext())
            {
                string confirmationLink = emailService.GenerateConfirmationLink(selectedRequest, "approve");
                //  emailService.SendConfirmationEmail(selectedRequest.User.email, confirmationLink);
                //  emailService.SendConfirmationEmail("tivoliteszt002@yahoo.com", confirmationLink);

               
                 emailService.SendEmailAsync(
                    fromEmail: "tivoliteszt002@gmail.com",
                    toEmail: "tivoliteszt002@yahoo.com",
                    subject: "Hello",
                    plainTextContent: "Hello, World!",
                    htmlContent: "<strong>Hello, World!</strong>"
                );

                selectedRequest.Status = "In Progress";
            }

        }
        

        internal void UpdateRequestStatus(int requestId, string v)
        {
            using (var context = new MyDatabaseContext())
            {
                RequestTivoli request = context.Requests.First(u => u.Id == requestId);
                request.Status = v;
            }

        }
    }
}
