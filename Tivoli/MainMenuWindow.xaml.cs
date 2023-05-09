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
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {


        DatabaseHelper dbhelper; 
        private User _currentUser;
        MyDatabaseContext context;

        public MainMenuWindow(User user, DatabaseHelper dbhelper , MyDatabaseContext context)
        {
            this.context = context;
            _currentUser = user;
            this.dbhelper = dbhelper;
            InitializeComponent();
            if (_currentUser.role == "Admin")
            {
                // Show admin buttons
                AdminSectionTitle.Visibility = Visibility.Visible;
                ManageUsersButton.Visibility = Visibility.Visible;
                CreateWorkgroupButton.Visibility = Visibility.Visible;
                AssignResponsibilitiesButton.Visibility = Visibility.Visible;
                ViewLogsButton.Visibility = Visibility.Visible;
            }
            
        }

        private void ViewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement view profile functionality
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the login screen
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ManageUsersButton_Click(object sender, RoutedEventArgs e)
        {
            UserManagementWindow userManagementWindow = new UserManagementWindow(dbhelper);
            userManagementWindow.ShowDialog(); // Use ShowDialog() to open the window as a modal dialog
        }

      

        private void AssignResponsibilitiesButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement assign responsibilities functionality
            AssignResponsibilitiesWindow assignResponsibilitiesWindow = new AssignResponsibilitiesWindow(dbhelper);
            assignResponsibilitiesWindow.ShowDialog();
        }

        private void ViewLogsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement view logs functionality

        }


        private void CreateWorkgroupButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWorkgroupWindow createWorkgroupWindow = new CreateWorkgroupWindow(context,dbhelper);
            bool? result = createWorkgroupWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                MessageBox.Show("Workgroup created successfully.");
                // Refresh the list of workgroups if necessary
            }
        }

        private void AssignUsersToWorkgroupsButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the UserWorkgroupAssignmentWindow
            AssignResponsibilitiesWindow assignResponsibilitiesWindow = new AssignResponsibilitiesWindow(dbhelper);
            assignResponsibilitiesWindow.ShowDialog();
        }

  

        private void ManageUserRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            ManageUserRequestsWindow manageUserRequestsWindow = new ManageUserRequestsWindow(context, dbhelper);
            manageUserRequestsWindow.ShowDialog();
        }


    }
}
