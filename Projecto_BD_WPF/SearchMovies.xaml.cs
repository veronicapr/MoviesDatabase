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
    /// Interaction logic for SearchMovies.xaml
    /// </summary>
    public partial class SearchMovies : Page
    {
        SqlConnection cnn;
        public SearchMovies()
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

            LoadData("SELECT * from movies.udf_GetMovies()");

            string getActorsQuery = "SELECT ssn, name FROM movies.udf_Actors()";

            complete_list_box(getActorsQuery, actors_combo);

            string getStudiosQuery = "SELECT id, name FROM movies.udf_studios()";

            complete_combo_box(getStudiosQuery, studio);
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

        private void LoadData(string query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Movies");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Movies");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchMovie = "movies.sp_searchMovies";

            char[] d = { ':', '-' };

            string movieTitle = title.Text;
            string movieCountry = Country.Text;
            string movieYear = Year.Text;
            string movieAgeRestriction = age_restriction.Text;
            string movieStudio = studio.Text.ToString().Split(d)[0];

            DataTable actors = new DataTable();
            actors.Clear();
            actors.Columns.Add("ssn");
            foreach (CheckBox s in actors_combo.Items)
            {
                if (s.IsChecked.HasValue && s.IsChecked.Value)
                {
                    DataRow row = actors.NewRow();
                    row["ssn"] = s.Content.ToString().Split(d)[0];
                    actors.Rows.Add(row);
                }
            }

            SqlCommand searchQuery = new SqlCommand(searchMovie, cnn);
            searchQuery.CommandType = CommandType.StoredProcedure;
            if (movieTitle != "")
                searchQuery.Parameters.AddWithValue("@Title", movieTitle);
            if (movieAgeRestriction != "")
                searchQuery.Parameters.AddWithValue("@Age_restriction", movieAgeRestriction);
            if (movieCountry != "")
                searchQuery.Parameters.AddWithValue("@Country", movieCountry);
            if (movieStudio != "")
                searchQuery.Parameters.AddWithValue("@Studio_id", Convert.ToInt32(movieStudio));
            if (movieYear != "")
                searchQuery.Parameters.AddWithValue("@Year", Convert.ToInt32(movieYear));
            if (actors.Rows.Count > 0) 
            {
                SqlParameter param_actors = searchQuery.Parameters.AddWithValue("@Actors", actors);
                param_actors.SqlDbType = SqlDbType.Structured;
                param_actors.TypeName = "movies.actorlist";
            }

            try
            {
                SearchData(searchQuery);
            }
            catch
            {
                MessageBox.Show("Error on searching Movies in the database");
                return;
            }
 
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            Country.Text = "";
            Year.Text = "";
            age_restriction.Text = "";
            studio.Text = "";

            foreach (CheckBox s in actors_combo.Items)
            {
                s.IsChecked = false;                
            }
            LoadData("SELECT * from movies.udf_GetMovies()");
        }
    }
}
