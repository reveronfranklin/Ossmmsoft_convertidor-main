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

       
        
        
        public DbSet<ADM_V_NOTAS_TERCEROS> ADM_V_NOTAS_TERCEROS { get; set; }

        public DbSet<ADM_V_PAGAR_A_LA_OP_TERCEROS> ADM_V_PAGAR_A_LA_OP_TERCEROS { get; set; }
      
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
        
        public DbSet<ADM_LOTE_PAGO> ADM_LOTE_PAGO { get; set; }
        public DbSet<ADM_CHEQUES> ADM_CHEQUES { get; set; }
        public DbSet<ADM_PRODUCTOS> ADM_PRODUCTOS { get; set; }
        public DbSet<ADM_SOL_COMPROMISO> ADM_SOL_COMPROMISO { get; set; }
        public DbSet<ADM_DETALLE_SOL_COMPROMISO> ADM_DETALLE_SOL_COMPROMISO { get; set; }
        public DbSet<ADM_RETENCIONES> ADM_RETENCIONES { get; set; }
        public DbSet<ADM_V_COMPROMISO_PENDIENTE> ADM_V_COMPROMISO_PENDIENTE { get; set; }
        public DbSet<ADM_V_OP_POR_PAGAR> ADM_V_OP_POR_PAGAR { get; set; }
        public DbSet<ADM_V_OP_POR_PAGAR_BENE> ADM_V_OP_POR_PAGAR_BENE { get; set; }
        
        public DbSet<ADM_BENEFICIARIOS_CH> ADM_BENEFICIARIOS_CH { get; set; }
        public DbSet<ADM_PAGOS_ELECTRONICOS> ADM_PAGOS_ELECTRONICOS { get; set; }
        
        public DbSet<ADM_V_NOTAS> ADM_V_NOTAS { get; set; }

        
        
        
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
                .Entity<ADM_LOTE_PAGO>(builder =>
                {
                  
                    builder.HasKey(table => new
                    {
                        table.CODIGO_LOTE_PAGO,

                    });
                    builder.ToTable("ADM_LOTE_PAGO");
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
                    
                .Entity<ADM_V_PAGAR_A_LA_OP_TERCEROS>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_PAGAR_A_LA_OP_TERCEROS");
                    
                    // Mapeo de propiedades a nombres de columna
                    builder.Property(e => e.CodigoProveedor).HasColumnName("CODIGO_PROVEEDOR");
                    builder.Property(e => e.CodigoContactoProveedor).HasColumnName("CODIGO_CONTACTO_PROVEEDOR");
                    builder.Property(e => e.PagarALaOrdenDe).HasColumnName("PAGAR_A_LA_ORDEN_DE");
                    builder.Property(e => e.NombreProveedor).HasColumnName("NOMBRE_PROVEEDOR");
                    builder.Property(e => e.CodigoEmpresa).HasColumnName("CODIGO_EMPRESA");
                });
            modelBuilder
                .Entity<ADM_V_COMPROMISO_PENDIENTE>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_COMPROMISO_PENDIENTE");
                });
            modelBuilder
                .Entity<ADM_V_NOTAS>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_NOTAS");
                });
            modelBuilder
                .Entity<ADM_V_NOTAS_TERCEROS>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_NOTAS_TERCEROS");
                });
            
            
            modelBuilder
                .Entity<ADM_V_OP_POR_PAGAR>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_OP_POR_PAGAR");
                });
            modelBuilder
                .Entity<ADM_V_OP_POR_PAGAR_BENE>(builder =>
                {
                    builder.HasNoKey();
                  
                    builder.ToTable("ADM_V_OP_POR_PAGAR_BENE");
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


            });
            modelBuilder.Entity<ADM_BENEFICIARIOS_CH>(entity =>
            {
              

                entity.ToTable("ADM_BENEFICIARIOS_CH");
                entity.HasKey(table => new
                {
                    table.CODIGO_BENEFICIARIO_CH,

                });


            });
            modelBuilder.Entity<ADM_PAGOS_ELECTRONICOS>(entity =>
            {
              

                entity.ToTable("ADM_PAGOS_ELECTRONICOS");
                entity.HasKey(table => new
                {
                    table.CODIGO_PAGO_ELECTRONICO,

                });


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
