using System;
using System.Windows;
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
        private UserTivoli _user;
        UserTivoli _currentUser;
        DatabaseHelper dbHelper;


        public UserEditWindow(UserTivoli user, UserTivoli current, DatabaseHelper dbhelper)
        {
            InitializeComponent();
            _user = user;
            InitializeInputFields();
            this._currentUser = current;
            this.dbHelper = dbhelper;


        }

        private void InitializeInputFields()
        {
            if (_user != null)
            {
                UsernameTextBox.Text = _user.username;
                FullNameTextBox.Text = _user.fullname;
                EmailTextBox.Text = _user.email;
                RoleComboBox.SelectedIndex = _user.role == "Admin" ? 1 : 0;
                EmailConfirmedCheckBox.IsChecked = _user.EmailConfirmed;
                IsAdminCheckBox.IsChecked = _user.IsAdmin;
                LockoutEndDatePicker.SelectedDate = _user.LockoutEnd;
                FailedLoginAttemptsTextBox.Text = _user.FailedLoginAttempts.ToString();
                PasswordLastSetDatePicker.SelectedDate = _user.PasswordLastSet;
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
                _user = new UserTivoli
                (
                     UsernameTextBox.Text,
                     UserTivoli.HashPassword(PasswordBox.Password), // Hash the password before storing
                     RoleComboBox.SelectedIndex == 1 ? "Admin" : "UserTivoli",
                     FullNameTextBox.Text,
                     EmailTextBox.Text,
                     true
                );

                _user.EmailConfirmed = EmailConfirmedCheckBox.IsChecked ?? false;
                _user.IsAdmin = IsAdminCheckBox.IsChecked ?? false;
                _user.LockoutEnd = LockoutEndDatePicker.SelectedDate;
                _user.FailedLoginAttempts = int.TryParse(FailedLoginAttemptsTextBox.Text, out var failedLoginAttempts) ? failedLoginAttempts : 0;
                _user.PasswordLastSet = PasswordLastSetDatePicker.SelectedDate ?? DateTime.Now;

                // Add the new user to the database
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
                    _user.passwordHash = UserTivoli.HashPassword(PasswordBox.Password);
                }
                _user.role = RoleComboBox.SelectedIndex == 1 ? "Admin" : "UserTivoli";
                _user.EmailConfirmed = EmailConfirmedCheckBox.IsChecked ?? false;
                _user.IsAdmin = IsAdminCheckBox.IsChecked ?? false;
                _user.LockoutEnd = LockoutEndDatePicker.SelectedDate;
                _user.FailedLoginAttempts = int.TryParse(FailedLoginAttemptsTextBox.Text, out var failedLoginAttempts) ? failedLoginAttempts : 0;
                _user.PasswordLastSet = PasswordLastSetDatePicker.SelectedDate ?? DateTime.Now;

                // Update the user in the database
                UpdateUserInDatabase(_user);
            }

            // Close the window and set the DialogResult to true to indicate that the operation was successful
            DialogResult = true;
            Close();
        }

        private void UpdateUserInDatabase(UserTivoli user)
        {
            // Implement your logic to update the user in the database
            dbHelper.UpdateUser(_user, _currentUser);
        }

        private void AddUserToDatabase(UserTivoli user)
        {
            // Implement your logic to add the user to the database
            dbHelper.UpdateUser(_user, _currentUser);

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window and set the DialogResult to false to indicate that the operation was canceled
            DialogResult = false;
            Close();
        }
    }
}
