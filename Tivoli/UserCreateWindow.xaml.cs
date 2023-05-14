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
using Tivoli.Data;
using Tivoli.Logic;
using Tivoli.Models;

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for UserCreateWindow.xaml
    /// </summary>
    public partial class UserCreateWindow : Window
    {
        private UserTivoli currentUser;

        public UserCreateWindow(UserTivoli currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser.role == "Admin")
            {
                var registration = new UserRegistration()
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordBox.Password,
                    Role =  ((ComboBoxItem)RoleComboBox.SelectedItem).Content.ToString(),
                    FullName = FullNameTextBox.Text,
                    Email = EmailTextBox.Text,
                };

                UserTivoli newUser = UserRegistration.RegisterUser(registration);

                if (newUser != null)
                {
                    MessageBox.Show("User successfully created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create user. Username may already be taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Only admin users can create new users.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
