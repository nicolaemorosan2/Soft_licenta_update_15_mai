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
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Python.Runtime;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using IronPython;
using IronPython.Runtime;
using IronPython.Hosting;
using IronPython.Compiler.Ast;
using static IronPython.Modules._ast;
using Path = System.IO.Path;
using static Community.CsharpSqlite.Sqlite3;
using MessageBox = System.Windows.Forms.MessageBox;
using ExcelDataReader;

namespace Soft_licenta_2
{
    /*public interface IPagini { 
        List<Window> pagini { get; set; }
        public void refresh_grafice() { MessageBox.Show("Ok"); }
    }*/

    public partial class MainWindow : Window
    {                                                     //Changed server, database
        SqlConnection con = new SqlConnection(@"Data Source=NICOLAE;Initial Catalog=Istoric_financiar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlConnection con_clienti = new SqlConnection(@"Data Source=NICOLAE;Initial Catalog=Clienti;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlConnection con_users = new SqlConnection(@"Data Source=NICOLAE;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public MainWindow(bool is_admin, bool is_admin_partial)
        {
            InitializeComponent();
            this.SizeToContent = System.Windows.SizeToContent.WidthAndHeight; //ma lasa sa pun proprietatea cu min/max width/height la window
            Incarca_Grid();
            Incarca_Grid_clienti();
            groupbox_crud_label.IsEnabled = is_admin;
            groupbox_crud_textbox.IsEnabled = is_admin;
            groupbox_crud_button.IsEnabled = is_admin;
            tabitem_crud_conturi.IsEnabled = is_admin_partial;
            add_items_combobox_sort_by();
            //Ca idee, cu Python voi face asa - un script care a umbla doar la baza de date, iar gridul asta ar fi fain sa 
            //faca in timp real update la tabel si asa

            //id, gender, reason applying, parent edu, residency, 
            //Cambridge, child_satisf, time in our company,
            //writing, reading&listening, speaking

        }

        public void add_items_combobox_sort_by()
        {
            //This is where we add every category we want to sort the database by
            combobox_sort_by.Items.Add("Gender");
            combobox_sort_by.Items.Add("Age");
            combobox_sort_by.Items.Add("Channel used for applying");
            combobox_sort_by.Items.Add("Parental level of education");
            combobox_sort_by.Items.Add("Residency");
            combobox_sort_by.Items.Add("Cambridge admission");
            combobox_sort_by.Items.Add("Child satisfaction level");
            combobox_sort_by.Items.Add("Enrollment date");
            combobox_sort_by.Items.Add("Method of learning");
            combobox_sort_by.Items.Add("Reading&Listening Score");
            combobox_sort_by.Items.Add("Speaking Score");
            combobox_sort_by.Items.Add("Writing Score");
        }
        //Import date din BD
        public void Incarca_Grid()
        {
            //Datagrid date financiare
            SqlCommand comanda_financiar = new SqlCommand("SELECT * FROM dbo.Situatie_financiara_1 LEFT JOIN dbo.Indicatori ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori.Id_indicatori " +
                "LEFT JOIN dbo.Indicatori_roi ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori_roi.Id_roi " +
                "LEFT JOIN dbo.Indicatori_dob_simpla ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori_dob_simpla.Id_dob_simpla " +
                "LEFT JOIN dbo.Indicatori_dob_compusa ON dbo.Situatie_financiara_1.Id_informatie = dbo.Indicatori_dob_compusa.Id_dob_compusa", con);

            /*SqlCommand comanda_1 = new SqlCommand("SELECT * FROM dbo.Situatie_financiara LEFT JOIN dbo.Indicatori ON ", con);
            SqlCommand comanda_2 = new SqlCommand("SELECT * FROM dbo.Situatie_financiara LEFT JOIN dbo.Indicatori ON ", con);
            SqlCommand comanda_3 = new SqlCommand("SELECT * FROM dbo.Situatie_financiara LEFT JOIN dbo.Indicatori ON ", con);*/

            SqlCommand comanda_useri = new SqlCommand("SELECT * FROM dbo.Users", con_users);

            DataTable tabel = new DataTable();
            DataTable tabel1 = new DataTable();

            con.Open();
            SqlDataReader reader = comanda_financiar.ExecuteReader();
            tabel.Load(reader);
            datagrid_date_financiare.ItemsSource = tabel.DefaultView;
            con.Close();

            con_users.Open();
            SqlDataReader reader1 = comanda_useri.ExecuteReader();
            tabel1.Load(reader1);
            datagrid_users.ItemsSource = tabel1.DefaultView;
            con_users.Close();

        }
        public void Incarca_Grid_clienti()
        {
            //Asa parca: se conecta la baza de date, scotea informatii si le punea intr-un tabel - sper sa fie unde trebuie

            SqlCommand comanda_clienti = new SqlCommand("SELECT * FROM dbo.StudentsPerformance", con_clienti);
            DataTable tabel_clienti = new DataTable();
            con_clienti.Open();
            SqlDataReader reader = comanda_clienti.ExecuteReader();
            tabel_clienti.Load(reader);
            datagrid_clienti.ItemsSource = tabel_clienti.DefaultView;
            con_clienti.Close();
        }

        public void Incarca_Grid_clienti_test(string sorter)
        {
            //Asa parca: se conecta la baza de date, scotea informatii si le punea intr-un tabel - sper sa fie unde trebuie

            SqlCommand comanda_clienti = new SqlCommand("SELECT * FROM dbo.StudentsPerformance", con_clienti);
            DataTable tabel_clienti = new DataTable();
            con_clienti.Open();
            SqlDataReader reader = comanda_clienti.ExecuteReader();
            tabel_clienti.Load(reader);

            datagrid_clienti.ItemsSource = tabel_clienti.DefaultView;
            con_clienti.Close();
        }

        private void Refresh_campuri(object sender, RoutedEventArgs e)
        {
            curata_date();
        }

        private void sort_database(object sender, RoutedEventArgs e)
        {
            //This is where the "Sort" button sorts the database based on our category from the combobox
            //instance of python engine
            run_cmd(@"C:\\Windows\\py.exe", "N:\\Facultate\\Master\\An 1\\Computational Thinking\\Project\\Scripts\\venv\\linear_model.py");
        }
        public void run_python_script(string cmd, string args)
        {
            //S-ar putea sa nu mearga pt ca nu


            /*string fileName = @"N:\Facultate\Master\An 1\Computational Thinking\Project\Scripts\main.py";
            
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\Windows\py.exe", fileName)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            System.Windows.MessageBox.Show(output);*/
            ////////////////////////////////////////////////////////////
            ///

            //IronPython - e beta
            /*Process process = new Process();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = cmd;//cmd is full path to python.exe
            start.Arguments = args;//args is path to .py file and any cmd line args
            
            var engine = Python.CreateEngine - asta nu merge ca IronPython nu stie de python 3.
            
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    System.Windows.MessageBox.Show(result);
                    //Console.Write(result);
                }
            }*/

            //Framework care gestioneaza conexiunile - ORM (si la Java) - SQLAlchemy
            //In view sa pun pagini - gen 226 entries - cate 23 de entries per pagina, 10 pagini spre ex
            //****//In python sa fac scriptul ca o librarie si s-o export ca un API - ca sa-l pot apela

            /*string EnvPath = @"C:\Windows\py.exe";
            string pythonPath = @"C:\Users\Justin\AppData\Local\Programs\Python\Python310\Lib\site-packages\pythonnet";
            Environment.SetEnvironmentVariable("PATH", EnvPath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONHOME", EnvPath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", pythonPath, EnvironmentVariableTarget.Process);
            PythonEngine.PythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);
            PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);

            PythonEngine.Initialize();
            using (Py.GIL())
            {

                dynamic np = Py.Import("numpy");

                dynamic sin = np.sin;

                Console.WriteLine(sin(5));

                Console.ReadKey();

            }*/

            //Console.WriteLine(output);

            //Console.ReadLine();


            /*var engine = 
            PythonEngine.Initialize();
            //reading code from file
            var source = engine.CreateScriptSourceFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "N:\\Facultate\\Master\\An 1\\Computational Thinking\\Project\\Scripts\\main.py"));
            var scope = engine.CreateScope();
            //executing script in scope
            source.Execute(scope);
            var classCalculator = scope.GetVariable("calculator"); //aici imi ia un obiect din clasa pe care o am, deci ar trebui sa fac o clasa sort sau cv
            //initializing class
            var calculatorInstance = engine.Operations.CreateInstance(classCalculator); //aici face o instanta din clasa de mai sus
            Console.WriteLine("From Iron Python");
            Console.WriteLine("5 + 10 = {0}", calculatorInstance.add(5, 10)); //aici apeleaza metoda clasei din fisierul python
            Console.WriteLine("5++ = {0}", calculatorInstance.increment(5)); //aici la fel*/


            //intai asta cu run script from bash cmd
            //Fast API a doua - dar trag pt prima - ar fi cam necesar

            //Pt baza de date - fac un tabel temporar si acolo ii tot dau import/empty si de acolo sa 
            //isi preia datagridu datele
            //select * from st_per order by ... 
            //statement 2 insert into
            //with statement - gen select temp WITH insert into new table (cumva dintr-un foc)

            //Code snippet
            /*WITH temporaryTable(averageValue) AS
                (SELECT avg(Salary)
                FROM Employee)
            SELECT EmployeeID,Name, Salary 
            FROM Employee, temporaryTable 
            WHERE Employee.Salary > temporaryTable.averageValue;*/
            //si in loc de select pot sa ii dau direct
            //insert into



            //Java - Jython - nu are suport calumea
            //mai dificil - HTTP - json, xml, e f flexibil - arhitectura pe microservicii
            //OpenAPI - un contract intre microservicii - define parametrii
            //Flask, Django - ca sa primeasca pythonu cereri http
            //My datagrid's data source should be the dataframe
            //Other way would be to alter the table - order by sth

            //cu sql select by ... order by ... asc/desc - asta e mai ok
            //drop table create de la 0

        }

        private void run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = cmd;//cmd is full path to python.exe
            start.Arguments = args;//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
        private void button1_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fil = new OpenFileDialog();
            fil.ShowDialog();
            string path = fil.FileName.ToString();
            ExcelFileReader(path);
        }

        public void ExcelFileReader(string path)
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read); 
            var reader = ExcelReaderFactory.CreateReader(stream);
            var result = reader.AsDataSet();
            var tables = result.Tables.Cast<DataTable>();
            try
            {
                foreach (DataTable table in tables)
                {
                    datagrid_rezultate.ItemsSource = table.DefaultView;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}