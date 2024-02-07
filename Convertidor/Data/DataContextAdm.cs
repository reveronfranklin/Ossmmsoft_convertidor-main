using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextAdm: DbContext
    {
        public DataContextAdm(DbContextOptions<DataContextAdm> options) : base(options)
        {

        }

       

        public DbSet<ADM_DESCRIPTIVAS> ADM_DESCRIPTIVAS { get; set; }

        public DbSet<ADM_TITULOS> ADM_TITULOS { get; set; }

        public DbSet<ADM_ACT_PROVEEDOR> ADM_ACT_PROVEEDOR { get; set; }
        public DbSet<ADM_PROVEEDORES> ADM_PROVEEDORES { get; set; }
        public DbSet<ADM_CONTACTO_PROVEEDOR> ADM_CONTACTO_PROVEEDOR { get; set; }
        public DbSet<ADM_DIR_PROVEEDOR> ADM_DIR_PROVEEDOR { get; set; }
        public DbSet<ADM_COM_PROVEEDOR> ADM_COM_PROVEEDOR { get; set; }
        public DbSet<ADM_SOLICITUDES> ADM_SOLICITUDES { get; set; }
        public DbSet<ADM_DETALLE_SOLICITUD> ADM_DETALLE_SOLICITUD { get; set; }
        public DbSet<ADM_PUC_SOLICITUD> ADM_PUC_SOLICITUD { get; set; }
        public DbSet<ADM_REINTEGROS> ADM_REINTEGROS { get; set; }
        public DbSet<ADM_PUC_REINTEGRO> ADM_PUC_REINTEGRO { get; set; }
        public DbSet<ADM_ORDEN_PAGO> ADM_ORDEN_PAGO { get; set; }
        public DbSet<ADM_COMPROMISO_OP> ADM_COMPROMISO_OP { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);





            modelBuilder
                .Entity<ADM_DESCRIPTIVAS>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.DESCRIPCION_ID,

                    });
                    builder.ToTable("ADM_DESCRIPTIVAS");
                });

            modelBuilder
                .Entity<ADM_TITULOS>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.TITULO_ID,

                    });
                    builder.ToTable("ADM_TITULOS");
                });
            modelBuilder
                .Entity<ADM_ACT_PROVEEDOR>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_ACT_PROVEEDOR,

                    });
                    builder.ToTable("ADM_ACT_PROVEEDOR");
                });
            modelBuilder
                .Entity<ADM_PROVEEDORES>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_PROVEEDOR,

                    });
                    builder.ToTable("ADM_PROVEEDORES");
                });
            modelBuilder
                .Entity<ADM_CONTACTO_PROVEEDOR>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_CONTACTO_PROVEEDOR,

                    });
                    builder.ToTable("ADM_CONTACTO_PROVEEDOR");
                });
            modelBuilder
                .Entity<ADM_DIR_PROVEEDOR>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_DIR_PROVEEDOR,

                    });
                    builder.ToTable("ADM_DIR_PROVEEDOR");
                });
            modelBuilder
                .Entity<ADM_COM_PROVEEDOR>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_COM_PROVEEDOR,

                    });
                    builder.ToTable("ADM_COM_PROVEEDOR");
                });

            modelBuilder
                .Entity<ADM_SOLICITUDES>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_SOLICITUD,

                    });
                    builder.ToTable("ADM_SOLICITUDES");
                });

            modelBuilder
                .Entity<ADM_DETALLE_SOLICITUD>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_DETALLE_SOLICITUD,

                    });
                    builder.ToTable("ADM_DETALLE_SOLICITUD");
                });

            modelBuilder
                .Entity<ADM_PUC_SOLICITUD>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_PUC_SOLICITUD,

                    });
                    builder.ToTable("ADM_PUC_SOLICITUD");
                });
            modelBuilder
                .Entity<ADM_REINTEGROS>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_REINTEGRO,

                    });
                    builder.ToTable("ADM_REINTEGROS");
                });
            modelBuilder
                .Entity<ADM_PUC_REINTEGRO>(builder =>
                {
                    //builder.HasNoKey();
                    builder.HasKey(table => new
                    {
                        table.CODIGO_PUC_REINTEGRO,

                    });
                    builder.ToTable("ADM_PUC_REINTEGRO");
                });
            modelBuilder
               .Entity<ADM_ORDEN_PAGO>(builder =>
               {
                   //builder.HasNoKey();
                   builder.HasKey(table => new
                   {
                       table.CODIGO_ORDEN_PAGO,

                   });
                   builder.ToTable("ADM_ORDEN_PAGO");
               });
            modelBuilder
              .Entity<ADM_COMPROMISO_OP>(builder =>
              {
                  //builder.HasNoKey();
                  builder.HasKey(table => new
                  {
                      table.CODIGO_COMPROMISO_OP,

                  });
                  builder.ToTable("ADM_COMPROMISO_OP");
              });
            
        }



    }
}
