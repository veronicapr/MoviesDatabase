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
    /// Interaction logic for SearchWriters.xaml
    /// </summary>
    public partial class SearchWriters : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;
        public SearchWriters()
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

            LoadData("SELECT * from movies.udf_GetWriters()");
        }
        private void LoadData(string query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Writers");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }
        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Writers");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchWriters = "movies.sp_searchWriters";


            char[] d = { ':', '-', '/' };
            string wName = name.Text;
            string wRank = rank.Text;

            DateTime wBdate = new DateTime(1, 1, 1);

            if (birth_date != null && birth_date.Text != "")
            {
                int year, month, day;
                string[] dates = new string[3];
                dates = birth_date.Text.Split(d);
                year = Convert.ToInt32(dates[2]);
                month = Convert.ToInt32(dates[1]);
                day = Convert.ToInt32(dates[0]);

                if (year > 20)
                    year += 1900;
                else
                    year += 2000;

                wBdate = new DateTime(year, month, day);
            }

            cmd = new SqlCommand(searchWriters, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (wName != "")
                cmd.Parameters.AddWithValue("@name", wName);
            if (wBdate.ToString("u") != "0001-01-01 00:00:00Z")
                cmd.Parameters.AddWithValue("@bdate", wBdate.ToString("u"));
            if (wRank != "")
                cmd.Parameters.AddWithValue("@rank", wRank);

            try
            {
                SearchData(cmd);
            }
            catch
            {
                MessageBox.Show("Error on searching Writers in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            birth_date.Text = "";
            rank.Text = "";
            LoadData("SELECT * from movies.udf_GetWriters()");
        }
    }
}
