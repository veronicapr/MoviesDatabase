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
    /// Interaction logic for SearchDirectors.xaml
    /// </summary>
    public partial class SearchDirectors : Page
    {
        SqlConnection cnn;
        public SearchDirectors()
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

            LoadData("SELECT * from movies.udf_GetDirectors()");

            string getMovieIdQuery = "SELECT * FROM movies.udf_movieIdsNames() order by id";

            complete_combo_box(getMovieIdQuery, movie);
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
            DataTable dt = new DataTable("Directors");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }
        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Directors");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchDirectors = "movies.sp_searchDirectors";


            char[] d = { ':', '-', '/' };
            string wName = name.Text;
            string wRank = rank.Text;
            string wMovie = movie.Text.ToString().Split(d)[0];
            DateTime wBdate = new DateTime(1,1,1);

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

                wBdate = new DateTime(year, month, day);
            }

            SqlCommand cmd = new SqlCommand(searchDirectors, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (wName != "")
                cmd.Parameters.AddWithValue("@name", wName);
            if (wBdate.ToString("u") != "0001-01-01 00:00:00Z")
                cmd.Parameters.AddWithValue("@bdate", wBdate.ToString("u"));
            if (wRank != "")
                cmd.Parameters.AddWithValue("@rank", wRank);
            if (wMovie != "")
                cmd.Parameters.AddWithValue("@movieID", wMovie);
            try
            {
                SearchData(cmd);
            }
            catch
            {
                MessageBox.Show("Error on searching Director in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            movie.Text = "";
            name.Text = "";
            date.Text = "";
            rank.Text = "";
            LoadData("SELECT * from movies.udf_GetDirectors()");
        }
    }
}
