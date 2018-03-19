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
    /// Interaction logic for SearchStudios.xaml
    /// </summary>
    public partial class SearchStudios : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;
        public SearchStudios()
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

            LoadData("SELECT * from movies.udf_GetStudios()");

        }
        private void LoadData(string query)
        {
            SqlCommand command = new SqlCommand(query, cnn);
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Studios");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }
        private void SearchData(SqlCommand command)
        {
            SqlDataAdapter sda = new SqlDataAdapter(command);
            DataTable dt = new DataTable("Studios");
            sda.Fill(dt);
            search_result.ItemsSource = dt.DefaultView;
        }
        
        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchStudios = "movies.sp_searchStudios";

            char[] d = { ':', '-', '/' };
            string sName = name.Text;
            string sLocation = location.Text;
           

            cmd = new SqlCommand(searchStudios, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (sName != "")
                cmd.Parameters.AddWithValue("@name", sName);
            if (sLocation != null)
                cmd.Parameters.AddWithValue("@location", sLocation);


            try
            {
                SearchData(cmd);
            }
            catch
            {
                MessageBox.Show("Error on searching Studios in the database");
                return;
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            location.Text = "";
            LoadData("SELECT * from movies.udf_GetStudios()");
        }
    }
}
