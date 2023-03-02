
using Convertidor.Data.Entities.RentasMunicipales;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextRm : DbContext
    {
        public DataContextRm(DbContextOptions<DataContextRm> options) : base(options)
        {

        }
        public DbSet<RM_UNIDADES_CALCULOS> RM_UNIDADES_CALCULOS { get; set; }
        public DbSet<RM_CONTRIBUYENTES> RM_CONTRIBUYENTES { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
          .Entity<RM_UNIDADES_CALCULOS>(builder =>
          {
              builder.HasNoKey();
              builder.ToTable("RM_UNIDADES_CALCULOS");
          });

            modelBuilder
        .Entity<RM_CONTRIBUYENTES>(builder =>
        {
            builder.HasNoKey();
            builder.ToTable("RM_CONTRIBUYENTES");
        });

        }



    }
}
