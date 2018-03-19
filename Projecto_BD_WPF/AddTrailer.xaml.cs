using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Projecto_BD_WPF
{
    /// <summary>
    /// Interaction logic for AddTrailer.xaml
    /// </summary>
    public partial class AddTrailer : Page
    {
        SqlConnection cnn;
        public AddTrailer()
        {
            InitializeComponent();

            cnn = Connection.getConnection();
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                return;
            }

            string getMovieIdQuery = "SELECT * FROM movies.udf_movieIdsNames() order by id";

            complete_combo_box(getMovieIdQuery, movie_id_combo);
        }

        private void complete_combo_box(string query, ComboBox combobox)
        {
            SqlCommand command = new SqlCommand(query, cnn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                int count = reader.FieldCount;
                while (reader.Read())
                {
                    string item = reader.GetValue(0).ToString();

                    for (int i = 1; i < count; i++)
                    {
                        item += " - " + reader.GetValue(i).ToString();
                    }

                    combobox.Items.Add(item);
                }
            }
        }

        private void cancel_add_release_button_Click(object sender, RoutedEventArgs e)
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

        private void add_release_button_Click(object sender, RoutedEventArgs e)
        {
            string insertTrailer = "movies.sp_AddTrailer";
            Trailer a = new Trailer();
            char[] d = { ':', '-', '/' };
            try
            {
                a.id = Convert.ToInt32(id.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument ID takes integers only");
                return;
            }

            if (name.Text.Length == 0)
            {
                MessageBox.Show("Title Field is required");
                return;
            }
            else
                a.title = name.Text;

            if (language.Text.Length == 0)
            {
                MessageBox.Show("Language Field is required");
                return;
            }
            else
                a.language = language.Text;

            if (date != null && date.Text != "")
            {
                int year, month, day;
                string[] dates = new string[3];
                dates = date.Text.Split(d);
                year = Convert.ToInt32(dates[2]);
                month = Convert.ToInt32(dates[1]);
                day = Convert.ToInt32(dates[0]);

                if (year > 20)
                    year += 1900;
                else
                    year += 2000;

                a.date = new DateTime(year, month, day);

                if (movie_id_combo.Text == "")
                {
                    MessageBox.Show("Please select a Movie ID");
                    return;
                }
                else
                    a.movieID = Convert.ToInt32(movie_id_combo.Text.Split(d)[0]);

                string[] time = duration.Text.Split(d);
                try
                {
                    a.duration = new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
                }
                catch (FormatException)
                {
                    MessageBox.Show("Argument Duration takes 3 integers \nFormat: HH:MM:SS");
                    return;
                }

            }

            SqlCommand insertQuery = new SqlCommand(insertTrailer, cnn);
            insertQuery.CommandType = CommandType.StoredProcedure;

            insertQuery.Parameters.AddWithValue("@id", a.id);
            insertQuery.Parameters.AddWithValue("@title", a.title);
            insertQuery.Parameters.AddWithValue("@language", a.language);
            insertQuery.Parameters.AddWithValue("@movieID", a.movieID);
            insertQuery.Parameters.AddWithValue("@date", a.date.ToString("u"));
            insertQuery.Parameters.AddWithValue("@duration", a.duration.ToString(@"hh\:mm\:ss"));

            try
            {
                insertQuery.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting Trailer to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }
    }
}
