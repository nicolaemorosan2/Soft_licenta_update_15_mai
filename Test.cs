using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soft_licenta_2
{
    public class Test
    {
        public List<Date_financiare> Data { get; set; }
        //public List<Date_financiare> Data_test { get; set; }

        public Test()
        {
           
            /*Data_test = new List<Date_financiare>()
            {
                new Date_financiare { Data = 202205, Venituri = 1234, Venituri_din_investitii = 180, Cheltuieli = 990, Investitii = 100},
                new Date_financiare { Data = 202206, Venituri = 2234, Venituri_din_investitii = 380, Cheltuieli = 1990, Investitii = 230},
                new Date_financiare { Data = 202207, Venituri = 3234, Venituri_din_investitii = 480, Cheltuieli = 2990, Investitii = 330},
                new Date_financiare { Data = 202204, Venituri = 4234, Venituri_din_investitii = 580, Cheltuieli = 3990, Investitii = 430}
            };*/
            Data = new List<Date_financiare>()
            {
                new Date_financiare { Data = 202201, Venituri = 1234, Venituri_din_investitii = 180, Cheltuieli = 990, Investitii = 100},
                new Date_financiare { Data = 202202, Venituri = 2234, Venituri_din_investitii = 380, Cheltuieli = 1990, Investitii = 230},
                new Date_financiare { Data = 202203, Venituri = 3234, Venituri_din_investitii = 480, Cheltuieli = 2990, Investitii = 330},
                new Date_financiare { Data = 202204, Venituri = 4234, Venituri_din_investitii = 580, Cheltuieli = 3990, Investitii = 430}
            };
        }
    }
}
