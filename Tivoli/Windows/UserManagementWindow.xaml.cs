﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
     

        private void LoadUsers()
        {
            // Replace this with your actual method of retrieving users from the database
            List<User> users = GetUsersFromDatabase();

            UsersDataGrid.ItemsSource = users;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserEditWindow userEditWindow = new UserEditWindow(null); // Pass null for adding a new user
            bool? result = userEditWindow.ShowDialog();
            if (result.HasValue && result.Value)
            {
                LoadUsers(); // Refresh the user list after adding a new user
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser != null)
            {
                UserEditWindow userEditWindow = new UserEditWindow(selectedUser);
                bool? result = userEditWindow.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    LoadUsers(); // Refresh the user list after editing a user
                }
            }
        }


        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser != null)
            {
                // Replace this with your actual method of deleting users from the database
                DeleteUserFromDatabase(selectedUser);
                LoadUsers(); // Refresh the user list after deleting a user
            }
        }


        private void ArchiveUserButton_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser != null)
            {
                // Replace this with your actual method of archiving users in the database
                ArchiveUserInDatabase(selectedUser);
                LoadUsers(); // Refresh the user list after archiving a user
            }
        }


        public UserManagementWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

    }
}
