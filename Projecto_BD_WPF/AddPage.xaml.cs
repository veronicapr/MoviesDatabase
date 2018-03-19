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
    /// Interaction logic for AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMovies addMovie = new AddMovies();
            this.NavigationService.Navigate(addMovie);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddActors addActor = new AddActors();
            this.NavigationService.Navigate(addActor);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddStudios addStudio = new AddStudios();
            this.NavigationService.Navigate(addStudio);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddReleases addRelease = new AddReleases();
            this.NavigationService.Navigate(addRelease);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddTrailer addTrailer = new AddTrailer();
            this.NavigationService.Navigate(addTrailer);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            this.NavigationService.Navigate(home);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            AddUsers users = new AddUsers();
            this.NavigationService.Navigate(users);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            AddWriter writer = new AddWriter();
            this.NavigationService.Navigate(writer);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AddReview review = new AddReview();
            this.NavigationService.Navigate(review);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            AddAward award = new AddAward();
            this.NavigationService.Navigate(award);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            AddDirector director = new AddDirector();
            this.NavigationService.Navigate(director);
        }
    }
}
