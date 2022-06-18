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
        public float row_id = 0, rezultat = 0;
        public int i = 0;
        List<TextBox> textbox_fluxuri_bani;
        List<Window> pagini = new List<Window>();
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Istoric_financiar_4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        Grafice grafice = new Grafice(); //inteligent - practic instantiez obiectul nou de tipul Grafice (fiind clasa)

    }
}
