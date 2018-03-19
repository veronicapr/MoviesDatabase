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
    /// Interaction logic for SearchUsers.xaml
    /// </summary>
    public partial class SearchUsers : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;
        public SearchUsers()
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

            LoadData("SELECT * from movies.udf_GetUsers()");

        }
        private void LoadData(string query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Users");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }
        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Users");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchUsers = "movies.sp_searchUsers";

            char[] d = { ':', '-', '/' };
            string  uUsername= username.Text;
            string uName = name.Text;
            string uEmail = email.Text;
            string uCountry = country.Text;

            DateTime uBdate = new DateTime(1, 1, 1);

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

                uBdate = new DateTime(year, month, day);
            }

            cmd = new SqlCommand(searchUsers, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (uUsername != "")
                cmd.Parameters.AddWithValue("@username", uUsername);
            if (uName != "")
                cmd.Parameters.AddWithValue("@name", uName);
            if (uBdate.ToString("u") != "0001-01-01 00:00:00Z")
                cmd.Parameters.AddWithValue("@bdate", uBdate.ToString("u"));
            if (uEmail != "")
                cmd.Parameters.AddWithValue("@email", uEmail);
            if (uCountry != "")
                cmd.Parameters.AddWithValue("@country", uCountry);

            try
            {
                SearchData(cmd);
            }
            catch
            {
                MessageBox.Show("Error on searching Users in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            username.Text = "";
            name.Text = "";
            birth_date.Text = "";
            email.Text = "";
            country.Text = "";
            LoadData("SELECT * from movies.udf_GetUsers()");
        }
    }
}
