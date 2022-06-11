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
    public partial class MainWindow : Window
    {
        public float row_id = 0, rezultat = 0;
        public int i = 0;
        List<TextBox> textbox_fluxuri_bani;
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Istoric_financiar_4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public MainWindow(bool is_admin, bool is_admin_partial)
        {
            InitializeComponent();
            this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            Incarca_Grid();
            groupbox_crud_label.IsEnabled = is_admin;
            groupbox_crud_textbox.IsEnabled = is_admin;
            groupbox_crud_button.IsEnabled = is_admin;
            tabitem_crud_conturi.IsEnabled = is_admin_partial;
        }
        
        //CRUD
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
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET ID_Informatie = '" + textbox_id.Text +"', Data = '" + textbox_data.Text + "', Venituri1 = '" + textbox_venituri.Text + "', Venituri2 = '" + textbox_venituri_provenite.Text + "', Cheltuieli = '" + textbox_cheltuieli.Text + "', Investitii = '" + textbox_investitie.Text + "', Descriere = '" + textbox_descriere.Text + "' WHERE ID_Informatie = '" + textbox_id.Text+"' ", con);
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

        //Functii extra CRUD
        public void Incarca_Grid()
        {
            //Datagrid date financiare
            
            SqlCommand comanda = new SqlCommand("SELECT * FROM dbo.Situatie_financiara", con);
            SqlCommand comanda1 = new SqlCommand("SELECT * FROM dbo.Utilizatori", con);
            DataTable tabel = new DataTable();
            DataTable tabel1 = new DataTable();
            con.Open();
            SqlDataReader reader = comanda.ExecuteReader();
            tabel.Load(reader);
            SqlDataReader reader1 = comanda1.ExecuteReader();
            tabel1.Load(reader1);
            con.Close();
            datagrid_date_financiare.ItemsSource = tabel.DefaultView;
            datagrid_utilizatori.ItemsSource = tabel1.DefaultView;
            
        }
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
        public void curata_date_utilizatori()
        {
            textbox_nume_utilizator.Clear();
            textbox_parola.Clear();
            textbox_tip_cont.Clear();
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

        //Operatii CRUD utilizatori
        private void Adauga_utilizator(object sender, RoutedEventArgs e)
        {
            SqlCommand comanda = new SqlCommand("INSERT INTO dbo.Utilizatori (nume_utilizator, parola, tip_cont) VALUES (@nume_utilizator, @parola, @tip_de_cont)", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@nume_utilizator", textbox_nume_utilizator.Text);
            comanda.Parameters.AddWithValue("@parola", textbox_parola.Text);
            comanda.Parameters.AddWithValue("@tip_de_cont", textbox_tip_cont.Text);
            con.Open();
            comanda.ExecuteNonQuery();
            Incarca_Grid();
            curata_date_utilizatori();
            MessageBox.Show("Cont adaugat!", "Inregistrare noua!", MessageBoxButton.OK, MessageBoxImage.Information);
            con.Close();
        }
        private void Editeaza_utilizator(object sender, RoutedEventArgs e)
        {
            
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Utilizatori SET nume_utilizator = '" + textbox_nume_utilizator.Text + "', parola = '" + textbox_parola.Text + "', tip_cont = '" + textbox_tip_cont.Text + "' WHERE nume_utilizator = '" + textbox_nume_utilizator.Text + "' ", con); 
            try
            {
                con.Open();
                comanda.ExecuteNonQuery();
                MessageBox.Show("Editare facuta cu succes!", "Editat!", MessageBoxButton.OK, MessageBoxImage.Information);
                curata_date_utilizatori();
                Incarca_Grid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sterge_utilizator(object sender, RoutedEventArgs e)
        {
            
            SqlCommand comanda = new SqlCommand("DELETE FROM dbo.Utilizatori WHERE nume_utilizator = " + textbox_nume_utilizator.Text + " ", con);
            try
            {
                con.Open();
                comanda.ExecuteNonQuery();
                MessageBox.Show("Inregistrarea a fost stearsa cu succes!", "Stergere efectuata", MessageBoxButton.OK, MessageBoxImage.Information);
                curata_date_utilizatori();
                Incarca_Grid();
                con.Close();
            }
            catch (SqlException ex)
            {
                // MessageBox.Show("Eroare! Stergerea nu a putut fi realizata!" + ex.Message);
            }
        }


        //Algoritmi si calcule
        private void calculeaza_profit(object sender, RoutedEventArgs e)
        {
            rezultat = float.Parse(textbox_profit_venituri_obtinute.Text) - float.Parse(textbox_profit_cheltuieli_obtinute.Text);
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET Profit = @rezultat WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@rezultat", rezultat);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            if(rezultat > 0)
                MessageBox.Show("Profit inregistrat!", "Camp calculat!", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (rezultat == 0)
                MessageBox.Show("Suntem pe 0 luna aceasta!", "Camp calculat!", MessageBoxButton.OK, MessageBoxImage.Information); 
            else
                MessageBox.Show("Am inregistrat pierderi :(", "Camp calculat!", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }
        private void calculeaza_amort_liniara(object sender, RoutedEventArgs e)
        {
            rezultat = float.Parse(textbox_suma_investita_amort_liniara.Text) / float.Parse(perioada_investita_amort_liniara.Text);
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET Amortizare_liniara = @rezultat WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@rezultat", rezultat);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            MessageBox.Show("Amortizarea liniara a fost inserata!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void calculeaza_amort_degresiva(object sender, RoutedEventArgs e)
        {
            double k = 1.5, cota_amort, suma_ramasa_amortizat, suma_amortizare_constanta, an,
                suma_amortizare_degresiva, durata_artificiu_liniara, durata_artificiu_degresiva;

            /*void parcugere_for()
            {
                durata_artificiu_degresiva = float.Parse(perioada_amort_degresiva.Text) / 2;

                for (an = 1; an <= durata_artificiu_degresiva; an++)
                {
                    suma_amortizare_degresiva = suma_ramasa_amortizat * cota_amort;
                    //.suma_ramasa_amortizat = .suma_ramasa_amortizat - .suma_amortizare_degresiva;
                    suma_ramasa_amortizat = suma_ramasa_amortizat * (1 - cota_amort);
                    Console.WriteLine(an + " | " + Math.Round(suma_amortizare_degresiva, 2) + " | " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n");
                }

                suma_amortizare_constanta = suma_ramasa_amortizat / durata_artificiu_liniara;

                for (an = durata_artificiu_degresiva + 1; an <= float.Parse(perioada_amort_degresiva.Text); an++)
                {
                    suma_ramasa_amortizat = suma_ramasa_amortizat - suma_amortizare_constanta;
                    Console.WriteLine(an + " | " + Math.Round(suma_amortizare_constanta, 2) + " | " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n");
                }
            }*/

            textbox_amort_degresiva_rezultat.Text = "";
            if (float.Parse(textbox_perioada_amort_degresiva.Text) < 2)
                System.Windows.MessageBox.Show("Ati introdus fie o perioada < 2 ani, fie o perioada eronata! Reincercati cu un numar valid!");
            /*{
                String mesaj = "Perioada trebuie sa fie > de 2 ani! Va rugam sa o reintroduceti";
                String titlu = "Alerta! Date eronate!";
                MessageBoxButton buton = MessageBoxButton.OK;
                DialogResult alerta = (DialogResult)System.Windows.MessageBox.Show(mesaj, titlu, buton);
                if (alerta == System.Windows.Forms.DialogResult.OK) //asa cred ca inseamna sa alegi o optiune, conform docs.microsoft
                {
                    perioada_amort_degresiva.Text = "";
                    //inchide message box-ul
                    this.Close();
                }
            } - aceasta optiune era foarte complicata si gresita - cel mai simplu cu un system.win.msgbox */
            /*SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET ROI = @rezultat WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@rezultat", rezultat);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            MessageBox.Show("Valoare inserata in tabel!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);*/
            else if (2 <= float.Parse(textbox_perioada_amort_degresiva.Text) && float.Parse(textbox_perioada_amort_degresiva.Text) < 5)
            {
                k = 1.5;
            }
            else if (5 <= float.Parse(textbox_perioada_amort_degresiva.Text) && float.Parse(textbox_perioada_amort_degresiva.Text) <= 10)
            {
                k = 2;
            }
            else
            {
                k = 2.5;
            }

            cota_amort = k / float.Parse(textbox_perioada_amort_degresiva.Text); //aici imi arunca exceptie
            suma_ramasa_amortizat = float.Parse(textbox_suma_amort_degresiva.Text);

            if (float.Parse(textbox_perioada_amort_degresiva.Text) % 2 == 0)
            {
                durata_artificiu_liniara = float.Parse(textbox_perioada_amort_degresiva.Text) / 2;
                //parcurgere_for();
                durata_artificiu_degresiva = float.Parse(textbox_perioada_amort_degresiva.Text) / 2;

                for (an = 1; an <= durata_artificiu_degresiva; an++)
                {
                    suma_amortizare_degresiva = suma_ramasa_amortizat * cota_amort;
                    //.suma_ramasa_amortizat = .suma_ramasa_amortizat - .suma_amortizare_degresiva;
                    suma_ramasa_amortizat = suma_ramasa_amortizat * (1 - cota_amort);
                    textbox_amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_degresiva, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }

                suma_amortizare_constanta = suma_ramasa_amortizat / durata_artificiu_liniara;

                for (an = durata_artificiu_degresiva + 1; an <= float.Parse(textbox_perioada_amort_degresiva.Text); an++)
                {
                    suma_ramasa_amortizat = suma_ramasa_amortizat - suma_amortizare_constanta;
                    textbox_amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_constanta, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }
            }
            else
            {
                durata_artificiu_liniara = Math.Truncate(float.Parse(textbox_perioada_amort_degresiva.Text) / 2) + 1;
                //parcurgere_for();
                durata_artificiu_degresiva = Math.Truncate(float.Parse(textbox_perioada_amort_degresiva.Text) / 2);

                for (an = 1; an <= durata_artificiu_degresiva; an++)
                {
                    suma_amortizare_degresiva = suma_ramasa_amortizat * cota_amort;
                    //.suma_ramasa_amortizat = .suma_ramasa_amortizat - .suma_amortizare_degresiva;
                    suma_ramasa_amortizat = suma_ramasa_amortizat * (1 - cota_amort);
                    textbox_amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_degresiva, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }

                suma_amortizare_constanta = suma_ramasa_amortizat / durata_artificiu_liniara;

                for (an = durata_artificiu_liniara; an <= float.Parse(textbox_perioada_amort_degresiva.Text); an++)
                {
                    suma_ramasa_amortizat = suma_ramasa_amortizat - suma_amortizare_constanta;
                    textbox_amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_constanta, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }
            }
        }
        /* private void afiseaza_optiuni_roi_direct(object sender, RoutedEventArgs e)
         {
             buton_direct_apasat = true;
             buton_defalcat_apasat = false;
             suma_investita_roi_label.Visibility = Visibility.Visible;
             suma_investita_roi.Visibility = Visibility.Visible;
             castig_obtinut_roi_label.Visibility = Visibility.Visible;
             castig_obtinut_roi.Visibility = Visibility.Visible;
             roi_rezultat_label.Visibility = Visibility.Visible;
             roi_rezultat.Visibility = Visibility.Visible;
         }

         private void afiseaza_optiuni_roi_defalcat(object sender, RoutedEventArgs e)
         {
             buton_defalcat_apasat = true;
             buton_direct_apasat = false;
         }*/
        private void calculeaza_roi(object sender, RoutedEventArgs e)
        {
            float castig_obt, suma_inv, temp;
            castig_obt = float.Parse(castig_obtinut_roi.Text);
            suma_inv = float.Parse(suma_investita_roi.Text);
            temp = (castig_obt - suma_inv) / suma_inv * 100;
            rezultat = (float)Math.Round(temp, 2);
            //Verificare valori exacte - temp si rezultat - pentru a vedea daca in coloana se petrece "truncherea"
            //MessageBox.Show("temp este " + temp + " iar rezultat este " + rezultat);

            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET ROI = @rezultat WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@rezultat", rezultat);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            MessageBox.Show("ROI este " + rezultat + "!\n" + "Valoare inserata in tabel!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }
        private void calc_dob_simpla(object sender, RoutedEventArgs e)
        {
            float suma_finala = 0, dobanda = 0;
            float sum_init = float.Parse(dob_simpla_textbox_suma_initiala.Text);
            float rata_dob = float.Parse(dob_simpla_textbox_rata_dob.Text);
            float perioada = float.Parse(dob_simpla_textbox_perioada.Text);
            if (perioada < 1)
                MessageBox.Show("Ati introdus o perioada eronata dpdv logic!", "Va rugam sa reincercati!", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (1 <= perioada && perioada <= 12)
            {
                suma_finala = (float)Math.Round(sum_init * (1 + rata_dob / 100 * perioada / 12), 3);
                dobanda = (float)Math.Round((suma_finala - sum_init), 3);
            }
            else
                MessageBox.Show("Ati introdus o perioada > 12 luni, iar acest caz este acoperit la optiunea 'Dobanda Compusa'", "Va rugam sa reincercati!", MessageBoxButton.OK, MessageBoxImage.Information);
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET Dobanda_simpla = @dobanda, Suma_finala_dobanda_simpla = @suma_finala WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@dobanda", dobanda);
            comanda.Parameters.AddWithValue("@suma_finala", suma_finala);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            MessageBox.Show("Dobanda simpla si suma finala au fost inserate!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void calc_dob_compusa(object sender, RoutedEventArgs e)
        {
            float suma_finala = 0, dobanda = 0, suma_init = 0, rata_dob = 0, perioada = 0;
            suma_init = float.Parse(dob_compusa_textbox_suma_initiala.Text);
            rata_dob = float.Parse(dob_compusa_textbox_rata_dob.Text);
            perioada = float.Parse(dob_compusa_textbox_perioada.Text);
            if (perioada < 1)
                MessageBox.Show("Ati introdus o perioada eronata dpdv logic!", "Va rugam sa reincercati!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                suma_finala = (float)Math.Round(suma_init * Math.Pow((1 + rata_dob / 100), perioada), 3);
                dobanda = (float)Math.Round((suma_finala - suma_init), 3);
            }
            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET Dobanda_compusa = @dobanda, Suma_finala_dobanda_compusa = @suma_finala WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@dobanda", dobanda);
            comanda.Parameters.AddWithValue("@suma_finala", suma_finala);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
            MessageBox.Show("Dobanda compusa si suma finala au fost inserate!", "Salvat!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void introduce_fluxuri_de_bani(object sender, RoutedEventArgs e)
        {
            textbox_fluxuri_bani = new List<TextBox>();
            //label_fluxuri_bani = new List<Label>();

            //Curatare textboxuri vechi
            foreach(Control item in stackpanel_fluxuri_bani.Children.OfType<TextBox>())
            {
                stackpanel_fluxuri_bani.Children.Clear();
                textbox_fluxuri_bani.Clear();
            }
            /*foreach(Control item in stackpanel_fluxuri_bani.Children.OfType<Label>())
            {
                stackpanel_fluxuri_bani.Children.Clear();
            }*/
            //Generare label-uri si textbox-uri in mod dinamic
            for (i = 0; i < int.Parse(textbox_rir_perioada.Text); i++)
            {
                //Label label = new Label();
                //label.Name = "label_fluxuri_bani_" + i.ToString();

                //textbox.Name = "textbox_fluxuri_bani_" + i.ToString();
                //textbox_fluxuri_bani.Add(textbox);
                //label_fluxuri_bani.Add(label);
                //stackpanel_fluxuri_bani.Children.Insert(i, label);
                TextBox textbox = new TextBox();
                stackpanel_fluxuri_bani.Children.Insert(i, textbox);
                textbox_fluxuri_bani.Add(textbox);
            }

        }
        private void calculeaza_rir(object sender, RoutedEventArgs e)
        {
            double r = 0.01, r_prim = 0.5, r1 = 0, r2 = 0, VAN1 = -1, VAN2 = 1, VAN_temp_RIR, RIR; 

            while (r < 1 && VAN2 > 0)
            {
                VAN_temp_RIR = 0;
                for (i = 0; i < int.Parse(textbox_rir_perioada.Text); i++)
                {
                    VAN_temp_RIR += float.Parse(textbox_fluxuri_bani[i].Text) / (Math.Pow((1 + r), (i+1)));
                }
                VAN2 = VAN_temp_RIR - float.Parse(textbox_rir_suma_investita.Text);
                r2 = r;
                r += 0.01;
            }
            //MessageBox.Show("Am calculat r, care este " + r);
            while (r_prim > 0 && VAN1 < 0)
            {
                VAN_temp_RIR = 0;
                for (i = 0; i < int.Parse(textbox_rir_perioada.Text); i++)
                {
                    VAN_temp_RIR += float.Parse(textbox_fluxuri_bani[i].Text) / (Math.Pow((1 + r_prim), (i+1)));
                }
                VAN1 = VAN_temp_RIR - float.Parse(textbox_rir_suma_investita.Text);
                r1 = r_prim;
                r_prim = r_prim - 0.01;
            }
            //MessageBox.Show("Am calculat r_prim, care este " + r_prim);
            RIR =  Math.Round(((r1 + ((r2 -  r1) *  VAN1) / (VAN1 + Math.Abs(VAN2))) * 100), 2);
            //Afisare RIR - vedem daca a calculat
            MessageBox.Show("RIR este " + RIR + "%");

            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET RIR = @RIR WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@RIR", RIR);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
        }

        private void introduce_fluxuri_de_bani_van(object sender, RoutedEventArgs e)
        {
            textbox_fluxuri_bani = new List<TextBox>();
            
            foreach (Control item in stackpanel_van_fluxuri_bani.Children.OfType<TextBox>())
            {
                stackpanel_van_fluxuri_bani.Children.Clear();
                textbox_fluxuri_bani.Clear();
            }
           
            for (i = 0; i < int.Parse(textbox_van_perioada.Text); i++)
            {
                TextBox textbox = new TextBox();
                stackpanel_van_fluxuri_bani.Children.Insert(i, textbox);
                textbox_fluxuri_bani.Add(textbox);
            }
        }

        private void calculeaza_van(object sender, RoutedEventArgs e)
        {
            float VAN_temp = 0, VAN = 0;
            for (i = 0; i < int.Parse(textbox_van_perioada.Text); i++)
            {
                VAN_temp = VAN_temp + float.Parse(textbox_fluxuri_bani[i].Text) / (float)(Math.Pow((1 + float.Parse(textbox_van_rata_randament.Text)), (i+1)));
            }
            VAN = VAN_temp - float.Parse(textbox_van_suma_investita.Text);
            MessageBox.Show("VAN este " + VAN);

            SqlCommand comanda = new SqlCommand("UPDATE dbo.Situatie_financiara SET VAN = @VAN WHERE ID_Informatie = @row_id", con);
            comanda.CommandType = CommandType.Text;
            comanda.Parameters.AddWithValue("@VAN", VAN);
            comanda.Parameters.AddWithValue("@row_id", row_id);
            con.Open();
            comanda.ExecuteNonQuery();
            con.Close();
            Incarca_Grid();
        }

        //Event handlere de la datagriduri (SelectionChanged) pentru a introduce datele in textboxuri
        private void Autofill_utilizatori(object sender, SelectionChangedEventArgs e)
        {
            TabItem optiuni = (TabItem)tabcontrol_optiuni.SelectedItem;
            DataGrid dg = (DataGrid)sender;
            DataRowView rand_selectat = dg.SelectedItem as DataRowView;
            if (rand_selectat != null)
            {
                textbox_nume_utilizator.Text = rand_selectat["nume_utilizator"].ToString();
                textbox_parola.Text = rand_selectat["parola"].ToString();
                textbox_tip_cont.Text = rand_selectat["tip_cont"].ToString();
            }
        }

        

        private void Introdu_date_in_textboxuri(object sender, SelectionChangedEventArgs e)
        {
            TabItem optiuni = (TabItem)tabcontrol_optiuni.SelectedItem;
            DataGrid dg = (DataGrid)sender;
            DataRowView rand_selectat = dg.SelectedItem as DataRowView;
            
          if(rand_selectat != null)
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
