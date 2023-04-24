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
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {



        private User _currentUser;

        public UserProfileWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;

            // Populate the UI elements with the user's information
            UsernameTextBlock.Text = _currentUser.username;
            FullNameTextBlock.Text = _currentUser.fullname;
            EmailTextBlock.Text = _currentUser.email;
            RoleTextBlock.Text = _currentUser.role;
        }

        private void ViewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow userProfileWindow = new UserProfileWindow(_currentUser);
            userProfileWindow.ShowDialog(); // Use ShowDialog() to open the window as a modal dialog
        }


    }
}
