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
        public DbSet<RH_MOV_NOMINA> RH_MOV_NOMINA { get; set; }
        public DbSet<RH_COMUNICACIONES> RH_COMUNICACIONES { get; set; }
        public DbSet<RH_ADMINISTRATIVOS> RH_ADMINISTRATIVOS { get; set; }

        public DbSet<RH_FAMILIARES> RH_FAMILIARES { get; set; }
        public DbSet<RH_DOCUMENTOS> RH_DOCUMENTOS { get; set; }
        public DbSet<RH_DOCUMENTOS_DETALLES> RH_DOCUMENTOS_DETALLES { get; set; }

        public DbSet<RH_EXP_LABORAL> RH_EXP_LABORAL { get; set; }
        public DbSet<RH_PERSONAS_MOV_CONTROL> RH_PERSONAS_MOV_CONTROL { get; set; }
        public DbSet<RH_ARI> RH_ARI { get; set; }

        public DbSet<RH_TMP_RETENCIONES_SSO> RH_TMP_RETENCIONES_SSO { get; set; }

        public DbSet<RH_TMP_RETENCIONES_FAOV> RH_TMP_RETENCIONES_FAOV { get; set; }

        public DbSet<RH_TMP_RETENCIONES_INCES> RH_TMP_RETENCIONES_INCES { get; set; }

        public DbSet<RH_TMP_RETENCIONES_FJP> RH_TMP_RETENCIONES_FJP { get; set; }

        public DbSet<RH_TMP_RETENCIONES_CAH> RH_TMP_RETENCIONES_CAH { get; set; }

        public DbSet<RH_TMP_RETENCIONES_SIND> RH_TMP_RETENCIONES_SIND { get; set; }

        public DbSet<RH_H_RETENCIONES_SSO> RH_H_RETENCIONES_SSO { get; set; }

        public DbSet<RH_H_RETENCIONES_FAOV> RH_H_RETENCIONES_FAOV { get; set; }

        public DbSet<RH_H_RETENCIONES_FJP> RH_H_RETENCIONES_FJP { get; set; }

        public DbSet<RH_H_RETENCIONES_INCES> RH_H_RETENCIONES_INCES { get; set; }

        public DbSet<RH_H_RETENCIONES_CAH> RH_H_RETENCIONES_CAH { get; set; }

        public DbSet<RH_H_RETENCIONES_SIND> RH_H_RETENCIONES_SIND { get; set; }
        public DbSet<RH_H_PERIODOS> RH_H_PERIODOS { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RH_HISTORICO_NOMINA>(builder =>
            {
                    builder.HasNoKey();
                    builder.ToTable("RH_HISTORICO_NOMINA");
            });

            modelBuilder.Entity<RH_TMP_RETENCIONES_SSO>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TMP_RETENCIONES_SSO");
            });

            modelBuilder.Entity<RH_TMP_RETENCIONES_FAOV>(builder =>
            {
            builder.HasNoKey();
            builder.ToTable("RH_TMP_RETENCIONES_FAOV");
            });

            modelBuilder.Entity<RH_TMP_RETENCIONES_INCES>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TMP_RETENCIONES_INCES");
            });

            modelBuilder.Entity<RH_TMP_RETENCIONES_FJP>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TMP_RETENCIONES_FJP");
            });
            modelBuilder.Entity<RH_TMP_RETENCIONES_CAH>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TMP_RETENCIONES_CAH");
            });
            modelBuilder.Entity<RH_TMP_RETENCIONES_SIND>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TMP_RETENCIONES_SIND");
            });
            modelBuilder.Entity<RH_TMP_RETENCIONES_SIND>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TMP_RETENCIONES_SIND");
            });

            modelBuilder.Entity<RH_H_RETENCIONES_SSO>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_RETENCION_APORTE,

                });
                builder.ToTable("RH_H_RETENCIONES_SSO");
            });

            modelBuilder.Entity<RH_H_RETENCIONES_FAOV>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_RETENCION_APORTE,

                });
                builder.ToTable("RH_H_RETENCIONES_FAOV");
            });

            modelBuilder.Entity<RH_H_RETENCIONES_FJP>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_RETENCION_APORTE,

                });
                builder.ToTable("RH_H_RETENCIONES_FJP");
            });
            modelBuilder.Entity<RH_H_RETENCIONES_INCES>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_RETENCION_APORTE,

                });
                builder.ToTable("RH_H_RETENCIONES_INCES");
            });

            modelBuilder.Entity<RH_H_RETENCIONES_CAH>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_RETENCION_APORTE,

                });
                builder.ToTable("RH_H_RETENCIONES_CAH");
            });

            modelBuilder.Entity<RH_H_RETENCIONES_SIND>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_RETENCION_APORTE,

                });
                builder.ToTable("RH_H_RETENCIONES_SIND");
            });

            modelBuilder.Entity<RH_HISTORICO_PERSONAL_CARGO>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_HISTORICO_PERSONAL_CARGO");
            });

            modelBuilder.Entity<PRE_INDICE_CAT_PRG>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_INDICE_CAT_PRG");
            });
            
            modelBuilder.Entity<RH_V_HISTORICO_MOVIMIENTOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_V_HISTORICO_MOVIMIENTOS_ALL");
            });
            modelBuilder.Entity<RH_PERIODOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_PERIODO,

                });
                builder.ToTable("RH_PERIODOS");
            });
            modelBuilder.Entity<RH_H_PERIODOS>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_H_PERIODO,

                });
                builder.ToTable("RH_H_PERIODOS");
            });

            modelBuilder.Entity<RH_TIPOS_NOMINA>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_TIPOS_NOMINA");
            });
            
            modelBuilder.Entity<RH_PERSONAS>(builder =>
            {

                builder.HasKey(table => new
                {
                    table.CODIGO_PERSONA,

                });
                builder.ToTable("RH_PERSONAS");
            });
            modelBuilder.Entity<RH_EDUCACION>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_EDUCACION,

                });
                builder.ToTable("RH_EDUCACION");
            });

            modelBuilder.Entity<RH_DOCUMENTOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_DOCUMENTO,

                });
                builder.ToTable("RH_DOCUMENTOS");
            });
            modelBuilder.Entity<RH_DOCUMENTOS_DETALLES>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_DOCUMENTO_DETALLE,

                });
                builder.ToTable("RH_DOCUMENTOS_DETALLES");
            });
            modelBuilder.Entity<RH_DESCRIPTIVAS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("RH_DESCRIPTIVAS");
            });
            modelBuilder.Entity<RH_DIRECCIONES>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_DIRECCION,

                }); 
                builder.ToTable("RH_DIRECCIONES");
            });

            modelBuilder.Entity<RH_EXP_LABORAL>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_EXP_LABORAL,

                });
                builder.ToTable("RH_EXP_LABORAL");
            });

            modelBuilder.Entity<RH_PERSONAS_MOV_CONTROL>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_PERSONA_MOV_CTRL,

                });
                builder.ToTable("RH_PERSONAS_MOV_CONTROL");
            });
            modelBuilder.Entity<RH_ARI>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_ARI,

                });
                builder.ToTable("RH_ARI");
            });

            modelBuilder.Entity<RH_RELACION_CARGOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_RELACION_CARGO,

                });
                builder.ToTable("RH_RELACION_CARGOS");
            });
            modelBuilder.Entity<RH_MOV_NOMINA>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_MOV_NOMINA,

                });
                builder.ToTable("RH_MOV_NOMINA");
            });

            modelBuilder.Entity<RH_PROCESOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_PROCESO,

                });
                builder.ToTable("RH_PROCESOS");
            });
            modelBuilder.Entity<RH_PROCESOS_DETALLES>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_DETALLE_PROCESO,

                });
                builder.ToTable("RH_PROCESOS_DETALLES");
            });
            modelBuilder.Entity<RH_ADMINISTRATIVOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_ADMINISTRATIVO,

                });
                builder.ToTable("RH_ADMINISTRATIVOS");
            });
            modelBuilder.Entity<RH_FAMILIARES>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_FAMILIAR,

                });
                builder.ToTable("RH_FAMILIARES");
            });
            modelBuilder.Entity<RH_CONCEPTOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_CONCEPTO,

                });
                builder.ToTable("RH_CONCEPTOS");
            });

            modelBuilder.Entity<RH_COMUNICACIONES>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_COMUNICACION,

                });
                builder.ToTable("RH_COMUNICACIONES");
            });

            modelBuilder.Entity<RH_ADMINISTRATIVOS>(builder =>
            {
                builder.HasKey(table => new
                {
                    table.CODIGO_ADMINISTRATIVO,

                });
                builder.ToTable("RH_ADMINISTRATIVOS");
            });


        }
    }
}