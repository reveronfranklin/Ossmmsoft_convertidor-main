


using CConvertidor.Data.Entities.Sis;
using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextSis : DbContext
    {
        public DataContextSis(DbContextOptions<DataContextSis> options) : base(options)
        {

        }
        public DbSet<SIS_USUARIOS> SIS_USUARIOS { get; set; }
        public DbSet<SIS_V_SISTEMA_USUARIO_PROGRAMA> SIS_V_SISTEMA_USUARIO_PROGRAMA { get; set; }
    
        public DbSet<SIS_UBICACION_NACIONAL> SIS_UBICACION_NACIONAL { get; set; }
        public DbSet<OSS_CONFIG> OSS_CONFIG { get; set; }
        
        public virtual DbSet<OssCalculo> OssCalculos { get; set; } = null!;
        public virtual DbSet<OssCalculoHistorico> OssCalculoHistoricos { get; set; } = null!;
        public virtual DbSet<OssFormula> OssFormulas { get; set; } = null!;
        public virtual DbSet<OssFuncion> OssFunciones { get; set; } = null!;
        public virtual DbSet<OssModulo> OssModulos { get; set; } = null!;
        public virtual DbSet<OssVariable> OssVariables { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
            .Entity<SIS_V_SISTEMA_USUARIO_PROGRAMA>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("SIS_V_SISTEMA_USUARIO_PROGRAMA");
            })
            .Entity<SIS_USUARIOS>(builder =>
            {
            //builder.HasNoKey();
            builder.HasKey(table => new
            {
            table.CODIGO_USUARIO,

            });
            builder.ToTable("SIS_USUARIOS");
            })
            .Entity<OSS_CONFIG>(builder =>
            {
            //builder.HasNoKey();
                builder.HasKey(table => new
                {
                table.ID,

                });
                builder.ToTable("OSS_CONFIG");
            })
           
            .Entity<OssCalculo>(entity =>
            {
                entity.ToTable("OSS_CALCULO");
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CodeVariable)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODE_VARIABLE");

                entity.Property(e => e.CodeVariableExterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODE_VARIABLE_EXTERNO");

                entity.Property(e => e.CodigoEmpresa)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.FechaCalculo)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CALCULO");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FechaUpd)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.Formula)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA");

                entity.Property(e => e.FormulaDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA_DESCRIPCION");

                entity.Property(e => e.FormulaValor)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA_VALOR");

                entity.Property(e => e.IdCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ID_CALCULO");

                entity.Property(e => e.IdCalculoExterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_CALCULO_EXTERNO");

                entity.Property(e => e.IdVariable)
                    .HasPrecision(10)
                    .HasColumnName("ID_VARIABLE");

                entity.Property(e => e.ModuloId)
                    .HasPrecision(10)
                    .HasColumnName("MODULO_ID");

                entity.Property(e => e.OrdenCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ORDEN_CALCULO");

                entity.Property(e => e.Query)
                    .IsUnicode(false)
                    .HasColumnName("QUERY");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");

                entity.Property(e => e.Valor)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VALOR");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.OssCalculos)
                    .HasForeignKey(d => d.ModuloId)
                    .HasConstraintName("OSS_CALCULO_OSS_MODULO_FK");

                entity.HasOne(d => d.ModuloNavigation)
                    .WithMany(p => p.OssCalculos)
                    .HasForeignKey(d => d.ModuloId)
                    .HasConstraintName("OSS_CALCULO_OSS_VARIABLES_FK");
            })

            .Entity<OssCalculoHistorico>(entity =>
            {
                entity.ToTable("OSS_CALCULO_HISTORICO");
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CodeVariable)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODE_VARIABLE");

                entity.Property(e => e.CodeVariableExterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODE_VARIABLE_EXTERNO");

                entity.Property(e => e.CodigoEmpresa)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.FechaCalculo)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CALCULO");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FechaUpd)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.Formula)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA");

                entity.Property(e => e.FormulaDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA_DESCRIPCION");

                entity.Property(e => e.FormulaValor)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA_VALOR");

                entity.Property(e => e.IdCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ID_CALCULO");

                entity.Property(e => e.IdCalculoExterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID_CALCULO_EXTERNO");

                entity.Property(e => e.IdVariable)
                    .HasPrecision(10)
                    .HasColumnName("ID_VARIABLE");

                entity.Property(e => e.ModuloId)
                    .HasPrecision(10)
                    .HasColumnName("MODULO_ID");

                entity.Property(e => e.OrdenCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ORDEN_CALCULO");

                entity.Property(e => e.Query)
                    .IsUnicode(false)
                    .HasColumnName("QUERY");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");

                entity.Property(e => e.Valor)
                    .HasColumnType("NUMBER")
                    .HasColumnName("VALOR");
            })

         
            .Entity<OssFormula>(entity =>
            {
                entity.ToTable("OSS_FORMULA");
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CodeVariable)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODE_VARIABLE");

                entity.Property(e => e.CodigoEmpresa)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.CodigoExterno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_EXTERNO");

                entity.Property(e => e.FechaIns)
                    .HasPrecision(10)
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FechaUpd)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.Formula)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA");

                entity.Property(e => e.FormulaDescripcion)
                    .IsUnicode(false)
                    .HasColumnName("FORMULA_DESCRIPCION");

                entity.Property(e => e.IdVariable)
                    .HasPrecision(10)
                    .HasColumnName("ID_VARIABLE");

                entity.Property(e => e.ModuloId)
                    .HasPrecision(10)
                    .HasColumnName("MODULO_ID");

                entity.Property(e => e.OrdenCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ORDEN_CALCULO");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");

                entity.HasOne(d => d.IdVariableNavigation)
                    .WithMany(p => p.OssFormulas)
                    .HasForeignKey(d => d.IdVariable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OSS_FORMULA_OSS_VARIABLES_FK");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.OssFormulas)
                    .HasForeignKey(d => d.ModuloId)
                    .HasConstraintName("OSS_FORMULA_OSS_MODULO_FK");
            })

            .Entity<OssFuncion>(entity =>
            {
                entity.HasNoKey();
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.ToTable("OSS_FUNCIONES");

                entity.HasIndex(e => e.Id, "OSS_FUNCIONES_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CodigoEmpresa)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.EsSql)
                    .IsRequired()
                    .HasPrecision(1)
                    .HasColumnName("ES_SQL")
                    .HasDefaultValueSql("1 ");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FechaUpd)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.Funcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FUNCION");

                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .HasColumnName("ID");

                entity.Property(e => e.ModuloId)
                    .HasPrecision(10)
                    .HasColumnName("MODULO_ID");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");

                entity.HasOne(d => d.Modulo)
                    .WithMany()
                    .HasForeignKey(d => d.ModuloId)
                    .HasConstraintName("OSS_FUNCIONES_OSS_MODULO_FK");
            })

            .Entity<OssModulo>(entity =>
            {
                entity.ToTable("OSS_MODULO");
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO");

                entity.Property(e => e.CodigoEmpresa)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FechaUpd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");
            })

            .Entity<OssVariable>(entity =>
            {
                entity.ToTable("OSS_VARIABLES");
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CODE");

                entity.Property(e => e.CodigoEmpresa)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaIns)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FechaUpd)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.Longitud)
                    .HasPrecision(10)
                    .HasColumnName("LONGITUD");

                entity.Property(e => e.LongitudDecimal)
                    .HasPrecision(10)
                    .HasColumnName("LONGITUD_DECIMAL");

                entity.Property(e => e.LongitudRedondeo)
                    .HasPrecision(10)
                    .HasColumnName("LONGITUD_REDONDEO");

                entity.Property(e => e.LongitudTruncado)
                    .HasPrecision(10)
                    .HasColumnName("LONGITUD_TRUNCADO");

                entity.Property(e => e.ModuloId)
                    .HasPrecision(10)
                    .HasColumnName("MODULO_ID");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.OssVariables)
                    .HasForeignKey(d => d.ModuloId)
                    .HasConstraintName("OSS_VARIABLES_OSS_MODULO_FK");
            })

            
            .Entity<SIS_UBICACION_NACIONAL>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("SIS_UBICACION_NACIONAL");
            });

        }



    }
}
