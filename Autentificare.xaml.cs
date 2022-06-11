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
        public bool verificare_utilizator_crud_date = false, verificare_utilizator_crud_conturi = false;
        public Autentificare()
        {
            InitializeComponent();
        }
        

        private void button_Autentificare_verifica_credentiale(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Istoric_financiar_4;Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(1) FROM [dbo].[Utilizatori] WHERE nume_utilizator=@nume_utilizator AND parola=@parola", sqlCon);
                //SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(1) FROM [Utilizatori] WHERE '"StrComp("'[Username]'","'@Username'",CompareMethod.Text) = 0 AND StrComp([Parola],@Parola,CompareMethod.Text) = 0", sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@nume_utilizator", textbox_Username.Text);
                sqlCmd.Parameters.AddWithValue("@parola", textbox_Parola.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    if (textbox_Username.Text == "admin") { 
                        verificare_utilizator_crud_date = true; //pt crud date
                        verificare_utilizator_crud_conturi = true; //pt crud utilizatori
                    }
                    else if (textbox_Username.Text == "nicolae")
                    {
                        verificare_utilizator_crud_date = true; //doar crud date
                    }

                    MainWindow aplicatie = new MainWindow(verificare_utilizator_crud_date, verificare_utilizator_crud_conturi);
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
