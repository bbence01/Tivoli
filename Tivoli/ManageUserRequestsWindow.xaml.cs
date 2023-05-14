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
using Tivoli.Logic;

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
        EmailService emailService;
        public ManageUserRequestsWindow(UserTivoli user, MyDatabaseContext context, DatabaseHelper dbhelper, EmailService emailSender)
        {
            this._currentUser = user;
            this.context = context;
            this.dbHelper = dbhelper;
            this.emailService = emailSender;
            InitializeComponent();
            

            LoadUserRequests();
        }


        private void LoadUserRequests()
        {
            List<RequestTivoli> userRequests = dbHelper.GetUserRequests(_currentUser.id);
            UserRequestsDataGrid.ItemsSource = userRequests;
            

        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            RequestTivoli selectedRequest = (RequestTivoli)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {








                if (selectedRequest.Status == "Approved")
                {
                    dbHelper.ApproveRequest(selectedRequest.Id, _currentUser.id);

                }
                else if (selectedRequest.Status == "In Progress")
                {
                    ConfirmCodeWindow confirmCodeWindow = new ConfirmCodeWindow(_currentUser);
                

                    if (confirmCodeWindow.ShowDialog() == true)
                    {
                        int code = confirmCodeWindow.EnteredCode;
                        dbHelper.ConfirmRequestCode(code, emailService, selectedRequest, _currentUser);
                    }
                    else
                    {
                        MessageBox.Show("Code does not match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }


                }
                else
                {
                    dbHelper.SendEmailConfirmation(selectedRequest, emailService, _currentUser);

                }






                // Refresh the user request list in the UI
                LoadUserRequests();
            }
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {/*
            RequestTivoli selectedRequest = (RequestTivoli)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {
                // Update the RequestTivoli status to "Rejected"
                selectedRequest.Status = "Rejected";
                dbHelper.RejectRequest(selectedRequest.Id, _currentUser.id, _currentUser);

                // Refresh the user request list in the UI
                LoadUserRequests();
            }*/

            RequestTivoli selectedRequest = (RequestTivoli)UserRequestsDataGrid.SelectedItem;
            if (selectedRequest != null)
            {








                if (selectedRequest.Status == "Approved")
                {
                    dbHelper.RejectRequest(selectedRequest.Id, _currentUser.id, _currentUser);

                }
                else if (selectedRequest.Status == "In Progress")
                {
                    ConfirmCodeWindow confirmCodeWindow = new ConfirmCodeWindow(_currentUser);

                    if (confirmCodeWindow.ShowDialog() == true)
                    {
                        int code = confirmCodeWindow.EnteredCode;
                        dbHelper.ConfirmRequestCode(code, emailService, selectedRequest, _currentUser);
                    }
                    else
                    {
                        // Handle case when the ConfirmCodeWindow was closed without entering a code

                    }


                }
                else
                {
                    dbHelper.SendEmailConfirmation(selectedRequest, emailService, _currentUser);

                }






                // Refresh the user request list in the UI
                LoadUserRequests();
            }


        }
    }

}
