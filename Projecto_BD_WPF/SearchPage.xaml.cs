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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchMovies searchMovie = new SearchMovies();
            this.NavigationService.Navigate(searchMovie);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SearchActors actors = new SearchActors();
            this.NavigationService.Navigate(actors);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SearchStudios studios = new SearchStudios();
            this.NavigationService.Navigate(studios);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SearchUsers users = new SearchUsers();
            this.NavigationService.Navigate(users);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SearchWriters writers = new SearchWriters();
            this.NavigationService.Navigate(writers);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SearchTrailers searchTrailer = new SearchTrailers();
            this.NavigationService.Navigate(searchTrailer);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SearchReview reviews = new SearchReview();
            this.NavigationService.Navigate(reviews);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SearchAwards awards = new SearchAwards();
            this.NavigationService.Navigate(awards);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            this.NavigationService.Navigate(home);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            SearchDirectors dir = new SearchDirectors();
            this.NavigationService.Navigate(dir);

        }
    }
}
