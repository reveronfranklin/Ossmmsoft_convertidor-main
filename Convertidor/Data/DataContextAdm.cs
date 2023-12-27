using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Catastro;
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
            
            
            

        }



    }
}
