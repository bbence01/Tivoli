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
        UserTivoli _currentUser;
        public ManageUserRequestsWindow(UserTivoli user, MyDatabaseContext context, DatabaseHelper dbhelper)
        {
            _currentUser = user;
            this.context = context;
            this.dbHelper = dbhelper;
            InitializeComponent();
            

            LoadUserRequests();
        }


        private void LoadUserRequests()
        {
            List<RequestTivoli> userRequests = dbHelper.GetUserRequests(_currentUser);
            UserRequestsDataGrid.ItemsSource = userRequests;
            

        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTivoli selectedRequest = (RequestTivoli)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {
                // Update the RequestTivoli status to "Approved"
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
            RequestTivoli selectedRequest = (RequestTivoli)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {
                // Update the RequestTivoli status to "Rejected"
                selectedRequest.Status = "Rejected";
                dbHelper.UpdateUserRequest(selectedRequest);

                // Refresh the user request list in the UI
                LoadUserRequests();
            }
        }
    }

}
