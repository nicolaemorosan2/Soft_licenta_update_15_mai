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
//using System.Windows.Forms;


namespace Soft_licenta_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void initializare_cashflowuri_van(object sender, RoutedEventArgs e)
        {
            int i;
            //if(perioada_van.Text is String)
            for (i = 1; i <= Int32.Parse(textbox_perioada_van.Text); i++)
            {
                //Textbox cashflow = new Textbox();
                //cashflow.Location = new Point(150,150)
                //List<TextBox> cashflow = new List<TextBox>();
                //cashflow.Location = new Point((i*2)*30, (i * 2) * 30);
            }

        }

        private void calculeaza_amort_liniara(object sender, RoutedEventArgs e)
        {
            amort_liniara_rezultat.Text = (float.Parse(suma_investita_amort_liniara.Text) / float.Parse(perioada_investita_amort_liniara.Text)).ToString("#0.00");
        }

        private void calculeaza_amort_degresiva(object sender, RoutedEventArgs e)
        {
            //int an;
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
            amort_degresiva_rezultat.Text = "";
            if (float.Parse(perioada_amort_degresiva.Text) < 2)
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
            else if (2 <= float.Parse(perioada_amort_degresiva.Text) && float.Parse(perioada_amort_degresiva.Text) < 5)
            {
                k = 1.5;
            }
            else if (5 <= float.Parse(perioada_amort_degresiva.Text) && float.Parse(perioada_amort_degresiva.Text) <= 10)
            {
                k = 2;
            }
            else
            {
                k = 2.5;
            }

            cota_amort = k / float.Parse(perioada_amort_degresiva.Text); //aici imi arunca exceptie
            suma_ramasa_amortizat = float.Parse(suma_amort_degresiva.Text);

            if (float.Parse(perioada_amort_degresiva.Text) % 2 == 0)
            {
                durata_artificiu_liniara = float.Parse(perioada_amort_degresiva.Text) / 2;
                //parcurgere_for();
                durata_artificiu_degresiva = float.Parse(perioada_amort_degresiva.Text) / 2;

                for (an = 1; an <= durata_artificiu_degresiva; an++)
                {
                    suma_amortizare_degresiva = suma_ramasa_amortizat * cota_amort;
                    //.suma_ramasa_amortizat = .suma_ramasa_amortizat - .suma_amortizare_degresiva;
                    suma_ramasa_amortizat = suma_ramasa_amortizat * (1 - cota_amort);
                    amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_degresiva, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }

                suma_amortizare_constanta = suma_ramasa_amortizat / durata_artificiu_liniara;

                for (an = durata_artificiu_degresiva + 1; an <= float.Parse(perioada_amort_degresiva.Text); an++)
                {
                    suma_ramasa_amortizat = suma_ramasa_amortizat - suma_amortizare_constanta;
                    amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_constanta, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }
            }
            else
            {
                durata_artificiu_liniara = Math.Truncate(float.Parse(perioada_amort_degresiva.Text) / 2) + 1;
                //parcurgere_for();
                durata_artificiu_degresiva = Math.Truncate(float.Parse(perioada_amort_degresiva.Text) / 2);

                for (an = 1; an <= durata_artificiu_degresiva; an++)
                {
                    suma_amortizare_degresiva = suma_ramasa_amortizat * cota_amort;
                    //.suma_ramasa_amortizat = .suma_ramasa_amortizat - .suma_amortizare_degresiva;
                    suma_ramasa_amortizat = suma_ramasa_amortizat * (1 - cota_amort);
                    amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_degresiva, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }

                suma_amortizare_constanta = suma_ramasa_amortizat / durata_artificiu_liniara;

                for (an = durata_artificiu_liniara; an <= float.Parse(perioada_amort_degresiva.Text); an++)
                {
                    suma_ramasa_amortizat = suma_ramasa_amortizat - suma_amortizare_constanta;
                    amort_degresiva_rezultat.Text += " " + Math.Round(an) + "  |  " + Math.Round(suma_amortizare_constanta, 2) + "  |  " + Math.Abs(Math.Round(suma_ramasa_amortizat, 2)) + "\n";
                }
            }
        }
        bool buton_direct_apasat = false;
        bool buton_defalcat_apasat = false;
        private void afiseaza_optiuni_roi_direct(object sender, RoutedEventArgs e)
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
        }
        private void calculeaza_roi(object sender, RoutedEventArgs e)
        {
            float castig_obt = float.Parse(castig_obtinut_roi.Text);
            float suma_inv = float.Parse(suma_investita_roi.Text);
            if (buton_direct_apasat == true)
            {
                roi_rezultat.Text = Math.Round(((castig_obt - suma_inv) / suma_inv * 100), 2).ToString() + "%";
            }
            else if (buton_defalcat_apasat == true)
            {
                //calcul cu costuri si cashflow-uri
            }

        }

        private void calc_dob_simpla(object sender, RoutedEventArgs e)
        {
            float sum_init = float.Parse(dob_simpla_textbox_suma_initiala.Text);
            float rata_dob = float.Parse(dob_simpla_textbox_rata_dob.Text);
            float perioada = float.Parse(dob_simpla_textbox_perioada.Text);
            if (perioada < 1)
                System.Windows.MessageBox.Show("Ati introdus o perioada imposibila! Reincercati!");
            else if (1 <= perioada && perioada <= 12)
            {
                dob_simpla_textbox_suma_finala.Text = Math.Round(sum_init * (1 + rata_dob / 100 * perioada / 12), 3).ToString();
                float sum_fin = float.Parse(dob_simpla_textbox_suma_finala.Text);
                dob_simpla_textbox_dobanda.Text = Math.Round((sum_fin - sum_init), 3).ToString();
            }
            else
                System.Windows.MessageBox.Show("Ati introdus o perioada > 12 luni, iar acest caz este acoperit la optiunea 'Dobanda Compusa'");
        }
        

    }
}
