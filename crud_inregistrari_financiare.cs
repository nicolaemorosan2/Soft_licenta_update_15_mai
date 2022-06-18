using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.Sql;

namespace Soft_licenta_2
{
    //Crud date registru contabil
    public partial class MainWindow: Window
    {
        public void curata_date()
        {
            textbox_id.Clear();
            textbox_data.Clear();
            textbox_venituri.Clear();
            textbox_venituri_provenite.Clear();
            textbox_cheltuieli.Clear();
            textbox_investitie.Clear();
            textbox_descriere.Clear();
        }
        public bool este_valid()
        {
            if (textbox_id.Text == String.Empty /*|| textbox_id.Text == */)
            {
                MessageBox.Show("Ai uitat sa introduci ID-ul!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_data.Text == String.Empty)
            {
                MessageBox.Show("Ai uitat sa introduci Data!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_venituri.Text == String.Empty)
            {
                MessageBox.Show("Ai uitat sa introduci Veniturile!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_venituri_provenite.Text == String.Empty)
            {
                MessageBox.Show("Ai uitat sa introduci Veniturile provenite din investitii!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_cheltuieli.Text == String.Empty)
            {
                MessageBox.Show("Ai uitat sa introduci Cheltuielile!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_investitie.Text == String.Empty)
            {
                MessageBox.Show("Ai uitat sa introduci Investitiile!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_descriere.Text == String.Empty)
            {
                MessageBox.Show("Ai uitat sa introduci Descrierea investitiilor!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void Refresh_inreg(object sender, RoutedEventArgs e)
        {
            curata_date();
        }
        private void Adauga_inreg(object sender, RoutedEventArgs e)
        {

            try
            {
                if (este_valid())
                {
                    SqlCommand comanda = new SqlCommand("INSERT INTO dbo.Situatie_financiara (ID_informatie, Data, Venituri1, Venituri2, Cheltuieli, Investitii, Descriere) VALUES (@ID_informatie, @Data, @Venituri1, @Venituri2, @Cheltuieli, @Investitii, @Descriere)", con);
                    comanda.CommandType = CommandType.Text;
                    comanda.Parameters.AddWithValue("@ID_informatie", textbox_id.Text);
                    comanda.Parameters.AddWithValue("@Data", textbox_data.Text);
                    comanda.Parameters.AddWithValue("@Venituri1", textbox_venituri.Text);
                    comanda.Parameters.AddWithValue("@Venituri2", textbox_venituri_provenite.Text);
                    comanda.Parameters.AddWithValue("@Cheltuieli", textbox_cheltuieli.Text);
                    comanda.Parameters.AddWithValue("@Investitii", textbox_investitie.Text);
                    comanda.Parameters.AddWithValue("@Descriere", textbox_descriere.Text);
                    con.Open();
                    comanda.ExecuteNonQuery();
                    con.Close();
                    Incarca_Grid();
                    MessageBox.Show("Adaugarea a reusit!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);
                    curata_date();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Editeaza_inreg(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET ID_Informatie = '" + textbox_id.Text + "', Data = '" + textbox_data.Text + "', Venituri1 = '" + textbox_venituri.Text + "', Venituri2 = '" + textbox_venituri_provenite.Text + "', Cheltuieli = '" + textbox_cheltuieli.Text + "', Investitii = '" + textbox_investitie.Text + "', Descriere = '" + textbox_descriere.Text + "' WHERE ID_Informatie = '" + textbox_id.Text + "' ", con);
            try
            {
                comanda.ExecuteNonQuery();
                MessageBox.Show("Editare facuta cu succes!", "Editat!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                curata_date();
                Incarca_Grid();
                con.Close();
            }
        }
        private void Sterge_inreg(object sender, RoutedEventArgs e)
        {

            SqlCommand comanda = new SqlCommand("DELETE FROM dbo.Situatie_financiara WHERE ID_informatie = " + textbox_id.Text + " ", con);
            try
            {
                con.Open();
                comanda.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inregistrarea a fost stearsa cu succes!", "Stergere efectuata", MessageBoxButton.OK, MessageBoxImage.Information);
                curata_date();
                Incarca_Grid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Eroare! Stergerea nu a putut fi realizata!" + ex.Message);
            }
        }
    }
}
