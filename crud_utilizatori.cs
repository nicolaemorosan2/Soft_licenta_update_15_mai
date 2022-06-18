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
using LiveCharts;
using LiveCharts.Wpf;

namespace Soft_licenta_2
{
    //Crud useri
    public partial class MainWindow: Window
    {
        //Operatii CRUD utilizatori
        public void curata_date_utilizatori()
        {
            textbox_id_utilizator.Clear();
            textbox_nume_utilizator.Clear();
            textbox_parola.Clear();
            textbox_tip_cont.Clear();
        }
        private void Autofill_utilizatori(object sender, SelectionChangedEventArgs e)
        {
            TabItem optiuni = (TabItem)tabcontrol_optiuni.SelectedItem;
            DataGrid dg = (DataGrid)sender;
            DataRowView rand_selectat = dg.SelectedItem as DataRowView;
            if (rand_selectat != null)
            {
                textbox_id_utilizator.Text = rand_selectat["Id_utilizator"].ToString();
                textbox_nume_utilizator.Text = rand_selectat["nume_utilizator"].ToString();
                textbox_parola.Text = rand_selectat["parola"].ToString();
                textbox_tip_cont.Text = rand_selectat["tip_cont"].ToString();
            }
        }
        private void Adauga_utilizator(object sender, RoutedEventArgs e)
        {
            SqlCommand comanda = new SqlCommand("INSERT INTO Utilizatori (nume_utilizator, parola, tip_cont) VALUES (@nume_utilizator, @parola, @tip_cont)", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@nume_utilizator", textbox_nume_utilizator.Text);
            comanda.Parameters.AddWithValue("@parola", textbox_parola.Text);
            comanda.Parameters.AddWithValue("@tip_cont", textbox_tip_cont.Text);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            curata_date_utilizatori();
            MessageBox.Show("Cont adaugat!", "Inregistrare noua!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Editeaza_utilizator(object sender, RoutedEventArgs e)
        {
            SqlCommand comanda = new SqlCommand("UPDATE Utilizatori SET nume_utilizator = '" + textbox_nume_utilizator.Text + "', parola = '" + textbox_parola.Text + "', tip_cont = '" + textbox_tip_cont.Text + "' WHERE Id_utilizator = '" + textbox_id_utilizator.Text + "' ", con);
            try
            {
                con.Open();
                comanda.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Editare facuta cu succes!", "Editat!", MessageBoxButton.OK, MessageBoxImage.Information);
                curata_date_utilizatori();
                Incarca_Grid();
                /*con.Close();*/
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Sterge_utilizator(object sender, RoutedEventArgs e)
        {
            SqlCommand comanda = new SqlCommand("DELETE FROM Utilizatori WHERE Id_utilizator = " + textbox_id_utilizator.Text + " ", con);
            try
            {
                con.Open();
                comanda.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Inregistrarea a fost stearsa cu succes!", "Stergere efectuata", MessageBoxButton.OK, MessageBoxImage.Information);
                curata_date_utilizatori();
                Incarca_Grid();
                /*con.Close();*/
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Eroare! Stergerea nu a putut fi realizata!" + ex.Message);
            }
        }
    }
}
