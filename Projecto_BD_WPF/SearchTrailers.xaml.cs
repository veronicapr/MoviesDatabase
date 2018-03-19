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
    /// Interaction logic for SearchTrailers.xaml
    /// </summary>
    public partial class SearchTrailers : Page
    {
        SqlConnection cnn;
        public SearchTrailers()
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

            LoadData("SELECT * from movies.udf_GetTrailers()");

            string getMovieIdQuery = "SELECT * FROM movies.udf_movieIdsNames() order by id";

            complete_combo_box(getMovieIdQuery, movie);

            string getLanguagesQuery = "SELECT * FROM movies.udf_GetLanguages()";

            complete_combo_box(getLanguagesQuery, language);
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
            DataTable dt = new DataTable("Trailers");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Trailers");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchTrailer = "movies.sp_searchTrailers";

            char[] d = { ':', '-' };

            string trailerTitle = title.Text;
            string trailerLanguage = language.Text;
            string trailerYear = Year.Text;
            string movieID = movie.Text.ToString().Split(d)[0];

            SqlCommand searchQuery = new SqlCommand(searchTrailer, cnn);
            searchQuery.CommandType = CommandType.StoredProcedure;
            if (trailerTitle != "")
                searchQuery.Parameters.AddWithValue("@Title", trailerTitle);
            if (trailerLanguage != "")
                searchQuery.Parameters.AddWithValue("@Language", trailerLanguage);
            if (trailerYear != "")
                searchQuery.Parameters.AddWithValue("@Year", trailerYear);
            if (movieID != "")
                searchQuery.Parameters.AddWithValue("@MovieID", Convert.ToInt32(movieID));

            try
            {
                SearchData(searchQuery);
            }
            catch
            {
                MessageBox.Show("Error on searching Trailers in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            language.Text = "";
            Year.Text = "";
            movie.Text = "";
            LoadData("SELECT * from movies.udf_GetTrailers()");
        }
    }
}
