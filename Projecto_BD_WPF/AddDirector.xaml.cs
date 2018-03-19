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
    /// Interaction logic for AddDirector.xaml
    /// </summary>
    public partial class AddDirector : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;
        public AddDirector()
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

        private void add_writer_button_Click(object sender, RoutedEventArgs e)
        {
            string insertDirector = "movies.sp_AddDirector";

            Writer w = new Writer();

            try
            {
                w.ssn = Convert.ToInt32(ssn.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument SSN takes integers only");
                return;
            }

            if (name.Text.Length == 0)
            {
                MessageBox.Show("Name Field is required");
                return;
            }
            else
                w.name = name.Text;

            char[] d = { ':', '-', '/' };
            if (birth_date != null && birth_date.Text != "")
            {
                int year, month, day;
                string[] date = new string[3];
                date = birth_date.Text.Split(d);
                year = Convert.ToInt32(date[2]);
                month = Convert.ToInt32(date[1]);
                day = Convert.ToInt32(date[0]);

                if (year > 20)
                    year += 1900;
                else
                    year += 2000;

                w.bdate = new DateTime(year, month, day);
            }
            else 
            {
                MessageBox.Show("Birth Date Field is required");
                return;                
            }

            try
            {
                w.rank = Convert.ToInt32(rank.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Rank takes integers only");
                return;
            }

            cmd = new SqlCommand(insertDirector, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ssn", w.ssn);
            cmd.Parameters.AddWithValue("@name", w.name);
            cmd.Parameters.AddWithValue("@birth_date", w.bdate.ToString("u"));
            cmd.Parameters.AddWithValue("@rank", w.rank);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on inserting Director to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }
        private void cancel_add_writer_button_Click(object sender, RoutedEventArgs e)
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
    }
}
