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
    /// Interaction logic for AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Page
    {
        SqlConnection cnn;
        SqlCommand cmd;

        public AddUsers()
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
        private void add_users_button_Click(object sender, RoutedEventArgs e)
        {
            string insertUser = "movies.sp_AddUser";

            User u = new User();

            if (username.Text.Length == 0)
            {
                MessageBox.Show("Username Field is required");
                return;
            }
            else
                u.username = username.Text;

            if (name.Text.Length == 0)
            {
                MessageBox.Show("Name Field is required");
                return;
            }
            else
                u.name = name.Text;

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

                u.bdate = new DateTime(year, month, day);
            }

            if (email.Text.Length == 0)
            {
                MessageBox.Show("Email Field is required");
                return;
            }
            else
                u.email = email.Text;

            if (country.Text.Length == 0)
            {
                MessageBox.Show("Country Field is required");
                return;
            }
            else
                u.country = country.Text;

            cmd = new SqlCommand(insertUser, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", u.username);
            cmd.Parameters.AddWithValue("@name", u.name);
            cmd.Parameters.AddWithValue("@bdate", u.bdate.ToString("u"));
            cmd.Parameters.AddWithValue("@email", u.email);
            cmd.Parameters.AddWithValue("@country", u.country);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting User to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }
        private void cancel_add_users_button_Click(object sender, RoutedEventArgs e)
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

