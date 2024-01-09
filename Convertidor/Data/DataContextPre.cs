using Convertidor.Data.Entities.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextPre : DbContext
    {
        public DataContextPre(DbContextOptions<DataContextPre> options) : base(options)
        {

        }
        public DbSet<PRE_INDICE_CAT_PRG> PRE_INDICE_CAT_PRG { get; set; }

        public DbSet<PRE_PRESUPUESTOS> PRE_PRESUPUESTOS { get; set; }
        public DbSet<PRE_V_SALDOS> PRE_V_SALDOS { get; set; }
        public DbSet<PRE_V_DENOMINACION_PUC> PRE_V_DENOMINACION_PUC { get; set; }
        public DbSet<PRE_V_DENOMINACION_ICP_PUC> PRE_V_DENOMINACION_ICP_PUC { get; set; }
        public DbSet<PRE_V_MTR_DENOMINACION_PUC> PRE_V_MTR_DENOMINACION_PUC { get; set; }
        public DbSet<PRE_V_MTR_UNIDAD_EJECUTORA> PRE_V_MTR_UNIDAD_EJECUTORA { get; set; }
        public DbSet<PRE_V_DOC_COMPROMISOS> PRE_V_DOC_COMPROMISOS { get; set; }
        public DbSet<PRE_V_DOC_CAUSADO> PRE_V_DOC_CAUSADO { get; set; }
        public DbSet<PRE_V_DOC_PAGADO> PRE_V_DOC_PAGADO { get; set; }
        public DbSet<PRE_V_DOC_BLOQUEADO> PRE_V_DOC_BLOQUEADO { get; set; }
        public DbSet<PRE_V_DOC_MODIFICADO> PRE_V_DOC_MODIFICADO { get; set; }
        public DbSet<PRE_PLAN_UNICO_CUENTAS> PRE_PLAN_UNICO_CUENTAS { get; set; }
        public DbSet<PRE_SALDOS_DIARIOS> PRE_SALDOS_DIARIOS { get; set; }
        public DbSet<PRE_ASIGNACIONES> PRE_ASIGNACIONES { get; set; }
        public DbSet<PRE_RELACION_CARGOS> PRE_RELACION_CARGOS { get; set; }
        public DbSet<PRE_CARGOS> PRE_CARGOS { get; set; }
        public DbSet<PRE_DESCRIPTIVAS> PRE_DESCRIPTIVAS { get; set; }
        public DbSet<PRE_TITULOS> PRE_TITULOS { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);
          

            modelBuilder
            .Entity<PRE_V_SALDOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_SALDOS");
            })
             .Entity<PRE_SALDOS_DIARIOS>(builder =>
             {
                 builder.HasNoKey();
                 builder.ToTable("PRE_SALDOS_DIARIOS");
             })
              .Entity<PRE_ASIGNACIONES>(builder =>
              {
                  builder.HasNoKey();
                  builder.ToTable("PRE_ASIGNACIONES");
              })
            .Entity<PRE_V_DOC_COMPROMISOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DOC_COMPROMISOS");
            })
            .Entity<PRE_V_DOC_CAUSADO>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DOC_CAUSADO");
            })
            .Entity<PRE_V_DOC_PAGADO>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DOC_PAGADO");
            })
             .Entity<PRE_V_DOC_BLOQUEADO>(builder =>
             {
                 builder.HasNoKey();
                 builder.ToTable("PRE_V_DOC_BLOQUEADO");
             })
              .Entity<PRE_V_DOC_MODIFICADO>(builder =>
              {
                  builder.HasNoKey();
                  builder.ToTable("PRE_V_DOC_MODIFICADO");
              })
            .Entity<PRE_V_DENOMINACION_PUC>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DENOMINACION_PUC");
            })
              .Entity<PRE_V_MTR_DENOMINACION_PUC>(builder =>
              {
                  builder.HasNoKey();
                  builder.ToTable("PRE_V_MTR_DENOMINACION_PUC");
              })
             .Entity<PRE_V_MTR_UNIDAD_EJECUTORA>(builder =>
             {
                 builder.HasNoKey();
                 builder.ToTable("PRE_V_MTR_UNIDAD_EJECUTORA");
             })
            .Entity<PRE_V_DENOMINACION_ICP_PUC>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DENOMINACION_ICP_PUC");
            })
             .Entity<PRE_INDICE_CAT_PRG>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_ICP,

                 });
                 builder.ToTable("PRE_INDICE_CAT_PRG");
             })
              .Entity<PRE_PLAN_UNICO_CUENTAS>(builder =>
              {
                  //builder.HasNoKey();
                  builder.HasKey(table => new
                  {
                      table.CODIGO_PUC,

                  });
                  builder.ToTable("PRE_PLAN_UNICO_CUENTAS");
              })
                 .Entity<PRE_RELACION_CARGOS>(builder =>
                 {
                     //builder.HasNoKey();
                     builder.HasKey(table => new
                     {
                         table.CODIGO_RELACION_CARGO,

                     });
                     builder.ToTable("PRE_RELACION_CARGOS");
                 })
            .Entity<PRE_CARGOS>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.CODIGO_CARGO,

                });
                builder.ToTable("PRE_CARGOS");
            })
            .Entity<PRE_DESCRIPTIVAS>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.DESCRIPCION_ID,

                });
                builder.ToTable("PRE_DESCRIPTIVAS");
            })
            .Entity<PRE_TITULOS>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.TITULO_ID,

                });
                builder.ToTable("PRE_TITULOS");
            })
                     
            .Entity<PRE_PRESUPUESTOS>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.CODIGO_PRESUPUESTO,

                });
                builder.ToTable("PRE_PRESUPUESTOS");
            });
           

        }



    }
}
