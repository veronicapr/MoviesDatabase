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
    /// Interaction logic for AddActors.xaml
    /// </summary>
    public partial class AddActors : Page
    {
        SqlConnection cnn;

        public AddActors()
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

        }

        private void cancel_add_actor_button_Click(object sender, RoutedEventArgs e)
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

        private void add_actor_button_Click(object sender, RoutedEventArgs e)
        {
            string insertActor = "movies.sp_AddActor";
            Actor a = new Actor();
            char[] d = { ':', '-', '/'};
            try
            {
                a.ssn = Convert.ToInt32(ssn.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument SSN takes integers only");
                return;
            }

            try
            {
                a.rank = Convert.ToInt32(rank.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument Rank takes integers only");
                return;
            }

            if (name.Text.Length == 0)
            {
                MessageBox.Show("Name Field is required");
                return;
            }
            else
                a.name = name.Text;

            if (bio.Text.Length == 0)
            {
                MessageBox.Show("Bio Field is required");
                return;
            }
            else
                a.bio = bio.Text;

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

                a.Bdate = new DateTime(year, month, day);
            }

            SqlCommand insertQuery = new SqlCommand(insertActor, cnn);
            insertQuery.CommandType = CommandType.StoredProcedure;

            insertQuery.Parameters.AddWithValue("@ssn", a.ssn);
            insertQuery.Parameters.AddWithValue("@name", a.name);
            insertQuery.Parameters.AddWithValue("@bio", a.bio);
            insertQuery.Parameters.AddWithValue("@rank", a.rank);
            insertQuery.Parameters.AddWithValue("@bdate", a.Bdate.ToString("u"));

            try
            {
                insertQuery.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting Actor to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }
    }
}
