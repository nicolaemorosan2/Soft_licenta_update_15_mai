using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DB_Model
{
    public partial class DB_Model_Entity_Model : DbContext
    {
        public DB_Model_Entity_Model()
            : base("name=DB_Model_Entity_Model")
        {
        }

        public virtual DbSet<Date_financiare> Date_financiare { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
