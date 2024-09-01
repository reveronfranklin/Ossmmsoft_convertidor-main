using Convertidor.Data.Entities.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextCat: DbContext
    {
        public DataContextCat(DbContextOptions<DataContextCat> options) : base(options)
        {

        }

        public DbSet<CAT_FICHA> CAT_FICHA { get; set; }

        public DbSet<CAT_ROLES> CAT_ROLES { get; set; }

        public DbSet<CAT_DESCRIPTIVAS> CAT_DESCRIPTIVAS { get; set; }

        public DbSet<CAT_TITULOS> CAT_TITULOS { get; set; }

        public DbSet<CAT_UBICACION_NAC> CAT_UBICACION_NAC { get; set; }

        public DbSet<CAT_AFOROS_INMUEBLES> CAT_AFOROS_INMUEBLES { get; set; }
        public DbSet<CAT_ARRENDAMIENTOS_INMUEBLES> CAT_ARRENDAMIENTOS_INMUEBLES { get; set; }
        public DbSet<CAT_AUDITORIA> CAT_AUDITORIA { get; set; }
        public DbSet<CAT_AVALUO_CONSTRUCCION> CAT_AVALUO_CONSTRUCCION { get; set; }
        public DbSet<CAT_AVALUO_TERRENO> CAT_AVALUO_TERRENO { get; set; }
        public DbSet<CAT_CALC_X_TRIANGULACION> CAT_CALC_X_TRIANGULACION { get; set; }
        public DbSet<CAT_CONTROL_PARCELAS> CAT_CONTROL_PARCELAS { get; set; }
        public DbSet<CAT_DESGLOSE> CAT_DESGLOSE { get; set; }
        public DbSet<CAT_DIRECCIONES> CAT_DIRECCIONES { get; set; }
        public DbSet<CAT_DOCUMENTOS_LEGALES> CAT_DOCUMENTOS_LEGALES { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
          .Entity<CAT_FICHA>(builder =>
          {
              builder.HasNoKey();
              builder.ToTable("CAT_FICHA");
          });


            modelBuilder
                      .Entity<CAT_DESCRIPTIVAS>(builder =>
                      {
                          builder.HasKey(table => new
                          {
                              table.DESCRIPCION_ID,

                          });
                          builder.ToTable("CAT_DESCRIPTIVAS");
                      });

            modelBuilder
          .Entity<CAT_TITULOS>(builder =>
          {
              builder.HasKey(table => new
              {
                  table.TITULO_ID,

              });
              builder.ToTable("CAT_TITULOS");
          });

            modelBuilder
                 .Entity<CAT_UBICACION_NAC>(builder =>
                 {
                     builder.HasNoKey();
                     builder.ToTable("CAT_UBICACION_NAC");
                 });

            modelBuilder
                .Entity<CAT_ROLES>(builder =>
                {
                    builder.HasNoKey();
                    builder.ToTable("CAT_ROLES");
                });

            modelBuilder
                      .Entity<CAT_AFOROS_INMUEBLES>(builder =>
                      {
                          builder.HasKey(table => new
                          {
                              table.CODIGO_AFORO_INMUEBLE,

                          });
                          builder.ToTable("CAT_AFOROS_INMUEBLES");
                      });

            modelBuilder
                     .Entity<CAT_ARRENDAMIENTOS_INMUEBLES>(builder =>
                     {
                         builder.HasKey(table => new
                         {
                             table.CODIGO_ARRENDAMIENTO_INMUEBLE,

                         });
                         builder.ToTable("CAT_ARRENDAMIENTOS_INMUEBLES");
                     });

            modelBuilder
                     .Entity<CAT_AUDITORIA>(builder =>
                     {
                         builder.HasKey(table => new
                         {
                             table.CODIGO_AUDITORIA,

                         });
                         builder.ToTable("CAT_AUDITORIA");
                     });

            modelBuilder
                     .Entity<CAT_AVALUO_CONSTRUCCION>(builder =>
                     {
                         builder.HasKey(table => new
                         {
                             table.CODIGO_AVALUO_CONSTRUCCION,

                         });
                         builder.ToTable("CAT_AVALUO_CONSTRUCCION");
                     });

            modelBuilder
                 .Entity<CAT_AVALUO_TERRENO>(builder =>
                 {
                     builder.HasKey(table => new
                     {
                         table.CODIGO_AVALUO_TERRENO,

                     });
                     builder.ToTable("CAT_AVALUO_TERRENO");
                 });

            modelBuilder
                .Entity<CAT_CALC_X_TRIANGULACION>(builder =>
                {
                    builder.HasKey(table => new
                    {
                        table.CODIGO_TRIANGULACION,

                    });
                    builder.ToTable("CAT_CALC_X_TRIANGULACION");
                });

            modelBuilder
               .Entity<CAT_CONTROL_PARCELAS>(builder =>
               {
                   builder.HasKey(table => new
                   {
                       table.CODIGO_CONTROL_PARCELA,

                   });
                   builder.ToTable("CAT_CONTROL_PARCELAS");
               });

            modelBuilder
              .Entity<CAT_DESGLOSE>(builder =>
              {
                  builder.HasKey(table => new
                  {
                      table.CODIGO_DESGLOSE,

                  });
                  builder.ToTable("CAT_DESGLOSE");
              });
                        modelBuilder
              .Entity<CAT_DIRECCIONES>(builder =>
              {
                  builder.HasKey(table => new
                  {
                      table.CODIGO_DIRECCION,

                  });
                  builder.ToTable("CAT_DIRECCIONES");
              });

            modelBuilder
              .Entity<CAT_DOCUMENTOS_LEGALES>(builder =>
              {
                  builder.HasKey(table => new
                  {
                      table.CODIGO_DOCUMENTOS_LEGALES,

                  });
                  builder.ToTable("CAT_DOCUMENTOS_LEGALES");
              });
            


        }

        

    }
}
