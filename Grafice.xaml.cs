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

namespace Soft_licenta_2
{
    public partial class Grafice : Window
    {
        //Ca idee, formatul datei din tabela este luna/zi/an

        /*private MainWindow mainWindow;*/
        /*List<Grafice> graficeList;*/
        /*public MainWindow baza { get; private set; }*/
        
        /*int i = 0; //test while*/
        List<Grafice> pagini_grafice = new List<Grafice>(); //lista care contine paginile cu grafice nou-formate
        public Grafice( /*List<Grafice> niste_grafice, MainWindow baza*/)
        {
            InitializeComponent();
            pagini_grafice.Add(this);
            /*this.a = stackpanel_toggle;*/
            
            /*this.baza = baza;*/
            /*this.graficeList = niste_grafice;*/
        }

        /*public Grafice(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }*/

        private void refresh_grafice(object sender, RoutedEventArgs e)
        {
            if (pagini_grafice.Count == 1)
            {
                /*pagini[0].Close();*/
                pagini_grafice[0].Close();
                pagini_grafice.Clear();
            }
            Grafice grafice = new Grafice();
            pagini_grafice.Add(grafice);
            /*MessageBox.Show("Avem " + pagini.Count + " elem");*/
            pagini_grafice[0].Show();


            /*while ((bool)checkbox_venituri.IsChecked)
                col_venituri.Visibility = Visibility.Visible;*/

            /*do
            {
                col_venituri.Visibility = (bool)checkbox_venituri.IsChecked ? Visibility.Visible : Visibility.Hidden;
                col_venituri_inv.Visibility = (bool)checkbox_venituri_inv.IsChecked ? Visibility.Visible : Visibility.Hidden;
                col_cheltuieli.Visibility = (bool)checkbox_cheltuieli.IsChecked ? Visibility.Visible : Visibility.Hidden;
                col_investitii.Visibility = (bool)checkbox_investitii.IsChecked ? Visibility.Visible : Visibility.Hidden;
            }
            while (i < 2);*/

            /*if (this.graficeList.Count == 1)
            {
                this.graficeList[0].Close();
                this.graficeList.Clear();
            }
            Grafice grafice = new Grafice(this.graficeList);
            this.graficeList.Add(grafice);
            MessageBox.Show("Avem " + this.graficeList.Count + " elem");
            this.graficeList[0].Show();*/
        }

        private void inchide_fereastra(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
