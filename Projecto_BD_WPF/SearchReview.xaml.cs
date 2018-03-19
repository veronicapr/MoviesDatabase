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
    /// Interaction logic for SearchReview.xaml
    /// </summary>
    public partial class SearchReview : Page
    {
        SqlConnection cnn;
        public SearchReview()
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

            LoadData("SELECT * from movies.udf_GetReviews()");

            string getMovieIdQuery = "SELECT * FROM movies.udf_movieIdsNames() order by id";

            complete_combo_box(getMovieIdQuery, movie);

            string getUsersQuery = "SELECT username, name FROM movies.udf_GetUsers()";

            complete_list_box(getUsersQuery, users_combo);

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
            DataTable dt = new DataTable("Reviews");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
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
            string searchReviews = "movies.sp_searchReviews";

            char[] d = { ':', '-' };

            string reviewRating = rating.Text;
            string reviewYear = year.Text;
            string movieID = movie.Text.ToString().Split(d)[0];

            DataTable users = new DataTable();
            users.Clear();
            users.Columns.Add("username");
            foreach (CheckBox s in users_combo.Items)
            {
                if (s.IsChecked.HasValue && s.IsChecked.Value)
                {
                    DataRow row = users.NewRow();
                    row["username"] = s.Content.ToString().Split(d)[0];
                    users.Rows.Add(row);
                }
            }

            SqlCommand searchQuery = new SqlCommand(searchReviews, cnn);
            searchQuery.CommandType = CommandType.StoredProcedure;

            if (reviewRating != "")
                searchQuery.Parameters.AddWithValue("@Rating", reviewRating);
            if (reviewYear != "")
                searchQuery.Parameters.AddWithValue("@Year", reviewYear);
            if (movieID != "")
                searchQuery.Parameters.AddWithValue("@MovieID", Convert.ToInt32(movieID));
            if (users.Rows.Count > 0)
            {
                SqlParameter param_actors = searchQuery.Parameters.AddWithValue("@Users", users);
                param_actors.SqlDbType = SqlDbType.Structured;
                param_actors.TypeName = "movies.userlist";
            }

            try
            {
                SearchData(searchQuery);
            }
            catch
            {
                MessageBox.Show("Error on searching Reviews in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            movie.Text = "";
            rating.Text = "";
            year.Text = "";
            foreach (CheckBox s in users_combo.Items)
            {
                s.IsChecked = false;
            }
            LoadData("SELECT * from movies.udf_GetReviews()");
        }
    }
}
