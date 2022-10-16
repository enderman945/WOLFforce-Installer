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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WOLFforce_Installer
{
    /// <summary>
    /// Logique d'interaction pour JavaIn.xaml
    /// </summary>
    public partial class JavaIn : Page
    {
        public JavaIn()
        {
            InitializeComponent();
        }

        private void JavaInCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment annuler l'installation ?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Environment.Exit(1);
            }
        }

        private void InstallJavaExe(string JavaPath)
        {
            Process.Start(JavaPath);
        }
    }
}
