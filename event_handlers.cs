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
    public partial class MainWindow: Window
    {
        //Event handlere de la datagriduri (SelectionChanged) pentru a introduce datele in textboxuri
        private void afiseaza_grafice(object sender, RoutedEventArgs e)
        {
            if (pagini.Count == 1)
            {
                pagini[0].Close();
                pagini.Clear();
            }
            Grafice grafice2 = new Grafice();
            pagini.Add(grafice2);
            pagini[0].Show();
        }
        private void Introdu_date_in_textboxuri(object sender, SelectionChangedEventArgs e)
        {
            TabItem optiuni = (TabItem)tabcontrol_optiuni.SelectedItem;
            DataGrid dg = (DataGrid)sender;
            DataRowView rand_selectat = dg.SelectedItem as DataRowView;

            if (rand_selectat != null)
            {
                textbox_id.Text = rand_selectat["ID_Informatie"].ToString();
                textbox_data.Text = rand_selectat["Data"].ToString();
                textbox_venituri.Text = rand_selectat["Venituri1"].ToString();
                textbox_venituri_provenite.Text = rand_selectat["Venituri2"].ToString();
                textbox_cheltuieli.Text = rand_selectat["Cheltuieli"].ToString();
                textbox_investitie.Text = rand_selectat["Investitii"].ToString();
                textbox_descriere.Text = rand_selectat["Descriere"].ToString();

                if (optiuni == tabitem_profituri)
                {
                    textbox_profit_venituri_obtinute.Text = rand_selectat["Venituri1"].ToString();
                    textbox_profit_cheltuieli_obtinute.Text = rand_selectat["Cheltuieli"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_amortizare_liniara)
                {
                    textbox_suma_investita_amort_liniara.Text = rand_selectat["Investitii"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_amortizare_degresiva)
                {
                    textbox_suma_amort_degresiva.Text = rand_selectat["Investitii"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_van)
                {
                    textbox_van_suma_investita.Text = rand_selectat["Investitii"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_rir)
                {
                    textbox_rir_suma_investita.Text = rand_selectat["Investitii"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_roi)
                {
                    suma_investita_roi.Text = rand_selectat["Investitii"].ToString();
                    castig_obtinut_roi.Text = rand_selectat["Venituri2"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_dobanda_simpla)
                {
                    dob_simpla_textbox_suma_initiala.Text = rand_selectat["Investitii"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
                else if (optiuni == tabitem_dobanda_compusa)
                {
                    dob_compusa_textbox_suma_initiala.Text = rand_selectat["Investitii"].ToString();
                    row_id = float.Parse(rand_selectat["ID_Informatie"].ToString());
                }
            }

        }
    }
}
