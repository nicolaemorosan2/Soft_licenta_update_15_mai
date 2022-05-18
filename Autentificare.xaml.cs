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
using System.Windows.Shapes;

namespace Soft_licenta_2
{
    /// <summary>
    /// Interaction logic for Autentificare.xaml
    /// </summary>
        
public partial class Autentificare : Window
    {
        public bool verificare_utilizator = false;
        public Autentificare()
        {
            InitializeComponent();
        }
        

        private void button_Autentificare_verifica_credentiale(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Utilizatori;Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(1) FROM Utilizatori WHERE Username=@Username AND Parola=@Parola", sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", textbox_Username.Text);
                sqlCmd.Parameters.AddWithValue("@Parola", textbox_Parola.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    if (textbox_Username.Text == "admin")
                        verificare_utilizator = true;
                    else
                        verificare_utilizator = false;

                    MainWindow aplicatie = new MainWindow(verificare_utilizator);
                    aplicatie.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
