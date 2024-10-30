
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.EntitiesDestino.PRE;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DestinoDataContext : DbContext
    {
        public DestinoDataContext(DbContextOptions<DestinoDataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<HistoricoNomina> HistoricoNomina { get; set; }
        public DbSet<HistoricoPersonalCargo> HistoricoPersonalCargo { get; set; }
        public DbSet<IndiceCategoriaPrograma> IndiceCategoriaPrograma { get; set; }
        public DbSet<ConceptosRetenciones> ConceptosRetenciones { get; set; }
        public DbSet<HistoricoRetenciones> HistoricoRetenciones { get; set; }
        
        public DbSet<ADM_BENEFICIARIOS_OP> ADM_BENEFICIARIOS_OP { get; set; }
        public DbSet<ADM_CONTACTO_PROVEEDOR> ADM_CONTACTO_PROVEEDOR { get; set; }
        public DbSet<ADM_DESCRIPTIVAS> ADM_DESCRIPTIVAS { get; set; }
        public DbSet<ADM_ORDEN_PAGO> ADM_ORDEN_PAGO { get; set; }
        public DbSet<ADM_PUC_ORDEN_PAGO> ADM_PUC_ORDEN_PAGO { get; set; }
        public DbSet<PRE_V_SALDOS> PRE_V_SALDOS { get; set; }
        public DbSet<ADM_RETENCIONES_OP> ADM_RETENCIONES_OP { get; set; }
        public DbSet<ADM_PROVEEDORES> ADM_PROVEEDORES { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            
            modelBuilder
                .Entity<ADM_BENEFICIARIOS_OP>(builder =>
                {

                    builder.ToTable("ADM_BENEFICIARIOS_OP");
                    builder.HasKey(table => new
                    {
                        table.CODIGO_BENEFICIARIO_OP,
                   

                    });
                   
                });
            modelBuilder
                .Entity<ADM_CONTACTO_PROVEEDOR>(builder =>
                {

                    builder.ToTable("ADM_CONTACTO_PROVEEDOR");
                    builder.HasKey(table => new
                    {
                        table.CODIGO_CONTACTO_PROVEEDOR,
                   

                    });
                   
                });
            modelBuilder
                .Entity<ADM_DESCRIPTIVAS>(builder =>
                {

                    builder.ToTable("ADM_DESCRIPTIVAS");
                    builder.HasKey(table => new
                    {
                        table.DESCRIPCION_ID,
                   

                    });
                   
                });
            
            modelBuilder
                .Entity<ADM_ORDEN_PAGO>(builder =>
                {

                    builder.ToTable("ADM_ORDEN_PAGO");
                    builder.HasKey(table => new
                    {
                        table.CODIGO_ORDEN_PAGO,
                   

                    });
                   
                });
            modelBuilder
                .Entity<ADM_PUC_ORDEN_PAGO>(builder =>
                {

                    builder.ToTable("ADM_PUC_ORDEN_PAGO");
                    builder.HasKey(table => new
                    {
                        table.CODIGO_PUC_ORDEN_PAGO,
                   

                    });
                   
                });
            modelBuilder
                .Entity<ADM_RETENCIONES_OP>(builder =>
                {

                    builder.ToTable("ADM_RETENCIONES_OP");
                    builder.HasKey(table => new
                    {
                        table.CODIGO_RETENCION_OP,
                   

                    });
                   
                });
            
            modelBuilder
                .Entity<ADM_PROVEEDORES>(builder =>
                {

                    builder.ToTable("ADM_PROVEEDORES");
                    builder.HasKey(table => new
                    {
                        table.CODIGO_PROVEEDOR,
                   

                    });
                   
                });
            
            
            modelBuilder
                .Entity<PRE_V_SALDOS>(builder =>
                {
                    builder.ToTable("PRE_V_SALDOS");
                    builder.HasKey(table => new
                    {
                       
                        table.CODIGO_SALDO,
                      

                    });
                   
                });

            
            
            modelBuilder
           .Entity<ConceptosRetenciones>(builder =>
           {

               builder.ToTable("ConceptosRetenciones");
               builder.HasKey(table => new
               {
                   table.CODIGO_CONCEPTO,
                   table.Titulo

               });
               builder.Property(e => e.CODIGO_CONCEPTO).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.Titulo)
                  .HasMaxLength(1000)
                  .HasDefaultValueSql("('')");
           });
            
            
            modelBuilder
          .Entity<HistoricoRetenciones>(builder =>
          {

              builder.ToTable("HistoricoRetenciones");
              builder.HasKey(e => e.CODIGO_HISTORICO_NOMINA);


          });
            modelBuilder
            .Entity<HistoricoNomina>(builder =>
            {

                builder.ToTable("HistoricoNomina");
                builder.HasKey(e => e.CODIGO_HISTORICO_NOMINA);
                builder.HasIndex(e => e.CODIGO_HISTORICO_NOMINA)
                   .HasName("IDX_CODIGO_HISTORICO_NOMINA")
                   .IsUnique();
                builder.Property(e => e.CODIGO_EMPRESA).HasColumnType("numeric(10, 0)");
                builder.Property(e => e.CODIGO_TIPO_NOMINA).HasColumnType("numeric(10, 0)");
                builder.Property(e => e.CODIGO_PERIODO).HasColumnType("numeric(10, 0)");
                builder.Property(e => e.CODIGO_PERSONA).HasColumnType("numeric(10, 0)");
                builder.HasOne(d => d.Codigo)
                   .WithMany(p => p.HistoricoNomina)
                   .HasForeignKey(d => new { d.CODIGO_EMPRESA, d.CODIGO_TIPO_NOMINA, d.CODIGO_PERIODO,d.CODIGO_PERSONA })
                   .HasConstraintName("FK_HistoricoNomina_HistoricoPersonalCargo");

            });

            modelBuilder
           .Entity<HistoricoPersonalCargo>(builder =>
           {

               builder.ToTable("HistoricoPersonalCargo");
               builder.HasKey(table => new
               {
                   table.CODIGO_EMPRESA,
                   table.CODIGO_TIPO_NOMINA,
                   table.CODIGO_PERIODO,
                   table.CODIGO_PERSONA

               });
              
                 
               builder.HasOne(d => d.IndiceCategoriaPrograma)
                 .WithMany(p => p.HistoricoPersonalCargo)
                 .HasForeignKey(d => d.CODIGO_ICP)
                 .HasConstraintName("FK_IndiceCategoriaPrograma_HistoricoPersonalCargo");


               builder.Property(e => e.CODIGO_EMPRESA).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_TIPO_NOMINA).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_PERIODO).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_PERSONA).HasColumnType("numeric(10, 0)");

               builder.Property(e => e.CEDULA).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.FOTO)
                   .HasMaxLength(44)
                   .HasDefaultValueSql("('')");
               builder.Property(e => e.NOMBRE)
                   .HasMaxLength(50)
                   .HasDefaultValueSql("('')");
               builder.Property(e => e.APELLIDO)
                  .HasMaxLength(50)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.NACIONALIDAD)
                 .HasMaxLength(1)
                 .HasDefaultValueSql("('')");
               builder.Property(e => e.DESCRIPCION_NACIONALIDAD)
                 .HasMaxLength(10)
                 .HasDefaultValueSql("('')");
               builder.Property(e => e.SEXO)
                .HasMaxLength(1)
                .HasDefaultValueSql("('')");
               builder.Property(e => e.STATUS)
                .HasMaxLength(1)
                .HasDefaultValueSql("('')");
               builder.Property(e => e.DESCRIPCION_STATUS)
                .HasMaxLength(10)
                .HasDefaultValueSql("('')");

               builder.Property(e => e.DESCRIPCION_SEXO)
               .HasMaxLength(9)
               .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_RELACION_CARGO).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_CARGO).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CARGO_CODIGO)
              .HasMaxLength(10)
              .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_ICP).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_ICP_UBICACION).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_ICP_UBICACION).HasColumnType("numeric(18, 4)");
               builder.Property(e => e.DESCRIPCION_CARGO)
               .HasMaxLength(100)
              .HasDefaultValueSql("('')");

               builder.Property(e => e.TIPO_NOMINA)
              .HasMaxLength(100)
              .HasDefaultValueSql("('')");
               builder.Property(e => e.FRECUENCIA_PAGO_ID).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_SECTOR)
               .HasMaxLength(2)
               .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_PROGRAMA)
               .HasMaxLength(2)
               .HasDefaultValueSql("('')");
               builder.Property(e => e.TIPO_CUENTA_ID).HasColumnType("numeric(5, 0)");
               builder.Property(e => e.DESCRIPCION_TIPO_CUENTA)
               .HasMaxLength(100)
                .HasDefaultValueSql("('')");
               builder.Property(e => e.BANCO_ID).HasColumnType("numeric(5, 0)");
               builder.Property(e => e.DESCRIPCION_BANCO)
              .HasMaxLength(100)
               .HasDefaultValueSql("('')");
               builder.Property(e => e.NO_CUENTA)
              .HasMaxLength(20)
               .HasDefaultValueSql("('')");
               builder.Property(e => e.EXTRA1)
             .HasMaxLength(100)
              .HasDefaultValueSql("('')");
               builder.Property(e => e.EXTRA2)
             .HasMaxLength(100)
              .HasDefaultValueSql("('')");
               builder.Property(e => e.EXTRA3)
             .HasMaxLength(100)
              .HasDefaultValueSql("('')");
               builder.Property(e => e.USUARIO_INS).HasColumnType("numeric(10, 0)");

               builder.Property(e => e.FECHA_INS).HasColumnType("date");
               builder.Property(e => e.USUARIO_UPD).HasColumnType("numeric(10, 0)");

               builder.Property(e => e.FECHA_UPD).HasColumnType("date");

               builder.Property(e => e.FECHA_NOMINA).HasColumnType("date");
               builder.Property(e => e.FECHA_INGRESO).HasColumnType("date");
           });


            modelBuilder
           .Entity<IndiceCategoriaPrograma>(builder =>
           {

               builder.ToTable("IndiceCategoriaPrograma");
               builder.HasKey(e => e.CODIGO_ICP);

               builder.Property(e => e.CODIGO_ICP).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.ANO).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.ESCENARIO).HasColumnType("numeric(10, 0)");
               builder.Property(e => e.CODIGO_SECTOR)
                  .HasMaxLength(2)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_PROGRAMA)
                  .HasMaxLength(2)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_SUBPROGRAMA)
                  .HasMaxLength(2)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_PROYECTO)
                  .HasMaxLength(2)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_ACTIVIDAD)
                  .HasMaxLength(2)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.DENOMINACION)
                  .HasMaxLength(200)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.UNIDAD_EJECUTORA)
                  .HasMaxLength(200)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.DESCRIPCION)
                  .HasMaxLength(4000)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_FUNCIONARIO).HasColumnType("numeric(10, 0)");

               builder.Property(e => e.FECHA_INI).HasColumnType("date");
               builder.Property(e => e.FECHA_FIN).HasColumnType("date");
               builder.Property(e => e.EXTRA1)
                  .HasMaxLength(100)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.EXTRA2)
                  .HasMaxLength(100)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.EXTRA3)
                  .HasMaxLength(100)
                  .HasDefaultValueSql("('')");
               builder.Property(e => e.USUARIO_INS).HasColumnType("numeric(10, 0)");

               builder.Property(e => e.FECHA_INS).HasColumnType("date");
               builder.Property(e => e.USUARIO_UPD).HasColumnType("numeric(10, 0)");

               builder.Property(e => e.FECHA_UPD).HasColumnType("date");

               builder.Property(e => e.CODIGO_OFICINA)
                 .HasMaxLength(2)
                 .HasDefaultValueSql("('')");
               builder.Property(e => e.CODIGO_PRESUPUESTO).HasColumnType("numeric(10, 0)");


           });


        }



    }
}
