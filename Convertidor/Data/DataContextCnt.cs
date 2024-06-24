
using Convertidor.Data.Entities.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextCnt : DbContext
    {
        public DataContextCnt(DbContextOptions<DataContextCnt> options) : base(options)
        {

        }

        public DbSet<CNT_DESCRIPTIVAS> CNT_DESCRIPTIVAS { get; set; }
        public DbSet<CNT_TITULOS> CNT_TITULOS { get; set; }
        public DbSet<CNT_BANCO_ARCHIVO> CNT_BANCO_ARCHIVO { get; set; }
        public DbSet<CNT_BANCO_ARCHIVO_CONTROL> CNT_BANCO_ARCHIVO_CONTROL { get; set; }
        public DbSet<CNT_DETALLE_EDO_CTA> CNT_DETALLE_EDO_CTA { get; set; }
        public DbSet<CNT_DETALLE_LIBRO> CNT_DETALLE_LIBRO { get; set; }
        public DbSet<CNT_ESTADO_CUENTAS> CNT_ESTADO_CUENTAS { get; set; }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder
          .Entity<CNT_DESCRIPTIVAS>(builder =>
          {
              builder.HasKey(table => new
              {
                  table.DESCRIPCION_ID,

              });
              builder.ToTable("CNT_DESCRIPTIVAS");
          });

            modelBuilder
          .Entity<CNT_TITULOS>(builder =>
          {
              builder.HasKey(table => new
              {
                  table.TITULO_ID,

              });
              builder.ToTable("CNT_TITULOS");
          });

            modelBuilder
      .Entity<CNT_BANCO_ARCHIVO>(builder =>
      {
          builder.HasKey(table => new
          {
              table.CODIGO_BANCO_ARCHIVO,

          });
          builder.ToTable("CNT_BANCO_ARCHIVO");
      });
            modelBuilder
     .Entity<CNT_BANCO_ARCHIVO_CONTROL>(builder =>
     {
          builder.HasKey(table => new
          {
             table.CODIGO_BANCO_ARCHIVO_CONTROL,

          });
          builder.ToTable("CNT_BANCO_ARCHIVO_CONTROL");
     });

            modelBuilder
     .Entity<CNT_DETALLE_EDO_CTA>(builder =>
     {
          builder.HasKey(table => new
          {
             table.CODIGO_DETALLE_EDO_CTA,

          });
          builder.ToTable("CNT_DETALLE_EDO_CTA");
     });
            modelBuilder
     .Entity<CNT_DETALLE_LIBRO>(builder =>
     {
         builder.HasKey(table => new
         {
             table.CODIGO_DETALLE_LIBRO,

         });
         builder.ToTable("CNT_DETALLE_LIBRO");
     });
      modelBuilder
     .Entity<CNT_ESTADO_CUENTAS>(builder =>
     {
         builder.HasKey(table => new
         {
             table.CODIGO_ESTADO_CUENTA,

         });
         builder.ToTable("CNT_ESTADO_CUENTAS");
     });
            
        }

        
    }

}

