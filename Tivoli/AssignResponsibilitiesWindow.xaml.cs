using System;
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
using Tivoli.Models;
using Tivoli.Data;
using Tivoli.Logic;

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for AssignResponsibilitiesWindow.xaml
    /// </summary>
    public partial class AssignResponsibilitiesWindow : Window
    {
        DatabaseHelper databaseHelper;
        UserTivoli _currentUser;

        public AssignResponsibilitiesWindow(UserTivoli user, DatabaseHelper databaseHelper)
        {
            this._currentUser = user;

            this.databaseHelper = databaseHelper;
            InitializeComponent();

            LoadUsers();
            LoadWorkgroups();

        }

        private void LoadUsers()
        {
            // Get the list of users from the database.
            List<UserTivoli> users = databaseHelper.GetAllUsers(_currentUser);

            // Bind the list of users to the UserComboBox.
            UserComboBox.ItemsSource = users;
            UserComboBox.DisplayMemberPath = "fullname";
            UserComboBox.SelectedValuePath = "id";
        }

        private void LoadWorkgroups()
        {
            // Get the list of workgroups from the database.
            List<WorkgroupTivoli> workgroups = databaseHelper.GetAllWorkgroups(_currentUser);

            // Bind the list of workgroups to the WorkgroupComboBox.
            WorkgroupComboBox.ItemsSource = workgroups;
            WorkgroupComboBox.DisplayMemberPath = "Name";
            WorkgroupComboBox.SelectedValuePath = "Id";
        }

        private void AssignResponsibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedValue is not null && WorkgroupComboBox.SelectedValue is not null)
            {
                int userId = int.Parse(UserComboBox.SelectedValue.ToString());
                int workgroupId = int.Parse(WorkgroupComboBox.SelectedValue.ToString());

                UserTivoli user = databaseHelper.getUserById(userId, _currentUser);



                if (user.IsActive)
                {

                    // Assign the responsibility in the database.
                    if (databaseHelper.AddUserToWorkgroup(userId, workgroupId, _currentUser.id))
                    {
                        Logger.Log($" {_currentUser.username} Responsibility assigned successfully");

                        MessageBox.Show("Responsibility assigned successfully.");
                    }
                    else
                    {
                        Logger.Log($" {_currentUser.username} User is already part of a group.");

                        MessageBox.Show("User is already part of a group.");

                    }

                }
                else
                {
                    Logger.Log($" {_currentUser.username} Please activate the  a {user.username} .");

                    MessageBox.Show($"{_currentUser.username} Please activate the  a {user.username} .");
                }

             
            }
            else
            {
                Logger.Log($" {_currentUser.username} Please select a user and a workgroup.");

                MessageBox.Show("Please select a user and a workgroup.");
            }
        }

        private void RemoveResponsibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedValue is not null && WorkgroupComboBox.SelectedValue is not null)
            {
                int userId = int.Parse(UserComboBox.SelectedValue.ToString());
                int workgroupId = int.Parse(WorkgroupComboBox.SelectedValue.ToString());

                // Assign the responsibility in the database.
                databaseHelper.removeUserFromWorkgroup(userId, workgroupId, _currentUser.id);
                Logger.Log($" {_currentUser.username} Responsibility assigned successfully");

                MessageBox.Show("Responsibility assigned successfully.");
            }
            else
            {
                Logger.Log($" {_currentUser.username} Please select a user and a workgroup..");

                MessageBox.Show("Please select a user and a workgroup.");
            }
        }


    }

}
