


using CConvertidor.Data.Entities.Sis;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.RentasMunicipales;
using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextBm : DbContext
    {
        public DataContextBm(DbContextOptions<DataContextBm> options) : base(options)
        {

        }
        public DbSet<BM_V_BM1> BM_V_BM1 { get; set; }
     
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
                .Entity<BM_V_BM1>(builder =>
                {
                    builder.HasNoKey();
                    builder.ToTable("BM_V_BM1");
                });
        

        }



    }
}
