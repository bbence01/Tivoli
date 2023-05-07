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
        public AssignResponsibilitiesWindow(DatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
            InitializeComponent();

            LoadUsers();
            LoadWorkgroups();
           
        }

        private void LoadUsers()
        {
            // Get the list of users from the database.
            List<User> users = databaseHelper.GetAllUsers();

            // Bind the list of users to the UserComboBox.
            UserComboBox.ItemsSource = users;
            UserComboBox.DisplayMemberPath = "fullname";
            UserComboBox.SelectedValuePath = "id";
        }

        private void LoadWorkgroups()
        {
            // Get the list of workgroups from the database.
            List<Workgroup> workgroups = databaseHelper.GetAllWorkgroups();

            // Bind the list of workgroups to the WorkgroupComboBox.
            WorkgroupComboBox.ItemsSource = workgroups;
            WorkgroupComboBox.DisplayMemberPath = "Name";
            WorkgroupComboBox.SelectedValuePath = "Id";
        }

        private void AssignResponsibilityButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = (int)UserComboBox.SelectedValue;
            int workgroupId = (int)WorkgroupComboBox.SelectedValue;

            // Assign the responsibility in the database.
            databaseHelper.AssignResponsibility(userId, workgroupId);

            MessageBox.Show("Responsibility assigned successfully.");
        }


    }

}
