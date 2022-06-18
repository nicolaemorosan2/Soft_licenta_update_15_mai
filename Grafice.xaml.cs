using System;
using System.Collections.Generic;
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
    public partial class Grafice : Window
    {
        List<Window> pagini = new List<Window>();
        public Grafice()
        {
            InitializeComponent();
        }
        private void refresh_grafice(object sender, RoutedEventArgs e)
        {
            if (pagini.Count == 1)
            {
                pagini[0].Close();
                pagini.Clear();
            }
            Grafice grafice = new Grafice();
            pagini.Add(grafice);
            MessageBox.Show("Avem " + pagini.Count + " elem");
            pagini[0].Show();
        }

        private void inchide_fereastra(object sender, RoutedEventArgs e)
        {

        }
    }
}
