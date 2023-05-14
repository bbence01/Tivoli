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
    /// Interaction logic for CreateWorkgroupWindow.xaml
    /// </summary>
    public partial class CreateWorkgroupWindow : Window
    {
        MyDatabaseContext context;
        DatabaseHelper dbhelper;
        UserTivoli _currentUser;
        public CreateWorkgroupWindow(UserTivoli user,MyDatabaseContext context, DatabaseHelper dbhelper)
        {
            this.context = context;
            InitializeComponent();
            this.dbhelper = dbhelper;
            this._currentUser = user;

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string workgroupName = NameTextBox.Text.Trim();
            string workgroupDescription = DescriptionTextBox.Text.Trim();

            if (string.IsNullOrEmpty(workgroupName) || string.IsNullOrEmpty(workgroupDescription))
            {
                Logger.Log($" {_currentUser.username} Please enter a name and description for the workgroup..");

                MessageBox.Show("Please enter a name and description for the workgroup.");
                return;
            }

            // Create a new workgroup object
            WorkgroupTivoli newWorkgroup = new WorkgroupTivoli
            {
                Name = workgroupName,
                Description = workgroupDescription
            };

            // Save the new workgroup to the database
            /* using (context )
             {
                 context.Workgroups.Add(newWorkgroup);
                 context.SaveChanges();
             }*/

            dbhelper.AddWorkgroup(newWorkgroup, _currentUser);

            // Close the window
            this.DialogResult = true;
            this.Close();
        }
    }

}
