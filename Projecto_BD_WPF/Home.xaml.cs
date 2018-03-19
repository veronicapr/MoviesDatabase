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

namespace Projecto_BD_WPF
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void goto_add()
        {
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }

        private void goto_search()
        {
            SearchPage search = new SearchPage();
            this.NavigationService.Navigate(search);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            goto_add();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            goto_search();
        }
    }
}
