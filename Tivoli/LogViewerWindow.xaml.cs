using Microsoft.Extensions.Logging.Abstractions;
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
    /// Interaction logic for LogViewerWindow.xaml
    /// </summary>
    public partial class LogViewerWindow : Window
    {
        private void LoadLogEntries()
        {/*
            // Replace this with your actual method of retrieving log entries
            // e.g., from a file or a database
            List<Microsoft.IdentityModel.Abstractions.LogEntry> logEntries = GetLogEntries();

            LogDataGrid.ItemsSource = logEntries;*/
        }

        public LogViewerWindow()
        {
            InitializeComponent();
            LoadLogEntries();
        }

        private void ViewLogsButton_Click(object sender, RoutedEventArgs e)
        {
            LogViewerWindow logViewerWindow = new LogViewerWindow();
            logViewerWindow.ShowDialog(); // Use ShowDialog() to open the window as a modal dialog
        }


    }
}
