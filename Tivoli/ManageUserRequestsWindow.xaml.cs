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
using System.Windows;
using Tivoli.Data;
using Tivoli.Models;

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for ManageUserRequestsWindow.xaml
    /// </summary>
    

    public partial class ManageUserRequestsWindow : Window
    {
        MyDatabaseContext context;
        DatabaseHelper dbHelper;
        public ManageUserRequestsWindow(MyDatabaseContext context, DatabaseHelper dbhelper)
        {
            this.context = context;
            InitializeComponent();
            this.dbHelper = dbhelper;
        }


        private void LoadUserRequests()
        {
            var userRequests = dbHelper.GetUserRequests();
            UserRequestsDataGrid.ItemsSource = userRequests;
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            UserRequest selectedRequest = (UserRequest)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {
                // Update the UserRequest status to "Approved"
                selectedRequest.Status = "Approved";
                dbHelper.UpdateUserRequest(selectedRequest);

                // Assign user to the workgroup
                selectedRequest.User.workgroupId = selectedRequest.WorkgroupId;
                dbHelper.UpdateUser(selectedRequest.User);

                // Refresh the user request list in the UI
                LoadUserRequests();
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            UserRequest selectedRequest = (UserRequest)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {
                // Update the UserRequest status to "Rejected"
                selectedRequest.Status = "Rejected";
                dbHelper.UpdateUserRequest(selectedRequest);

                // Refresh the user request list in the UI
                LoadUserRequests();
            }
        }
    }

}
