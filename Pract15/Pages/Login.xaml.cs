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

namespace Pract15.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public string pinCode { get; set; } = null!;

        public Login()
        {
            InitializeComponent();
        }

        private void ButtonLoginCustomer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Magazine(false));
        }

        private void ButtonLoginAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (pinCode != "1234")
                MessageBox.Show("Неверный пин-код");
            else
            NavigationService.Navigate(new Magazine(true));
        }
    }
}
