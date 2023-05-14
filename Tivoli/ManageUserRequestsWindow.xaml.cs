﻿using System;
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
        public ManageUserRequestsWindow(UserTivoli user, MyDatabaseContext context, DatabaseHelper dbhelper)
        {
            this._currentUser = user;
            this.context = context;
            this.dbHelper = dbhelper;
            this.emailService = new EmailService();
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





                dbHelper.SendEmailConfirmation(selectedRequest);





                dbHelper.ApproveRequest(selectedRequest.Id, _currentUser.id);


             



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
                dbHelper.RejectRequest(selectedRequest.Id, _currentUser.id);

                // Refresh the user request list in the UI
                LoadUserRequests();
            }
        }
    }

}
