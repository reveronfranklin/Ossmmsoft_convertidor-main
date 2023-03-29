

using Convertidor.Data.Entities.RentasMunicipales;
using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextSis : DbContext
    {
        public DataContextSis(DbContextOptions<DataContextSis> options) : base(options)
        {

        }
        public DbSet<SIS_USUARIOS> SIS_USUARIOS { get; set; }
    
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
          .Entity<SIS_USUARIOS>(builder =>
          {
              //builder.HasNoKey();
              builder.HasKey(table => new
              {
                  table.CODIGO_USUARIO,

              });
              builder.ToTable("SIS_USUARIOS");
          });


        }



    }
}
