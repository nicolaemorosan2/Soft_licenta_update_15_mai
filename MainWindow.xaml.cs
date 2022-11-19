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
    /*public interface IPagini { 
        List<Window> pagini { get; set; }
        public void refresh_grafice() { MessageBox.Show("Ok"); }
    }*/
    
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Istoric_financiar_2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public MainWindow(bool is_admin, bool is_admin_partial)
        {
            InitializeComponent();
            this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight; //ma lasa sa pun proprietatea cu min/max width/height la window
            Incarca_Grid();
            groupbox_crud_label.IsEnabled = is_admin;
            groupbox_crud_textbox.IsEnabled = is_admin;
            groupbox_crud_button.IsEnabled = is_admin;
            tabitem_crud_conturi.IsEnabled = is_admin_partial;
        }
        //Import date din BD
        public void Incarca_Grid()
        {
            //Datagrid date financiare
            SqlCommand comanda = new SqlCommand("SELECT * FROM dbo.Situatie_financiara_1 LEFT JOIN dbo.Indicatori ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori.Id_indicatori " +
                "LEFT JOIN dbo.Indicatori_roi ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori_roi.Id_roi " +
                "LEFT JOIN dbo.Indicatori_dob_simpla ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori_dob_simpla.Id_dob_simpla " +
                "LEFT JOIN dbo.Indicatori_dob_compusa ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori_dob_compusa.Id_dob_compusa", con);

            /*SqlCommand comanda_1 = new SqlCommand("SELECT * FROM dbo.Situatie_financiara LEFT JOIN dbo.Indicatori ON ", con);
            SqlCommand comanda_2 = new SqlCommand("SELECT * FROM dbo.Situatie_financiara LEFT JOIN dbo.Indicatori ON ", con);
            SqlCommand comanda_3 = new SqlCommand("SELECT * FROM dbo.Situatie_financiara LEFT JOIN dbo.Indicatori ON ", con);*/

            SqlCommand comanda1 = new SqlCommand("SELECT * FROM dbo.Utilizatori", con);
            DataTable tabel = new DataTable();
            DataTable tabel1 = new DataTable();
            con.Open();
            SqlDataReader reader = comanda.ExecuteReader();
            tabel.Load(reader);
            SqlDataReader reader1 = comanda1.ExecuteReader();
            tabel1.Load(reader1);
            /*con.Close();*/
            datagrid_date_financiare.ItemsSource = tabel.DefaultView;
            datagrid_utilizatori.ItemsSource = tabel1.DefaultView;
            con.Close();
        }

        private void Refresh_campuri(object sender, RoutedEventArgs e)
        {
            curata_date();
        }
    }
}