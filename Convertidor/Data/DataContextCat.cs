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
            
        }



    }
}
