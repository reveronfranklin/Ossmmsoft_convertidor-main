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
        public DbSet<ADM_PUC_ORDEN_PAGO> ADM_PUC_ORDEN_PAGO { get; set; }
        public DbSet<ADM_RETENCIONES_OP> ADM_RETENCIONES_OP { get; set; }
        public DbSet<ADM_DOCUMENTOS_OP> ADM_DOCUMENTOS_OP { get; set; }
        public DbSet<ADM_COMPROBANTES_DOCUMENTOS_OP> ADM_COMPROBANTES_DOCUMENTOS_OP { get; set; }
        public DbSet<ADM_IMPUESTOS_DOCUMENTOS_OP> ADM_IMPUESTOS_DOCUMENTOS_OP { get; set; }
        public DbSet<ADM_IMPUESTOS_OP> ADM_IMPUESTOS_OP { get; set; }
        public DbSet<ADM_BENEFICIARIOS_OP> ADM_BENEFICIARIOS_OP { get; set; }
        public DbSet<ADM_PERIODICO_OP> ADM_PERIODICO_OP { get; set; }
        public DbSet<ADM_H_ORDEN_PAGO> ADM_H_ORDEN_PAGO { get; set; }
        public DbSet<ADM_CONTRATOS> ADM_CONTRATOS { get; set; }
        public DbSet<ADM_VAL_CONTRATO> ADM_VAL_CONTRATO { get; set; }
        public DbSet<ADM_DETALLE_VAL_CONTRATO> ADM_DETALLE_VAL_CONTRATO { get; set; }
        public DbSet<ADM_PUC_CONTRATO> ADM_PUC_CONTRATO { get; set; }
        public DbSet<ADM_CHEQUES> ADM_CHEQUES { get; set; }
        public DbSet<ADM_PRODUCTOS> ADM_PRODUCTOS { get; set; }
        public DbSet<ADM_SOL_COMPROMISO> ADM_SOL_COMPROMISO { get; set; }
        public DbSet<ADM_DETALLE_SOL_COMPROMISO> ADM_DETALLE_SOL_COMPROMISO { get; set; }
        public DbSet<ADM_RETENCIONES> ADM_RETENCIONES { get; set; }
        public DbSet<ADM_V_COMPROMISO_PENDIENTE> ADM_V_COMPROMISO_PENDIENTE { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);



            modelBuilder
                .Entity<ADM_RETENCIONES>(builder =>
                {
                  
                    builder.HasKey(table => new
                    {
                        table.CODIGO_RETENCION,

                    });
                    builder.ToTable("ADM_RETENCIONES");
                });

            modelBuilder
                .Entity<ADM_DESCRIPTIVAS>(builder =>
                {
                  
                    builder.HasKey(table => new
                    {
                        table.DESCRIPCION_ID,

                    });
                    builder.ToTable("ADM_DESCRIPTIVAS");
                });
            modelBuilder
                .Entity<ADM_PRODUCTOS>(builder =>
                {
                 
                    builder.HasKey(table => new
                    {
                        table.CODIGO_PRODUCTO,

                    });
                    builder.ToTable("ADM_PRODUCTOS");
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
                .Entity<ADM_V_COMPROMISO_PENDIENTE>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_COMPROMISO_PENDIENTE");
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
            modelBuilder
           .Entity<ADM_PUC_ORDEN_PAGO>(builder =>
           {
               //builder.HasNoKey();
               builder.HasKey(table => new
               {
                   table.CODIGO_PUC_ORDEN_PAGO,

               });
               builder.ToTable("ADM_PUC_ORDEN_PAGO");
           });

            modelBuilder
          .Entity<ADM_RETENCIONES_OP>(builder =>
          {
              //builder.HasNoKey();
              builder.HasKey(table => new
              {
                  table.CODIGO_RETENCION_OP,

              });
              builder.ToTable("ADM_RETENCIONES_OP");
          });
            modelBuilder
        .Entity<ADM_DOCUMENTOS_OP>(builder =>
        {
            //builder.HasNoKey();
            builder.HasKey(table => new
            {
                table.CODIGO_DOCUMENTO_OP,

            });
            builder.ToTable("ADM_DOCUMENTOS_OP");
        });
            modelBuilder
        .Entity<ADM_COMPROBANTES_DOCUMENTOS_OP>(builder =>
        {
            //builder.HasNoKey();
            builder.HasKey(table => new
            {
                table.CODIGO_COMPROBANTE_DOC_OP,

            });
            builder.ToTable("ADM_COMPROBANTES_DOCUMENTOS_OP");
        });
            modelBuilder
       .Entity<ADM_IMPUESTOS_DOCUMENTOS_OP>(builder =>
       {
           //builder.HasNoKey();
           builder.HasKey(table => new
           {
               table.CODIGO_IMPUESTO_DOCUMENTO_OP,

           });
           builder.ToTable("ADM_IMPUESTOS_DOCUMENTOS_OP");
       });
            modelBuilder
       .Entity<ADM_IMPUESTOS_OP>(builder =>
       {
           //builder.HasNoKey();
           builder.HasKey(table => new
           {
               table.CODIGO_IMPUESTO_OP,

           });
           builder.ToTable("ADM_IMPUESTOS_OP");
       });
            modelBuilder
      .Entity<ADM_BENEFICIARIOS_OP>(builder =>
      {
          //builder.HasNoKey();
          builder.HasKey(table => new
          {
              table.CODIGO_BENEFICIARIO_OP,

          });
          builder.ToTable("ADM_BENEFICIARIOS_OP");
      });
            modelBuilder
     .Entity<ADM_PERIODICO_OP>(builder =>
     {
         //builder.HasNoKey();
         builder.HasKey(table => new
         {
             table.CODIGO_PERIODICO_OP,

         });
         builder.ToTable("ADM_PERIODICO_OP");
     });
            modelBuilder
    .Entity<ADM_H_ORDEN_PAGO>(builder =>
    {
        //builder.HasNoKey();
        builder.HasKey(table => new
        {
            table.CODIGO_H_ORDEN_PAGO,

        });
        builder.ToTable("ADM_H_ORDEN_PAGO");
    });
                modelBuilder
       .Entity<ADM_CONTRATOS>(builder =>
       {
           //builder.HasNoKey();
           builder.HasKey(table => new
           {
               table.CODIGO_CONTRATO,

           });
           builder.ToTable("ADM_CONTRATOS");
       });
                modelBuilder
      .Entity<ADM_VAL_CONTRATO>(builder =>
      {
          //builder.HasNoKey();
          builder.HasKey(table => new
          {
              table.CODIGO_VAL_CONTRATO,

          });
          builder.ToTable("ADM_VAL_CONTRATO");
      });
                modelBuilder
     .Entity<ADM_DETALLE_VAL_CONTRATO>(builder =>
     {
         //builder.HasNoKey();
         builder.HasKey(table => new
         {
             table.CODIGO_DETALLE_VAL_CONTRATO,

         });
         builder.ToTable("ADM_DETALLE_VAL_CONTRATO");
     });
                modelBuilder
    .Entity<ADM_PUC_CONTRATO>(builder =>
    {
        //builder.HasNoKey();
        builder.HasKey(table => new
        {
            table.CODIGO_PUC_CONTRATO,

        });
        builder.ToTable("ADM_PUC_CONTRATO");
    });

            modelBuilder.Entity<ADM_CHEQUES>(entity =>
            {
              

                entity.ToTable("ADM_CHEQUES");
                entity.HasKey(table => new
                {
                    table.CODIGO_CHEQUE,

                });


                entity.Property(e => e.ANO)
                    .HasPrecision(4)
                    .HasColumnName("ANO");

                entity.Property(e => e.CODIGO_CHEQUE)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_CHEQUE");

                entity.Property(e => e.CODIGO_CONTACTO_PROVEEDOR)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_CONTACTO_PROVEEDOR");

                entity.Property(e => e.CODIGO_CUENTA_BANCO)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_CUENTA_BANCO");

                entity.Property(e => e.CODIGO_EMPRESA)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_EMPRESA");

                entity.Property(e => e.CODIGO_PRESUPUESTO)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_PRESUPUESTO");

                entity.Property(e => e.CODIGO_PROVEEDOR)
                    .HasPrecision(10)
                    .HasColumnName("CODIGO_PROVEEDOR");

                entity.Property(e => e.ENDOSO)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ENDOSO")
                    .IsFixedLength();

                entity.Property(e => e.EXTRA1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXTRA1");

                entity.Property(e => e.EXTRA2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXTRA2");

                entity.Property(e => e.EXTRA3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EXTRA3");

                entity.Property(e => e.FECHA_ANULACION)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ANULACION");

                entity.Property(e => e.FECHA_CHEQUE)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CHEQUE");

                entity.Property(e => e.FECHA_CONCILIACION)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_CONCILIACION");

                entity.Property(e => e.FECHA_ENTREGA)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_ENTREGA");

                entity.Property(e => e.FECHA_INS)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_INS");

                entity.Property(e => e.FECHA_UPD)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHA_UPD");

                entity.Property(e => e.MOTIVO)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("MOTIVO");

                entity.Property(e => e.NUMERO_CHEQUE)
                    .HasPrecision(10)
                    .HasColumnName("NUMERO_CHEQUE");

                entity.Property(e => e.NUMERO_CHEQUERA)
                    .HasPrecision(10)
                    .HasColumnName("NUMERO_CHEQUERA");

                entity.Property(e => e.PRINT_COUNT)
                    .HasPrecision(5)
                    .HasColumnName("PRINT_COUNT");

                entity.Property(e => e.STATUS)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .IsFixedLength();

                entity.Property(e => e.TIPO_BENEFICIARIO)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_BENEFICIARIO")
                    .IsFixedLength();

                entity.Property(e => e.TIPO_CHEQUE_ID)
                    .HasPrecision(10)
                    .HasColumnName("TIPO_CHEQUE_ID");

                entity.Property(e => e.USUARIO_ENTREGA)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_ENTREGA");

                entity.Property(e => e.USUARIO_INS)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_INS");

                entity.Property(e => e.USUARIO_UPD)
                    .HasPrecision(10)
                    .HasColumnName("USUARIO_UPD");
            });

                        modelBuilder
            .Entity<ADM_SOL_COMPROMISO>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.CODIGO_SOL_COMPROMISO,

                });
                builder.ToTable("ADM_SOL_COMPROMISO");
            });

            modelBuilder
           .Entity<ADM_DETALLE_SOL_COMPROMISO>(builder =>
           {
               //builder.HasNoKey();
               builder.HasKey(table => new
               {
                   table.CODIGO_DETALLE_SOLICITUD,

               });
               builder.ToTable("ADM_DETALLE_SOL_COMPROMISO");
           });

           
        }

        
    }
}
