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

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
       

         
        private User _currentUser;

        public MainMenuWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;

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
            UserManagementWindow userManagementWindow = new UserManagementWindow();
            userManagementWindow.ShowDialog(); // Use ShowDialog() to open the window as a modal dialog
        }


        private void CreateWorkgroupButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement create workgroup functionality
        }

        private void AssignResponsibilitiesButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement assign responsibilities functionality
        }

        private void ViewLogsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement view logs functionality

        }


    }
}
