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
    /// Interaction logic for AddStudios.xaml
    /// </summary>
    public partial class AddStudios : Page
    {
        SqlConnection cnn;
        public AddStudios()
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

            string getLocationsQuery = "SELECT * FROM movies.udf_UniqueLocations()";

            complete_list_box(getLocationsQuery, locations);
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

        private void add_studio_button_Click(object sender, RoutedEventArgs e)
        {
            string insertStudio = "movies.sp_AddStudio";
            int studio_id;
            string studio_name;
            char[] d = { ':', '-', '/' };
            try
            {
                studio_id = Convert.ToInt32(id.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Argument ID takes integers only");
                return;
            }

            if (name.Text.Length == 0)
            {
                MessageBox.Show("Name Field is required");
                return;
            }
            else
                studio_name = name.Text;

            DataTable studio_locations = new DataTable();
            studio_locations.Clear();
            studio_locations.Columns.Add("location");
            foreach (CheckBox s in locations.Items)
            {
                if (s.IsChecked.HasValue && s.IsChecked.Value)
                {
                    DataRow row = studio_locations.NewRow();
                    row["location"] = s.Content.ToString();
                    studio_locations.Rows.Add(row);
                }
            }

            SqlCommand insertQuery = new SqlCommand(insertStudio, cnn);
            insertQuery.CommandType = CommandType.StoredProcedure;

            insertQuery.Parameters.AddWithValue("@id", studio_id);
            insertQuery.Parameters.AddWithValue("@name", studio_name);
            insertQuery.Parameters.AddWithValue("@locations", studio_locations);

            try
            {
                insertQuery.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Error on inserting Studio to database");
                return;
            }

            cnn.Close();
            AddPage add = new AddPage();
            this.NavigationService.Navigate(add);
        }

        private void cancel_add_studio_button_Click(object sender, RoutedEventArgs e)
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

        private void add_location_button_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.Content = add_new_location.Text;
            add_new_location.Text = "";

            checkbox.IsChecked = true;

            locations.Items.Add(checkbox);
        }
    }
}
