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
        public DbSet<PRE_ASIGNACIONES_DETALLE> PRE_ASIGNACIONES_DETALLE { get; set; }
        
        
        public DbSet<PRE_RELACION_CARGOS> PRE_RELACION_CARGOS { get; set; }
        public DbSet<PRE_CARGOS> PRE_CARGOS { get; set; }
        public DbSet<PRE_DESCRIPTIVAS> PRE_DESCRIPTIVAS { get; set; }
        public DbSet<PRE_TITULOS> PRE_TITULOS { get; set; }
        public DbSet<PRE_PUC_SOL_MODIFICACION> PRE_PUC_SOL_MODIFICACION { get; set; }
        
        public DbSet<PRE_SALDOS> PRE_SALDOS { get; set; }
        public DbSet<PRE_RESUMEN_SALDOS> PRE_RESUMEN_SALDOS { get; set; }
        public DbSet<PRE_DIRECTIVOS> PRE_DIRECTIVOS { get; set; }
        public DbSet<PRE_COMPROMISOS> PRE_COMPROMISOS { get; set; }
        public DbSet<PRE_DETALLE_COMPROMISOS> PRE_DETALLE_COMPROMISOS { get; set; }
        public DbSet<PRE_PUC_COMPROMISOS> PRE_PUC_COMPROMISOS { get; set; }
        public DbSet<PRE_SOL_MODIFICACION> PRE_SOL_MODIFICACION { get; set; }
        public DbSet<PRE_MODIFICACION> PRE_MODIFICACION { get; set; }
        public DbSet<PRE_PUC_MODIFICACION> PRE_PUC_MODIFICACION { get; set; }
        public DbSet<PRE_METAS> PRE_METAS { get; set; }
        public DbSet<PRE_NIVELES_PUC> PRE_NIVELES_PUC { get; set; }
        public DbSet<PRE_OBJETIVOS> PRE_OBJETIVOS { get; set; }
        public DbSet<PRE_ORGANISMOS> PRE_ORGANISMOS { get; set; }
        public DbSet<PRE_PARTICIPA_FINANCIERA_ORG> PRE_PARTICIPA_FINANCIERA_ORG { get; set; }
        public DbSet<PRE_PORC_GASTOS_MENSUALES> PRE_PORC_GASTOS_MENSUALES { get; set; }











        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
            .Entity<PRE_V_SALDOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_SALDOS");
            })
            .Entity<PRE_RESUMEN_SALDOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_RESUMEN_SALDOS");
            })
            .Entity<PRE_SALDOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_SALDO,

                });
                builder.ToTable("PRE_SALDOS");
            })


             .Entity<PRE_SALDOS_DIARIOS>(builder =>
             {
                 builder.HasNoKey();
                 builder.ToTable("PRE_SALDOS_DIARIOS");
             })
             .Entity<PRE_PUC_SOL_MODIFICACION>(builder =>
             {
                 builder.HasKey(table => new
                 {
                     table.CODIGO_PUC_SOL_MODIFICACION,

                 });
                 builder.ToTable("PRE_PUC_SOL_MODIFICACION");
             })
              .Entity<PRE_ASIGNACIONES>(builder =>
              {
                  builder.HasKey(table => new
                  {
                      table.CODIGO_ASIGNACION,

                  });
                  builder.ToTable("PRE_ASIGNACIONES");
              })
              .Entity<PRE_ASIGNACIONES_DETALLE>(builder =>
              {
                  builder.HasKey(table => new
                  {
                      table.CODIGO_ASIGNACION_DETALLE,

                  });
                  builder.ToTable("PRE_ASIGNACIONES_DETALLE");
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
            })
            .Entity<PRE_DIRECTIVOS>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_DIRECTIVO,

                 });
                 builder.ToTable("PRE_DIRECTIVOS");
             })

            .Entity<PRE_COMPROMISOS>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.CODIGO_COMPROMISO,

                });
                builder.ToTable("PRE_COMPROMISOS");
            })

             .Entity<PRE_DETALLE_COMPROMISOS>(builder =>
              {
                  //builder.HasNoKey();
                  builder.HasKey(table => new
                  {
                      table.CODIGO_DETALLE_COMPROMISO,

                  });
                  builder.ToTable("PRE_DETALLE_COMPROMISOS");
              })
            .Entity<PRE_PUC_COMPROMISOS>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_PUC_COMPROMISO,

                 });
                 builder.ToTable("PRE_PUC_COMPROMISOS");
             })

            .Entity<PRE_SOL_MODIFICACION>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_SOL_MODIFICACION,

                 });
                 builder.ToTable("PRE_SOL_MODIFICACION");
             })

            .Entity<PRE_MODIFICACION>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_MODIFICACION,

                 });
                 builder.ToTable("PRE_MODIFICACION");
             })

             .Entity<PRE_PUC_MODIFICACION>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_PUC_MODIFICACION,

                 });
                 builder.ToTable("PRE_PUC_MODIFICACION");
             })


             .Entity<PRE_METAS>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_META,

                 });
                 builder.ToTable("PRE_METAS");
             })

            .Entity<PRE_NIVELES_PUC>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_GRUPO,

                 });
                 builder.ToTable("PRE_NIVELES_PUC");
             })

             .Entity<PRE_OBJETIVOS>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_OBJETIVO,

                 });
                 builder.ToTable("PRE_OBJETIVOS");
             })

             .Entity<PRE_ORGANISMOS>(builder =>
             {
                 //builder.HasNoKey();
                 builder.HasKey(table => new
                 {
                     table.CODIGO_ORGANISMO,

                 });
                 builder.ToTable("PRE_ORGANISMOS");
             })

              .Entity<PRE_PARTICIPA_FINANCIERA_ORG>(builder =>
              {
                  //builder.HasNoKey();
                  builder.HasKey(table => new
                  {
                      table.CODIGO_PARTICIPA_FINANC_ORG,

                  });
                  builder.ToTable("PRE_PARTICIPA_FINANCIERA_ORG");
              })

               .Entity<PRE_PORC_GASTOS_MENSUALES>(builder =>
               {
                   //builder.HasNoKey();
                   builder.HasKey(table => new
                   {
                       table.CODIGO_POR_GASTOS_MES,

                   });
                   builder.ToTable("PRE_PORC_GASTOS_MENSUALES");
               });
              






        }



    }
}
