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
        public LogViewerWindow(UserTivoli user)
        {
            InitializeComponent();
            Logger.Log($" {user.username} Opened logs");

            string logs = Logger.ReadLogs(); // Logger should be the class where you defined the Log method
            LogTextBox.Text = logs;
        }
    }

}
