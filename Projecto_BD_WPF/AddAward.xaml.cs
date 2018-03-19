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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AddAward : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;

        public AddAward()
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

            cmd = new SqlCommand();
            cmd.Connection = cnn;
        }
        private void cancel_add_award_button_Click(object sender, RoutedEventArgs e)
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

        private void add_award_button_Click(object sender, RoutedEventArgs e)
        {
            string insertAward = "movies.sp_AddAward";

            Award a = new Award();

            try
            {
                a.year = Convert.ToInt32(year.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Year takes integers only");
                return;
            }

            if (type.Text.Length == 0)
            {
                MessageBox.Show("Type Field is required");
                return;
            }
            else
                a.type = type.Text;

            if (designation.Text.Length == 0)
            {
                MessageBox.Show("Designation Field is required");
                return;
            }
            else
                a.designation = designation.Text;


            try
            {
                a.movie_id = Convert.ToInt32(movie_id.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Movie ID takes integers only");
                return;
            }




            cmd = new SqlCommand(insertAward, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@year", a.year);
            cmd.Parameters.AddWithValue("@type", a.type);
            cmd.Parameters.AddWithValue("@designation", a.designation);
            cmd.Parameters.AddWithValue("@movie_id", a.movie_id);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting Award to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }
    }
}
