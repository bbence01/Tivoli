using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tivoli.Data;

namespace Tivoli
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {/*
        static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Egyetem\4.felev\INFOBiz\feleves\Tivoli\Tivoli\Data\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30";

      


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            if (e.Args.Length > 0)
            {
                Uri uri = new Uri(e.Args[0]);
                string[] parts = uri.AbsolutePath.TrimStart('/').Split('/');
                if (parts.Length == 3)
                {
                    int requestId = int.Parse(parts[0]);
                    string action = parts[1];
                    string token = parts[2];

                    using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("TivoliTeszt")))
                    {
                        string message = $"{requestId}/{action}";
                        byte[] expectedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                        string expectedToken = Convert.ToBase64String(expectedHash);

                        if (SecureEquals(token, expectedToken))
                        {
                            DatabaseHelper dbHelper = new DatabaseHelper(ConnectionString);
                            dbHelper.UpdateRequestStatus(requestId, "Confirmed");
                        }
                    }
                }
            }
        }

        private bool SecureEquals(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }*/
    }

}
