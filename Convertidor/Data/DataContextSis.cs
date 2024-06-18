


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
        public virtual DbSet<OssFormula> OssFormulas { get; set; } = null!;
        public virtual DbSet<OssFuncion> OssFunciones { get; set; } = null!;
        public virtual DbSet<OssModeloCalculo> OssModeloCalculos { get; set; } = null!;
        public virtual DbSet<OssModulo> OssModulos { get; set; } = null!;
        public virtual DbSet<OssVariable> OssVariables { get; set; } = null!;
        public virtual DbSet<SIS_SOURCE> SIS_SOURCE { get; set; } = null!;
        public virtual DbSet<SIS_P_SOURCE> SIS_P_SOURCE { get; set; } = null!;
        public virtual DbSet<SIS_DET_SOURCE> SIS_DET_SOURCE { get; set; } = null!;
        
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
                });
            modelBuilder.Entity<OssCalculo>(entity =>
            {
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.ToTable("OSS_CALCULO");

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

                entity.Property(e => e.IdModeloCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ID_MODELO_CALCULO");

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
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("VALOR");
                entity.Property(e => e.AcumulaAlTotal)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ACUMULA_AL_TOTAL");
               
                
                
            });

       
            modelBuilder.Entity<OssFormula>(entity =>
            {
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.ToTable("OSS_FORMULA");

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

                entity.Property(e => e.IdModeloCalculo)
                    .HasPrecision(10)
                    .HasColumnName("ID_MODELO_CALCULO");

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
                
                entity.Property(e => e.AcumulaAlTotal)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ACUMULA_AL_TOTAL");
            });

            modelBuilder.Entity<OssFuncion>(entity =>
            {
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
                    .IsUnicode(false)
                    .HasColumnName("FUNCION");

                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .HasColumnName("ID");

                entity.Property(e => e.IdVariable)
                    .HasPrecision(10)
                    .HasColumnName("ID_VARIABLE");

                entity.Property(e => e.ModuloId)
                    .HasPrecision(10)
                    .HasColumnName("MODULO_ID");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");
            });

            modelBuilder.Entity<OssModeloCalculo>(entity =>
            {
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.ToTable("OSS_MODELO_CALCULO");

                entity.Property(e => e.Id)
                    .HasPrecision(10)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

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
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");
            });

            modelBuilder.Entity<OssModulo>(entity =>
            {
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.ToTable("OSS_MODULO");

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
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.UsuarioIns)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.UsuarioUpd)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");
            });

            modelBuilder.Entity<OssVariable>(entity =>
            {
                entity.HasKey(table => new
                {
                    table.Id,

                });
                entity.ToTable("OSS_VARIABLES");

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
            });
           
                
                modelBuilder.Entity<SIS_SOURCE>(builder =>
                {
                    builder.HasKey(table => new
                    {
                        table.CODIGO_SOURCE,

                    });
                    builder.ToTable("SIS_SOURCE");
                });
                modelBuilder.Entity<SIS_P_SOURCE>(builder =>
                {
                    builder.HasKey(table => new
                    {
                        table.CODIGO_P_SOURCE,

                    });
                    builder.ToTable("SIS_P_SOURCE");
                });
                modelBuilder.Entity<SIS_DET_SOURCE>(builder =>
                {
                    builder.HasKey(table => new
                    {
                        table.CODIGO_DET_SOURCE,

                    });
                    builder.ToTable("SIS_DET_SOURCE");
                });
                
                
                modelBuilder.Entity<SIS_UBICACION_NACIONAL>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("SIS_UBICACION_NACIONAL");
            });

        }



    }
}
