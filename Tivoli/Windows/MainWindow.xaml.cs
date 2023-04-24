using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Tivoli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DatabaseHelper _databaseHelper;


        public MainWindow()
        {
            InitializeComponent();
            // _databaseHelper = new DatabaseHelper(YourConnectionString);

            Database.SetInitializer(new MyDatabaseInitializer());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            User authenticatedUser = Authenticate(username, password);

            if (authenticatedUser != null)
            {
                MainMenuWindow mainMenuWindow = new MainMenuWindow(authenticatedUser);
                mainMenuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            UserRegistration registration = new UserRegistration
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password,
               // Role = RoleComboBox.SelectedItem.ToString() // Assuming you have a ComboBox for the role selection
               Role = "User"
            };

            var validationResults = ValidateUserRegistration(registration);

            if (validationResults.Count > 0)
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show(errorMessage, "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                User registeredUser = RegisterUser(registration);

                if (registeredUser != null)
                {
                    MessageBox.Show("User registration successful.", "Registration", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Optionally, navigate back to the login screen or perform any other desired action
                }
                else
                {
                    MessageBox.Show("Registration failed. The username might already be taken.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



    }
}
