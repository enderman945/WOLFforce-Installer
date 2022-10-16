using System;
using System.Net;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;
using System.Reflection.Emit;
using System.IO;

namespace WOLFforce_Installer
{
    /// <summary>
    /// Logique d'interaction pour JavaDl.xaml
    /// </summary>
    public partial class JavaDl : Page
    {
        public JavaDl()
        {
            string TempPath = $@"C:\Users\{Environment.UserName}\AppData\Local\Temp";
            InitializeComponent();
            TempPathCheck(TempPath);
            StartDownload(TempPath);
        }


        private void JavaDlCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment annuler l'installation ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Environment.Exit(1);
            }
        }

        private void JavaDlNext_Click(object sender, RoutedEventArgs e)
        {
            // Change content to JavaIn at XXX
            //Application.Current.Dispatcher.Invoke(new System.Action(() => XXX ));
            //Window.GetWindow(this.Parent).Content = new JavaIn();
        }

        public void TempPathCheck(string TempPath)
        {
            Jprogressbar.IsIndeterminate = false;
            Jpercentage.Visibility = Visibility.Visible;
            System.IO.Directory.CreateDirectory(TempPath);

            // Determine whether the directory exists.
            if (Directory.Exists(TempPath))
            {
                Console.WriteLine("That path exists already.");
            }
            else
            {
                // Try to create the directory.
                System.IO.Directory.CreateDirectory(TempPath);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(TempPath));
            }
        }

        public void StartDownload(string TempPath)
        {
            Thread thread = new Thread(() =>
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri("https://cdn-fastly.obsproject.com/downloads/OBS-Studio-28.0.2-Full-Installer-x64.exe"), $@"{TempPath}\java.exe");
            });
            thread.Start();

        }
        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            string dlj = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;

            if (Application.Current.Dispatcher.CheckAccess())
            { Update_UI(percentage); }
            else
            { Application.Current.Dispatcher.Invoke(new System.Action(() => Update_UI(percentage))); }

        }
        public void Update_UI(double percentage)
        {
            Jprogressbar.Value = int.Parse(Math.Truncate(percentage).ToString());
            Jpercentage.Text = Math.Truncate(percentage).ToString() + "%";
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new System.Action(() => NextJDL.Visibility = Visibility.Visible));
            Application.Current.Dispatcher.Invoke(new System.Action(() => Window.GetWindow(this.Parent).Content = new JavaIn()));
        }

    }
}
