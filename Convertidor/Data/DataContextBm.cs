using Convertidor.Data.Entities.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextBm : DbContext
    {
        public DataContextBm(DbContextOptions<DataContextBm> options) : base(options)
        {

        }
        
        
        public DbSet<BM_V_UBICA_RESPONSABLE> BM_V_UBICA_RESPONSABLE { get; set; }
        public DbSet<BM_V_UBICACIONES> BM_V_UBICACIONES { get; set; }
        public DbSet<BM_V_BM1> BM_V_BM1 { get; set; }
        public DbSet<BM_TITULOS> BM_TITULOS { get; set; }
        public DbSet<BM_DESCRIPTIVAS> BM_DESCRIPTIVAS { get; set; }
        public DbSet<BM_ARTICULOS> BM_ARTICULOS { get; set; }
        public DbSet<BM_BIENES> BM_BIENES { get; set; }
        public DbSet<BM_CLASIFICACION_BIENES> BM_CLASIFICACION_BIENES { get; set; }
        public DbSet<BM_DETALLE_BIENES> BM_DETALLE_BIENES { get; set; }
        public DbSet<BM_DETALLE_ARTICULOS> BM_DETALLE_ARTICULOS { get; set; }
        public DbSet<BM_DIR_BIEN> BM_DIR_BIEN { get; set; }
        public DbSet<BM_DIR_H_BIEN> BM_DIR_H_BIEN { get; set; }
        public DbSet<BM_MOV_BIENES> BM_MOV_BIENES { get; set; }
        public DbSet<BM_SOL_MOV_BIENES> BM_SOL_MOV_BIENES { get; set; }
        public DbSet<BM_BIENES_FOTO> BM_BIENES_FOTO { get; set; }
        public DbSet<BM_CONTEO> BM_CONTEO { get; set; }
        public DbSet<BM_CONTEO_DETALLE> BM_CONTEO_DETALLE { get; set; }
        
        public DbSet<BM_CONTEO_HISTORICO> BM_CONTEO_HISTORICO { get; set; }
        public DbSet<BM_CONTEO_DETALLE_HISTORICO> BM_CONTEO_DETALLE_HISTORICO { get; set; }
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
                .Entity<BM_V_UBICA_RESPONSABLE>(builder =>
                {

                    builder.HasNoKey();
                    builder.ToTable("BM_V_UBICA_RESPONSABLE");
                });
            
            modelBuilder
                .Entity<BM_V_UBICACIONES>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.CODIGO_DIR_BIEN,

                    });
                    builder.ToTable("BM_V_UBICACIONES");
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
               .Entity<BM_BIENES_FOTO>(builder =>
               {

                   builder.HasKey(table => new
                   {
                       table.CODIGO_BIEN_FOTO,

                   });
                   builder.ToTable("BM_BIENES_FOTO");
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

            modelBuilder
             .Entity<BM_DIR_H_BIEN>(builder =>
             {

                 builder.HasKey(table => new
                 {
                     table.CODIGO_H_DIR_BIEN,

                 });
                 builder.ToTable("BM_DIR_H_BIEN");
             });

            modelBuilder
             .Entity<BM_MOV_BIENES>(builder =>
             {

                 builder.HasKey(table => new
                 {
                     table.CODIGO_MOV_BIEN,

                 });
                 builder.ToTable("BM_MOV_BIENES");
             });

            modelBuilder
            .Entity<BM_SOL_MOV_BIENES>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_MOV_BIEN,

                });
                builder.ToTable("BM_SOL_MOV_BIENES");
            });
            modelBuilder
                .Entity<BM_CONTEO>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.CODIGO_BM_CONTEO,

                    });
                    builder.ToTable("BM_CONTEO");
                });
            modelBuilder
                .Entity<BM_CONTEO_DETALLE>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.CODIGO_BM_CONTEO_DETALLE,

                    });
                    builder.ToTable("BM_CONTEO_DETALLE");
                });
            modelBuilder
                .Entity<BM_CONTEO_HISTORICO>(builder =>
                {

                    builder.HasKey(table => new
                    {
                        table.CODIGO_BM_CONTEO,

                    });
                    builder.ToTable("BM_CONTEO_HISTORICO");
                });
            modelBuilder
                .Entity<BM_CONTEO_DETALLE_HISTORICO>(builder =>
                {

                    builder.HasKey(s => new { s.CODIGO_BM_CONTEO, s.CONTEO ,s.CODIGO_BIEN});
                    builder.ToTable("BM_CONTEO_DETALLE_HISTORICO");
                });
            
        }




    }
}
