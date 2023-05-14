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

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for ConfirmCodeWindow.xaml
    /// </summary>
    public partial class ConfirmCodeWindow : Window
    {
        public int EnteredCode { get; private set; }

        public ConfirmCodeWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have a TextBox called CodeInput
            if (int.TryParse(CodeInput.Text, out int code))
            {
                EnteredCode = code;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                // Handle invalid input
                MessageBox.Show("Please enter a valid code.");
            }
        }
    }

}
