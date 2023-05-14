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
using Tivoli.Data;
using Tivoli.Logic;
using Tivoli.Models;
using Microsoft.Extensions.Configuration;


namespace Tivoli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DatabaseHelper _databaseHelper;
        private string ConnectionString;
        private MyDatabaseContext _databaseContext;
        private MyDatabaseInitializer myDatabaseInitializer;

        EmailService emailService;

        public MainWindow()
        {
            _databaseContext = new MyDatabaseContext();
            myDatabaseInitializer = new MyDatabaseInitializer(_databaseContext);

            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Egyetem\4.felev\INFOBiz\feleves\Tivoli\Tivoli\Data\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            _databaseHelper = new DatabaseHelper(ConnectionString);

             this.emailService = new EmailService();
            try
            {
                // Your database initialization code here
                Database.SetInitializer(myDatabaseInitializer);
            }
            catch (Exception ex)
            {
                // Log the exception and display a message to the user
                Console.WriteLine(ex.ToString());
                MessageBox.Show("An error occurred while initializing the database. See the application logs for more details.");
            }

                InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            UserTivoli authenticatedUser = UserTivoli.Authenticate(username, password,_databaseContext);



            if (authenticatedUser != null)
            {
                Logger.Log($"{username} logged in successfully.");

                MainMenuWindow mainMenuWindow = new MainMenuWindow(authenticatedUser, _databaseHelper, _databaseContext, emailService);
                mainMenuWindow.Show();
                this.Close();
            }
            else
            {
                Logger.Log($"Failed login attempt for {username}.");

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
               Role = "UserTivoli"
            };

            var validationResults = UserRegistration.ValidateUserRegistration(registration);

            if (validationResults.Count > 0)
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                Logger.Log($"Registration Failed\".\", \"Registration\" {registration.Username}.");

                MessageBox.Show(errorMessage, "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserTivoli registeredUser = UserRegistration.RegisterUser(registration);

                if (registeredUser != null)
                {
                    Logger.Log($"UserTivoli registration successful.\", \"Registration\"{registration.Username}.");

                    MessageBox.Show("UserTivoli registration successful.", "Registration", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Optionally, navigate back to the login screen or perform any other desired action
                }
                else
                {
                    Logger.Log($"UserTivoli registration successful.\", \"Registration\"r {registration.Username}.");

                    MessageBox.Show("Registration failed. The username might already be taken.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



    }
}
