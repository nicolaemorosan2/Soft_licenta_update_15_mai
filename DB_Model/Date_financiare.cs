namespace DB_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Date_financiare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_indicator { get; set; }

        public double Venituri_totale { get; set; }

        public double Venitur_din_investitii { get; set; }

        public double Cheltuieli { get; set; }

        public double Investitii { get; set; }

        [Column("Descriere investitii")]
        [Required]
        [StringLength(50)]
        public string Descriere_investitii { get; set; }

        public double VAN { get; set; }

        public double Amortizare_liniara { get; set; }

        public double Amortizare_degresiva { get; set; }

        public double Dobanda_simpla { get; set; }

        public double Dobanda_compusa { get; set; }

        public double RIR { get; set; }

        public double ROI { get; set; }
    }
}
