using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
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
    public class Model_date
    {
        int numar, data;
        float venituri, venituri_inv, chelt, inv;
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Istoric_financiar_4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        
        public List<Date_financiare> Lista_Date { get; set; }

        public Model_date()
        {
            Lista_Date = new List<Date_financiare>();
            /*Lista_Date = new List<Date_financiare>()
            {
               new Date_financiare { Data = 202200, Venituri = 1234, Venituri_din_investitii = 123, Cheltuieli = 123, Investitii = 123},
               new Date_financiare { Data = 202215, Venituri = 1000, Venituri_din_investitii = 100, Cheltuieli = 900, Investitii = 10 }
            };*/

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Situatie_financiara]", con);
            cmd.CommandType = CommandType.Text;
            numar = Convert.ToInt32(cmd.ExecuteScalar());
            
            //MessageBox.Show(numar.ToString()); - imi detecteaza bine cate inregistrari am

            for (int i = 2; i < numar+2; i++)
                {
                
                SqlCommand cmd_data = new SqlCommand("SELECT Data FROM [dbo].[Situatie_financiara] WHERE ID_informatie = '" + i + "' ", con);
                SqlCommand cmd_venituri = new SqlCommand("SELECT Venituri1 FROM [dbo].[Situatie_financiara] WHERE ID_informatie = '" + i + "' ", con);
                SqlCommand cmd_venituri_inv = new SqlCommand("SELECT Venituri2 FROM [dbo].[Situatie_financiara] WHERE ID_informatie = '" + i + "' ", con);
                SqlCommand cmd_cheltuieli = new SqlCommand("SELECT Cheltuieli FROM [dbo].[Situatie_financiara] WHERE ID_informatie = '" + i + "' ", con);
                SqlCommand cmd_investitii = new SqlCommand("SELECT Investitii FROM [dbo].[Situatie_financiara] WHERE ID_informatie = '" + i + "' ", con);

                cmd_data.CommandType = CommandType.Text;
                cmd_venituri.CommandType = CommandType.Text;
                cmd_venituri_inv.CommandType = CommandType.Text;
                cmd_cheltuieli.CommandType = CommandType.Text;
                cmd_investitii.CommandType = CommandType.Text;

                this.data = Int32.Parse(cmd_data.ExecuteScalar().ToString()); //Versiune demo de la grafice
                this.venituri = float.Parse(cmd_venituri.ExecuteScalar().ToString());
                this.venituri_inv = float.Parse(cmd_venituri_inv.ExecuteScalar().ToString());
                this.chelt = float.Parse(cmd_cheltuieli.ExecuteScalar().ToString());
                this.inv = float.Parse(cmd_cheltuieli.ExecuteScalar().ToString());

                Lista_Date.Add(new Date_financiare{
                    Data = this.data,
                    Venituri = this.venituri,
                    Venituri_din_investitii = this.venituri_inv,
                    Cheltuieli = this.chelt,
                    Investitii = this.inv
                                            }
                );
                //MessageBox.Show(Lista_Date[i-2].Venituri.ToString()); //imi preia bine datele si lista e ok
            }
            con.Close();
        }
    }
}
