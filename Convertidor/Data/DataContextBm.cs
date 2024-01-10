﻿

using Convertidor.Data.Entities;
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
        public DbSet<BM_TITULOS> BM_TITULOS { get; set; }
        public DbSet<BM_DESCRIPTIVAS> BM_DESCRIPTIVAS { get; set; }
        public DbSet<BM_ARTICULOS> BM_ARTICULOS { get; set; }
        public DbSet<BM_BIENES> BM_BIENES { get; set; }
        public DbSet<BM_CLASIFICACION_BIENES> BM_CLASIFICACION_BIENES { get; set; }
        public DbSet<BM_DETALLE_BIENES> BM_DETALLE_BIENES { get; set; }
        public DbSet<BM_DETALLE_ARTICULOS> BM_DETALLE_ARTICULOS { get; set; }
        public DbSet<BM_DIR_BIEN> BM_DIR_BIEN { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
                .Entity<BM_V_BM1>(builder =>
                {
                    builder.HasNoKey();
                    builder.ToTable("BM_V_BM1");
                });

            modelBuilder
                .Entity<BM_TITULOS>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.TITULO_ID,

                    });
                    builder.ToTable("BM_TITULOS");
                });
            modelBuilder
                .Entity<BM_DESCRIPTIVAS>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.DESCRIPCION_ID,

                    });
                    builder.ToTable("BM_DESCRIPTIVAS");
                });
            modelBuilder
                .Entity<BM_ARTICULOS>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.CODIGO_ARTICULO,

                    });
                    builder.ToTable("BM_ARTICULOS");
                });
           modelBuilder
            .Entity<BM_BIENES>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_BIEN,

                });
                builder.ToTable("BM_BIENES");
            });

            modelBuilder
               .Entity<BM_CLASIFICACION_BIENES>(builder =>
               {

                   builder.HasKey(table => new
                   {
                       table.CODIGO_CLASIFICACION_BIEN,

                   });
                   builder.ToTable("BM_CLASIFICACION_BIENES");
               });

            modelBuilder
               .Entity<BM_DETALLE_BIENES>(builder =>
               {

                   builder.HasKey(table => new
                   {
                       table.CODIGO_DETALLE_BIEN,

                   });
                   builder.ToTable("BM_DETALLE_BIENES");
               });
            modelBuilder
              .Entity<BM_DETALLE_ARTICULOS>(builder =>
              {

                  builder.HasKey(table => new
                  {
                      table.CODIGO_DETALLE_ARTICULO,

                  });
                  builder.ToTable("BM_DETALLE_ARTICULOS");
              });
            modelBuilder
              .Entity<BM_DIR_BIEN>(builder =>
              {

                  builder.HasKey(table => new
                  {
                      table.CODIGO_DIR_BIEN,

                  });
                  builder.ToTable("BM_DIR_BIEN");
              });
           
        }




}
}
