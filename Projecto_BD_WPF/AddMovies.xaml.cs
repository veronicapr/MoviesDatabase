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
    /// Interaction logic for AddMovies.xaml
    /// </summary>
    public partial class AddMovies : Page
    {
        SqlConnection cnn;

        public AddMovies()
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
            }

            string getActorsQuery = "SELECT ssn, name FROM movies.udf_Actors()";

            complete_list_box(getActorsQuery, add_movie_actors);

            string getWritersQuery = "SELECT ssn, name FROM movies.udf_Writers()";

            complete_list_box(getWritersQuery, add_movie_writers);

            string getGenresQuery = "SELECT * FROM movies.udf_UniqueGenres()";

            complete_list_box(getGenresQuery, genre_listbox);

            string getDirectorsQuery = "SELECT ssn, name FROM movies.udf_Directors()";

            complete_combo_box(getDirectorsQuery, directors_combo_box);

            string getStudiosQuery = "SELECT id, name FROM movies.udf_studios()";

            complete_combo_box(getStudiosQuery, studios_combo_box);
        }

        private void complete_list_box(string query, ListBox listbox) 
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

                    CheckBox checkbox = new CheckBox();
                    checkbox.Content = item;

                    listbox.Items.Add(checkbox);
                }
            }
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

        private void add_movie_button_Click(object sender, RoutedEventArgs e)
        {
            string insertMovie = "movies.sp_AddMovie";

            Movie m = new Movie();

            char[] d = { ':', '-' };
            try
            {
                m.movieId = Convert.ToInt32(movie_id.Text);
            }
            catch (FormatException) 
            {
                MessageBox.Show("Argument Movie Id takes integers only");
                return;
            }
            
            string[] time = duration.Text.Split(d);
            try
            {
                m.duration = new TimeSpan(Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Duration takes 3 integers \nFormat: HH:MM:SS");
                return;
            }

            if (description.Text.Length > 500)
            {
                MessageBox.Show("description is too big");
                return;
            }
            else
                m.description = description.Text;

            if (age_restriction.Text == "") 
            {
                MessageBox.Show("Please select an Age Restriction");
                return;
            }
            else
                m.age_restriction = age_restriction.Text;

            try
            {
                m.rating = Convert.ToInt32(rating.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Rating takes integers only");
                return;
            }

            if (studios_combo_box.Text == "")
            {
                MessageBox.Show("Please select a Studio");
                return;
            }
            else 
            {
                m.studio_id = Convert.ToInt32(studios_combo_box.Text.Split(d)[0]); 
            }

            if (directors_combo_box.Text == "")
            {
                MessageBox.Show("Please select a Director");
                return;
            }
            else
            {
                m.director_ssn = Convert.ToInt32(directors_combo_box.Text.Split(d)[0]);
            }

            
            SqlCommand insertQuery = new SqlCommand(insertMovie, cnn);
            insertQuery.CommandType = CommandType.StoredProcedure;

            DataTable genres = new DataTable();
            genres.Clear();
            genres.Columns.Add("genre");
            foreach (CheckBox s in genre_listbox.Items)
            {
                if (s.IsChecked.HasValue && s.IsChecked.Value)
                {
                    DataRow row = genres.NewRow();
                    row["genre"] = s.Content.ToString();
                    genres.Rows.Add(row);
                }
            }

            DataTable actors = new DataTable();
            actors.Clear();
            actors.Columns.Add("ssn");
            foreach (CheckBox s in add_movie_actors.Items)
            {
                if (s.IsChecked.HasValue && s.IsChecked.Value)
                {
                    DataRow row = actors.NewRow();
                    row["ssn"] = s.Content.ToString().Split(d)[0];
                    actors.Rows.Add(row);
                }
            }

            DataTable writers = new DataTable();
            writers.Clear();
            writers.Columns.Add("ssn");
            foreach (CheckBox s in add_movie_writers.Items)
            {
                if (s.IsChecked.HasValue && s.IsChecked.Value)
                {
                    DataRow row = writers.NewRow();
                    row["ssn"] = s.Content.ToString().Split(d)[0];
                    writers.Rows.Add(row);
                }
            }

            insertQuery.Parameters.AddWithValue("@id", m.movieId);
            insertQuery.Parameters.AddWithValue("@duration", m.duration.ToString(@"hh\:mm\:ss"));
            insertQuery.Parameters.AddWithValue("@description", m.description);
            insertQuery.Parameters.AddWithValue("@age_restriction", m.age_restriction);
            insertQuery.Parameters.AddWithValue("@rating", m.rating);
            insertQuery.Parameters.AddWithValue("@studio_id", m.studio_id);
            insertQuery.Parameters.AddWithValue("@director_ssn", m.director_ssn);
            SqlParameter param_genre = insertQuery.Parameters.AddWithValue("@Genre", genres);
            param_genre.SqlDbType = SqlDbType.Structured;
            param_genre.TypeName = "movies.genrelist";
            SqlParameter param_actors = insertQuery.Parameters.AddWithValue("@Actors", actors);
            param_actors.SqlDbType = SqlDbType.Structured;
            param_actors.TypeName = "movies.actorlist";
            SqlParameter param_writers = insertQuery.Parameters.AddWithValue("@Writers", writers);
            param_writers.SqlDbType = SqlDbType.Structured;
            param_writers.TypeName = "movies.writerlist";

            try
            {
                insertQuery.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting Movie to database");
                return;
            }

            cnn.Close();
            AddReleases add = new AddReleases(m.movieId);
            this.NavigationService.Navigate(add);
        }

        private void cancel_add_movie_button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.Content = addNewGenres.Text;
            addNewGenres.Text = "";

            checkbox.IsChecked = true;

            genre_listbox.Items.Add(checkbox);
        }
    }
}
