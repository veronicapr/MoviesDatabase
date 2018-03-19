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
using System.Data.SqlClient;
using System.Data;

namespace Projecto_BD_WPF
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class AddReview : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;
        public AddReview()
        {
            InitializeComponent();

            //ligação à base de dados
            cnn = Connection.getConnection();

            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open connection to database due to the following error:\n" + ex.Message);
            }

            cmd = new SqlCommand();
            cmd.Connection = cnn;
        }
        private void add_review_button_Click(object sender, RoutedEventArgs e)
        {
            string insertReview = "movies.sp_AddReview";

            Review r = new Review();

            try
            {
                r.id = Convert.ToInt32(id.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument ID takes integers only");
                return;
            }

            try
            {
                r.rating = Convert.ToInt32(rating.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Rating takes integers only");
                return;
            }

            if (review.Text.Length == 0)
            {
                MessageBox.Show("Review Field is required");
                return;
            }
            else
                r.review = review.Text;

            char[] d = { ':', '-', '/' };
            if (date != null && date.Text != "")
            {
                int year, month, day;
                string[] date2 = new string[3];
                date2 = date.Text.Split(d);
                year = Convert.ToInt32(date2[2]);
                month = Convert.ToInt32(date2[1]);
                day = Convert.ToInt32(date2[0]);

                if (year > 20)
                    year += 1900;
                else
                    year += 2000;

                r.date = new DateTime(year, month, day);
            }

            try
            {
                r.movie_id = Convert.ToInt32(movie_id.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Movie ID takes integers only");
                return;
            }

            if (username.Text.Length == 0)
            {
                MessageBox.Show("Username Field is required");
                return;
            }
            else
                r.username = username.Text;



            cmd = new SqlCommand(insertReview, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", r.id);
            cmd.Parameters.AddWithValue("@rating", r.rating);
            cmd.Parameters.AddWithValue("@review", r.review);
            cmd.Parameters.AddWithValue("@date", r.date.ToString("u"));
            cmd.Parameters.AddWithValue("@movie_id", r.movie_id);
            cmd.Parameters.AddWithValue("@username", r.username);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting Review to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }
        private void cancel_add_review_button_Click(object sender, RoutedEventArgs e)
        {
            string message = "This will discard all inputed data";
            string caption = "Confirm?";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            // Show message box
            MessageBoxResult result = MessageBox.Show(message, caption, buttons);


            if (result == MessageBoxResult.OK)
            {
                cnn.Close();
                AddPage add = new AddPage();
                this.NavigationService.Navigate(add);
            }
        }
    }
}

