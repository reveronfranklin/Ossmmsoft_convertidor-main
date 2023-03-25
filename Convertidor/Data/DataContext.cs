using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<RH_HISTORICO_NOMINA> RH_HISTORICO_NOMINA { get; set; }
        public DbSet<RH_HISTORICO_PERSONAL_CARGO> RH_HISTORICO_PERSONAL_CARGO { get; set; }
        public DbSet<PRE_INDICE_CAT_PRG> PRE_INDICE_CAT_PRG { get; set; }
        public DbSet<RH_V_HISTORICO_MOVIMIENTOS> RH_V_HISTORICO_MOVIMIENTOS { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);

            modelBuilder
            .Entity<RH_HISTORICO_NOMINA>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_HISTORICO_NOMINA");
            });


            modelBuilder
           .Entity<RH_HISTORICO_PERSONAL_CARGO>(builder =>
           {
               builder.HasNoKey();
               builder.ToTable("RH_HISTORICO_PERSONAL_CARGO");
           });

            modelBuilder
          .Entity<PRE_INDICE_CAT_PRG>(builder =>
          {
              builder.HasNoKey();
              builder.ToTable("PRE_INDICE_CAT_PRG");
          });
            modelBuilder
         .Entity<RH_V_HISTORICO_MOVIMIENTOS>(builder =>
         {
             builder.HasNoKey();
             builder.ToTable("RH_V_HISTORICO_MOVIMIENTOS");
         });

        }



    }
}
