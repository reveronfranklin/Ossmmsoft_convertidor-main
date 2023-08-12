using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Presupuesto;
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
        public DbSet<RH_PERIODOS> RH_PERIODOS { get; set; }
        public DbSet<RH_TIPOS_NOMINA> RH_TIPOS_NOMINA { get; set; }
        public DbSet<RH_PERSONAS> RH_PERSONAS { get; set; }
        public DbSet<RH_EDUCACION> RH_EDUCACION { get; set; }
        public DbSet<RH_DESCRIPTIVAS> RH_DESCRIPTIVAS { get; set; }
        public DbSet<RH_DIRECCIONES> RH_DIRECCIONES { get; set; }
        public DbSet<RH_CONCEPTOS> RH_CONCEPTOS { get; set; }
        public DbSet<RH_RELACION_CARGOS> RH_RELACION_CARGOS { get; set; }
        public DbSet<RH_PROCESOS> RH_PROCESOS { get; set; }
        public DbSet<RH_PROCESOS_DETALLES> RH_PROCESOS_DETALLES { get; set; }
        
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
             builder.ToTable("RH_V_HISTORICO_MOVIMIENTOS_ALL");
         });
            modelBuilder
        .Entity<RH_PERIODOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_PERIODOS");
            });
            modelBuilder
       .Entity<RH_TIPOS_NOMINA>(builder =>
       {
           builder.HasNoKey();
           builder.ToTable("RH_TIPOS_NOMINA");
       });
            modelBuilder
         .Entity<RH_PERSONAS>(builder =>
         {
             builder.HasNoKey();
             builder.ToTable("RH_PERSONAS");
         });
            modelBuilder
        .Entity<RH_EDUCACION>(builder =>
        {
            builder.HasNoKey();
            builder.ToTable("RH_EDUCACION");
        });
            modelBuilder
       .Entity<RH_DESCRIPTIVAS>(builder =>
       {
           builder.HasNoKey();
           builder.ToTable("RH_DESCRIPTIVAS");
       });
            modelBuilder
          .Entity<RH_DIRECCIONES>(builder =>
          {
              builder.HasNoKey();
              builder.ToTable("RH_DIRECCIONES");
          });
            modelBuilder
                    .Entity<RH_RELACION_CARGOS>(builder =>
                    {
                        builder.HasKey(table => new
                        {
                            table.CODIGO_RELACION_CARGO,

                        });
                        builder.ToTable("RH_RELACION_CARGOS");
                    });
            modelBuilder
               .Entity<RH_PROCESOS>(builder =>
               {
                   builder.HasKey(table => new
                   {
                       table.CODIGO_PROCESO,

                   });
                   builder.ToTable("RH_PROCESOS");
               });
            modelBuilder
            .Entity<RH_PROCESOS_DETALLES>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_DETALLE_PROCESO,

                });
                builder.ToTable("RH_PROCESOS_DETALLES");
            });

            
            modelBuilder
        .Entity<RH_CONCEPTOS>(builder =>
        {
            builder.HasNoKey();
            builder.ToTable("RH_CONCEPTOS");
        });

        }


     

    }
}
