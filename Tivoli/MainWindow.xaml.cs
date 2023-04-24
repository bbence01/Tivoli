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
        private string YourConnectionString;
        private MyDatabaseContext _databaseContext;
        private MyDatabaseInitializer myDatabaseInitializer;

        public MainWindow()
        {
            _databaseContext = new MyDatabaseContext();
            myDatabaseInitializer = new MyDatabaseInitializer(_databaseContext);

            YourConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Egyetem\4.felev\INFOBiz\feleves\Tivoli\Tivoli\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            _databaseHelper = new DatabaseHelper(YourConnectionString);
           
            Database.SetInitializer(myDatabaseInitializer);

           
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            User authenticatedUser = User.Authenticate(username, password,_databaseContext);



            if (authenticatedUser != null)
            {
                MainMenuWindow mainMenuWindow = new MainMenuWindow(authenticatedUser, _databaseHelper);
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

            var validationResults = UserRegistration.ValidateUserRegistration(registration);

            if (validationResults.Count > 0)
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show(errorMessage, "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                User registeredUser = UserRegistration.RegisterUser(registration);

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
