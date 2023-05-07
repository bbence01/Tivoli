using System;
using System.Collections.Generic;
using System.Data;
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
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        private User _user;

        public UserEditWindow(User user)
        {
            InitializeComponent();
            _user = user;
            InitializeInputFields();
        }

        private void InitializeInputFields()
        {
            if (_user != null)
            {
                UsernameTextBox.Text = _user.username;
                FullNameTextBox.Text = _user.fullname;
                EmailTextBox.Text = _user.email;
                // Don't populate the password field for security reasons
                RoleComboBox.SelectedIndex = _user.role == "Admin" ? 1 : 0;
            }
            else
            {
                RoleComboBox.SelectedIndex = 0;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                // Create a new user object and populate it with data from the input fields
                _user = new User
                (
                     UsernameTextBox.Text,
                     FullNameTextBox.Text,
                     EmailTextBox.Text,
                     User.HashPassword(PasswordBox.Password), // Hash the password before storing
                     RoleComboBox.SelectedIndex == 1 ? "Admin" : "User",
                     true
                );

                // Add the new user to


                // Replace this with your actual method for adding a user to the database
                AddUserToDatabase(_user);
            }
            else
            {
                // Update the existing user object with the data from the input fields
                _user.username = UsernameTextBox.Text;
                _user.fullname = FullNameTextBox.Text;
                _user.email = EmailTextBox.Text;
                // Only update the password if the user has entered a new one
                if (!string.IsNullOrEmpty(PasswordBox.Password))
                {
                    _user.passwordHash = User.HashPassword(PasswordBox.Password);
                }
                _user.role = RoleComboBox.SelectedIndex == 1 ? "Admin" : "User";

                // Update the user in the database
                // Replace this with your actual method for updating a user in the database
                UpdateUserInDatabase(_user);
            }

            // Close the window and set the DialogResult to true to indicate that the operation was successful
            DialogResult = true;
            Close();
        }

        private void UpdateUserInDatabase(User user)
        {
            throw new NotImplementedException();
        }

        private void AddUserToDatabase(User user)
        {
            throw new NotImplementedException();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window and set the DialogResult to false to indicate that the operation was canceled
            DialogResult = false;
            Close();
        }

    }
}

