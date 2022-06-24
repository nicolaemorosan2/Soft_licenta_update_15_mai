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
    //OBSERVATIE: la autoincrement in baza de date nu face increment de la ultimul prezent in baza de date, ceea ce nu e ok deloc si apar probleme masive, in sensul
    //in care nu mai poate face join ca are id-uri false si nereale
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
            /*if (textbox_id.Text == String.Empty *//*|| textbox_id.Text == *//*)
            {
                MessageBox.Show("Ai uitat sa introduci ID-ul!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }*/
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
        /*private void Refresh_inreg(object sender, RoutedEventArgs e)
        {
            curata_date();
        }*/
        private void Adauga_inreg(object sender, RoutedEventArgs e)
        {
            try
            {
                if (este_valid())
                {
                    con.Open();
                    SqlCommand comanda1 = new SqlCommand("INSERT INTO dbo.Situatie_financiara_1 (Id_informatie, Data, Venituri1, Cheltuieli) VALUES (@Id_informatie, @Data, @Venituri1, @Cheltuieli)", con);
                    SqlCommand comanda2 = new SqlCommand("INSERT INTO dbo.Indicatori (Id_indicatori, Investitii, Descriere) VALUES (@Id_indicatori, @Investitii, @Descriere)", con);
                    SqlCommand comanda3 = new SqlCommand("INSERT INTO dbo.Indicatori_roi (Id_roi, Venituri2) VALUES (@Id_roi, @Venituri2)", con);
                    SqlCommand comanda4 = new SqlCommand("INSERT INTO dbo.Indicatori_dob_simpla (Id_dob_simpla, Investitii) VALUES (@Id_dob_simpla, @Investitii)", con);
                    SqlCommand comanda5 = new SqlCommand("INSERT INTO dbo.Indicatori_dob_compusa (Id_dob_compusa, Investitii) VALUES (@Id_dob_compusa, @Investitii)", con);

                    comanda1.CommandType = CommandType.Text;
                    comanda2.CommandType = CommandType.Text;
                    comanda3.CommandType = CommandType.Text;
                    comanda4.CommandType = CommandType.Text;
                    comanda5.CommandType = CommandType.Text;

                    comanda1.Parameters.AddWithValue("@Id_informatie", textbox_id.Text); 
                    comanda1.Parameters.AddWithValue("@Data", textbox_data.Text);
                    comanda1.Parameters.AddWithValue("@Venituri1", textbox_venituri.Text);
                    comanda1.Parameters.AddWithValue("@Cheltuieli", textbox_cheltuieli.Text);

                    comanda2.Parameters.AddWithValue("@Id_indicatori", textbox_id.Text);
                    comanda2.Parameters.AddWithValue("@Investitii", textbox_investitie.Text); 
                    comanda2.Parameters.AddWithValue("@Descriere", textbox_descriere.Text);

                    comanda3.Parameters.AddWithValue("@Id_roi", textbox_id.Text);
                    comanda3.Parameters.AddWithValue("@Venituri2", textbox_venituri_provenite.Text);

                    comanda4.Parameters.AddWithValue("@Id_dob_simpla", textbox_id.Text);
                    comanda4.Parameters.AddWithValue("@Investitii", textbox_investitie.Text);

                    comanda5.Parameters.AddWithValue("@Id_dob_compusa", textbox_id.Text);
                    comanda5.Parameters.AddWithValue("@Investitii", textbox_investitie.Text);

                    comanda1.ExecuteNonQuery();
                    comanda2.ExecuteNonQuery();
                    comanda3.ExecuteNonQuery();
                    comanda4.ExecuteNonQuery();
                    comanda5.ExecuteNonQuery();
                    con.Close();
                    Incarca_Grid();
                    curata_date();
                    MessageBox.Show("Adaugarea a reusit!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);
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
            SqlCommand comanda1 = new SqlCommand("UPDATE dbo.Situatie_financiara_1 SET ID_Informatie = '" + textbox_id.Text + "', Data = '" + textbox_data.Text + "', Venituri1 = '" + textbox_venituri.Text + "', Cheltuieli = '" + textbox_cheltuieli.Text + "' WHERE ID_informatie = '" + textbox_id.Text + "' ", con);
            SqlCommand comanda2 = new SqlCommand("UPDATE dbo.Indicatori SET Id_indicatori = '" + textbox_id.Text + "', Investitii = '" + textbox_investitie.Text + "', Descriere = '" + textbox_descriere.Text + "' WHERE ID_indicatori = '" + textbox_id.Text + "' ", con);
            SqlCommand comanda3 = new SqlCommand("UPDATE dbo.Indicatori_roi SET Id_roi = '" + textbox_id.Text + "', Venituri2 = '" + textbox_venituri_provenite.Text + "' WHERE ID_roi = '" + textbox_id.Text + "' ", con);
            SqlCommand comanda4 = new SqlCommand("UPDATE dbo.Indicatori_dob_simpla SET Id_dob_simpla = '" + textbox_id.Text + "', Investitii = '" + textbox_investitie.Text + "' WHERE Id_dob_simpla = '" + textbox_id.Text + "' ", con);
            SqlCommand comanda5 = new SqlCommand("UPDATE dbo.Indicatori_dob_compusa SET Id_dob_compusa = '" + textbox_id.Text + "', Investitii = '" + textbox_investitie.Text + "' WHERE Id_dob_compusa = '" + textbox_id.Text + "' ", con);
            try
            {
                comanda1.ExecuteNonQuery();
                comanda2.ExecuteNonQuery();
                comanda3.ExecuteNonQuery();
                comanda4.ExecuteNonQuery();
                comanda5.ExecuteNonQuery();
                MessageBox.Show("Editare facuta cu succes!", "Editat!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                curata_date();
                Incarca_Grid();
            }
        }
        private void Sterge_inreg(object sender, RoutedEventArgs e)
        {
            SqlCommand comanda1 = new SqlCommand("DELETE FROM dbo.Situatie_financiara_1 WHERE ID_informatie = " + textbox_id.Text + " ", con);
            SqlCommand comanda2 = new SqlCommand("DELETE FROM dbo.Indicatori WHERE ID_indicatori = " + textbox_id.Text + " ", con);
            SqlCommand comanda3 = new SqlCommand("DELETE FROM dbo.Indicatori_roi WHERE ID_roi = " + textbox_id.Text + " ", con);
            SqlCommand comanda4 = new SqlCommand("DELETE FROM dbo.Indicatori_dob_simpla WHERE ID_dob_simpla = " + textbox_id.Text + " ", con);
            SqlCommand comanda5 = new SqlCommand("DELETE FROM dbo.Indicatori_dob_compusa WHERE ID_dob_compusa = " + textbox_id.Text + " ", con);
            try
            {
                con.Open();
                comanda1.ExecuteNonQuery();
                comanda2.ExecuteNonQuery();
                comanda3.ExecuteNonQuery();
                comanda4.ExecuteNonQuery();
                comanda5.ExecuteNonQuery();
                con.Close();
                curata_date();
                Incarca_Grid();
                MessageBox.Show("Inregistrarea a fost stearsa cu succes!", "Stergere efectuata", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Eroare! Stergerea nu a putut fi realizata!" + ex.Message);
            }
        }
    }
}
