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
    /// Interaction logic for SearchAwards.xaml
    /// </summary>
    public partial class SearchAwards : Page
    {
        SqlConnection cnn;
        public SearchAwards()
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

            LoadData("SELECT * from movies.udf_GetAwards()");

            string getMovieIdQuery = "SELECT * FROM movies.udf_movieIdsNames() order by id";

            complete_combo_box(getMovieIdQuery, movie);

            string getTypesQuery = "SELECT distinct [type] FROM movies.udf_GetAwards ()";

            complete_combo_box(getTypesQuery, type);
        }

        private void LoadData(string query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Reviews");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
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

        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Reviews");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchAwards = "movies.sp_searchAwards";

            char[] d = { ':', '-' };

            string awardType = type.Text;
            string awardYear = year.Text;
            string movieID = movie.Text.ToString().Split(d)[0];

            SqlCommand searchQuery = new SqlCommand(searchAwards, cnn);
            searchQuery.CommandType = CommandType.StoredProcedure;

            if (awardType != "")
                searchQuery.Parameters.AddWithValue("@Type", awardType);
            if (awardYear != "")
                searchQuery.Parameters.AddWithValue("@Year", Convert.ToInt32(awardYear));
            if (movieID != "")
                searchQuery.Parameters.AddWithValue("@MovieID", Convert.ToInt32(movieID));

            try
            {
                SearchData(searchQuery);
            }
            catch
            {
                MessageBox.Show("Error on searching Awards in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            movie.Text = "";
            type.Text = "";
            year.Text = "";
            LoadData("SELECT * from movies.udf_GetAwards()");
        }
    }
}
